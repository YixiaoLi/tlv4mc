using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public static class ApplicationDatas
    {
        public static readonly string Name = "TraceLogVisualizer";
        public static readonly string Version = "1.0b";
        public static readonly string Path = Application.ExecutablePath;
		public static readonly Json Setting;
        public static readonly IFileContext<CommonFormatTraceLog> ActiveFileContext = new FileContext<CommonFormatTraceLog>();

		static ApplicationDatas()
		{
			string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\";
			if (!File.Exists(appPath + Properties.Resources.SettingFileName))
			{
				Json setting = new Json(new Dictionary<string, Json>());
				setting.Add("ResourceHeadersDirectoryPath", appPath + Properties.Resources.DefaultResourceHeadersDirectoryName);
				setting.Add("ConvertRulesDirectoryPath", appPath + Properties.Resources.DefaultConvertRulesDirectoryName);
				setting.Add("VisualizeRulesDirectoryPath", appPath + Properties.Resources.DefaultVisualizeRulesDirectoryName);
				Setting = setting;
				File.WriteAllText(appPath + Properties.Resources.SettingFileName, setting.ToJsonString());
			}
			else
			{
				Setting = new Json().Parse(File.ReadAllText(appPath + Properties.Resources.SettingFileName));
			}
		}
    }
}
