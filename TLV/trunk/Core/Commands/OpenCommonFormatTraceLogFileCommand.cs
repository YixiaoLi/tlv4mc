using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class OpenCommonFormatTraceLogFileCommand : ICommand
    {
        public string Text
        {
            get;
            set;
        }

        public bool CanUndo { get { return false; } }

        public void Do()
        {
            ApplicationFactory.CommandManager.Do(new FileChangeCommand(() =>
            { 
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = Properties.Resources.CommonFormatTraceLogFileExtension;
                ofd.Filter = "Common Format TraceLog File (*." + ofd.DefaultExt + ")|*." + ofd.DefaultExt;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ApplicationDatas.ActiveFileContext.Open(ofd.FileName);
                }
            }));
        }

        public void Undo()
        {
        }

        public OpenCommonFormatTraceLogFileCommand()
        {
            Text = "共通形式トレースログファイルを開く";
        }

    }
}
