
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class ChangeSubWindowEnabledCommand : ICommand
    {
        private bool _canUndo = true;

        private SubWindow _subWindow;
        private bool _enabled;

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
            _subWindow.Enabled = _enabled;
        }

        public void Undo()
        {
            if (_canUndo)
            {
                _subWindow.Enabled = !_enabled;
            }
        }

        public ChangeSubWindowEnabledCommand(SubWindow subWindow, bool enabled)
        {
            _subWindow = subWindow;
            _enabled = enabled;
            string str = _enabled ? "有効" : "無効";
            Text = subWindow.Text + "を" + str + "にする";
        }
    }
}
