using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ApplicationBlackBoard
	{
		public EventHandler<GeneralChangedEventArgs<Time>> CursorTimeChanged;
		public EventHandler<GeneralChangedEventArgs<Pair<Time,Time>>> SelectedTimeRangeChanged;

		private Time _cursorTime;
		public Time CursorTime { get { return _cursorTime; } set { setValue<Time>(ref _cursorTime, value, CursorTimeChanged); } }
		private Pair<Time, Time> _selectedTimeRange;
		public Pair<Time, Time> SelectedTimeRange { get { return _selectedTimeRange; } set { setValue<Pair<Time, Time>>(ref _selectedTimeRange, value, SelectedTimeRangeChanged); } }

		private void setValue<T>(ref T nowValue, T newValue, EventHandler<GeneralChangedEventArgs<T>> changedEvent)
		{
			if (!nowValue.Equals(newValue))
			{
				T old = nowValue;
				nowValue = newValue;
				if (changedEvent != null)
					changedEvent(this, new GeneralChangedEventArgs<T>(old, nowValue));
			}
		}

		public ApplicationBlackBoard()
		{
			_cursorTime = Time.Empty;
			_selectedTimeRange = null;
		}
	}
}
