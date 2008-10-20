using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class SaveCommand : ICommand
    {
        public string Text
        {
            get;
            set;
        }

        public bool CanUndo { get { return false; } }

        public void Do()
        {
            if(ApplicationDatas.ActiveFileContext.IsOpened)
            {
                try
                {
                    ApplicationDatas.ActiveFileContext.Save();
                }
                catch (FilePathUndefinedException)
                {
                    ApplicationFactory.CommandManager.Do(new SaveAsCommand());
                }
            }
        }

        public void Undo()
        {

        }

        public SaveCommand()
        {
            Text = "保存する";
        }
    }
}
