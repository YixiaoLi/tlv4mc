using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Commands;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class MainForm : Form
    {
        private IWindowManager _windowManager;
        private CommandManager _commandManager;

        public MainForm()
        {
            InitializeComponent();

            settingLoad();

            _windowManager = ApplicationFactory.WindowManager;
            _commandManager = ApplicationFactory.CommandManager;
            Text = ApplicationDatas.Name + " " + ApplicationDatas.Version;
        }

        protected override void OnLoad(EventArgs evntArgs)
        {
            base.OnLoad(evntArgs);

            #region ApplicationDatasイベント設定
            ApplicationDatas.ActiveFileContext.PathChanged += (o, e) => { textReflesh(); };
            ApplicationDatas.ActiveFileContext.IsSavedChanged += (o, e) =>
            {
                textReflesh();
                saveSToolStripMenuItem.Enabled = !ApplicationDatas.ActiveFileContext.IsSaved;
                saveToolStripButton.Enabled = !ApplicationDatas.ActiveFileContext.IsSaved;
            };
            ApplicationDatas.ActiveFileContext.IsOpenedChanged += (o, e) =>
            {
                textReflesh();
                saveAsToolStripMenuItem.Enabled = ApplicationDatas.ActiveFileContext.IsOpened;
            };
            ApplicationDatas.ActiveFileContext.DataChanged += (o, e) => { textReflesh(); };
            #endregion

            #region コマンド管理初期化
            undoToolStripMenuItem.SetCommandManagerAsUndo(_commandManager);
            redoToolStripMenuItem.SetCommandManagerAsRedo(_commandManager);
            EventHandler<GeneralChangedEventArgs<DockState>> d = (o, e) => { _commandManager.Done(new ChangeSubWindowDockStateCommand(((SubWindow)o), e.Old, e.New)); };
            EventHandler<GeneralChangedEventArgs<bool>> v = (o, e) => { _commandManager.Done(new ChangeSubWindowVisiblityCommand(((SubWindow)o), e.New)); };
            #endregion

            #region サブウィンドウ管理初期化
            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "サブウィンドウ1" },
                new SubWindow("sb2", new Control(), DockState.DockLeft) { Text = "サブウィンドウ2" },
                new SubWindow("sb3", new Control(), DockState.DockRight) { Text = "サブウィンドウ3" },
            };
            _windowManager.Parent = this.toolStripContainer.ContentPanel;
            _windowManager.MainPanel = new Control();
            _windowManager.AddSubWindow(sws);
            _windowManager.SubWindowDockStateChanged += d;
            _windowManager.SubWindowVisibleChanged += v;
            viewToolStripMenuItem.SetWindowManager(_windowManager);
            #endregion

            #region メニューバーイベント設定

            #region 表示メニュー
            showAllToolStripMenuItem.Click += (o, e) =>
            {
                _windowManager.SubWindowVisibleChanged -= v;
                _commandManager.Do(new MacroCommand(
                    from sw in sws where !sw.Visible && sw.Enabled
                    select (ICommand)(new ChangeSubWindowVisiblityCommand(sw, true))) { Text= "すべてのウィンドウを表示する" });
                _windowManager.SubWindowVisibleChanged += v;
            };
            hideAllToolStripMenuItem.Click += (o, e) =>
            {
                _windowManager.SubWindowVisibleChanged -= v;
                _commandManager.Do(new MacroCommand(
                    from sw in sws
                    where sw.Visible && sw.Enabled
                    select (ICommand)(new ChangeSubWindowVisiblityCommand(sw, false))) { Text = "すべてのウィンドウを非表示にする" });
                _windowManager.SubWindowVisibleChanged += v;
            };
            #endregion

            #region ファイルメニュー
            openCommonFormatTraceLogFileToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new OpenCommonFormatTraceLogFileCommand());
            };

            openResourceFileAndTraceLogFileToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new OpenResourceFileAndTraceLogFileCommand());
            };

            closeToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new CloseCommand(this));
            };

            saveSToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new SaveCommonFormatTraceLogFileCommand());
            };

            saveAsToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new SaveAsCommonFormatTraceLogFileCommand());
            };

            #endregion

            #endregion

            #region ツールバーイベント設定

            openToolStripButton.Click += (o, e) =>
            {
                _commandManager.Do(new OpenCommonFormatTraceLogFileCommand());
            };

            saveToolStripButton.Click += (o, e) =>
            {
                _commandManager.Do(new SaveCommonFormatTraceLogFileCommand());
            };

            #endregion

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            settingSave();

            if (ApplicationDatas.ActiveFileContext.IsOpened
                && !ApplicationDatas.ActiveFileContext.IsSaved)
            {
                e.Cancel = true;
                _commandManager.Do(new CloseCommand(this));
            }
        }

        private void settingLoad()
        {
            ClientSize = Properties.Settings.Default.ClientSize;
            Location = Properties.Settings.Default.Location;
            WindowState = Properties.Settings.Default.WindowState;
        }

        private void settingSave()
        {
            Properties.Settings.Default.ClientSize = ClientSize;
            Properties.Settings.Default.Location = Location;
            Properties.Settings.Default.WindowState = WindowState;
            Properties.Settings.Default.Save();
        }

        protected void textReflesh()
        {
            Text = "";

            if (ApplicationDatas.ActiveFileContext.Path == string.Empty)
                Text += "新規トレースログ";
            else
                Text += Path.GetFileNameWithoutExtension(ApplicationDatas.ActiveFileContext.Path);

            if (!ApplicationDatas.ActiveFileContext.IsSaved)
                Text += " *";

            Text += " - ";

            Text += ApplicationDatas.Name + " " + ApplicationDatas.Version;
        }
    }
}
