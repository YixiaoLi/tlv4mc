using System;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Commands;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class MainForm : Form
    {
        private WindowManager _windowManager;
        private CommandManager _commandManager;
		private StatusManager _statusManager;

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
            ApplicationData.FileContext.PathChanged += (o, e) =>
            {
                Invoke((MethodInvoker)(() => 
                {
                    textReflesh();
                }));
            };
            ApplicationData.FileContext.IsSavedChanged += (o, e) =>
            {
                Invoke((MethodInvoker)(() => 
                {
                    textReflesh();
                    saveSToolStripMenuItem.Enabled = !ApplicationData.FileContext.IsSaved;
                    saveToolStripButton.Enabled = !ApplicationData.FileContext.IsSaved;
                }));
            };
            ApplicationData.FileContext.IsOpenedChanged += (o, e) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    textReflesh();
                    closeToolStripMenuItem.Enabled = ApplicationData.FileContext.IsOpened;
                    saveAsToolStripMenuItem.Enabled = ApplicationData.FileContext.IsOpened;
                }));
            };
            ApplicationData.FileContext.DataChanged += (o, e) =>
            {
                Invoke((MethodInvoker)(() =>
                {
					if (ApplicationData.FileContext.Data == null)
					{
						saveSToolStripMenuItem.Enabled = false;
						saveToolStripButton.Enabled = false;
					}
                    textReflesh();
                }));
            };
			ApplicationData.FileContext.Saving += (o, e) =>
			{
				Invoke((MethodInvoker)(() => 
				{
					Cursor.Current = Cursors.WaitCursor;
					_statusManager.ShowProcessing(this.GetType().ToString() + ":saving", "保存中");
				}));
			};
			ApplicationData.FileContext.Saved += (o, e) =>
			{
				Invoke((MethodInvoker)(() => 
				{
					if (Cursor.Current == Cursors.WaitCursor)
						Cursor.Current = Cursors.Default;

					if (_statusManager.IsProcessingShown(this.GetType().ToString() + ":saving"))
						_statusManager.HideProcessing(this.GetType().ToString() + ":saving");
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
                new SubWindow("traceLogViewer", new TraceLogViewer(), DockState.DockRight) { Text = "トレースログビューア" },
                new SubWindow("resourceExplorer", new ResourceExplorer(), DockState.DockLeft) { Text = "リソースエクスプローラ" },
                new SubWindow("visualizeRuleExplorer", new VisualizeRuleExplorer(), DockState.DockLeft) { Text = "可視化ルールエクスプローラ" },
            };
            _windowManager.Parent = this.toolStripContainer.ContentPanel;
			_windowManager.MainPanel = new TraceLogDisplayPanel();
			_windowManager.AddSubWindow(sws);
			_windowManager.Load();
			_windowManager.Show();
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

			#region ステータスバー設定

			_statusManager = ApplicationFactory.StatusManager;
			_statusManager.StatusStrip = statusStrip;

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

			//_windowManager.MainPanel.Resize += (o, e) => _windowManager.MainPanel.Invalidate();
			//_windowManager.MainPanel.Paint += (o, e) =>
			//    {
			//        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			//        if (ApplicationData.FileContext.IsOpened)
			//        {
			//            float i = 0.0f;
			//            foreach (Shapes ss in ApplicationData.FileContext.Data.VisualizeData.Shapes)
			//            {
			//                float w = (float)_windowManager.MainPanel.ClientSize.Width / (float)ApplicationData.FileContext.Data.VisualizeData.Shapes.Count;

			//                e.Graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF(w * i, 0.0f, w, w));
			//                e.Graphics.DrawRectangle(new System.Drawing.Pen(Color.Black), new Rectangle((int)(w * i), 0, (int)w, (int)w));

			//                foreach (Shape s in ss)
			//                {
			//                    e.Graphics.DrawShape(s, new RectangleF(w * i, 0.0f, w, w));
			//                }
			//                i++;
			//            }
			//        }
			//    };
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            settingSave();

            if (ApplicationData.FileContext.IsOpened
                && !ApplicationData.FileContext.IsSaved)
            {
                e.Cancel = true;
                _commandManager.Do(new ExitCommand(this));
            }

			if (Directory.Exists(ApplicationData.Setting.TemporaryDirectoryPath))
				Directory.Delete(ApplicationData.Setting.TemporaryDirectoryPath, true);
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
			ClientSize = Properties.Settings.Default.ClientSize;
			Location = Properties.Settings.Default.Location;
			WindowState = Properties.Settings.Default.WindowState;
        }

        private void settingSave()
		{
			ApplicationData.Setting.Save();

			_windowManager.Save();

			Properties.Settings.Default.Location = Location;
			Properties.Settings.Default.WindowState = WindowState;
			if (WindowState == FormWindowState.Normal)
				Properties.Settings.Default.ClientSize = ClientSize;
			Properties.Settings.Default.Save();
        }

        protected void textReflesh()
        {
            Text = "";

            if(ApplicationData.FileContext.Data != null)
            {
                if (ApplicationData.FileContext.Path == string.Empty)
                    Text += "新規トレースログ";
                else
                    Text += Path.GetFileNameWithoutExtension(ApplicationData.FileContext.Path);

                if (!ApplicationData.FileContext.IsSaved)
                    Text += " *";

                Text += " - ";
            }
            Text += ApplicationData.Name + " " + ApplicationData.Version;
        }
    }
}
