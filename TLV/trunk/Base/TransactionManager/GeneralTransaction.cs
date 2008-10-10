using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class GeneralTransaction : ITransaction
    {
        public string Text { get; set; }
        public Action DoAction { get; set; }
        public Action UndoAction { get; set; }

        public GeneralTransaction(string text, Action _do, Action undo)
        {
            Text = text;
            DoAction = _do;
            UndoAction = undo;
        }

        public void Do()
        {
            if (DoAction != null)
            {
                DoAction();
            }
        }

        public void Undo()
        {
            if (UndoAction != null)
            {
                UndoAction();
            }
        }
    }

}
