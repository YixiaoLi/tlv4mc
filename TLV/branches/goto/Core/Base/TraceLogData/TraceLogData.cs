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
	public class TraceLogData
	{
		private ResourceData _resourceData;
		private Json _data = new Json();
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
			MaxTime = long.MinValue;

			_data.makeObject();

			foreach (KeyValuePair<string,Json> kvp in _resourceData.Resources)
			{
				_data.Add(kvp.Key, new List<Json>());
				foreach (Json res in kvp.Value)
				{
					_data[kvp.Key].Add(new Dictionary<string, Json>());
				}
			}

			foreach(KeyValuePair<string, ResourceType> type in _resourceData.ResourceHeader)
			{
				for (int i = 0; i < _data[type.Key].Count; i++)
				{
					foreach (KeyValuePair<string, Attribute> attr in type.Value.Attributes)
					{
						_data[type.Key][i].Add(attr.Key, new List<Json>());
						_data[type.Key][i][attr.Key].Add(new TimeValuePair ( 0, attr.Value.Default ));
					}
				}
			}

			foreach (KeyValuePair<string, Json> kvp in _resourceData.Resources)
			{
				int i = 0;
				foreach (Json res in kvp.Value)
				{
					foreach(KeyValuePair<string, Json> attr in res.GetKeyValuePaierEnumerator())
					{
						_data[kvp.Key][i][attr.Key][0] = new Json(new TimeValuePair( 0, attr.Value.ToString() ));
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
				ComparisonExpressionList conditions = new ComparisonExpressionList();
				foreach (string condition in getObjectCondition(log).Replace(" ", "").Split(','))
				{
					conditions.Add(new ComparisonExpression(condition));
				}

				foreach (Json j in getResources(type, conditions))
				{
					j[attr].Add(new TimeValuePair(Convert.ToInt64(time, _resourceData.TimeRadix), val));
				}
			}

		}

		public string GetAttributeValue(string condition)
		{
			return GetAttributeValue(Convert.ToString(MaxTime, _resourceData.TimeRadix), condition);
		}

		/// <summary>
		/// 指定した条件の属性の値を得る
		/// </summary>
		/// <param name="time">時間</param>
		/// <param name="condition">条件 "Type(atr1==xxx, atr2!=yyy).atr3"</param>
		/// <returns></returns>
		public string GetAttributeValue(string time, string condition)
		{
			string type = getObjectType(condition);
			string attr = getAttribute(condition);
			ComparisonExpressionList conditions = new ComparisonExpressionList();
			foreach (string c in getObjectCondition(condition).Replace(" ", "").Split(','))
			{
				conditions.Add(new ComparisonExpression(c));
			}

			return getAttributeValue(Convert.ToInt64(time, _resourceData.TimeRadix), type, conditions, attr);
		}

		private string getAttributeValue(long time, string type, ComparisonExpressionList conditions, string attribute)
		{

			List<Json> list = getResources(type, conditions);
			if (list.Count != 1)
			{
				return string.Empty;
			}
			else
			{
				return ((TimeValuePair)(list[0][attribute].Last<Json>(j =>
				{
					return ((TimeValuePair)(j.Value)).Time < time;
				})).Value).Value;
			}
		}

		private List<Json> getResources(string type, ComparisonExpressionList conditions)
		{

			List<Json> result = new List<Json>();

			for (int i = 0; i < _data[type].Count; i++)
			{
				bool f = true;
				foreach (ComparisonExpression c in conditions)
				{
					string value = ((TimeValuePair)(_data[type][i][c.Left].Last<Json>().Value)).Value;

					ComparisonExpression ce = new ComparisonExpression(c.Right, c.Ope, value);

					if (!ce.Result(_resourceData.ResourceHeader[type].Attributes[c.Left].VariableType))
						f = false;
				}
				if(f)
				{
					result.Add(_data[type][i]);
				}
			}

			return result;
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
	}

	public class TimeValuePair
	{
		public long Time { get; private set; }
		public string Value { get; private set; }

		public TimeValuePair(long time, string value)
		{
			Time = time;
			Value = value;
		}
	}
}
