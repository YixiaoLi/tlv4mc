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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class JsonReader : IJsonReader
	{
		private Newtonsoft.Json.JsonReader _reader;

		public Newtonsoft.Json.JsonReader Reader { get { return _reader; } }

		public JsonReader(Newtonsoft.Json.JsonReader reader)
		{
			_reader = reader;
		}

		public bool Read()
		{
			return _reader.Read();
		}

		public  NU.OJL.MPRTOS.TLV.Base.JsonTokenType TokenType
		{
			get
			{
				switch (_reader.TokenType)
				{
					case JsonToken.StartObject:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartObject;
					case JsonToken.StartArray:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartArray;
					case JsonToken.EndObject:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndObject;
					case JsonToken.EndArray:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndArray;
					case JsonToken.StartConstructor:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartConstructor;
					case JsonToken.EndConstructor:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndConstructor;
					case JsonToken.PropertyName:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.PropertyName;
					case JsonToken.Integer:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Integer;
					case JsonToken.Float:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Float;
					case JsonToken.String:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.String;
					case JsonToken.Boolean:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Boolean;
					case JsonToken.Null:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Null;
					case JsonToken.Comment:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Comment;
					case JsonToken.Date:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Date;
					case JsonToken.None:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.None;
					case JsonToken.Raw:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Raw;
					case JsonToken.Undefined:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Undefined;
					default:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Undefined;
				}
			}
		}

		public object Value
		{
			get { return _reader.Value; }
		}

		public void Skip()
		{
			_reader.Skip();
		}

	}
}
