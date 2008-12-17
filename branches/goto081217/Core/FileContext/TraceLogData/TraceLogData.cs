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
	public class TraceLogData : IJsonable<TraceLogData>
	{
		private ResourceData _resourceData;
		private LogDataBase _data = new LogDataBase();
		
		public Time MinTime { get; private set; }
		public Time MaxTime { get; private set; }
		public TraceLogList TraceLogs { get; private set; }
		public LogDataBase LogDataBase { get { return _data; } }

		public TraceLogData(TraceLogList traceLogs, ResourceData resourceData)
			: this(resourceData)
		{
			foreach (TraceLog log in traceLogs)
			{
				Add(log);
			}
		}
		public TraceLogData(ResourceData resourceData)
			:base()
		{
			TraceLogs = new TraceLogList();
			_resourceData = resourceData;
			MinTime = new Time(Time.MaxTime.ToString(), _resourceData.TimeRadix);
			MaxTime = new Time(Time.MinTime.ToString(), _resourceData.TimeRadix);
		}

		public void Add(TraceLog log)
		{
			log = objectFinalize(log);
			TraceLogs.Add(log);

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
					BehaviorHappenLogData logData = new BehaviorHappenLogData(time, res, log.Behavior, getArguments(res.Type, log.Behavior, log.Arguments));
					_data.Add(logData);
				}
			}

		}

		private TraceLog objectFinalize(TraceLog log)
		{
			Resource res = GetObject(log).First();
			log.Object = res.Name;
			return log;
		}

		private Json getValue(string value, string type, string attr)
		{
			switch (_resourceData.ResourceHeaders[type].Attributes[attr].VariableType)
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

		private ArgumentList getArguments(string type, string behavior, string arguments)
		{
			if (arguments == string.Empty)
				return new ArgumentList();

			string[] args = arguments.Split(',');
			ArgumentList argList = new ArgumentList();

			int i = 0;
			foreach (ArgumentType argType in _resourceData.ResourceHeaders[type].Behaviors[behavior].Arguments)
			{
				switch (argType.Type)
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
				i++;
			}

			return argList;
		}

		public Json GetAttributeValue(string condition)
		{
			return GetAttributeValue(new TraceLog(condition));
		}

		public Json GetAttributeValue(TraceLog condition)
		{
			Json result = new Json();

			IEnumerable<Resource> objs = GetObject(condition);
			int num = objs.Count();

			if (num > 1)
				throw new Exception("複数のリソースがマッチします。\n" + "\"" + condition + "\"");

			if (num == 0)
				throw new Exception("マッチするリソースがありません。\n" + "\"" + condition + "\"");

			Resource res = objs.First();

			Time time = condition.HasTime ? new Time(condition.Time, _resourceData.TimeRadix) : MaxTime;

			if (condition.Attribute == "_name")
				result = new Json(res.Name);
			else if (condition.Attribute == "_displayName")
				result = new Json(res.DisplayName);
			else if (condition.Attribute == "_type")
				result = new Json(res.Type);
			else
			{

				if (!_resourceData.ResourceHeaders[res.Type].Attributes.ContainsKey(condition.Attribute))
					throw new Exception("リソースタイプ" + condition.ObjectType + "は指定された属性" + condition.Attribute + "を持っていません。\n" + "\"" + condition + "\"");


				if (_resourceData.ResourceHeaders[res.Type].Attributes[condition.Attribute].AllocationType == AllocationType.Static)
				{
					result = res.Attributes[condition.Attribute];
				}
				else
				{
					try
					{
						result = ((AttributeChangeLogData)(_data.Where<LogData>((d) =>
						{
							return d.Object.Name == res.Name
								&& d.Type == LogType.AttributeChange
								&& ((AttributeChangeLogData)d).Attribute == condition.Attribute;
						}).Last<LogData>(d => d.Time <= time))).Value;
					}
					catch
					{
						if (_resourceData.ResourceHeaders[res.Type].Attributes[condition.Attribute].Default != null)
							result = _resourceData.ResourceHeaders[res.Type].Attributes[condition.Attribute].Default;
					}
				}
			}

			return result;
		}

		public IEnumerable<Resource> GetObject(string log)
		{
			return GetObject(new TraceLog(log));
		}

		public IEnumerable<Resource> GetObject(TraceLog log)
		{
			string obj = log.Object;
			Match m = Regex.Match(obj, @"^\s*(?<type>[^\[\]\(\)\.\s]+)\s*(\((?<condition>[^\)]+)\))?\s*$");

			if (!m.Success || !m.Groups["type"].Success)
				throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + obj + "\"");
			
			string type = m.Groups["type"].Value.Replace(" ", "").Replace("\t", "");

			if (!m.Groups["condition"].Success)
			{
				if (!_resourceData.Resources.ContainsKey(type))
					throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + type + "\"という名前のリソースはありません。");

				yield return _resourceData.Resources[type];
			}
			else
			{
				string condition = m.Groups["condition"].Value.Replace(" ", "").Replace("\t", "");

				if (!_resourceData.ResourceHeaders.TypeNames.Contains<string>(type))
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
						LogData logData;
						string value;

						if (_resourceData.ResourceHeaders[type].Attributes[kvp.Key].AllocationType == AllocationType.Static)
						{
							value = res.Attributes[kvp.Key];
						}
						else
						{
							try
							{
								logData = _data.Last<LogData>((d) =>
								{
									return d.Object.Name == res.Name
										&& d.Type == LogType.AttributeChange
										&& ((AttributeChangeLogData)d).Attribute == kvp.Key
										&& d.Time <= time;
								});
							}
							catch
							{
								logData = null;
							}

							if (logData != null)
								value = ((AttributeChangeLogData)logData).Value.ToString();
							else if (_resourceData.ResourceHeaders[type].Attributes[kvp.Key].Default != null)
								value = _resourceData.ResourceHeaders[type].Attributes[kvp.Key].Default;
							else
								value = "null";
						}

						cnd = cnd.Replace(kvp.Key, value);
					}
					if (ConditionExpression.Result(cnd))
					{
						yield return res;
					}
				}
			}
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize(this);
		}

		public TraceLogData Parse(string data)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<TraceLogData>(data);
		}

	}
}
