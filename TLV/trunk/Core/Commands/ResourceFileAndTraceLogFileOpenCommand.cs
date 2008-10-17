using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class ResourceFileAndTraceLogFileOpenCommand : ICommand
    {
        public string Text
        {
            get;
            set;
        }

        public bool CanUndo
        {
            get { return false; }
        }

        public void Do()
        {

        }

        public void Undo()
        {
            
        }

        public ResourceFileAndTraceLogFileOpenCommand(string resourceFilePath, string traceLogFilePath, string convertRuleFilePath)
        {
            Text = "リソースファイルとトレースログファイルを開く";
            ConvertRule cr = ConvertRule.GetInstance(convertRuleFilePath);
        }

    }
}
