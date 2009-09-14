
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class GeneralCommand : ICommand
    {
        public string Text { get; set; }
        public Action DoAction { get; set; }
        public Action UndoAction { get; set; }
        public bool CanUndo { get; set; }

        public void Do()
        {
            if (DoAction != null)
            {
                DoAction();
            }
        }

        public void Undo()
        {
            if (CanUndo && UndoAction != null)
            {
                UndoAction();
            }
        }
        /// <summary>
        /// <c>GeneralCommand</c>のインスタンスを生成する
        /// </summary>
        /// <param name="text">コマンドの説明</param>
        /// <param name="_do">コマンドの処理</param>
        /// <param name="undo">コマンド結果を元に戻す処理</param>
        public GeneralCommand(string text, Action _do, Action undo)
        {
            Text = text;
            DoAction = _do;
            UndoAction = undo;
            CanUndo = (undo != null);
        }

    }

}
