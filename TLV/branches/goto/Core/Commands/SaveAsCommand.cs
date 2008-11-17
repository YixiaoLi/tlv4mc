using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class SaveAsCommand : ICommand
    {
        public string Text
        {
            get;
            set;
        }

        public bool CanUndo { get { return false; } set { } }

        public void Do()
        {
            if (ApplicationData.FileContext.IsOpened)
            {
                var sfd = new SaveFileDialog();
                sfd.DefaultExt = Properties.Resources.CommonFormatTraceLogFileExtension;
                sfd.Filter = "Common Format TraceLog File (*." + sfd.DefaultExt + ")|*." + sfd.DefaultExt;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ApplicationData.FileContext.SaveAs(sfd.FileName);
                }
            }
        }

        public void Undo()
        {

        }

        public SaveAsCommand()
        {
            Text = "名前を付けて保存する";
        }
    }
}
