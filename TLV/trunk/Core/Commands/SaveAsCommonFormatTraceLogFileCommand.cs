using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class SaveAsCommonFormatTraceLogFileCommand : ICommand
    {
        public string Text
        {
            get;
            set;
        }

        public bool CanUndo { get { return false; } }

        public void Do()
        {
            if (ApplicationDatas.ActiveFileContext.IsOpened)
            {
                var sfd = new SaveFileDialog();
                sfd.DefaultExt = Properties.Resources.CommonFormatTraceLogFileExtension;
                sfd.Filter = "Common Format TraceLog File (*." + sfd.DefaultExt + ")|*." + sfd.DefaultExt;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ApplicationDatas.ActiveFileContext.SaveAs(sfd.FileName);
                }
            }
        }

        public void Undo()
        {

        }

        public SaveAsCommonFormatTraceLogFileCommand()
        {
            Text = "名前を付けて保存する";
        }
    }
}
