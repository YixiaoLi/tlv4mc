using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class MacroCommand : ICommand
    {
        private bool _canUndo = true;
        private List<ICommand> _commandList;

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
            foreach(ICommand c in _commandList)
            {
                c.Do();
            }
        }

        public void Undo()
        {
            if(CanUndo)
            {
                foreach (ICommand c in _commandList.Reverse<ICommand>())
                {
                    c.Undo();
                }
            }
        }

        public MacroCommand(IEnumerable<ICommand> commands)
        {
            _commandList = new List<ICommand>(commands);

            if (_commandList.Count != 0)
            {
                Text = _commandList.First<ICommand>().Text + " から " + _commandList.Last<ICommand>().Text + " までの一連の動作";
            }
            else
            {
                _canUndo = false;
            }

            foreach (ICommand c in _commandList)
            {
                if(! c.CanUndo)
                {
                    _canUndo = false;
                    break;
                }
            }
        }
    }
}
