using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public static class ApplicationDatas
    {
        public static readonly string Name = "TraceLogVisualizer";
        public static readonly string Version = "1.0b";
        public static readonly string CommonFormatTraceLogRegex = string.Empty;
        public static readonly string RulesDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Properties.Resources.RulesDirectoryPath;
        public static readonly string ConvertRulesDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Properties.Resources.ConvertRulesDirectoryPath;
        public static readonly string VisualizeRulesDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Properties.Resources.VisualizeRulesDirectoryPath;
        public static readonly string ResourceSchemaFilePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Properties.Resources.ResourceSchemaFilePath;
        public static readonly FileContext<CommonFormatTraceLog> ActiveFileContext = new FileContext<CommonFormatTraceLog>();

        static ApplicationDatas()
        {
            CommonFormatTraceLogRegex = @"(_\[T\]_(_S_:_)?_O_\._(B)_END)+"
                .Replace("_", @"\s*")
                .Replace("T", @"(?<T>\d+)")
                .Replace("S", @"(?<S>((\w+)|(\(\s*(\s*\w+\s*=\s*\w+\s*,?\s*)+\s*\))))")
                .Replace("O", @"(?<O>((\w+)|(\(\s*(\s*\w+\s*=\s*\w+\s*,?\s*)+\s*\))))")
                .Replace("B", @"(?<B>\w+)")
                .Replace("END", @"[^\[]*");
        }
    }
}
