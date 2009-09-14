
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class LogData : IComparer<LogData>, IComparable<LogData>, IEqualityComparer<LogData>, IEquatable<LogData>
	{
		public virtual TraceLogType Type { get; private set; }
		public Time Time { get; private set; }
		public Resource Object { get; private set; }
		public long Id { get; set; }

		public LogData(Time time, Resource obj)
		{
			Time = time;
			Object = obj;
			Type = TraceLogType.None;
		}

		public int Compare(LogData x, LogData y)
		{
			return x.Id == y.Id ? 0 : x.Id < y.Id ? 1 : -1;
		}

		public int CompareTo(LogData other)
		{
			return Compare(this, other);
		}

		public bool Equals(LogData x, LogData y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(LogData obj)
		{
			return obj.Id.GetHashCode();
		}

		public bool Equals(LogData other)
		{
			return Equals(this, other);
		}

		public override string ToString()
		{
			return "[" + Time.ToString() + "]" + Object.Name;
		}

		public bool CheckAttributeOrBehavior(TraceLog log)
		{
			bool result = true;

			if (log.Attribute != null && log.Behavior == null && this is AttributeChangeLogData)
				result &= log.Attribute == ((AttributeChangeLogData)this).Attribute.Name && (log.Value != null ? log.Value == ((AttributeChangeLogData)this).Attribute.Value.ToString() : true);
			else if (log.Attribute == null && log.Behavior != null && this is BehaviorHappenLogData)
				result &= log.Behavior == ((BehaviorHappenLogData)this).Behavior.Name && (log.Arguments != null ? ((BehaviorHappenLogData)this).Behavior.Arguments.checkArgs(log.Arguments.Split(',')) : true);
			else
				result &= false;

			return result;
		}
	}
}
