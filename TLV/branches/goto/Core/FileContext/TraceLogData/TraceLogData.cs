using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogData
	{
		private ResourceData _resourceData;
		private LogDataBase _data = new LogDataBase();
		
		public Time MinTime { get; private set; }
		public Time MaxTime { get; private set; }
		public TraceLogList TraceLogList { get; private set; }

		public TraceLogData(TraceLogList traceLog, ResourceData resourceData)
			: this(resourceData)
		{
			foreach(TraceLog log in traceLog)
			{
				Add(log);
			}
		}
		public TraceLogData(ResourceData resourceData)
			:base()
		{
			TraceLogList = new TraceLogList();
			_resourceData = resourceData;
			MinTime = new Time(long.MaxValue.ToString(), _resourceData.TimeRadix);
			MinTime = new Time("0", _resourceData.TimeRadix);
		}

		public void Add(TraceLog log)
		{
			TraceLogList.Add(log);

			if (!log.HasTime)
				throw new Exception("ログに時間指定がありません。\n" + "\"" + log + "\"");

			Time time = new Time(log.Time, _resourceData.TimeRadix);

			MinTime = MinTime > time ? time : MinTime;
			MaxTime = MaxTime < time ? time : MaxTime;

			if (log.IsAttributeChangeLog)
			{
				foreach (Resource res in GetObject(log))
				{
					AttributeChangeLogData logData = new AttributeChangeLogData(time, res, log.Attribute, getValue(log.Value, res.Type, log.Attribute));
					_data.Add(logData);
				}
			}
			else if(log.IsBehaviorCallLog)
			{
				foreach (Resource res in GetObject(log))
				{
					BehaviorCallLogData logData = new BehaviorCallLogData(time, res, log.Behavior, getArguments(log.Arguments, res.Type, log.Behavior));
					_data.Add(logData);
				}
			}

		}

		private Json getValue(string value, string type, string attr)
		{
			switch (_resourceData.ResourceHeader[type].Attributes[attr].VariableType)
			{
				case JsonValueType.String:
					return new Json(value);
				case JsonValueType.Decimal:
					return new Json(Convert.ToDecimal(value));
				case JsonValueType.Boolean:
					return new Json(Convert.ToBoolean(value));
				default:
					return new Json("null");
			}
		}

		private ArgumentList getArguments(string arguments, string type, string behavior)
		{
			string[] args = arguments.Split(',');
			ArgumentList argList = new ArgumentList();

			for(int i = 0; i < args.Length; i++)
			{
				switch (_resourceData.ResourceHeader[type].Behaviors[behavior].Arguments[i].Type)
				{
					case JsonValueType.String:
						argList.Add(new Json(args[i]));
						break;
					case JsonValueType.Decimal:
						argList.Add(new Json(Convert.ToDecimal(args[i])));
						break;
					case JsonValueType.Boolean:
						argList.Add(new Json(Convert.ToBoolean(args[i])));
						break;
					default:
						argList.Add(new Json("null"));
						break;
				}
			}

			return argList;
		}

		public Json GetAttributeValue(string condition)
		{
			return GetAttributeValue(new TraceLog(condition));
		}

		public Json GetAttributeValue(TraceLog condition)
		{
			Json result;

			IEnumerable<Resource> objs = GetObject(condition);

			if (objs.Count() > 1)
				throw new Exception("複数のリソースがマッチします。\n" + "\"" + condition + "\"");

			if (objs.Count() == 0)
				throw new Exception("マッチするリソースがありません。\n" + "\"" + condition + "\"");

			Resource obj = objs.First();
			Time time = condition.HasTime ? new Time(condition.Time, _resourceData.TimeRadix) : MaxTime;
			result = ((AttributeChangeLogData)(_data.Where<LogData>( (d) =>
						{
							return  d.Object.Name == obj.Name
								&& d.Type == LogType.AttributeChange
								&& ((AttributeChangeLogData)d).Attribute == condition.Attribute;
						}).Last<LogData>(d=>d.Time <= time))).Value;

			return result;
		}

		public IEnumerable<Resource> GetObject(string log)
		{
			return GetObject(new TraceLog(log));
		}

		public IEnumerable<Resource> GetObject(TraceLog log)
		{
			string obj = log.Object;
			Match m = Regex.Match(obj, @"^\s*(?<type>[^\[\]\(\)\.\s]+)\s*\((?<condition>[^\)]+)\)\s*$");

			if (m.Success)
				throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + obj + "\"");

			string type = m.Groups["type"].Value.Replace(" ", "").Replace("\t", "");
			string condition = m.Groups["condition"].Value.Replace(" ", "").Replace("\t", "");

			if (!_resourceData.ResourceHeader.TypeNames.Contains<string>(type))
				throw new Exception("\"" + type + "\"というリソースの型は定義されていません。");

			Dictionary<string, string> attrOpeDic = new Dictionary<string, string>();
			Dictionary<string, string> replacedConditions = new Dictionary<string, string>();

			foreach (Match _m in Regex.Matches(condition, @"(?<attr>[^=!<>\s]+)\s*(?<ope>(==|!=|<=|>=|<|>))"))
			{
				attrOpeDic.Add(_m.Groups["attr"].Value, _m.Groups["ope"].Value);
			}

			Time time = log.HasTime ? new Time(log.Time, _resourceData.TimeRadix) : MaxTime;

			foreach (Resource res in _resourceData.Resources.Where<Resource>(r => r.Type == type))
			{
				string cnd = condition;
				foreach (KeyValuePair<string, string> kvp in attrOpeDic)
				{
					LogData logData = _data.Last<LogData>((d) =>
					{
						return d.Object.Name == res.Name
							&& d.Type == LogType.AttributeChange
							&& ((AttributeChangeLogData)d).Attribute == kvp.Key
							&& d.Time <= time;
					});

					string value;

					if (logData != null)
						value = ((AttributeChangeLogData)logData).Value.ToString();
					else if (_resourceData.ResourceHeader[type].Attributes[kvp.Key].Default != null)
						value = _resourceData.ResourceHeader[type].Attributes[kvp.Key].Default;
					else
						value = "null";

					cnd.Replace(kvp.Key, value);
				}
				if (ConditionExpression.Result(cnd))
				{
					yield return res;
				}
			}
		}
	}
}
