using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface ICommandManager
    {
        event EventHandler<GeneralEventArgs<ICommand>> CommandDone;
        event EventHandler<GeneralEventArgs<ICommand>> CommandUndone;
        event EventHandler<GeneralEventArgs<ICommand>> CommandRedone;
        event EventHandler UndoBecameEnable;
        event EventHandler UndoBecameDisEnable;
        event EventHandler RedoBecameDisenable;
        event EventHandler RedoBecameEnable;
        void Do(ICommand command);
        void Done(ICommand command);
        void Undo();
        void Redo();
        string UndoText { get; }
        string RedoText { get; }
    }


}
