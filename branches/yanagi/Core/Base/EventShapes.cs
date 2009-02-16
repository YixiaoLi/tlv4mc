﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Parallel;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class EventShapes
	{
		private Dictionary<string, List<EventShape>> _list = new Dictionary<string, List<EventShape>>();

		public IEnumerable<EventShape> GetShapes(Time from, Time to)
		{
			if (ApplicationData.FileContext.Data != null)
			{
				foreach (KeyValuePair<string, List<EventShape>> kvp in _list)
				{
					string key1 = kvp.Key.Split(':')[0];
					string key2 = kvp.Key.Split(':')[1];

					if (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(key1, key2)
						&& ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(key1, key2)
					)
					{
						foreach (EventShape es in kvp.Value.AsParallel().Where(l => l.From < to))
						{
							if (es.To > from)
								yield return es;
						}
					}
				}
			}
		}

		public void Add(EventShape eventShape)
		{
			string p = eventShape.Event.GetVisualizeRuleName() + ":" + eventShape.Event.Name;
			if(!_list.ContainsKey(p))
				_list.Add(p, new List<EventShape>());
			_list[p].Add(eventShape);
		}

		public void Optimize()
		{
			Dictionary<string, List<EventShape>> list = new Dictionary<string, List<EventShape>>();
			foreach (KeyValuePair<string, List<EventShape>> kvp in _list)
			{
				list.Add(kvp.Key, kvp.Value.OrderBy(l => l.From).ToList());
			}
			_list = list;
		}

		public void Clear()
		{
			_list.Clear();
		}
	}
}
