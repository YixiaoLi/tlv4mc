using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ConvertFunction
	{
		public static readonly string[] ConvertFunctions = new string[] { "COUNT", "EXIST", "ATTR", "NAME", "DISPLAYNAME" };

		public static string ApplyConvertFunc(TraceLogData traceLogData, string condition)
		{
			foreach (string func in ConvertFunctions)
			{
				foreach (Match m in Regex.Matches(condition, func + @"{(?<condition>\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)\s*)?\s*(\.\s*[^=!<>\(]+)?\s*)}"))
				{
					string val = calcConvertFunc(func, m.Groups["condition"].Value, traceLogData);
					condition = Regex.Replace(condition, Regex.Escape(m.Value), val);
				}
			}
			return condition;
		}

		private static string calcConvertFunc(string func, string condition, TraceLogData traceLogData)
		{
			string result;
			try
			{
				switch (func)
				{
					case "COUNT":
						result = traceLogData.GetObject(condition).Count().ToString();
						break;
					case "EXIST":
						result = traceLogData.GetObject(condition).Count() != 0 ? "True" : "False";
						break;
					case "ATTR":
						result = traceLogData.GetAttributeValue(condition).ToString();
						break;
					case "NAME":
						result = traceLogData.GetAttributeValue(condition + "._name").ToString();
						break;
					case "DISPLAYNAME":
						result = traceLogData.GetAttributeValue(condition + "._displayName").ToString();
						break;
					default:
						throw new Exception(func + "：未知の関数です。");
				}
			}
			catch (Exception e)
			{
				throw new Exception("リソース条件式が異常です。\n" + "\"" + condition + "\"\n" + e.Message);
			}
			return result;
		}
	}
}
