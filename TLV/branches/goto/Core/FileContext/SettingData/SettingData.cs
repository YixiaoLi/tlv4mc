using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class SettingData : IJsonable<SettingData>
	{
		public TraceLogViewerSetting TraceLogViewerSetting { get; set; }
		public ResourceExplorerSetting ResourceExplorerSetting { get; set; }
		public ResourcePropertyExplorerSetting ResourcePropertyExplorerSetting { get; set; }
		public ResourceTypeExplorerSetting ResourceTypeExplorerSetting { get; set; }

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize(this);
		}

		public SettingData Parse(string settingData)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<SettingData>(settingData);
		}

		public SettingData()
		{
			TraceLogViewerSetting = new TraceLogViewerSetting();
			ResourceExplorerSetting = new ResourceExplorerSetting();
			ResourceTypeExplorerSetting = new ResourceTypeExplorerSetting();
			ResourcePropertyExplorerSetting = new ResourcePropertyExplorerSetting();

			TraceLogViewerSetting.BecameDirty += BecameDirty;
			ResourceExplorerSetting.BecameDirty += BecameDirty;
			ResourceTypeExplorerSetting.BecameDirty += BecameDirty;
			ResourcePropertyExplorerSetting.BecameDirty += BecameDirty;
		}

		void BecameDirty(object sender, EventArgs e)
		{
			ApplicationData.FileContext.Data.IsDirty = true;
		}
	}
}
