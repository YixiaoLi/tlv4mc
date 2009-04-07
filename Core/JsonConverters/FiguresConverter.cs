/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class FiguresConverter : GeneralConverter<Figures>
	{

		protected override void WriteJson(IJsonWriter writer, Figures figures)
		{
			writeFigures(writer, figures);
		}

		private void writeFigures(IJsonWriter writer, Figures figures)
		{
			if (figures.Count > 1)
			{
				writeListFigure(writer, figures);
			}
			else if (figures.Count == 1)
			{
				writeFigure(writer, figures[0]);
			}
			else
			{
				writer.WriteValue(null);
			}
		}

		private void writeListFigure(IJsonWriter writer, Figures figures)
		{
			writer.WriteArray(w =>
			{
				foreach (Figure fg in figures)
				{
					writeFigure(w, fg);
				}
			});
		}

		private void writeFigure(IJsonWriter writer, Figure figure)
		{
			if (figure.IsShape)
			{
				writeShapeWithCondition(writer, figure);
			}
			else if (figure.IsFigures)
			{
				writeFiguresWithCondition(writer, figure);
			}
			else
			{
				writer.WriteValue(null);
			}
		}

		private void writeShapeWithCondition(IJsonWriter writer, Figure figure)
		{
			if (figure.Condition == null)
			{
				writeShape(writer, figure);
			}
			else
			{
				writer.WriteObject(w =>
				{
					w.WriteProperty(figure.Condition);
					writeShape(w, figure);
				});
			}
		}

		private void writeFiguresWithCondition(IJsonWriter writer, Figure figure)
		{
			if (figure.Condition == null)
			{
				writeFigures(writer, figure.Figures);
			}
			else
			{
				writer.WriteObject(w =>
				{
					w.WriteProperty(figure.Condition);
					writeFigures(w, figure.Figures);
				});
			}
		}

		private void writeShape(IJsonWriter writer, Figure figure)
		{
			string args = string.Empty;

			if (figure.Args != null)
				args = figure.Args.ToCSVString();

			writer.WriteValue(figure.Shape + "(" + args + ")");
		}

		public override object ReadJson(NU.OJL.MPRTOS.TLV.Base.IJsonReader reader)
		{
			return readFigures(reader);
		}

		private Figures readFigures(IJsonReader reader)
		{
			if(reader.TokenType == JsonTokenType.StartArray)
			{
				return readListFigures(reader);
			}
			else if (reader.TokenType == JsonTokenType.StartObject)
			{
				Figures result = new Figures();
				result.Add(readFigure(reader));
				return result;
			}
			else if (reader.TokenType == JsonTokenType.String)
			{
				Figures result = new Figures();
				result.Add(readSpahe(reader));
				return result;
			}
			else
			{
				return null;
			}
		}

		private Figures readListFigures(IJsonReader reader)
		{
			Figures result = new Figures();
			reader.Read();
			while(reader.TokenType != JsonTokenType.EndArray)
			{
				result.Add(readFigure(reader));
				reader.Read();
			}
			return result;
		}

		private Figure readFigure(IJsonReader reader)
		{
			Figure result = new Figure();
			if (reader.TokenType == JsonTokenType.StartArray)
			{
				Figures fgs = readListFigures(reader);
				if (fgs.Count > 1)
					result.Figures = fgs;
				else if (fgs.Count == 1)
					result = fgs[0];
			}
			else if (reader.TokenType == JsonTokenType.StartObject)
			{
				Figures fgs = readFigureWithCondition(reader);
				if (fgs.Count > 1)
					result.Figures = fgs;
				else if (fgs.Count == 1)
					result = fgs[0];
			}
			else if (reader.TokenType == JsonTokenType.String)
			{
				result = readSpahe(reader);
			}
			else
			{
				result = null;
			}
			return result;
		}

		private Figures readFigureWithCondition(IJsonReader reader)
		{
			Figures result = new Figures();
			reader.Read();
			while (reader.TokenType != JsonTokenType.EndObject)
			{
				string condition = reader.Value.ToString();
				reader.Read();

				Figure fg = readFigure(reader);
				fg.Condition = condition;

				result.Add(fg);
				reader.Read();
			}
			return result;
		}

		private Figure readSpahe(IJsonReader reader)
		{
			Figure result = new Figure();
			string s = reader.Value.ToString();

			s = Regex.Replace(s, @"\s", "");

			Match m = Regex.Match(s, @"(?<name>[^\(]+)(\((?<args>.*)\)$)?");

			result.Shape = m.Groups["name"].Value;
			string args = m.Groups["args"].Value;

			if (args != string.Empty)
			{
				result.Args = args.Split(',');
			}
			return result;
		}

	}
}
