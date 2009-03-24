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
	public class TraceLog : ICloneable
	{
		public string Time { get; private set; }
		public string Object { get; private set; }
		public string ObjectName { get; private set; }
		public string ObjectType { get; private set; }
		public string Behavior { get; private set; }
		public string Attribute { get; private set; }
		public string Value { get; private set; }
		public string Arguments { get; private set; }
		public bool HasTime { get; private set; }
		public bool HasObjectName { get; private set; }
		public bool HasObjectType { get; private set; }
		public TraceLogType Type { get; private set; }

		private string _log;

		public TraceLog(string log)
		{
			Match m;

			_log = Regex.Replace(log, @"\s","");

			m = Regex.Match(_log, @"\[(?<time>[0-9a-zA-Z]+)\]");
			if (m.Success)
				Time = m.Groups["time"].Value;
			HasTime = m.Success;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?(?<object>[^\[\]\(\)\.]+(\([^\)]+\))?)(\.[^\s]+)?$");
			if (m.Success)
				Object = m.Groups["object"].Value;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?(?<objectName>[^\[\]\(\)\.]+)(\.[^\s]+)?$");
			if (m.Success)
				ObjectName = m.Groups["objectName"].Value;
			HasObjectName = m.Success;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?(?<objectType>[^\[\]\(\)\.]+)\([^\)]+\)(\.[^\s]+)?$");
			if (m.Success)
				ObjectType = m.Groups["objectType"].Value;
			HasObjectType = m.Success;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?[^\[\]\(\)\.]+(\([^\)]+\))?\.(?<behavior>[^\(\s]+)\([^\)]*\)$");
			if (m.Success)
				Behavior = m.Groups["behavior"].Value;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?[^\[\]\(\)\.]+(\([^\)]+\))?\.(?<attribute>[^=!<>\(\)]+)([=!<>].*)?$");
			if (m.Success)
				Attribute = m.Groups["attribute"].Value;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?[^\[\]\(\)\.]+(\([^\)]+\))?\.[^=!<>\(\s]+(>=|>|<|<=|==|!=|=)(?<value>[^\s$]+)$");
			if (m.Success)
				Value = m.Groups["value"].Value;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?[^\[\]\(\)\.]+(\([^\)]+\))?\.[^=!<>\(\s]+\((?<args>[^\)]*)\)$");
			if (m.Success)
				Arguments = m.Groups["args"].Value;

			if (Behavior != null && Attribute != null)
				throw new Exception("不正なトレースログです。");
			else if (Behavior == null && Attribute != null)
				Type = TraceLogType.AttributeChange;
			else if (Behavior != null && Attribute == null)
				Type = TraceLogType.BehaviorHappen;


		}

		public static implicit operator string(TraceLog stdlog)
		{
			return stdlog.ToString();
		}
		public static implicit operator TraceLog(string str)
		{
			return new TraceLog(str);
		}

		public override string ToString()
		{
			return _log;
		}

		public Json GetValue(ResourceData resourceData)
		{
			if (!HasObjectType || HasObjectName)
				ObjectType = resourceData.Resources[ObjectName].Type;

			switch (resourceData.ResourceHeaders[ObjectType].Attributes[Attribute].VariableType)
			{
				case JsonValueType.String:
					return new Json(Value);
				case JsonValueType.Number:
					return new Json(Convert.ToDecimal(Value));
				case JsonValueType.Boolean:
					return new Json(Convert.ToBoolean(Value));
				default:
					return null;
			}
		}

		public ArgumentList GetArguments(ResourceData resourceData)
		{
			if (!HasObjectType || HasObjectName)
				ObjectType = resourceData.Resources[ObjectName].Type;

			if (Arguments == string.Empty)
				return new ArgumentList();

			string[] args = Arguments.Split(',');
			ArgumentList argList = new ArgumentList();

			int i = 0;
			foreach (ArgumentType argType in resourceData.ResourceHeaders[ObjectType].Behaviors[Behavior].Arguments)
			{
				if (args[i] == string.Empty)
					argList.Add(null);
				else
					switch (argType.Type)
					{
						case JsonValueType.String:
							argList.Add(new Json(args[i]));
							break;
						case JsonValueType.Number:
							argList.Add(new Json(Convert.ToDecimal(args[i])));
							break;
						case JsonValueType.Boolean:
							argList.Add(new Json(Convert.ToBoolean(args[i])));
							break;
						default:
							argList.Add(null);
							break;
					}

				i++;
				if (args.Length <= i)
					break;
			}

			return argList;
		}

		public object Clone()
		{
			TraceLog log = new TraceLog(_log);
			return log;
		}
	}
}
