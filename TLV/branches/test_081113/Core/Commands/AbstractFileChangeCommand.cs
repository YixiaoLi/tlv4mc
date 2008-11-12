﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public abstract class AbstractFileChangeCommand : ICommand 
    {
        public string Text { get; set; }

        public bool CanUndo
        {
            get { return false; }
            set { }
        }

        public void Do()
        {
            if (ApplicationData.ActiveFileContext.IsOpened
                && !ApplicationData.ActiveFileContext.IsSaved)
            {
                switch (MessageBox.Show("ファイルが更新されています。\n保存しますか？", "ファイルが更新されています", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        ApplicationFactory.CommandManager.Do(new SaveCommand());
                        if (ApplicationData.ActiveFileContext.IsSaved)
                        {
                            action();
                        }
                        break;
                    case DialogResult.No:
                        action();
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                action();
            }
        }

        public void Undo()
        {
        }

        public AbstractFileChangeCommand()
        {
        }

        protected virtual void action()
        {

        }
    }
}
