using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ApplicationSetting : ApplicationSettingsBase
	{
		[UserScopedSetting]
		public string ResourceHeadersDirectoryPath { get { return (string)this["ResourceHeadersDirectoryPath"]; } set { this["ResourceHeadersDirectoryPath"] = value; } }

		[UserScopedSetting]
		public string ConvertRulesDirectoryPath { get { return (string)this["ConvertRulesDirectoryPath"]; } set { this["ConvertRulesDirectoryPath"] = value; } }

		[UserScopedSetting]
		public string VisualizeRulesDirectoryPath { get { return (string)this["VisualizeRulesDirectoryPath"]; } set { this["VisualizeRulesDirectoryPath"] = value; } }

		[UserScopedSetting]
		public string TemporaryDirectoryPath { get { return (string)this["TemporaryDirectoryPath"]; } set { this["TemporaryDirectoryPath"] = value; } }

		public ApplicationSetting()
		{
			if (ResourceHeadersDirectoryPath == null)
				ResourceHeadersDirectoryPath = Path.Combine(ApplicationData.ApplicationDirectory, NU.OJL.MPRTOS.TLV.Core.Properties.Resources.DefaultResourceHeadersDirectoryName);

			if (ConvertRulesDirectoryPath == null)
				ConvertRulesDirectoryPath = Path.Combine(ApplicationData.ApplicationDirectory, NU.OJL.MPRTOS.TLV.Core.Properties.Resources.DefaultConvertRulesDirectoryName);

			if (VisualizeRulesDirectoryPath == null)
				VisualizeRulesDirectoryPath = Path.Combine(ApplicationData.ApplicationDirectory,  NU.OJL.MPRTOS.TLV.Core.Properties.Resources.DefaultVisualizeRulesDirectoryName);

			if (TemporaryDirectoryPath == null)
				TemporaryDirectoryPath = Path.Combine(Path.GetTempPath(), NU.OJL.MPRTOS.TLV.Core.Properties.Resources.DefaultTemporaryDirectoryName);

		}
	}
}
