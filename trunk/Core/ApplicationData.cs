
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public static class ApplicationData
    {
        public static readonly string Name = "TraceLogVisualizer";
        public static readonly string Version = "1.1alpha3";
		public static readonly string ApplicationPath = Application.ExecutablePath;
		public static readonly string ApplicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\";
		public static readonly ApplicationSetting Setting = new ApplicationSetting();
		public static readonly IFileContext<TraceLogVisualizerData> FileContext = new FileContext<TraceLogVisualizerData>();
    }
}
