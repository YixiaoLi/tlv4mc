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
            Text = ApplicationData.Name + " " + ApplicationData.Version;
        }

        protected override void OnLoad(EventArgs evntArgs)
        {
            base.OnLoad(evntArgs);

            #region ApplicationDatasイベント設定
            ApplicationData.ActiveFileContext.PathChanged += (o, e) =>
            {
                Invoke((MethodInvoker)(() => 
                {
                    textReflesh();
                }));
            };
            ApplicationData.ActiveFileContext.IsSavedChanged += (o, e) =>
            {
                Invoke((MethodInvoker)(() => 
                {
                    textReflesh();
                    saveSToolStripMenuItem.Enabled = !ApplicationData.ActiveFileContext.IsSaved;
                    saveToolStripButton.Enabled = !ApplicationData.ActiveFileContext.IsSaved;
                }));
            };
            ApplicationData.ActiveFileContext.IsOpenedChanged += (o, e) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    textReflesh();
                    closeToolStripMenuItem.Enabled = ApplicationData.ActiveFileContext.IsOpened;
                    saveAsToolStripMenuItem.Enabled = ApplicationData.ActiveFileContext.IsOpened;
                }));
            };
            ApplicationData.ActiveFileContext.DataChanged += (o, e) =>
            {
                Invoke((MethodInvoker)(() =>
                {
					if (ApplicationData.ActiveFileContext.Data == null)
					{
						saveSToolStripMenuItem.Enabled = false;
						saveToolStripButton.Enabled = false;
						((TraceLogViewer)(_windowManager.SubWindows.Single<SubWindow>(s => s.Name == "traceLogViewer").Control)).ClearData();
					}
					else
					{
						((TraceLogViewer)(_windowManager.SubWindows.Single<SubWindow>(s => s.Name == "traceLogViewer").Control)).SetData(ApplicationData.ActiveFileContext.Data.TraceLogData, ApplicationData.ActiveFileContext.Data.ResourceData);
					}
                    textReflesh();
                }));
            };
            #endregion

            #region コマンド管理初期化
            undoToolStripMenuItem.SetCommandManagerAsUndo(_commandManager);
            redoToolStripMenuItem.SetCommandManagerAsRedo(_commandManager);
            undoToolStripButton.SetCommandManagerAsUndo(_commandManager);
            redoToolStripButton.SetCommandManagerAsRedo(_commandManager);
            #endregion

            #region サブウィンドウ管理初期化
            SubWindow[] sws = new[]
            {
                new SubWindow("traceLogViewer", new TraceLogViewer(), DockState.DockRight) { Text = "トレースログ" },
            };
            _windowManager.Parent = this.toolStripContainer.ContentPanel;
            _windowManager.MainPanel = new Control();
            _windowManager.AddSubWindow(sws);
            _windowManager.SubWindowDockStateChanged += (o, e) => { _commandManager.Done(new ChangeSubWindowDockStateCommand(((SubWindow)o), e.Old, e.New)); };
            EventHandler<GeneralChangedEventArgs<bool>> v = (o, e) => { _commandManager.Done(new ChangeSubWindowVisiblityCommand(((SubWindow)o), e.New)); };
            _windowManager.SubWindowVisibleChanged += v;
            viewToolStripMenuItem.SetWindowManager(_windowManager);
            #endregion

            #region メニューバーイベント設定

            #region 表示メニュー
            showAllToolStripMenuItem.Click += (o, e) =>
            {
                // 非表示状態のウィンドウを探しコマンドを生成する
                var cswvc = from sw in sws
                         where !sw.Visible && sw.Enabled
                         select (ICommand)(new ChangeSubWindowVisiblityCommand(sw, true));
                if (cswvc.Count() != 0)
                {
                    // SubWindowVisibleChangedを無効にしておかないとundoスタックにすべてのウィンドウの表示コマンドが追加されてしまう
                    _windowManager.SubWindowVisibleChanged -= v;
                    _commandManager.Do(new MacroCommand(cswvc) { Text="すべてのウィンドウを表示する"});
                    _windowManager.SubWindowVisibleChanged += v;
                }
            };
            hideAllToolStripMenuItem.Click += (o, e) =>
            {
                // 表示状態のウィンドウを探しコマンドを生成する
                var cswvc = from sw in sws
                            where sw.Visible && sw.Enabled
                            select (ICommand)(new ChangeSubWindowVisiblityCommand(sw, false));
                if (cswvc.Count() != 0)
                {
                    // SubWindowVisibleChangedを無効にしておかないとundoスタックにすべてのウィンドウの非表示コマンドが追加されてしまう
                    _windowManager.SubWindowVisibleChanged -= v;
                    _commandManager.Do(new MacroCommand(cswvc) { Text = "すべてのウィンドウを非表示にする" });
                    _windowManager.SubWindowVisibleChanged += v;
                }
            };
            #endregion

            #region ファイルメニュー

            newToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new NewCommand());
            };

            openToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new OpenCommand());
            };

            saveSToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new SaveCommand());
            };

            saveAsToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new SaveAsCommand());
            };

            exitToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new ExitCommand(this));
            };
            closeToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new CloseCommand());
            };

            #endregion

            #endregion

            #region ツールバーイベント設定

            newToolStripButton.Click += (o, e) =>
            {
                _commandManager.Do(new NewCommand());
            };

            openToolStripButton.Click += (o, e) =>
            {
                _commandManager.Do(new OpenCommand());
            };

            saveToolStripButton.Click += (o, e) =>
            {
                _commandManager.Do(new SaveCommand());
            };

            #endregion

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            settingSave();

            if (ApplicationData.ActiveFileContext.IsOpened
                && !ApplicationData.ActiveFileContext.IsSaved)
            {
                e.Cancel = true;
                _commandManager.Do(new ExitCommand(this));
            }
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            string s = ((string[])(drgevent.Data.GetData(DataFormats.FileDrop)))[0];

            if(Path.GetExtension(s) == "." + Properties.Resources.CommonFormatTraceLogFileExtension 
                && drgevent.Data.GetDataPresent(DataFormats.FileDrop))
                drgevent.Effect = DragDropEffects.All;
            else
                drgevent.Effect = DragDropEffects.None;
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string s = ((string[])(drgevent.Data.GetData(DataFormats.FileDrop)))[0];

                _commandManager.Do(new OpenCommand(s));
            }
        }

        private void settingLoad()
        {
			ApplicationFactory.Setup();
			ApplicationData.Setup();
            ClientSize = Properties.Settings.Default.ClientSize;
            Location = Properties.Settings.Default.Location;
            WindowState = Properties.Settings.Default.WindowState;
        }

        private void settingSave()
		{
			ApplicationData.Setting.Save();
            Properties.Settings.Default.ClientSize = ClientSize;
            Properties.Settings.Default.Location = Location;
            Properties.Settings.Default.WindowState = WindowState;
            Properties.Settings.Default.Save();
        }

        protected void textReflesh()
        {
            Text = "";

            if(ApplicationData.ActiveFileContext.Data != null)
            {
                if (ApplicationData.ActiveFileContext.Path == string.Empty)
                    Text += "新規トレースログ";
                else
                    Text += Path.GetFileNameWithoutExtension(ApplicationData.ActiveFileContext.Path);

                if (!ApplicationData.ActiveFileContext.IsSaved)
                    Text += " *";

                Text += " - ";
            }
            Text += ApplicationData.Name + " " + ApplicationData.Version;
        }
    }
}
