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
		public Time CursorTime { get { return _cursorTime; } set { ApplicationMethod.SetValue<Time>(ref _cursorTime, value, CursorTimeChanged, this); } }
		private Pair<Time, Time> _selectedTimeRange;
		public Pair<Time, Time> SelectedTimeRange { get { return _selectedTimeRange; } set { ApplicationMethod.SetValue<Pair<Time, Time>>(ref _selectedTimeRange, value, SelectedTimeRangeChanged, this); } }

		public ApplicationBlackBoard()
		{
			_cursorTime = Time.Empty;
			_selectedTimeRange = null;
		}
	}
}
