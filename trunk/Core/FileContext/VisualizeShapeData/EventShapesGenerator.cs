using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    class EventShapesGenerator
    {
        private Dictionary<string, string> _cache = new Dictionary<string, string>();
        private VisualizeRule _rule;
        private Event _evnt;
        private List<Event> _evnts = new List<Event>();
        private EventShapes _drawShapes = new EventShapes();
        private Resource _target;

        private LogDataEnumeable _logData;
        private bool _dataSet = false;

        private ResourceData _resData;
        private TraceLogData _tracelogData;
        private VisualizeData _vizData;


        public string Name { get { return ToString(); } }
        public VisualizeRule Rule { get { return _rule; } }
        public Event Event { get { return _evnt; } }
        public Resource Target { get { return _target; } }
        public List<Event> Events { get { return _evnts; } }
        public bool IsDataSet { get { return _dataSet; } }

        public EventShapesGenerator(VisualizeRule rule)
            : this(rule, null, null) { }
        public EventShapesGenerator(VisualizeRule rule, Event evnt)
            : this(null, evnt, null) { }

        public EventShapesGenerator(Resource target)
            : this(null, null, target) { }
        public EventShapesGenerator(VisualizeRule rule, Resource target)
            : this(rule, null, target) { }
        public EventShapesGenerator(Event evnt, Resource target)
            : this(null, evnt, target) { }

        private EventShapesGenerator(VisualizeRule rule, Event evnt, Resource target)
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
        }

        public void SetData(TraceLogData tracelogData, VisualizeData vizData, ResourceData resData)
        {
            ClearData();

            _tracelogData = tracelogData;
            _vizData = vizData;
            _resData = resData; 

            _dataSet = false;
            _logData = new LogDataEnumeable(tracelogData.LogDataBase);
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
                foreach (VisualizeRule r in vizData.VisualizeRules.Where<VisualizeRule>(r => r.Target == _target.Type))
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
            if (evnt.When != null && evnt.From == null && evnt.To == null)
                addDrawShapeFromWhenEvent(evnt);
            else if (evnt.When == null && evnt.From != null && evnt.To != null)
                addDrawShapeFromBetweenEvent(evnt);
        }

        private void addDrawShapeFromBetweenEvent(Event evnt)
        {
            Dictionary<TraceLog, LogData> fromLogDic = new Dictionary<TraceLog, LogData>();
            Dictionary<TraceLog, List<Resource>> toLogDic = new Dictionary<TraceLog, List<Resource>>();

            LogDataEnumeable fromLogData;
            LogDataEnumeable toLogData;

            TraceLog fromLog = applyTARGET("TARGET", evnt.From, _target);
            fromLog = TLVFunction.Apply(fromLog, _resData, _tracelogData);
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
                foreach (TraceLog tl in delKeys)
                {
                    toLogDic.Remove(tl);
                }

                IEnumerable<Resource> fromRes = _tracelogData.GetObject("[" + (log.Time - 0.0000000001m).ToString() + "]" + (fromLog.ObjectName != null ? fromLog.ObjectName : fromLog.Object));

                if (log.CheckAttributeOrBehavior(fromLog) && fromRes.Contains(log.Object))
                {
                    TraceLog tmpToLog = null;
                    List<Resource> tmpToRes = null;

                    tmpToLog = applyTARGET("FROM_TARGET", toLog, log.Object);
                    tmpToLog = applyTIME("FROM_TIME", tmpToLog, log.Time);
                    if (log is AttributeChangeLogData)
                        tmpToLog = applyVAL("FROM_VAL", tmpToLog, ((AttributeChangeLogData)log).Attribute.Value.ToString());
                    if (log is BehaviorHappenLogData)
                        tmpToLog = applyARG("FROM_ARG", tmpToLog, ((BehaviorHappenLogData)log).Behavior.Arguments.ToString().Split(','));

                    tmpToLog = TLVFunction.Apply(tmpToLog, _resData, _tracelogData);
                    tmpToRes = _tracelogData.GetObject(tmpToLog.ObjectName != null ? tmpToLog.ObjectName : tmpToLog.Object).ToList();

                    toLogDic.Add(tmpToLog, tmpToRes.ToList());

                    fromLogDic.Add(tmpToLog, log);
                }
            }

        }

        private void addDrawShapeFromWhenEvent(Event evnt)
        {
            LogDataEnumeable logData;
            TraceLog log = applyTARGET("TARGET", evnt.When, _target);
            log = TLVFunction.Apply(log, _resData, _tracelogData);

            if (log.Attribute != null && log.Behavior == null)
                logData = new LogDataEnumeable(_logData.GetEnumerator<AttributeChangeLogData>(log.Attribute, log.Value));
            else if (log.Attribute == null && log.Behavior != null)
                logData = new LogDataEnumeable(_logData.GetEnumerator<BehaviorHappenLogData>(log.Behavior, log.Arguments != null ? log.Arguments.Split(',') : null));
            else
                logData = new LogDataEnumeable(_logData);

            IEnumerable<Resource> res = _tracelogData.GetObject(log.Object);

            if (log.Attribute != null && log.Behavior == null)
            {
                LogDataEnumeable firsts = new LogDataEnumeable(getFirstLogData(log).Where(l => ((AttributeChangeLogData)l).Attribute.Name == log.Attribute));
                logData = firsts + logData;
            }

            foreach (LogData[] logs in logData.GetPrevPostSetEnumerator())
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

            Time fromTime = from == null ? _tracelogData.MinTime : from.Time;
            Time toTime = to == null ? _tracelogData.MaxTime : to.Time;

            foreach (Figure fg in figures)
            {
                string condition = fg.Condition;

                condition = applyTemplate(from, to, condition);
                if (condition != null)
                {
                    if (!_cache.ContainsKey(condition))
                        _cache[condition] = TLVFunction.Apply(condition, _resData, _tracelogData);

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
                                _cache[spArgs[i]] = TLVFunction.Apply(spArgs[i], _resData, _tracelogData);

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
                        foreach (Shape sp in _vizData.Shapes[fg.Shape])
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

        public EventShapes GetEventShapes()
        {
            return _drawShapes; 
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