using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class TimeLineVisualizer : TimeLine
	{
		private	VisualizeRule _rule;
		private Event _evnt;
		private Resource _target;
		private LogDataEnumeable _logData;

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

			SetData(ApplicationData.FileContext.Data);

			InitializeComponent();
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);

			_logData = new LogDataEnumeable(_data.TraceLogData.LogDataBase);
			_logData.Filter(_target);
		}

		public override void Draw(PaintEventArgs e)
		{
			base.Draw(e);

			if (_logData == null)
				return;

			if(_rule != null && _evnt == null && _target == null)
			{
				drawRule(e, _rule, null);
			}
			else if(_rule == null && _evnt != null && _target == null)
			{
				drawEvent(e, _evnt, null);
			}
			else if(_rule == null && _evnt == null && _target != null)
			{
				foreach (VisualizeRule rule in _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(r => r.Target == _target.Type))
				{
					drawRule(e, rule, _target);
				}
			}
			else if(_rule != null && _evnt == null && _target != null)
			{
				drawRule(e, _rule, _target);
			}
			else if (_rule == null && _evnt != null && _target != null)
			{
				drawEvent(e, _evnt, _target);
			}

		}

		protected void drawRule(PaintEventArgs e, VisualizeRule rule, Resource target)
		{
			foreach (Event evnt in rule.Events.Where(_e => _data.SettingData.VisualizeRuleExplorerSetting.Check(rule, _e, target)))
			{
				drawEvent(e, evnt, target);
			}
		}

		protected void drawEvent(PaintEventArgs e, Event evnt, Resource target)
		{
			if (_from.Value == _to.Value)
				return;

			if ((evnt.Type & EventTypes.Between) == EventTypes.Between)
			{
				if ((evnt.Type & EventTypes.FromAttributeChange) == EventTypes.FromAttributeChange)
				{

				}
				else if ((evnt.Type & EventTypes.FromBehaviorHappen) == EventTypes.FromBehaviorHappen)
				{

				}

				if ((evnt.Type & EventTypes.ToAttributeChange) == EventTypes.ToAttributeChange)
				{

				}
				else if ((evnt.Type & EventTypes.ToBehaviorHappen) == EventTypes.ToBehaviorHappen)
				{

				}
			}
			else if ((evnt.Type & EventTypes.When) == EventTypes.When)
			{
				if ((evnt.Type & EventTypes.WhenAttributeChange) == EventTypes.WhenAttributeChange)
				{
					drawWhenEvent(e, evnt, target, _logData.GetEnumerator<AttributeChangeLogData>(evnt.When.Attribute));
				}
				else if ((evnt.Type & EventTypes.WhenBehaviorHappen) == EventTypes.WhenBehaviorHappen)
				{
					drawWhenEvent(e, evnt, target, _logData.GetEnumerator<BehaviorHappenLogData>(evnt.When.Behavior));
				}
			}
		}

		protected void drawWhenEvent(PaintEventArgs e, Event evnt, Resource target, IEnumerable<LogData> list)
		{
			LogDataEnumeable data = new LogDataEnumeable(list);
			data.Filter(_from, _to, true, true);

			foreach (LogData[] l in data.GetPrevPostSetEnumerator())
			{
				float sx = l[1].Time.ToX(_from, _to, e.ClipRectangle.Width);

				float w = ((l[2] == null) ? _data.TraceLogData.MaxTime.ToX(_from, _to, e.ClipRectangle.Width) : l[2].Time.ToX(_from, _to, e.ClipRectangle.Width)) - l[1].Time.ToX(_from, _to, e.ClipRectangle.Width);

				if (w == 0f)
					continue;

				foreach (KeyValuePair<string, GeneralNamedCollection<ShapeArgPair>> kvp in evnt.Shapes)
				{
					drawShape(kvp.Key, l[1], evnt, target, kvp.Value, e.Graphics, new RectangleF(e.ClipRectangle.X + sx, e.ClipRectangle.Y, w, e.ClipRectangle.Height));
				}
			}
		}

		protected void drawShape(string condition, LogData log, Event evnt, Resource target, GeneralNamedCollection<ShapeArgPair> shapes, Graphics g, RectangleF rect)
		{
			if (ConditionExpression.Result(applyValue(condition, log, evnt, target)))
			{
				foreach (ShapeArgPair sap in shapes)
				{
					foreach (Shape s in _data.VisualizeData.Shapes[sap.Name])
					{
						g.DrawShape(s, sap.Args, rect);
					}
				}
			}
		}

		protected string applyValue(string condition, LogData log, Event evnt, Resource target)
		{
			if (log is AttributeChangeLogData && evnt.When.Value != null)
			{
				return Regex.Replace(((AttributeChangeLogData)log).Value.ToString(), evnt.When.Value, condition);
			}
			else if (log is BehaviorHappenLogData && evnt.When.Arguments != null)
			{
				return Regex.Replace(((BehaviorHappenLogData)log).Arguments.ToString(), evnt.When.Arguments, condition);
			}
			return condition;
		}
	}
}
