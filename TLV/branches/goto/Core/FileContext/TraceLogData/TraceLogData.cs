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
		private Dictionary<string, List<Resource>> _resCache = new Dictionary<string, List<Resource>>();
		private Dictionary<string, string> _attrCache = new Dictionary<string, string>();
		private ResourceData _resourceData;
		private ResourceList _data = null;
		public long MinTime { get; private set; }
		public long MaxTime { get; private set; }
		public TraceLog TraceLog { get; private set; }

		public TraceLogData(TraceLog traceLog, ResourceData resourceData)
			: this(resourceData)
		{
			foreach(string log in traceLog)
			{
				Add(log);
			}
		}
		public TraceLogData(ResourceData resourceData)
			:base()
		{
			TraceLog = new TraceLog();
			_resourceData = resourceData;
			MinTime = long.MaxValue;
			MaxTime = 0;

			_data = new ResourceList();

			foreach (KeyValuePair<string, List<Resource>> kvp in _resourceData.Resources)
			{
				_data.Add(kvp.Key, new List<Resource>());
				foreach (Resource res in kvp.Value)
				{
					_data[kvp.Key].Add(new Resource());
				}
			}

			foreach(KeyValuePair<string, ResourceType> type in _resourceData.ResourceHeader)
			{
				for (int i = 0; i < _data[type.Key].Count; i++)
				{
					foreach (KeyValuePair<string, Attribute> attr in type.Value.Attributes)
					{
						if (attr.Value.AllocationType == AllocationType.Dynamic)
						{
							_data[type.Key][i].Add(attr.Key, new Json(new List<Json>()));
							_data[type.Key][i][attr.Key].Add(new TimeValuePair(0, attr.Value.Default));
						}
					}
				}
			}

			foreach (KeyValuePair<string, List<Resource>> kvp in _resourceData.Resources)
			{
				int i = 0;
				foreach (Resource res in kvp.Value)
				{
					foreach(KeyValuePair<string, Json> attr in res)
					{
						if (_resourceData.ResourceHeader[kvp.Key].Attributes[attr.Key].AllocationType == AllocationType.Dynamic)
						{
							_data[kvp.Key][i][attr.Key][0] = new Json(new TimeValuePair(0, attr.Value.ToString()));
						}
						else
						{
							_data[kvp.Key][i].Add(attr.Key, new Json( attr.Value.ToString()));
						}
					}
					i++;
				}
			}
		}

		public void Add(string log)
		{
			TraceLog.Add(log);
			MinTime = MinTime > getTime(log) ? getTime(log) : MinTime;
			MaxTime = MaxTime < getTime(log) ? getTime(log) : MaxTime;

			if (hasAttribute(log))
			{
				string time = Convert.ToString(getTime(log), _resourceData.TimeRadix);
				string type = getObjectType(log);
				string attr = getAttribute(log);
				string val = getValue(log);
				string conditions = getObjectCondition(log);

				foreach (Resource j in GetResources(type, conditions))
				{
					j[attr].Add(new TimeValuePair(Convert.ToInt64(time, _resourceData.TimeRadix), val));
				}
			}

		}
		/// <summary>
		/// 指定した条件の属性を得る
		/// </summary>
		/// <param name="condition">条件 "Type(atr1==xxx, atr2!=yyy).atr3"</param>
		/// <returns>属性値</returns>
		public string GetAttributeValue(string condition)
		{
			return GetAttributeValue(MaxTime.ToString(), condition);
		}
		/// <summary>
		/// 指定した条件の属性の値を得る
		/// </summary>
		/// <param name="time">時間</param>
		/// <param name="condition">条件 "Type(atr1==xxx, atr2!=yyy).atr3"</param>
		/// <returns>属性値</returns>
		public string GetAttributeValue(string time, string condition)
		{
			if(_attrCache.ContainsKey(time + condition))
				return _attrCache[time + condition];

			string type = getObjectType(condition);
			string attr = getAttribute(condition);
			string cond = getObjectCondition(condition);

			string result = getAttributeValue(Convert.ToInt64(time, _resourceData.TimeRadix), type, cond, attr);
			_attrCache.Add(time + condition, result);
			return result;
		}
		public string GetAttributeValue(string type, string objectCondition, string attribute)
		{
			return getAttributeValue(MaxTime, type, objectCondition, attribute);
		}
		public string GetAttributeValue(string time, string type, string objectCondition, string attribute)
		{
			if (_attrCache.ContainsKey(time + type + objectCondition + attribute))
				return _attrCache[time + type + objectCondition + attribute];

			string result = getAttributeValue(Convert.ToInt64(time, _resourceData.TimeRadix), type, objectCondition, attribute);

			_attrCache.Add(time + type + objectCondition + attribute, result);
			return result;
		}

		private string getAttributeValue(long time, string type, string objectCondition, string attribute)
		{
			if (_attrCache.ContainsKey(time + type + objectCondition + attribute))
				return _attrCache[time + type + objectCondition + attribute];

			string result;

			List<Resource> list = getResources(time, type, objectCondition);
			if(list.Count == 0)
			{
				result = "__NONE__";
			}
			else if (list.Count > 1)
			{
				result = "__MANY__";
			}
			else
			{
				result = getTimeValuePair(list[0][attribute], time).Value;
			}

			_attrCache.Add(time + type + objectCondition + attribute, result);
			return result;
		}

		private List<Resource> getResources(long time, string type, string objectCondition)
		{
			if (_resCache.ContainsKey(time + type + objectCondition))
				return _resCache[time + type + objectCondition];

			List<Resource> result = new List<Resource>();
			for (int i = 0; i < _data[type].Count; i++)
			{
				string cnd = objectCondition;

				foreach (Match m in Regex.Matches(objectCondition, @"(?<attrName>[^=!<>\s]+)\s*(?<ope>(==|!=|<=|>=|<|>))"))
				{
					if(_resourceData.ResourceHeader[type].Attributes[m.Groups["attrName"].Value].AllocationType == AllocationType.Dynamic)
					{
						cnd = cnd.Replace(m.Groups["attrName"].Value, getTimeValuePair(_data[type][i][m.Groups["attrName"].Value], time).Value);
					}
					else
					{
						cnd = cnd.Replace(m.Groups["attrName"].Value, _data[type][i][m.Groups["attrName"].Value]);
					}
				}

				if (ConditionExpression.Result(cnd))
				{
					result.Add(_data[type][i]);
				}
			}

			_resCache.Add(time + type + objectCondition, result);
			return result;
		}
		public List<Resource> GetResources(string type, string objectCondition)
		{
			return getResources(MaxTime, type, objectCondition);
		}

		private TimeValuePair getTimeValuePair(Json json, long time)
		{
			return (TimeValuePair)(json.Last<Json>(j =>
			{
				return ((TimeValuePair)(j.Value)).Time <= time;
			})).Value;
		}
		private TimeValuePair getTimeValuePair(Json json)
		{
			return (TimeValuePair)(json.Last<Json>()).Value;
		}

		protected long getTime(string item)
		{
			return Convert.ToInt64(Regex.Match(item, @"\[\s*(?<time>\w+)\s*\]").Groups["time"].Value, _resourceData.TimeRadix);
		}
		protected string getSubject(string item)
		{
			return Regex.Match(item, @"\s*(?<subject>[^:]+):").Groups["subject"].Value.Replace(" ", "").Replace("\t", "");
		}
		protected string getObject(string item)
		{
			return Regex.Match(item, @"\]?\s*(?<object>[^\.]+)\.").Groups["object"].Value.Replace(" ", "").Replace("\t", "");
		}
		protected string getBehavior(string item)
		{
			return Regex.Match(item, @"\.\s*(?<behavior>[^\(]+\([^\(]+\))").Groups["behavior"].Value.Replace(" ", "").Replace("\t", "");
		}
		protected string getAttribute(string item)
		{
			if (Regex.IsMatch(item, @"\.\s*(?<attribute>[^\s=]+)\s*="))
			{
				return Regex.Match(item, @"\.\s*(?<attribute>[^\s=]+)\s*=").Groups["attribute"].Value.Replace(" ", "").Replace("\t", "");
			}
			else
			{
				return Regex.Match(item, @"\.\s*(?<attribute>[^\s]+)\s*").Groups["attribute"].Value.Replace(" ", "").Replace("\t", "");
			}
		}
		protected string getValue(string item)
		{
			return Regex.Match(item, @"\.\s*(?<attribute>[^=\s]+)\s*=\s*(?<value>\w+)").Groups["value"].Value.Replace(" ", "").Replace("\t", "");
		}
		protected string getObjectType(string item)
		{
			if(Regex.IsMatch(item, @"\[\w+\]"))
			{
				return Regex.Match(item, @"\]\s*(?<type>[^\(]+)\([^\)]+\)\.[^=\(]+[=\(]?").Groups["type"].Value.Replace(" ", "").Replace("\t", "");
			}
			else
			{
				return Regex.Match(item, @"\s*(?<type>[^\(]+)\([^\)]+\)\.[^=\(]+[=\(]?").Groups["type"].Value.Replace(" ", "").Replace("\t", "");
			}
		}
		protected string getObjectCondition(string item)
		{
			if (Regex.IsMatch(item, @"\[\w+\]"))
			{
				return Regex.Match(item, @"\]\s*(?<type>[^\s\(]+)\((?<condition>[^\(]+)\)\.[^=\(]+[=\(]?").Groups["condition"].Value.Replace(" ", "").Replace("\t", "");
			}
			else
			{
				return Regex.Match(item, @"\s*(?<type>[^\s\(]+)\((?<condition>[^\(]+)\)\.[^=\(]+[=\(]?").Groups["condition"].Value.Replace(" ", "").Replace("\t", "");
			}
		}
		protected bool hasAttribute(string item)
		{
			return Regex.IsMatch(item, @"\.\s*(?<attribute>[^=\s]+)\s*=");
		}
		protected bool hasBehavior(string item)
		{
			return Regex.IsMatch(item, @"\.\s*(?<attribute>[^\(\s]+)\s*\(");
		}

		private struct TimeValuePair
		{
			public long Time { get; private set; }
			public string Value { get; private set; }

			public TimeValuePair(long time, string value)
				:this()
			{
				Time = time;
				Value = value;
			}
		}
	}
}
