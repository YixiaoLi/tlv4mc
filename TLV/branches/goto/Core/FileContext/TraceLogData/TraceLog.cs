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
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*(?<object>[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)))\s*(\.\s*[^=!<>\(\s]+)?\s*$");

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
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))\s*\.\s*(?<behavior>[^=!<>\(\s]+\s*\([^\)]+\))\s*$");

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
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))\s*\.\s*(?<attribute>[^=!<>\(\s]+)\s*$");

				if (m.Success)
					return m.Groups["attribute"].Value.Replace(" ", "").Replace("\t", "");
				else
					throw new Exception("属性指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string Value
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))\s*\.\s*[^=\s]+\s*=\s*(?<value>[^\s$]+)$");

				if (m.Success)
					return m.Groups["value"].Value.Replace(" ", "").Replace("\t", "");
				else
					throw new Exception("属性代入式のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string Arguments
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))\s*\.\s*[^=!<>\(\s]+\s*\((?<args>[^\)]+)\)\s*$");

				if (m.Success)
					return m.Groups["args"].Value.Replace(" ", "").Replace("\t", "");
				else
					throw new Exception("振舞いの引数指定フォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public bool HasTime
		{
			get
			{
				return Regex.IsMatch(_log, @"\s*\[\s*[0-9a-fA-F]+\s*\]\s*");
			}
		}
		public bool IsAttributeChangeLog
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\([^\)]+\))\s*\.\s*[^=\s]+\s*=\s*[^\s$]+$");

				if (m.Success)
					return true;
				else
					return false;
			}
		}
		public bool IsBehaviorCallLog
		{
			get
			{
				Match m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\([^\)]+\))\s*\.\s*[^=\s]+\s*\([^\)]*\)\s*$");

				if (m.Success)
					return true;
				else
					return false;
			}
		}
	}
}
