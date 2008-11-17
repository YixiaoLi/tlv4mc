using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class VisualizeRuleConverter : IJsonConverter
	{
		public Type Type { get { return typeof(VisualizeRule); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			if (((VisualizeRule)obj).IsMapped)
			{
				writer.WriteObject(w =>
					{
						foreach (KeyValuePair<string, string> kvp in ((VisualizeRule)obj))
						{
							w.WriteProperty(kvp.Key);
							w.WriteValue(kvp.Value);
						}
					});
			}
			else
			{
				writer.WriteValue(((VisualizeRule)obj).ToString());
			}
		}

		public object ReadJson(IJsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				return new VisualizeRule((string)reader.Value);
			}
			else if (reader.TokenType == JsonTokenType.StartObject)
			{
				VisualizeRule vr = new VisualizeRule(new Dictionary<string,string>());
				reader.Read();
				while(reader.TokenType != JsonTokenType.EndObject)
				{
					string key = (string)reader.Value;
					reader.Read();
					string val = (string)reader.Value;
					vr.Add(key, val);
					reader.Read();
				}
				return vr;
			}
			else
			{
				return null;
			}
		}
	}
}
