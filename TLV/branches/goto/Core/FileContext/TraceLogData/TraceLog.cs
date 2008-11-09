using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLog
	{
		private string _log;
		public TraceLog(string log)
		{
			_log = log;
		}
		public static implicit operator string(TraceLog stdlog)
		{
			return stdlog._log;
		}

		public string Time
		{
			get
			{
				Match m = Regex.Match(_log, @"\s*\[\s*(?<time>[0-9a-fA-F]+)\s*\]\s*");

				if (m.Success)
					return m.Groups["time"].Value.Replace(" ", "").Replace("\t", "");
				else
					throw new Exception("時間指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string Object
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*(?<object>[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?)\s*(\.\s*[^=!<>\(\s]+)?\s*$");

				if (m.Success)
					return m.Groups["object"].Value.Replace(" ", "").Replace("\t", "");
				else
					throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string Behavior
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)?)\s*\.\s*(?<behavior>[^=!<>\(\s]+\s*\([^\)]+\))\s*$");

				if (m.Success)
					return m.Groups["behavior"].Value.Replace(" ", "").Replace("\t", "");
				else
					throw new Exception("振舞い指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string Attribute
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)?)\s*\.\s*(?<attribute>[^=!<>\(\s]+)\s*$");

				if (m.Success)
					return m.Groups["attribute"].Value.Replace(" ", "").Replace("\t", "");
				else
					throw new Exception("属性指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string ObjectType(ResourceData resData)
		{
			Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*(?<object>[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?)\s*(\.\s*[^=!<>\(\s]+)?\s*$");

			if (!m.Success)
				throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + _log + "\"");

			string obj = m.Groups["object"].Value.Replace(" ", "").Replace("\t", "");

			m = Regex.Match(obj, @"^\s*(?<typeName>[^\[\]\(\)\.\s]+)\s*\([^\)]+\)\s*$");

			if (m.Success)
			{
				string o = m.Groups["typeName"].Value.Replace(" ", "").Replace("\t", "");

				if (resData.ResourceHeader.TypeNames.Contains<string>(o))
					return o;
				else
					throw new Exception("\"" + o + "\"というリソースの型は定義されていません。");
			}
			else
			{
				m = Regex.Match(obj, @"^\s*(?<resName>[^\[\]\(\)\.\s]+)\s*$");

				string o = m.Groups["resName"].Value.Replace(" ", "").Replace("\t", "");

				string result = string.Empty;

				foreach (KeyValuePair<string, GeneralNamedCollection<Resource>> resTypeList in resData.Resources)
				{
					foreach (Resource res in resTypeList.Value)
					{
						if (res.Name == o)
						{
							if (result != string.Empty)
								throw new Exception("\"" + o + "\"という名前のリソースは複数定義されています。");

							result = resTypeList.Key;
						}
					}
				}

				if (result != string.Empty)
					return result;
				else
					throw new Exception("\"" + o + "\"という名前のリソースは定義されていません。");
			}
		}
		public string Value
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)?)\s*\.\s*[^=\s]+\s*=\s*(?<value>[^\s$]+)$");

				if (m.Success)
					return m.Groups["value"].Value.Replace(" ", "").Replace("\t", "");
				else
					throw new Exception("属性代入式のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public bool hasTime
		{
			get
			{
				return Regex.IsMatch(condition, @"\s*\[\s*[0-9a-fA-F]+\s*\]\s*");
			}
		}
		public bool isAttributeChangeLog
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\([^\)]+\))?\s*\.\s*[^=\s]+\s*=\s*[^\s$]+$");

				if (m.Success)
					return true;
				else
					return false;
			}
		}
		public bool isBehaviorCallLog
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\([^\)]+\))?\s*\.\s*[^=\s]+\s*\([^\)]*\)\s*$");

				if (m.Success)
					return true;
				else
					return false;
			}
		}
	}
}
