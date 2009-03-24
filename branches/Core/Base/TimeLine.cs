using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLine : ICloneable
	{
		protected Time _from = Time.Empty;
		protected Time _to = Time.Empty;
		protected Time _min = Time.Empty;
		protected Time _max = Time.Empty;

		public event EventHandler<GeneralChangedEventArgs<TimeLine>> ViewingAreaChanged;
		
		public virtual Time FromTime { get { return _from; } private set { if (_from != value)_from = value; } }
		public virtual Time ToTime { get { return _to; } private set { if (_to != value)_to = value; } }
		public virtual Time MinTime { get { return _min; } set { if (_min != value)_min = value; } }
		public virtual Time MaxTime { get { return _max; } set { if (_max != value)_max = value; } }
		public virtual Time ViewableSpan { get { return _max - _min; } }
		public virtual Time ViewingSpan { get { return _to - _from; } }

		public TimeLine(Time from, Time to)
			: this(from, to, from, to) { }

		public TimeLine(Time from, Time to, Time min, Time max)
		{
			FromTime = from;
			ToTime = to;
			MinTime = min;
			MaxTime = max;
		}

		public virtual void SetTime(Time from, Time to)
		{
			TimeLine old = (TimeLine)this.Clone();

			Time span = ViewingSpan;

			if (!from.IsEmpty && from < MinTime)
				from = MinTime;
			if (!to.IsEmpty && to < MinTime)
				to = MinTime;

			if (!to.IsEmpty && to > MaxTime)
				to = MaxTime;
			if (!from.IsEmpty && from > MaxTime)
				from = MaxTime;

			if (!from.IsEmpty && !to.IsEmpty && from > to)
			{
				Time s = to;
				to = from;
				from = to;
			}
			if (!from.IsEmpty && !to.IsEmpty && from == to)
			{
				return;
			}

			if (from.IsEmpty && !to.IsEmpty && to <= FromTime)
				throw new ArgumentException("toはFromTimeより大きくなければなりません。");

			if (to.IsEmpty && !from.IsEmpty && from >= ToTime)
				throw new ArgumentException("fromはToTimeより小さくなければなりません。");

			if (from > ToTime)
			{
				if (!to.IsEmpty)
					ToTime = to;
				if (!from.IsEmpty)
					FromTime = from;
			}
			else
			{
				if (!from.IsEmpty)
					FromTime = from;
				if (!to.IsEmpty)
					ToTime = to;
			}

			ViewingAreaChanged(this, new GeneralChangedEventArgs<TimeLine>(old, this));
		}

		public virtual void MoveBySettingToTime(Time time)
		{
			if (time.IsEmpty)
				return;

			if (time == _to)
				return;

			if (time > _max)
			{
				time = _max;
			}
			if (time - ViewingSpan < _min)
			{
				time = _min + ViewingSpan;
			}

			SetTime(time - ViewingSpan, time);
		}

		public virtual void MoveBySettingFromTime(Time time)
		{
			if (time.IsEmpty)
				return;

			if (time == _from)
				return;

			if (time < _min)
			{
				time = _min;
			}
			if (time + ViewingSpan > _max)
			{
				time = _max - ViewingSpan;
			}

			SetTime(time, time + ViewingSpan);

		}

		public override string ToString()
		{
			return "[" + _from.ToString() + "～" + _to.ToString() + "] in (" + _min.ToString() + "～" + _max.ToString() + ")";
		}

		public object Clone()
		{
			return new TimeLine(FromTime, ToTime, MinTime, MaxTime);
		}

	}
}
