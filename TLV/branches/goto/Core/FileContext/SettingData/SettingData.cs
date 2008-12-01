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
		public ColorSetting ColorSetting { get; set; }
		public ResourceExplorerSetting ResourceExplorerSetting { get; set; }
		public VisualizeRuleExplorerSetting VisualizeRuleExplorerSetting { get; set; }

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
			ColorSetting = new ColorSetting();
			ResourceExplorerSetting = new ResourceExplorerSetting();
			VisualizeRuleExplorerSetting = new VisualizeRuleExplorerSetting();

			ColorSetting.BecameDirty += BecameDirty;
			ResourceExplorerSetting.BecameDirty += BecameDirty;
			VisualizeRuleExplorerSetting.BecameDirty += BecameDirty;
		}

		void BecameDirty(object sender, EventArgs e)
		{
			ApplicationData.FileContext.Data.IsDirty = true;
		}
	}
}
