using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLineMarkerManager
	{
		public event EventHandler SelectedMarkerChanged = null;

		public ObservableNamedCollection<TimeLineMarker> Markers { get; private set; }

		public IEnumerable<TimeLineMarker> GetSelectedMarker()
		{
			return Markers.Values.Where<TimeLineMarker>(m => m.Selected);
		}

		public TimeLineMarkerManager()
		{
			Markers = new ObservableNamedCollection<TimeLineMarker>();
			Markers.CollectionChanged += (o, e) =>
			{
				if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				{
					foreach (KeyValuePair<string, TimeLineMarker> tlm in e.NewItems)
					{
						tlm.Value.SelectedChanged += (_o, _e) =>
						{
							if (SelectedMarkerChanged != null)
								SelectedMarkerChanged(this, EventArgs.Empty);
						};
					}
				}
			};
		}

		public void AddMarker(Time time)
		{
			string jam = new Random().Next().ToString();
			string key = time.GetHashCode().ToString() + jam;
			while (Markers.ContainsKey(key))
			{
				jam = new Random().Next().ToString();
				key = time.GetHashCode().ToString() + jam;
			}

			Markers.Add(key, new TimeLineMarker(key, time));
		}

		public void DeleteMarker(string key)
		{
			Markers.Remove(key);
		}

		public void DeleteSelectedMarker()
		{
			List<String> delList = new List<string>();
			foreach (TimeLineMarker tlm in Markers.Values.Where<TimeLineMarker>(m => m.Selected))
			{
				delList.Add(tlm.Name);
			}
			foreach (string key in delList)
			{
				DeleteMarker(key);
			}
		}

		public void ResetSelect()
		{
			foreach (TimeLineMarker tlm in Markers.Values)
			{
				tlm.Unselect();
			}
		}

		public void AllSelect()
		{
			foreach (TimeLineMarker tlm in Markers.Values)
			{
				tlm.Select();
			}
		}
	}
}
