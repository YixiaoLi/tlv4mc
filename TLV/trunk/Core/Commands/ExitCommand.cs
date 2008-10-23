using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class ExitCommand : AbstractFileChangeCommand
    {
        private Form _form;

        public ExitCommand(Form form)
        {
            _form = form;
        }

        protected override void action()
        {
            ApplicationDatas.ActiveFileContext.Close();
            _form.Close();
        }
    }
}
