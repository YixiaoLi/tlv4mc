using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class ChangeSubWindowVisiblityCommand : ICommand
    {
        private SubWindow _subWindow;
        public bool _visiblity;

        public string Text
        {
            get;
            set;
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public void Do()
        {
            _subWindow.Visible = _visiblity;
        }

        public void Undo()
        {
            _subWindow.Visible = !_visiblity;
        }

        public ChangeSubWindowVisiblityCommand(SubWindow subWindow, bool visiblity)
        {
            _subWindow = subWindow;
            _visiblity = visiblity;
            string str = _visiblity ? "表示" : "非表示に";
            Text = subWindow.Text + "を" + str + "する";
        }
    }
}
