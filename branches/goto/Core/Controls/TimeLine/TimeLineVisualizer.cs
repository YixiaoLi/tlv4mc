using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;
using System.Threading;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class TimeLineVisualizer : TimeLineControl
	{

		private Dictionary<string, string> _cache = new Dictionary<string, string>();
		private VisualizeRule _rule;
		private Event _evnt;
		private List<Event> _evnts = new List<Event>();
		private List<DrawShape> _drawShapes = new List<DrawShape>();
		private Resource _target;
		private LogDataEnumeable _logData;
		private bool _dataSet = false;

		public Thread SetDataThread { get; private set; }
		public VisualizeRule Rule { get { return _rule; } }
		public Event Event { get { return _evnt; } }
		public Resource Target { get { return _target; } }

		public TimeLineVisualizer(VisualizeRule rule)
			: this(rule, null, null) { }
		public TimeLineVisualizer(VisualizeRule rule, Event evnt)
			: this(null, evnt, null) { }
		public TimeLineVisualizer(Resource target)
			: this(null, null, target) { }
		public TimeLineVisualizer(VisualizeRule rule, Resource target)
			: this(rule, null, target) { }
		public TimeLineVisualizer(Event evnt, Resource target)
			: this(null, evnt, target) { }
		private TimeLineVisualizer(VisualizeRule rule, Event evnt, Resource target)
			:base()
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

			SetDataThread = new Thread(new ThreadStart(() =>
			{
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
				_dataSet = true;
			}));

			InitializeComponent();
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);
			SetDataThread.Start();
		}

		public override void ClearData()
		{
			base.ClearData();
			_dataSet = false;
		}

		private void addEvents(VisualizeRule rule)
		{
			foreach (Event evnt in rule.Events)
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
			Stack<LogData> fromLogStack = new Stack<LogData>();
			Stack<TraceLog> toLogStack = new Stack<TraceLog>();
			Stack<IEnumerable<Resource>> toResStack = new Stack<IEnumerable<Resource>>();

			LogDataEnumeable fromLogData;
			LogDataEnumeable toLogData;

			TraceLog fromLog = applyTARGET("TARGET", evnt.From, _target);
			TraceLog toLog = applyTARGET("TARGET", evnt.To, _target);

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

			LogDataEnumeable logData = fromLogData + toLogData;

			IEnumerable<Resource> fromRes = _data.TraceLogData.GetObject(fromLog.ObjectName != null ? fromLog.ObjectName : fromLog.ObjectType);

			TraceLog tmpToLog = null;
			IEnumerable<Resource> toRes = null;

			foreach (LogData log in logData)
			{
				if (tmpToLog != null && toRes != null && log.CheckAttributeOrBehavior(tmpToLog) && toRes.Contains(log.Object))
				{
					LogData fl = fromLogStack.Pop();
					if (toLogStack.Count != 0 && toResStack.Count != 0)
					{
						tmpToLog = toLogStack.Pop();
						toRes = toResStack.Pop();
					}
					else
					{
						tmpToLog = null;
						toRes = null;
					}

					addDrawShape(evnt.Figures, fl, log, evnt);
				}
				if (log.CheckAttributeOrBehavior(fromLog) && fromRes.Contains(log.Object))
				{
					fromLogStack.Push(log);

					if (tmpToLog != null)
						toLogStack.Push(tmpToLog);

					tmpToLog = applyTARGET("FROM_TARGET", toLog, log.Object);
					if(log is AttributeChangeLogData)
						tmpToLog = applyVAL("FROM_VAL", tmpToLog, ((AttributeChangeLogData)log).Attribute.Value.ToString());
					if (log is BehaviorHappenLogData)
						tmpToLog = applyARG("FROM_ARG", tmpToLog, ((BehaviorHappenLogData)log).Behavior.Arguments.ToString().Split(','));

					toRes = _data.TraceLogData.GetObject(tmpToLog.ObjectName != null ? tmpToLog.ObjectName : tmpToLog.ObjectType);
				}
			}

		}

		private void addDrawShapeFromWhenEvent(Event evnt)
		{
			LogDataEnumeable logData;
			TraceLog log = applyTARGET("TARGET", evnt.When, _target);

			if (log.Attribute != null && log.Behavior == null)
				logData = new LogDataEnumeable(_logData.GetEnumerator<AttributeChangeLogData>(log.Attribute, log.Value));
			else if (log.Attribute == null && log.Behavior != null)
				logData = new LogDataEnumeable(_logData.GetEnumerator<BehaviorHappenLogData>(log.Behavior, log.Arguments != null ? log.Arguments.Split(',') : null));
			else
				logData = new LogDataEnumeable(_logData);

			IEnumerable<Resource> res = _data.TraceLogData.GetObject(log.Object);

			foreach(LogData[] logs in logData.GetPrevPostSetEnumerator())
			{
				if (res.Contains(logs[1].Object))
				{
					addDrawShape(evnt.Figures, logs[1], logs[2], evnt);
				}
			}
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

							_drawShapes.Add(new DrawShape(fromTime, toTime, s, evnt));

						}
					}
				}
			}
		}

		private string applyTemplate(LogData from, LogData to, string condition)
		{
			if (condition != null)
			{
				if (condition.Contains("TARGET"))
				{
					condition = applyTARGET("TARGET", condition, _target);
					if (from != null && condition.Contains("FROM"))
					{
						condition = applyTARGET("FROM_TARGET", condition, from.Object);
					}
					if (to != null && condition.Contains("TO"))
					{
						condition = applyTARGET("TO_TARGET", condition, to.Object);
					}
				}

				if (from != null)
				{

					if (from is AttributeChangeLogData && condition.Contains("VAL"))
					{
						condition = applyVAL("FROM_VAL", condition, ((AttributeChangeLogData)from).Attribute.Value.ToString());
						condition = applyVAL("VAL", condition, ((AttributeChangeLogData)from).Attribute.Value.ToString());
					}

					if (from is BehaviorHappenLogData && condition.Contains("ARG"))
					{
						condition = applyARG("FROM_ARG", condition, ((BehaviorHappenLogData)from).Behavior.Arguments.ToString().Split(','));
						condition = applyARG("ARG", condition, ((BehaviorHappenLogData)from).Behavior.Arguments.ToString().Split(','));
					}
				}
				if (to != null)
				{

					if (to is AttributeChangeLogData && condition.Contains("VAL"))
					{
						condition = applyVAL("TO_VAL", condition, ((AttributeChangeLogData)to).Attribute.Value.ToString());
						condition = applyVAL("VAL", condition, ((AttributeChangeLogData)to).Attribute.Value.ToString());
					}

					if (to is BehaviorHappenLogData && condition.Contains("ARG"))
					{
						condition = applyARG("TO_ARG", condition, ((BehaviorHappenLogData)to).Behavior.Arguments.ToString().Split(','));
						condition = applyARG("ARG", condition, ((BehaviorHappenLogData)to).Behavior.Arguments.ToString().Split(','));
					}
				}
			}
			return condition;
		}

		public override void Draw(Graphics g, Rectangle rect)
		{
			base.Draw(g, rect);

			if (TimeLine == null)
				return;

			if (_drawShapes == null)
				return;

			if (rect.Width == 0)
				return;

			if (!_dataSet)
				return;



			foreach (DrawShape ds in _drawShapes.Where<DrawShape>(ds =>
				_data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(ds.Event.GetVisualizeRuleName(), ds.Event.Name)
				&& _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(ds.Event.GetVisualizeRuleName(), ds.Event.Name)
				&& ds.To > TimeLine.FromTime && ds.From < TimeLine.ToTime))
			{
				float x1 = ds.From.ToX(TimeLine.FromTime, TimeLine.ToTime, rect.Width);
				float w = ds.To.ToX(TimeLine.FromTime, TimeLine.ToTime, rect.Width) - x1;

				if (w <= 0)
					w = 1;

				if (rect.X + x1 + w < 0)
					return;

				ds.Shape.Draw(g, new RectangleF(rect.X + x1, rect.Y, w, rect.Height));
			}
		}

		protected string applyTARGET(string type, string log, Resource resource)
		{
			if (resource == null)
				return log;

			if (!new string[] { "TARGET", "FROM_TARGET", "TO_TARGET" }.Contains(type))
				throw new ArgumentException("targetはTARGET, FROM_TARGET, TO_TARGETのいずれかでなければなりません。");

			string logstr = log;
			logstr = Regex.Replace(logstr, @"\${" + type + "}", resource.Name);
			return logstr;
		}

		protected TraceLog applyTARGET(string type, TraceLog log, Resource resource)
		{
			return new TraceLog(applyTARGET(type, log.ToString(), resource));
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

		class DrawShape
		{
			public Time From { get; set; }
			public Time To { get; set; }
			public Shape Shape { get; set; }
			public Event Event { get; set; }

			public DrawShape(Time from, Time to, Shape shape, Event evnt)
			{
				From = from;
				To = to;
				Shape = shape;
				Event = evnt;
			}
		}
	}
}
