using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class OpenResourceFileAndTraceLogFileOpenWindowCommand : ICommand
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
            var form = new OpenResourceFileAndTraceLogFileOpenForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(form.ResourceFilePath + ", " + form.TraceLogFilePath);
            }
        }

        public void Undo()
        {
            
        }

    }
}
