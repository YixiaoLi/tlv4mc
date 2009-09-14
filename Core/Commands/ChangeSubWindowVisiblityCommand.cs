
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class ChangeSubWindowVisiblityCommand : ICommand
    {
        private bool _canUndo = true;

        private SubWindow _subWindow;
        private bool _visiblity;

        public string Text
        {
            get;
            set;
        }

        public bool CanUndo
        {
            get { return _canUndo; }
            set { _canUndo = value; }
        }

        public void Do()
        {
            _subWindow.Visible = _visiblity;
        }

        public void Undo()
        {
            if (_canUndo)
            {
                _subWindow.Visible = !_visiblity;
            }
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
