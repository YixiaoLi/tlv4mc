using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class MacroCommand : ICommand
    {
        private List<ICommand> _commandList;

        public string Text
        {
            get;
            set;
        }

        public bool CanUndo
        {
            get
            {
                foreach (ICommand c in _commandList)
                {
                    if (!c.CanUndo)
                        return false;
                }
                return true;
            }
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
            Text = _commandList.First<ICommand>().Text + " から " + _commandList.Last<ICommand>().Text + " までの一連の動作";
        }
    }
}
