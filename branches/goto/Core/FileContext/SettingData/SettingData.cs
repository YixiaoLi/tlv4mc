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
		public ResourceExplorerSetting ResourceExplorerSetting { get; set; }
		public VisualizeRuleExplorerSetting VisualizeRuleExplorerSetting { get; set; }
		public TraceLogDisplayPanelSetting TraceLogDisplayPanelSetting { get; set; }

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
			ResourceExplorerSetting = new ResourceExplorerSetting();
			VisualizeRuleExplorerSetting = new VisualizeRuleExplorerSetting();
			TraceLogDisplayPanelSetting = new TraceLogDisplayPanelSetting();

			ResourceExplorerSetting.BecameDirty += BecameDirtyFactory("ResourceExplorerSetting");
			VisualizeRuleExplorerSetting.BecameDirty += BecameDirtyFactory("VisualizeRuleExplorerSetting");
			TraceLogDisplayPanelSetting.BecameDirty += BecameDirtyFactory("TraceLogDisplayPanelSetting");
		}

		SettingChangeEventHandler BecameDirtyFactory(string propertyName)
		{
			return (o, p) =>
				{
					ApplicationData.FileContext.Data.IsDirty = true;
				};
		}
	}
}
