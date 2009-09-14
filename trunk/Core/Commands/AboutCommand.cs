
using System;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class AboutCommand : ICommand
	{
        private AboutForm _aboutDialog = new AboutForm();

		public AboutCommand()
		{
		}

        public string Text { get; set; }

        public bool CanUndo
        {
            get { return false; }
            set { }
        }

        public void Do()
        {
            _aboutDialog.ShowDialog();
        }

        public void Undo()
        {
        }

    }
}
