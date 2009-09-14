
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using System.Threading;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public abstract class AbstractFileChangeCommand : ICommand 
    {
		private volatile bool flag = false;

        public string Text { get; set; }

        public bool CanUndo
        {
            get { return false; }
            set { }
        }

        public void Do()
        {
            if (ApplicationData.FileContext.IsOpened
                && !ApplicationData.FileContext.IsSaved)
            {
                switch (MessageBox.Show("ファイルが更新されています。\n保存しますか？", "ファイルが更新されています", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
						ApplicationFactory.CommandManager.Do(new SaveCommand());

						flag = false;
						for (; ; )
						{
							flag = ApplicationData.FileContext.IsSaved;

							if (flag)
								break;
							else
								Application.DoEvents();
						}

						if (ApplicationData.FileContext.IsSaved)
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
