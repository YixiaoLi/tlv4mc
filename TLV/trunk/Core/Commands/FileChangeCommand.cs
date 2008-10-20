using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class FileChangeCommand : ICommand 
    {
        Action _action = null;
        public string Text { get; set; }

        public bool CanUndo
        {
            get { return false; ; }
        }

        public void Do()
        {
            if (ApplicationDatas.ActiveFileContext.IsOpened
                && !ApplicationDatas.ActiveFileContext.IsSaved)
            {
                switch (MessageBox.Show("ファイルが変更されています。\n保存しますか？", "ファイルが変更されています", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        ApplicationFactory.CommandManager.Do(new SaveCommonFormatTraceLogFileCommand());
                        if (ApplicationDatas.ActiveFileContext.IsSaved)
                        {
                            _action.Invoke();
                        }
                        break;
                    case DialogResult.No:
                        ApplicationDatas.ActiveFileContext.Close();
                        _action.Invoke();
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                _action.Invoke();
            }
        }

        public void Undo()
        {
        }

        public FileChangeCommand(Action action)
        {
            _action = action;
        }
    }
}
