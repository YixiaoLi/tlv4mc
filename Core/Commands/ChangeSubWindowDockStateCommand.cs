
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class ChangeSubWindowDockStateCommand : ICommand
    {
        private bool _canUndo = true;

        private SubWindow _subWindow;
        public DockState _newDockState;
        public DockState _oldDockState;

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
            _subWindow.DockState = _newDockState;
        }

        public void Undo()
        {
            if (_canUndo)
            {
                _subWindow.DockState = _oldDockState;
            }
        }

        public ChangeSubWindowDockStateCommand(SubWindow subWindow, DockState oldDockState, DockState newDockState)
        {
            _subWindow = subWindow;
            _newDockState = newDockState;
            _oldDockState = oldDockState;

            Text = _subWindow.Text + "のドッキング箇所を" + _newDockState.ToText() + " にする";
        }
    }
}
