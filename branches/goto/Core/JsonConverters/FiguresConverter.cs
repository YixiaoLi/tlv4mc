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

			Match m = Regex.Match(s, @"(?<name>[^\(]+)(\((?<args>[^\)]*)\))?");

			result.Shape = m.Groups["name"].Value;
	
			if (m.Groups["args"].Value != string.Empty)
				result.Args= m.Groups["args"].Value.Split(',');

			return result;
		}

	}
}
