using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public static class ApplicationData
    {
        public static readonly string Name = "TraceLogVisualizer";
        public static readonly string Version = "1.0b";
        public static readonly string Path = Application.ExecutablePath;
		public static readonly IFileContext<Setting> Setting = new FileContext<Setting>();
        public static readonly IFileContext<TraceLogVisualizerData> ActiveFileContext = new FileContext<TraceLogVisualizerData>();

		public static void Setup()
		{
			string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\";
			if (!File.Exists(appPath + Properties.Resources.SettingFileName))
			{
				Setting.Data = new Setting();
				Setting.Data.Add("ResourceHeadersDirectoryPath", appPath + Properties.Resources.DefaultResourceHeadersDirectoryName);
				Setting.Data.Add("ConvertRulesDirectoryPath", appPath + Properties.Resources.DefaultConvertRulesDirectoryName);
				Setting.Data.Add("VisualizeRulesDirectoryPath", appPath + Properties.Resources.DefaultVisualizeRulesDirectoryName);
				Setting.Path = appPath + Properties.Resources.SettingFileName;
				Setting.Save();
			}
			else
			{
				Setting.Open(appPath + Properties.Resources.SettingFileName);
			}
		}
    }
}
