using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface ICommand
    {
        string Text { get; set; }
        bool CanUndo { get; set; }
        void Do();
        void Undo();
    }
}
