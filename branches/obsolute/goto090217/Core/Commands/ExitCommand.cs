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
			ApplicationData.FileContext.Close();
			_form.Invoke((MethodInvoker)(() =>
				{
					_form.Close();
				}));
        }
    }
}
