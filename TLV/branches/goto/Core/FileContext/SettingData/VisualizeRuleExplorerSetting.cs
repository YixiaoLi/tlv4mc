using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class VisualizeRuleExplorerSetting : ISetting
	{
		public event EventHandler BecameDirty = null;
		public ObservableDictionary<string, bool> VisualizeRuleVisibility { get; set; }
		public void SetVisualizeRuleBelongedResourceVisibility(bool value, params string[] name)
		{
			if (!ContainsVisualizeRuleBelongedResourceVisibility(name))
				VisualizeRuleVisibility.Add(arrayToString(name), value);
			else
				VisualizeRuleVisibility[arrayToString(name)] = value;
		}
		public bool GetVisualizeRuleBelongedResourceVisibility(params string[] name)
		{
			return VisualizeRuleVisibility[arrayToString(name)];
		}
		public bool ContainsVisualizeRuleBelongedResourceVisibility(params string[] name)
		{
			return VisualizeRuleVisibility.ContainsKey(arrayToString(name));
		}
		public IEnumerable<KeyValuePair<string[], bool>> GetResourceEnumeralor()
		{
			return VisualizeRuleVisibility.Select(kvp =>
				{
					if(kvp.Key.Contains(':'))
					{
						string[] data = kvp.Key.Split(':');
						return new KeyValuePair<string[], bool>(data, kvp.Value);
					}
					else
					{
						return new KeyValuePair<string[], bool>(new string[]{kvp.Key}, kvp.Value);
					}
				});
		}

		public VisualizeRuleExplorerSetting()
		{
			VisualizeRuleVisibility = new ObservableDictionary<string, bool>();
			VisualizeRuleVisibility.CollectionChanged += CollectionChanged;
		}

		void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (BecameDirty != null)
				BecameDirty(this, EventArgs.Empty);
		}

		private string arrayToString(params string[] name)
		{
			StringBuilder result = new StringBuilder();
			foreach(string str in name)
			{
				result.Append(str);
				result.Append(":");
			}
			result.Remove(result.Length - 1, 1);
			return result.ToString();
		}
	}
}
