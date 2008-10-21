﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class CloseCommand :ICommand
    {
        public string Text
        {
            get;
            set;
        }

        public bool CanUndo { get { return false; } }

        public void Do()
        {
            ApplicationFactory.CommandManager.Do(new FileChangeCommand(() =>
            {
                ApplicationDatas.ActiveFileContext.Close();
            }));
        }

        public void Undo()
        {
        }

        public CloseCommand()
        {
        }

    }
}