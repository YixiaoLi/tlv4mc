using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class SaveCommonFormatTraceLogFileCommand : ICommand
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
                    ApplicationFactory.CommandManager.Do(new SaveAsCommonFormatTraceLogFileCommand());
                }
            }
        }

        public void Undo()
        {

        }

        public SaveCommonFormatTraceLogFileCommand()
        {
            Text = "保存する";
        }
    }
}
