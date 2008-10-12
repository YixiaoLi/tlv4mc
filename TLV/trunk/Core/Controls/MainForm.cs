﻿using System;
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
        private IWindowManager _windowManager;
        private CommandManager _commandManager;

        public MainForm()
        {
            InitializeComponent();

            // アプリケーションに指定されたWindowManagerHandlerを使いWindowManagerを生成
            _windowManager = ApplicationFactory.WindowManager;
            _commandManager = ApplicationFactory.TransactionManager;
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
            _windowManager.SubWindowDockStateChanged += (o, _e) =>
                {
                    _commandManager.Done(new GeneralCommand(
                        ((SubWindow)o).Text + " のドッキング箇所を " + _e.New.ToText() + " にする",
                        () =>
                        {
                            ((SubWindow)o).DockState = _e.New;
                        },
                        () =>
                        {
                            ((SubWindow)o).DockState = _e.Old;
                        }));
                };
            _windowManager.SubWindowVisibleChanged += (o, _e) =>
                {
                    string visible = _e.New ? "表示" : "非表示";

                    _commandManager.Done(new GeneralCommand(
                        ((SubWindow)o).Text + " を " + visible + " にする",
                        () =>
                        {
                            ((SubWindow)o).Visible = _e.New;
                        },
                        () =>
                        {
                            ((SubWindow)o).Visible = _e.Old;
                        }));
                };

            viewToolStripMenuItem.SetWindowManager(_windowManager);
            undoToolStripMenuItem.SetUndoMenu(_commandManager);
            redoToolStripMenuItem.SetRedoMenu(_commandManager);
        }
    }
}
