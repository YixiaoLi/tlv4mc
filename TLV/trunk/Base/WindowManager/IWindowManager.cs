using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// サブウィンドウを管理するには必須ではないが、
    /// 備えておきたい機能のインターフェイス
    /// サブウィンドウ管理に必須な機能はIWindowManagerHandlerで定義する
    /// </summary>
    public interface IWindowManager : IWindowManagerHandler
    {
        ToolStripMenuItem Menu { get; set; }
    }
}
