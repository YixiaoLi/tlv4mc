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
		private Dictionary<string, Dictionary<string, Resource>> _resCache = new Dictionary<string, Dictionary<string, Resource>>();
		private Dictionary<string, string> _objTypeNameCache = new Dictionary<string, string>();
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

			foreach (KeyValuePair<string, GeneralNamedCollection<Resource>> res in _resourceData.Resources)
			{
				_data.Add(res.Key, new GeneralNamedCollection<Resource>());
				foreach (string type in res.Value.Keys)
				{
					_data[res.Key].Add(type, new Resource());
				}
			}

			foreach(string type in _resourceData.ResourceHeader.TypeNames)
			{
				foreach (Resource res in _resourceData.Resources[type])
				{
					foreach (AttributeType attr in _resourceData.ResourceHeader[type].Attributes)
					{
						if (attr.AllocationType == AllocationType.Dynamic)
						{
							_data[type][res.Name].Add(attr.Name, new Attribute() { Name = attr.Name, Value = new Json(new List<Json>()) });
							((Json)(_data[type][res.Name][attr.Name])).Add(new TimeValuePair(0, attr.Default));
						}
					}
				}
			}

			foreach (KeyValuePair<string, GeneralNamedCollection<Resource>> type in _resourceData.Resources)
			{
				int i = 0;
				foreach (Resource res in type.Value)
				{
					foreach(KeyValuePair<string, Json> attr in res)
					{
						if (_resourceData.ResourceHeader[type.Key].Attributes[attr.Key].AllocationType == AllocationType.Dynamic)
						{
							_data[type.Key][res.Key][attr.Key][0] = new Json(new TimeValuePair(0, attr.Value.ToString()));
						}
						else
						{
							_data[type.Key][res.Key].Add(attr.Key, new Json(attr.Value.ToString()));
						}
					}
					i++;
				}
			}
		}

		public void Add(string log)
		{
			long time = timeToLong(getTime(log));
			TraceLog.Add(log);
			MinTime = MinTime > time ? time : MinTime;
			MaxTime = MaxTime < time ? time : MaxTime;

			if (isAttributeChangeLog(log))
			{
				foreach (Resource res in GetResources(log).Values)
				{
					res[getAttribute(log)].Add(new TimeValuePair(time, getValue(log)));
				}
			}

		}

		public string GetAttributeValue(string condition)
		{
			condition = condition.Replace(" ", "").Replace("\t", "");

			string time = string.Empty;
			if (!hasTime(condition))
				time = timeToString(MaxTime);

			if (_attrCache.ContainsKey(time + condition))
				return _attrCache[condition];

			if (!Regex.IsMatch(condition, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)\s*)?\s*(\.\s*[^=!<>\(\s]+)?\s*$"))
				throw new Exception("属性指定の条件式が異常です。\n" + "\"" + condition + "\"");

			string result;

			Dictionary<string, Resource> list = GetResources(condition);

			if (list.Count > 1)
			{
				throw new Exception("複数のリソースがマッチします。\n" + "\"" + condition + "\"");
			}
			else if (list.Count == 0)
			{
				throw new Exception("マッチするリソースがありません。\n" + "\"" + condition + "\"");
			}
			else
			{
				if (_resourceData.ResourceHeader[type].Attributes[attribute].AllocationType == AllocationType.Dynamic)
				{
					result = getTimeValuePair(list.First().Value[attribute], time).Value;
				}
				else
				{
					result = list.First().Value[attribute].Value.ToString();
				}
			}

			_attrCache.Add(time + condition, result);
			return result;
		}
		public Dictionary<string, Resource> GetResources(string condition)
		{
			condition = condition.Replace(" ", "").Replace("\t", "");

			string time = string.Empty;
			if (!hasTime(condition))
				time = timeToString(MaxTime);

			if (_resCache.ContainsKey(time + condition))
				return _resCache[condition];

			if (!Regex.IsMatch(condition, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)\s*)?\s*$"))
				throw new Exception("リソース指定の条件式が異常です。\n" + "\"" + condition + "\"");

			Dictionary<string, Resource> result = new Dictionary<string, Resource>();
			foreach (KeyValuePair<string, Resource> res in _data[getObjectType(condition)])
			{
				string cnd = condition;

				foreach (Match m in Regex.Matches(condition, @"(?<attrName>[^=!<>\s]+)\s*(?<ope>(==|!=|<=|>=|<|>))"))
				{
					if (_resourceData.ResourceHeader[type].Attributes[m.Groups["attrName"].Value].AllocationType == AllocationType.Dynamic)
					{
						cnd = cnd.Replace(m.Groups["attrName"].Value, getTimeValuePair(_data[type][res.Key][m.Groups["attrName"].Value], time).Value);
					}
					else
					{
						cnd = cnd.Replace(m.Groups["attrName"].Value, _data[type][res.Key][m.Groups["attrName"].Value]);
					}
				}

				if (ConditionExpression.Result(cnd))
				{
					result.Add(res.Key, _data[type][res.Key]);
				}
			}

			_resCache.Add(time + condition, result);

			return result;
		}

		protected string getTime(string log)
		{
			Match m = Regex.Match(log, @"\s*\[\s*(?<time>[0-9a-fA-F]+)\s*\]\s*");

			if (m.Success)
				return m.Groups["time"].Value.Replace(" ", "").Replace("\t", "");
			else
				throw new Exception("時間指定のフォーマットが異常です。\n" + "\"" + log + "\"");
		}
		protected string getObject(string log)
		{
			Match m = Regex.Match(log, @"^\s*(\[\s*[^\]]+\s*\])?\s*(?<object>[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?)\s*(\.\s*[^=!<>\(\s]+)?\s*$");

			if (m.Success)
				return m.Groups["object"].Value.Replace(" ", "").Replace("\t", "");
			else
				throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + log + "\"");
		}
		protected string getBehavior(string log)
		{
			Match m = Regex.Match(log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)?)\s*\.\s*(?<behavior>[^=!<>\(\s]+\s*\([^\)]+\))\s*$");

			if (m.Success)
				return m.Groups["behavior"].Value.Replace(" ", "").Replace("\t", "");
			else
				throw new Exception("振舞い指定のフォーマットが異常です。\n" + "\"" + log + "\"");
		}
		protected string getAttribute(string log)
		{
			Match m = Regex.Match(log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)?)\s*\.\s*(?<attribute>[^=!<>\(\s]+)\s*$");

			if (m.Success)
				return m.Groups["attribute"].Value.Replace(" ", "").Replace("\t", "");
			else
				throw new Exception("属性指定のフォーマットが異常です。\n" + "\"" + log + "\"");
		}
		protected string getObjectType(string log)
		{
			Match m = Regex.Match(log, @"^\s*(\[\s*[^\]]+\s*\])?\s*(?<object>[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\))?)\s*(\.\s*[^=!<>\(\s]+)?\s*$");

			if (!m.Success)
				throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + log + "\"");

			string obj = m.Groups["object"].Value.Replace(" ", "").Replace("\t", "");

			m = Regex.Match(log, @"^\s*(?<typeName>[^\[\]\(\)\.\s]+)\s*\([^\)]+\)\s*$");

			if (m.Success)
			{
				string o = m.Groups["typeName"].Value.Replace(" ", "").Replace("\t", "");

				if (_resourceData.ResourceHeader.TypeNames.Contains<string>(o))
					return o;
				else
					throw new Exception("\"" + o + "\"というリソースの型は定義されていません。");
			}
			else
			{
				m = Regex.Match(log, @"^\s*(?<resName>[^\[\]\(\)\.\s]+)\s*$");
				
				string o = m.Groups["resName"].Value.Replace(" ", "").Replace("\t", "");

				if (_objTypeNameCache.ContainsKey(log))
					return _objTypeNameCache[log];

				string result = string.Empty;

				foreach(KeyValuePair<string, Dictionary<string, Resource>> resTypeList in _resourceData.Resources)
				{
					foreach(KeyValuePair<string, Resource> resList in resTypeList.Value)
					{
						if (resList.Key == o)
						{
							if (result != string.Empty)
								throw new Exception("\"" + o + "\"という名前のリソースは複数定義されています。");

							result = resTypeList.Key;
						}
					}
				}

				_objTypeNameCache.Add(log, o);

				throw new Exception("\"" + o + "\"という名前のリソースは定義されていません。");
			}
		}
		protected string getValue(string log)
		{
			Match m = Regex.Match(log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)?)\s*\.\s*[^=\s]+\s*=\s*(?<value>[^\s$]+)$");

			if (m.Success)
				return m.Groups["value"].Value.Replace(" ", "").Replace("\t", "");
			else
				throw new Exception("属性代入式のフォーマットが異常です。\n" + "\"" + log + "\"");
		}

		protected bool hasTime(string condition)
		{
			return Regex.IsMatch(condition, @"\s*\[\s*[0-9a-fA-F]+\s*\]\s*");
		}

		protected bool isAttributeChangeLog(string log)
		{
			Match m = Regex.Match(log, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)?)\s*\.\s*(?<attribute>[^=\s]+)\s*=\s*[^\s$]+$");

			if (m.Success)
				return true;
			else
				return false;
		}

		protected long timeToLong(string time)
		{
			return Convert.ToInt64(time, _resourceData.TimeRadix);
		}
		protected string timeToString(long time)
		{
			return Convert.ToString(time, _resourceData.TimeRadix);
		}

	}
}
