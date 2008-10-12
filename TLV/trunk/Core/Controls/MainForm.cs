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
        private IWindowManager _windowManager;
        private CommandManager _commandManager;

        public MainForm()
        {
            InitializeComponent();

            _windowManager = ApplicationFactory.WindowManager;
            _commandManager = ApplicationFactory.CommandManager;
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
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "サブウィンドウ5", Enabled = false },
            };

            undoToolStripMenuItem.SetCommandManagerAsUndo(_commandManager);
            redoToolStripMenuItem.SetCommandManagerAsRedo(_commandManager);

            EventHandler<GeneralChangedEventArgs<DockState>> d = (o, _e) => { _commandManager.Done(new ChangeSubWindowDockStateCommand(((SubWindow)o), _e.Old, _e.New)); };
            EventHandler<GeneralChangedEventArgs<bool>> v = (o, _e) => { _commandManager.Done(new ChangeSubWindowVisiblityCommand(((SubWindow)o), _e.New)); };

            _windowManager.Parent = this.toolStripContainer.ContentPanel;
            _windowManager.MainPanel = new Control();
            _windowManager.AddSubWindow(sws);
            _windowManager.SubWindowDockStateChanged += d;
            _windowManager.SubWindowVisibleChanged += v;
            viewToolStripMenuItem.SetWindowManager(_windowManager);

            showAllToolStripMenuItem.Click += (o, _e) =>
            {
                _windowManager.SubWindowVisibleChanged -= v;
                _commandManager.Do(new MacroCommand(
                    from sw in sws where !sw.Visible && sw.Enabled
                    select (ICommand)(new ChangeSubWindowVisiblityCommand(sw, true))) { Text= "すべてのウィンドウを表示する" });
                _windowManager.SubWindowVisibleChanged += v;
            };
            hideAllToolStripMenuItem.Click += (o, _e) =>
            {
                _windowManager.SubWindowVisibleChanged -= v;
                _commandManager.Do(new MacroCommand(
                    from sw in sws where sw.Visible && sw.Enabled
                    select (ICommand)(new ChangeSubWindowVisiblityCommand(sw, false))) { Text = "すべてのウィンドウを非表示にする" });
                _windowManager.SubWindowVisibleChanged += v;
            };
        }
    }
}
