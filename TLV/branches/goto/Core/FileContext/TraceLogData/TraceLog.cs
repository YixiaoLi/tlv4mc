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
		private string _time = null;
		private string _object = null;
		private string _objectName = null;
		private string _objectType = null;
		private string _behavior = null;
		private string _attribute = null;
		private string _value = null;
		private string _arguments = null;
		private bool _hasTime = false;
		private bool _isAttributeChageLog = false;
		private bool _isBehaviorLog = false;

		private string _log;

		public TraceLog(string log)
		{
			Match m;

			_log = log.Replace(" ", "").Replace("\t", "");

			m = Regex.Match(_log, @"\s*\[\s*(?<time>[0-9a-fA-F]+)\s*\]\s*");
			if (m.Success)
				_time = m.Groups["time"].Value.Replace(" ", "").Replace("\t", "");

			m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*(?<object>[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?)\s*(\.\s*[^\s]+)?\s*$");
			if (m.Success)
				_object = m.Groups["object"].Value.Replace(" ", "").Replace("\t", "");

			m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?\s*\.\s*(?<behavior>[^\(\s]+)\s*\([^\)]*\)\s*$");
			if (m.Success)
				_behavior =  m.Groups["behavior"].Value.Replace(" ", "").Replace("\t", "");

			m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?\s*\.\s*(?<attribute>[^=!<>\(\s]+).*$");
			if (m.Success)
				_attribute = m.Groups["attribute"].Value.Replace(" ", "").Replace("\t", "");

			m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?\s*\.\s*[^=\s]+\s*=\s*(?<value>[^\s$]+)$");
			if (m.Success)
				_value = m.Groups["value"].Value.Replace(" ", "").Replace("\t", "");

			m = Regex.Match(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?\s*\.\s*[^=!<>\(\s]+\s*\((?<args>[^\)]*)\)\s*$");
			if (m.Success)
				_arguments = m.Groups["args"].Value.Replace(" ", "").Replace("\t", "");

			_hasTime = Regex.IsMatch(_log, @"\s*\[\s*[0-9a-fA-F]+\s*\]\s*");
			_isAttributeChageLog = Regex.IsMatch(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\([^\)]+\))?\s*\.\s*[^\(\)=\s]+\s*=\s*[^\s$]+$");
			_isBehaviorLog = Regex.IsMatch(_log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\([^\)]+\))?\s*\.\s*[^=\s]+\s*\([^\)]*\)\s*$");

		}
		public static implicit operator string(TraceLog stdlog)
		{
			return stdlog.ToString();
		}

		public override string ToString()
		{
			return _log;
		}

		public string Time
		{
			get
			{
				if (_time != null)
					return _time;
				else
					throw new Exception("時間指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
			set
			{
				_time = value;
			}
		}
		public string Object
		{
			get
			{
				if (_object != null)
					return _object;
				else
					throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
			set
			{
				_object = value;
			}
		}
		public string ObjectName
		{
			get
			{
				return _objectName;
			}
			set
			{
				_objectName = value;
			}
		}
		public string ObjectType
		{
			get
			{
				return _objectType;
			}
			set
			{
				_objectType = value;
			}
		}
		public string Behavior
		{
			get
			{
				if (_behavior != null)
					return _behavior;
				else
					throw new Exception("振舞い指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string Attribute
		{
			get
			{
				if (_attribute != null)
					return _attribute;
				else
					throw new Exception("属性指定のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string Value
		{
			get
			{
				if (_value != null)
					return _value;
				else
					throw new Exception("属性代入式のフォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public string Arguments
		{
			get
			{
				if (_arguments != null)
					return _arguments;
				else
					throw new Exception("振舞いの引数指定フォーマットが異常です。\n" + "\"" + _log + "\"");
			}
		}
		public bool HasTime
		{
			get
			{
				return _hasTime;
			}
		}
		public bool IsAttributeChangeLog
		{
			get
			{
				return _isAttributeChageLog;
			}
		}
		public bool IsBehaviorCallLog
		{
			get
			{
				return _isBehaviorLog;
			}
		}
	}
}
