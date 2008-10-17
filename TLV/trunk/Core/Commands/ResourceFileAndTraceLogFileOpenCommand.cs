using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class ResourceFileAndTraceLogFileOpenCommand : ICommand
    {
        private string _resourceFilePath = string.Empty;
        private string _traceLogFilePath = string.Empty;
        private string _convertRuleFilePath = string.Empty;
        private bool _canUndo = false;

        public string Text
        {
            get;
            set;
        }

        public bool CanUndo
        {
            get { return _canUndo;}
            private set
            {
                if(_canUndo != value)
                {
                    _canUndo = value;
                }
            }
        }

        public void Do()
        {
            StringBuilder sb = new StringBuilder();
            CommonFormatConverter cfc = CommonFormatConverter.GetInstance(_convertRuleFilePath);
            if (!cfc.ConvertResourceFile(_resourceFilePath, new StringWriter(sb)))
            {
                MessageBox.Show(_resourceFilePath + "はスキーマに定義されている制約に準拠していません。\n" + sb.ToString(), _resourceFilePath + "はスキーマに定義されている制約に準拠していません。", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(sb.ToString());
            }
        }

        public void Undo()
        {
            
        }

        public ResourceFileAndTraceLogFileOpenCommand(string resourceFilePath, string traceLogFilePath, string convertRuleFilePath)
        {
            _resourceFilePath = resourceFilePath;
            _traceLogFilePath = traceLogFilePath;
            _convertRuleFilePath = convertRuleFilePath;
            Text = "リソースファイルとトレースログファイルを開く";
        }

    }
}
