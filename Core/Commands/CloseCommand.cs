
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class CloseCommand : AbstractFileChangeCommand
    {
        protected override void action()
        {
			ApplicationData.FileContext.Close();
			ApplicationData.Setting.Save();
        }
    }
}
