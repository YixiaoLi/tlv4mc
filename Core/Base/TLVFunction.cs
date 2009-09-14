
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TLVFunction
	{
		public static readonly Dictionary<string, Func<string[], ResourceData, TraceLogData, string>> TLVFunctions = new Dictionary<string, Func<string[], ResourceData, TraceLogData, string>>()
		{
			{
				"COUNT", (args, resData, logData) =>
				{
					return logData.GetObject(args[0]).Count().ToString();
				}
			},
			{
				"EXIST", (args, resData, logData) =>
				{
					return logData.GetObject(args[0]).Count() != 0 ? "True" : "False";
				}
			},
			{
				"ATTR", (args, resData, logData) =>
				{
					return logData.GetAttributeValue(args[0]).ToString();
				}
			},
			{
				"RES_NAME", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length > 1)
						throw new Exception("RES_NAME で 指定した条件のリソースは複数存在します。\n" + args[0]);
					return i[0].Name;
				}
			},
			{
				"RES_DISPLAYNAME", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length > 1)
						throw new Exception("RES_NAME で 指定した条件のリソースは複数存在します。\n" + args[0]);

					return i[0].DisplayName;
				}
			},
			{
				"RES_COLOR", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length > 1)
						throw new Exception("RES_NAME で 指定した条件のリソースは複数存在します。\n" + args[0]);

					return i[0].Color.Value.ToHexString();
				}
			}
		};

		public static TraceLog Apply(TraceLog value, ResourceData resData, TraceLogData logData)
		{
			return new TraceLog(Apply(value.ToString(), resData, logData));
		}

		public static string Apply(string value, ResourceData resData, TraceLogData logData)
		{
			if (value == null)
				return value;

			value = value.Replace("\\{", "___START_BIG_BRACKET___");
			value = value.Replace("\\}", "___END_BIG_BRACKET___");
			foreach (Match m in Regex.Matches(value, @"\$(?<func>[^{]+){(?<args>[^}]+)}"))
			{
				try
				{
					value = Regex.Replace(value, Regex.Escape(m.Value), apply(m.Groups["func"].Value, m.Groups["args"].Value, resData, logData));
				}
				catch (Exception e)
				{
					throw new Exception("リソース条件式が異常です。\n" + "\"" + value + "\"\n" + e.Message);
				}
			}
			value = value.Replace("___START_BIG_BRACKET___", "\\{");
			value = value.Replace("___END_BIG_BRACKET___", "\\}");
			return value;
		}

		private static string apply(string func, string tlvarguments, ResourceData resData, TraceLogData logData)
		{
			tlvarguments = tlvarguments.Replace("\\,", "___COMMAS___");
			tlvarguments = tlvarguments.Replace("\\\"", "___DOUBLE_QUOTE___");
			string[] tlvargs = tlvarguments.Split(',');
			for (int i=0;i<tlvargs.Length;i++)
			{
				tlvargs[i] = tlvargs[i].Replace("___COMMAS___", "\\,");
				tlvargs[i] = tlvargs[i].Replace("___DOUBLE_QUOTE___", "\\\"");
			}

			string result;

			if (TLVFunctions.ContainsKey(func))
				result = TLVFunctions[func](tlvargs, resData, logData);
			else
				throw new Exception("未定義の関数です。\n" + "\"" + func + "\"");

			return result;
		}

	}
}
