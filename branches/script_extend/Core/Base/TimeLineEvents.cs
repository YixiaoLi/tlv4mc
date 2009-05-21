/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Text.RegularExpressions;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLineEvents
	{
		protected TraceLogVisualizerData _data;
		private Dictionary<string, string> _cache = new Dictionary<string, string>();
		private VisualizeRule _rule;
		private Event _evnt;
		private List<Event> _evnts = new List<Event>();
		private EventShapes _drawShapes = new EventShapes();
		private Resource _target;
		private LogDataEnumeable _logData;
		private bool _dataSet = false;

		public string Name { get { return ToString(); } }
		//public Thread SetDataThread { get; private set; }
		public VisualizeRule Rule { get { return _rule; } }
		public Event Event { get { return _evnt; } }
		public Resource Target { get { return _target; } }
		public List<Event> Events { get { return _evnts; } }
		public bool IsDataSet { get { return _dataSet; } }

		public TimeLineEvents(VisualizeRule rule)
			: this(rule, null, null) { }
		public TimeLineEvents(VisualizeRule rule, Event evnt)
			: this(null, evnt, null) { }
		public TimeLineEvents(Resource target)
			: this(null, null, target) { }
		public TimeLineEvents(VisualizeRule rule, Resource target)
			: this(rule, null, target) { }
		public TimeLineEvents(Event evnt, Resource target)
			: this(null, evnt, target) { }
		private TimeLineEvents(VisualizeRule rule, Event evnt, Resource target)
		{
			if (rule != null && rule.IsBelongedTargetResourceType() && target == null)
				throw new ArgumentException("ターゲット指定があるルールはtargetをnullに出来ません。");

			if (!(rule != null && evnt == null && target == null
				|| rule == null && evnt != null && target == null
				|| rule == null && evnt == null && target != null
				|| rule != null && evnt == null && target != null
				|| rule == null && evnt != null && target != null))
				throw new ArgumentException("引数が異常です。");

			_rule = rule;
			_evnt = evnt;
			_target = target;

			//SetDataThread = new Thread(new ThreadStart(() =>
			//{
			//    _dataSet = false;
			//    if (_data.VisualizeData.EventShapes.ContainsKey(Name))
			//    {
			//        _drawShapes = _data.VisualizeData.EventShapes[Name];
			//    }
			//    else
			//    {
			//        _logData = new LogDataEnumeable(_data.TraceLogData.LogDataBase);
			//        _logData.Filter(_target);

			//        if (_rule != null && _evnt == null)
			//        {
			//            addEvents(_rule);
			//        }
			//        else if (_rule == null && _evnt != null)
			//        {
			//            _evnts.Add(_evnt);
			//        }
			//        else if (_rule == null && _evnt == null && _target != null)
			//        {
			//            foreach (VisualizeRule r in _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(r => r.Target == _target.Type))
			//            {
			//                addEvents(r);
			//            }
			//        }

			//        foreach (Event e in _evnts)
			//        {
			//            addDrawShapeByEvent(e);
			//        }

			//        _drawShapes.Optimize();
			//        _drawShapes.Name = Name;

			//        lock (_data.VisualizeData.EventShapes)
			//        {
			//            if (!_data.VisualizeData.EventShapes.ContainsKey(Name))
			//            {
			//                _data.VisualizeData.EventShapes.Add(_drawShapes);
			//            }
			//        }
			//    }
			//    _dataSet = true;

			//}));
		}

		public void SetData(TraceLogVisualizerData data)
		{
			ClearData();

			_data = data;


			_dataSet = false;
			_logData = new LogDataEnumeable(_data.TraceLogData.LogDataBase);
			_logData.Filter(_target);

			if (_rule != null && _evnt == null)
			{
				addEvents(_rule);
			}
			else if (_rule == null && _evnt != null)
			{
				_evnts.Add(_evnt);
			}
			else if (_rule == null && _evnt == null && _target != null)
			{
				foreach (VisualizeRule r in _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(r => r.Target == _target.Type))
				{
					addEvents(r);
				}
			}

			foreach (Event e in _evnts)
			{
				addDrawShapeByEvent(e);
			}

			_drawShapes.Optimize();

			_dataSet = true;


			//SetDataThread.Start();
		}

		public void ClearData()
		{
			_drawShapes.Clear();
			_dataSet = false;
		}

		private void addEvents(VisualizeRule rule)
		{
			foreach (Event evnt in rule.Shapes)
			{
				_evnts.Add(evnt);
			}
		}

		private void addDrawShapeByEvent(Event evnt)
		{
			if(evnt.When != null && evnt.From == null && evnt.To == null)
				addDrawShapeFromWhenEvent(evnt);
			else if(evnt.When == null && evnt.From != null && evnt.To != null)
				addDrawShapeFromBetweenEvent(evnt);
		}

		private void addDrawShapeFromBetweenEvent(Event evnt)
		{
			Dictionary<TraceLog, LogData> fromLogDic = new Dictionary<TraceLog, LogData>();
			Dictionary<TraceLog,List<Resource>> toLogDic = new Dictionary<TraceLog,List<Resource>>();

			LogDataEnumeable fromLogData;
			LogDataEnumeable toLogData;

			TraceLog fromLog = applyTARGET("TARGET", evnt.From, _target);
			fromLog = TLVFunction.Apply(fromLog, _data.ResourceData, _data.TraceLogData);
			TraceLog toLog = applyTARGET("TARGET", evnt.To, _target);

			LogDataEnumeable firsts = getFirstLogData(fromLog);

			if (fromLog.Attribute != null && fromLog.Behavior == null)
				fromLogData = new LogDataEnumeable(_logData.GetEnumerator<AttributeChangeLogData>(fromLog.Attribute, fromLog.Value));
			else if (fromLog.Attribute == null && fromLog.Behavior != null)
				fromLogData = new LogDataEnumeable(_logData.GetEnumerator<BehaviorHappenLogData>(fromLog.Behavior, fromLog.Arguments != null ? fromLog.Arguments.Split(',') : null));
			else
				fromLogData = new LogDataEnumeable(_logData);

			if (toLog.Attribute != null && toLog.Behavior == null)
				toLogData = new LogDataEnumeable(_logData.GetEnumerator<AttributeChangeLogData>(toLog.Attribute));
			else if (toLog.Attribute == null && toLog.Behavior != null)
				toLogData = new LogDataEnumeable(_logData.GetEnumerator<BehaviorHappenLogData>(toLog.Behavior));
			else
				toLogData = new LogDataEnumeable(_logData);

			LogDataEnumeable logData = firsts + fromLogData + toLogData;

			foreach (LogData log in logData)
			{
				List<TraceLog> delKeys = new List<TraceLog>();
				foreach (KeyValuePair<TraceLog, List<Resource>> kpv in toLogDic)
				{
					if (kpv.Key != null && kpv.Value != null && log.CheckAttributeOrBehavior(kpv.Key) && kpv.Value.Contains(log.Object))
					{

						LogData fl = fromLogDic[kpv.Key];

						addDrawShape(evnt.Figures, fl, log, evnt);

						delKeys.Add(kpv.Key);

					}
				}
				foreach(TraceLog tl in delKeys)
				{
					toLogDic.Remove(tl);
				}

				IEnumerable<Resource> fromRes = _data.TraceLogData.GetObject("[" + (log.Time - 0.0000000001m).ToString() + "]" + (fromLog.ObjectName != null ? fromLog.ObjectName : fromLog.Object));

				if (log.CheckAttributeOrBehavior(fromLog) && fromRes.Contains(log.Object))
				{
					TraceLog tmpToLog = null;
					List<Resource> tmpToRes = null;

					tmpToLog = applyTARGET("FROM_TARGET", toLog, log.Object);
					tmpToLog = applyTIME("FROM_TIME", tmpToLog, log.Time);
					if(log is AttributeChangeLogData)
						tmpToLog = applyVAL("FROM_VAL", tmpToLog, ((AttributeChangeLogData)log).Attribute.Value.ToString());
					if (log is BehaviorHappenLogData)
						tmpToLog = applyARG("FROM_ARG", tmpToLog, ((BehaviorHappenLogData)log).Behavior.Arguments.ToString().Split(','));

					tmpToLog = TLVFunction.Apply(tmpToLog, _data.ResourceData, _data.TraceLogData);
					tmpToRes = _data.TraceLogData.GetObject(tmpToLog.ObjectName != null ? tmpToLog.ObjectName : tmpToLog.Object).ToList();

					toLogDic.Add(tmpToLog,tmpToRes.ToList());

					fromLogDic.Add(tmpToLog, log);
				}
			}

		}

		private void addDrawShapeFromWhenEvent(Event evnt)
		{
			LogDataEnumeable logData;
			TraceLog log = applyTARGET("TARGET", evnt.When, _target);
			log = TLVFunction.Apply(log, _data.ResourceData, _data.TraceLogData);

			if (log.Attribute != null && log.Behavior == null)
				logData = new LogDataEnumeable(_logData.GetEnumerator<AttributeChangeLogData>(log.Attribute, log.Value));
			else if (log.Attribute == null && log.Behavior != null)
				logData = new LogDataEnumeable(_logData.GetEnumerator<BehaviorHappenLogData>(log.Behavior, log.Arguments != null ? log.Arguments.Split(',') : null));
			else
				logData = new LogDataEnumeable(_logData);

			IEnumerable<Resource> res = _data.TraceLogData.GetObject(log.Object);

			if (log.Attribute != null && log.Behavior == null)
			{
				LogDataEnumeable firsts = new LogDataEnumeable(getFirstLogData(log).Where(l => ((AttributeChangeLogData)l).Attribute.Name == log.Attribute));
				logData = firsts + logData;
			}

			foreach(LogData[] logs in logData.GetPrevPostSetEnumerator())
			{
				if (res.Contains(logs[1].Object))
				{
					addDrawShape(evnt.Figures, logs[1], logs[2], evnt);
				}
			}
		}

		private LogDataEnumeable getFirstLogData(TraceLog log)
		{
			if (_target == null)
				return new LogDataEnumeable(null);

			return LogDataEnumeable.GetFirstAttributeSetLogData(_target);
		}

		private void addDrawShape(Figures figures, LogData from, LogData to, Event evnt)
		{

			Time fromTime = from == null ? _data.TraceLogData.MinTime : from.Time;
			Time toTime = to == null ? _data.TraceLogData.MaxTime : to.Time;

			foreach (Figure fg in figures)
			{
				string condition = fg.Condition;

				condition = applyTemplate(from, to, condition);
				if (condition != null)
				{
					if (!_cache.ContainsKey(condition))
						_cache[condition] = TLVFunction.Apply(condition, _data.ResourceData, _data.TraceLogData);

					condition = _cache[condition];
				}
				string[] spArgs = null;

				if (fg.Args != null)
				{
					spArgs = new string[fg.Args.Length];

					for (int i = 0; i < fg.Args.Length; i++)
					{
						spArgs[i] = applyTemplate(from, to, fg.Args[i]);

						if (spArgs[i] != null)
						{
							if (!_cache.ContainsKey(spArgs[i]))
								_cache[spArgs[i]] = TLVFunction.Apply(spArgs[i], _data.ResourceData, _data.TraceLogData);

							spArgs[i] = _cache[spArgs[i]];
						}
					}
				}
				
				if (checkCondition(condition))
				{
					if (fg.IsFigures)
					{
						addDrawShape(fg.Figures, from, to, evnt);
					}
					else if (fg.IsShape)
					{
						foreach(Shape sp in _data.VisualizeData.Shapes[fg.Shape])
						{
							Shape s = (Shape)sp.Clone();

							if (spArgs != null && spArgs.Count() != 0)
								s.SetArgs(spArgs);

							s.SetDefaultValue();
							s.ChackValidate();

							_drawShapes.Add(new EventShape(fromTime, toTime, s, evnt));
						}
					}
				}
			}
		}

		private string applyTemplate(LogData from, LogData to, string condition)
		{
			if (condition != null)
			{
				if (condition.Contains("${TARGET}"))
					condition = applyTARGET("TARGET", condition, _target);

				if (from != null)
				{

					if (condition.Contains("${FROM_TARGET}"))
						condition = applyTARGET("FROM_TARGET", condition, from.Object);

					if (condition.Contains("${FROM_TIME}"))
						condition = applyTIME("FROM_TIME", condition, from.Time);

					if (condition.Contains("${TIME}"))
						condition = applyTIME("TIME", condition, from.Time);

					if (from is AttributeChangeLogData)
					{
						if (condition.Contains("${FROM_VAL}"))
							condition = applyVAL("FROM_VAL", condition, ((AttributeChangeLogData)from).Attribute.Value.ToString());
						if (condition.Contains("${VAL}"))
							condition = applyVAL("VAL", condition, ((AttributeChangeLogData)from).Attribute.Value.ToString());
					}

					if (from is BehaviorHappenLogData)
					{
						if (condition.Contains("${FROM_ARG"))
							condition = applyARG("FROM_ARG", condition, ((BehaviorHappenLogData)from).Behavior.Arguments.ToString().Split(','));
						if (condition.Contains("${ARG"))
							condition = applyARG("ARG", condition, ((BehaviorHappenLogData)from).Behavior.Arguments.ToString().Split(','));
					}
				}
				if (to != null)
				{
					if (condition.Contains("${TO_TARGET}"))
						condition = applyTARGET("TO_TARGET", condition, to.Object);

					if (condition.Contains("${TO_TIME}"))
						condition = applyTIME("TO_TIME", condition, to.Time);

					if (condition.Contains("${TIME}"))
						condition = applyTIME("TIME", condition, to.Time);

					if (to is AttributeChangeLogData)
					{
						if (condition.Contains("${TO_VAL}"))
							condition = applyVAL("TO_VAL", condition, ((AttributeChangeLogData)to).Attribute.Value.ToString());
						if (condition.Contains("${VAL}"))
							condition = applyVAL("VAL", condition, ((AttributeChangeLogData)to).Attribute.Value.ToString());
					}

					if (to is BehaviorHappenLogData)
					{
						if (condition.Contains("${TO_ARG"))
							condition = applyARG("TO_ARG", condition, ((BehaviorHappenLogData)to).Behavior.Arguments.ToString().Split(','));
						if (condition.Contains("${ARG"))
							condition = applyARG("ARG", condition, ((BehaviorHappenLogData)to).Behavior.Arguments.ToString().Split(','));
					}
				}
			}
			return condition;
		}

		protected string applyTARGET(string type, string log, Resource resource)
		{
			if (resource == null)
				return log;

			if (!new string[] { "TARGET", "FROM_TARGET", "TO_TARGET" }.Contains(type))
				throw new ArgumentException("typeはTARGET, FROM_TARGET, TO_TARGETのいずれかでなければなりません。");

			string logstr = log;
			logstr = Regex.Replace(logstr, @"\${" + type + "}", resource.Name);
			return logstr;
		}

		protected TraceLog applyTARGET(string type, TraceLog log, Resource resource)
		{
			return new TraceLog(applyTARGET(type, log.ToString(), resource));
		}

		protected string applyTIME(string type, string log, Time time)
		{
			if (time == null)
				return log;

			if (!new string[] { "TIME", "FROM_TIME", "TO_TIME" }.Contains(type))
				throw new ArgumentException("typeはTIME, FROM_TIME, TO_TIMEのいずれかでなければなりません。");

			string logstr = log;
			logstr = Regex.Replace(logstr, @"\${" + type + "}", time.ToString());
			return logstr;
		}

		protected TraceLog applyTIME(string type, TraceLog log, Resource resource)
		{
			return new TraceLog(applyTIME(type, log.ToString(), resource));
		}

		protected string applyVAL(string type, string log, string value)
		{
			if (value == null)
				throw new ArgumentException("valueがnullです。");

			if (!new string[] { "VAL", "FROM_VAL", "TO_VAL" }.Contains(type))
				throw new ArgumentException("typeはVAL, FROM_VAL, TO_VALのいずれかでなければなりません。");

			string logstr = log;
			logstr = Regex.Replace(logstr, @"\${" + type + "}", value);
			return logstr;
		}

		protected TraceLog applyVAL(string type, TraceLog log, string value)
		{
			return new TraceLog(applyVAL(type, log.ToString(), value));
		}

		protected string applyARG(string type, string log, string[] args)
		{
			if (!new string[] { "ARG", "FROM_ARG", "TO_ARG" }.Contains(type))
				throw new ArgumentException("typeはARG, FROM_ARG, TO_ARGのいずれかでなければなりません。");

			string logstr = log;
			foreach (Match m in Regex.Matches(logstr, @"\${" + type + @"(?<id>\d+)}"))
			{
				int id = int.Parse(m.Groups["id"].Value);
				string arg = m.Value;
				string value = string.Empty;

				if (args != null && args.Length > id)
					value = args[id];

				logstr = Regex.Replace(logstr, @"\${" + type + id.ToString() + @"}", value);
			}
			return logstr;
		}

		protected TraceLog applyARG(string type, TraceLog log, string[] args)
		{
			return new TraceLog(applyARG(type, log.ToString(), args));
		}

		protected bool checkCondition(string condition)
		{
			if (condition == null)
				return true;

			return ConditionExpression.Result(condition);
		}

		public IEnumerable<EventShape> GetEventShapes(Time from, Time to)
		{
			if (_drawShapes != null)
			{
				foreach (EventShape ds in _drawShapes.GetShapes(from, to))
				{
					yield return ds;
				}
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Event != null)
			{
				if (Event.GetVisualizeRuleName() != null)
				{
					sb.Append(Event.GetVisualizeRuleName());
					sb.Append(":");
				}
				sb.Append(Event.Name);
				sb.Append(":");
			}
			if (Target != null)
			{
				sb.Append(Target.Name);
			}
			return sb.ToString();
		}
	}
}
