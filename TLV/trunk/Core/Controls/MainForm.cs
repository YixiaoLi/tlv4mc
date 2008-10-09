using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class MainForm : Form
    {
        private WindowManager _windowManager;

        public MainForm()
        {
            InitializeComponent();

            // アプリケーションに指定されたWindowManagerHandlerを使いWindowManagerを生成
            _windowManager = new WindowManager(ApplicationFactory.WindowManagerHandler);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SubWindow[] sws = new[]
            {
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "サブウィンドウ3" },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "サブウィンドウ4", Visible = false },
                new SubWindow("sb1", new Control(), DockState.DockLeft),
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "サブウィンドウ2" },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "サブウィンドウ5" },
            };

            _windowManager.Parent = this.toolStripContainer.ContentPanel;
            _windowManager.MainPanel = new Control();
            _windowManager.AddSubWindow(sws);
            _windowManager.Menu = viewToolStripMenuItem;

        }
    }
}
