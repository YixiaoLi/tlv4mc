/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
using System.Linq.Parallel;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogData : IJsonable<TraceLogData>
	{
		private ResourceData _resourceData;
		
		public Time MinTime { get; private set; }
		public Time MaxTime { get; private set; }
		public TraceLogList TraceLogs { get; private set; }
		public LogDataBase LogDataBase { get; private set; }
        public string Path { get; set; }

		public TraceLogData(TraceLogList traceLogs, ResourceData resourceData)
			:base()
		{
			TraceLogs = new TraceLogList();
			LogDataBase = new LogDataBase();
			_resourceData = resourceData;
			MinTime = Time.MaxTime(_resourceData.TimeRadix);
			MaxTime = Time.MinTime(_resourceData.TimeRadix);
			if (traceLogs != null)
			{
				foreach (TraceLog log in traceLogs)
				{
					Add(log);
				}

				LogDataBase.SetIds();
			}
		}
		public TraceLogData(ResourceData resourceData)
			: this(null, resourceData)
		{
		}

		public void Add(TraceLog log)
		{
			//log = objectFinalize(log);
			TraceLogs.Add(log);

			if (!log.HasTime)
				throw new Exception("ログに時間指定がありません。\n" + "\"" + log + "\"");

			Time time = new Time(log.Time, _resourceData.TimeRadix);

			MinTime = MinTime > time ? time : MinTime;
			MaxTime = MaxTime < time ? time : MaxTime;

			switch(log.Type)
			{
				case TraceLogType.AttributeChange:
					foreach (Resource res in GetObject(log))
					{
						AttributeChangeLogData logData = new AttributeChangeLogData(time, res, log.Attribute, log.GetValue(_resourceData));
						LogDataBase.Add(logData);
					}
					break;
				case TraceLogType.BehaviorHappen:
					foreach (Resource res in GetObject(log))
					{
						BehaviorHappenLogData logData = new BehaviorHappenLogData(time, res, log.Behavior, log.GetArguments(_resourceData));
						LogDataBase.Add(logData);
					}
					break;
			}

		}

		//private TraceLog objectFinalize(TraceLog log)
		//{
		//    Resource res = GetObject(log).First();
		//    log.Object = res.Name;
		//    return log;
		//}

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

						LogDataEnumeable f = new LogDataEnumeable(LogDataEnumeable.GetFirstAttributeSetLogData(res).AsParallel().Where<LogData>((d) =>
						{
							return d.Type == TraceLogType.AttributeChange
								&& ((AttributeChangeLogData)d).Attribute.Name == condition.Attribute;
						}));

						int i = 0;
						foreach (LogData _l in LogDataBase)
						{
							_l.Id = i;
							i++;
						}

						LogDataEnumeable l = new LogDataEnumeable(LogDataBase.AsParallel().Where<LogData>(d=>
								d.Time <= time
								&& d.Object.Name == res.Name
								&& d.Type == TraceLogType.AttributeChange
								&& ((AttributeChangeLogData)d).Attribute.Name == condition.Attribute
							));

						result = ((AttributeChangeLogData)((f + l).Last<LogData>())).Attribute.Value;
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
			Match m = Regex.Match(obj, @"^\s*(?<typeOrName>[^\[\]\(\)\.\s]+)\s*(\((?<condition>[^\)]+)\))?\s*$");

			if (!m.Success || !m.Groups["typeOrName"].Success)
				throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + obj + "\"");

			string typeOrName = m.Groups["typeOrName"].Value.Replace(" ", "").Replace("\t", "");

			if (!m.Groups["condition"].Success)
			{
				if (_resourceData.Resources.ContainsKey(typeOrName))
				{
					yield return _resourceData.Resources[typeOrName];
				}
				else if (_resourceData.ResourceHeaders.ResourceTypes.Any(r => r.Name == typeOrName))
				{
					foreach (Resource res in _resourceData.Resources.Where<Resource>(r => r.Type == typeOrName))
					{
						yield return res;
					}
				}
				else if (!_resourceData.Resources.ContainsKey(typeOrName))
				{
					throw new Exception("オブジェクト指定のフォーマットが異常です。\n" + "\"" + typeOrName + "\"という名前のリソースはありません。");
				}
			}
			else
			{
				string condition = m.Groups["condition"].Value.Replace(" ", "").Replace("\t", "");

				if (!_resourceData.ResourceHeaders.TypeNames.Contains<string>(typeOrName))
					throw new Exception("\"" + typeOrName + "\"というリソースの型は定義されていません。");

				Dictionary<string, string> attrOpeDic = new Dictionary<string, string>();
				Dictionary<string, string> replacedConditions = new Dictionary<string, string>();

				foreach (Match _m in Regex.Matches(condition, @"(?<attr>[^=!<>\s&\|]+)\s*(?<ope>(==|!=|<=|>=|<|>))"))
				{
					attrOpeDic.Add(_m.Groups["attr"].Value, _m.Groups["ope"].Value);
				}

				Time time = log.HasTime ? new Time(log.Time, _resourceData.TimeRadix) : MaxTime;

				foreach (Resource r in _resourceData.Resources.Where<Resource>(r => r.Type == typeOrName))
				{
					string cnd = condition;
					foreach (KeyValuePair<string, string> kvp in attrOpeDic)
					{
						LogData logData;
						string value;

						if (_resourceData.ResourceHeaders[typeOrName].Attributes[kvp.Key].AllocationType == AllocationType.Static)
						{
							value = r.Attributes[kvp.Key];
						}
						else
						{

							LogDataEnumeable f = new LogDataEnumeable(LogDataEnumeable.GetFirstAttributeSetLogData(r).AsParallel().Where<LogData>((d) =>
							{
								return d.Type == TraceLogType.AttributeChange
									&& ((AttributeChangeLogData)d).Attribute.Name == kvp.Key;
							}));

							LogDataEnumeable l = new LogDataEnumeable(LogDataBase.AsParallel().Where<LogData>(d =>
									d.Time <= time
									&& d.Object.Name == r.Name
									&& d.Type == TraceLogType.AttributeChange
									&& ((AttributeChangeLogData)d).Attribute.Name == kvp.Key
								));

							int i = 0;
							foreach (LogData _l in LogDataBase)
							{
								_l.Id = i;
								i++;
							}

							logData = (f + l).LastOrDefault<LogData>();

							if (logData != null)
								value = ((AttributeChangeLogData)logData).Attribute.Value.ToString();
							else if (!r.Attributes[kvp.Key].IsEmpty)
								value = r.Attributes[kvp.Key];
							else if (_resourceData.ResourceHeaders[typeOrName].Attributes[kvp.Key].Default != null)
								value = _resourceData.ResourceHeaders[typeOrName].Attributes[kvp.Key].Default;
							else
								value = "null";
						}

						cnd = cnd.Replace(kvp.Key, value);
					}
					if (ConditionExpression.Result(cnd))
					{
						yield return r;
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
