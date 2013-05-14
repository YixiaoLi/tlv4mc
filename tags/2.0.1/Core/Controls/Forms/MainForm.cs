/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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

		private void invoke(MethodInvoker action)
		{
			if (IsHandleCreated)
				Invoke(action);
			else
				action.Invoke();
		}

        protected override void OnLoad(EventArgs evntArgs)
        {
            base.OnLoad(evntArgs);

            #region ApplicationDatasイベント設定
            ApplicationData.FileContext.PathChanged += (o, e) =>
			{
                invoke((MethodInvoker)(() =>
                {
                    textReflesh();
                    reloadToolStripButton.Enabled = ApplicationData.FileContext.Path == string.Empty;
                }
                ));
            };
            ApplicationData.FileContext.IsSavedChanged += (o, e) =>
            {
				invoke((MethodInvoker)(() => 
                {
                    textReflesh();
                    saveSToolStripMenuItem.Enabled = !ApplicationData.FileContext.IsSaved;
                    saveToolStripButton.Enabled = !ApplicationData.FileContext.IsSaved;
                }));
            };
            ApplicationData.FileContext.IsOpenedChanged += (o, e) =>
			{
				invoke((MethodInvoker)(() =>
                {
                    textReflesh();
                    closeToolStripMenuItem.Enabled = ApplicationData.FileContext.IsOpened;
                    saveAsToolStripMenuItem.Enabled = ApplicationData.FileContext.IsOpened;
                }));
            };
            ApplicationData.FileContext.DataChanged += (o, e) =>
			{
				invoke((MethodInvoker)(() =>
                {
					if (ApplicationData.FileContext.Data == null)
					{
						saveSToolStripMenuItem.Enabled = false;
						saveToolStripButton.Enabled = false;
					}
                    reloadToolStripButton.Enabled = ApplicationData.FileContext.Path == string.Empty;
                    textReflesh();
                }));
            };
			ApplicationData.FileContext.Saving += (o, e) =>
			{
				invoke((MethodInvoker)(() => 
				{
					Cursor.Current = Cursors.WaitCursor;
					_statusManager.ShowProcessing(this.GetType().ToString() + ":saving", "保存中");
				}));
			};
			ApplicationData.FileContext.Saved += (o, e) =>
			{
				invoke((MethodInvoker)(() => 
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
            _windowManager.Parent = this.toolStripContainer.ContentPanel;
            _windowManager.MainPanel = new TraceLogDisplayPanel();

			SubWindow[] sws = new[]
            {
                new SubWindow("macroViewer", new TimeLineMacroViewer(){ Text = "マクロビューア" }, DockState.DockBottom) { Text = "マクロビューア" },
                new SubWindow("traceLogViewer", new TraceLogViewer(){ Text = "トレースログビューア" }, DockState.DockRight) { Text = "トレースログビューア" },
                new SubWindow("resourceExplorer", new ResourceExplorer(){ Text = "リソースエクスプローラ" }, DockState.DockLeft) { Text = "リソースエクスプローラ" },
                new SubWindow("visualizeRuleExplorer", new VisualizeRuleExplorer(){ Text = "可視化ルールエクスプローラ" }, DockState.DockLeft) { Text = "可視化ルールエクスプローラ" },
                new SubWindow("statisticsExplorer", new StatisticsExplorer(this){ Text = "統計情報エクスプローラ" }, DockState.DockRight) { Text = "統計情報エクスプローラ" },
            };
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
            aboutAToolStripMenuItem.Click += (o, e) =>
            {
                _commandManager.Do(new AboutCommand());
            };
            captureToolStripeButton.Click += (o, e) =>
            {
               _commandManager.Do(new CaptureCommand((TraceLogDisplayPanel) _windowManager.MainPanel));
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
            reloadToolStripButton.Click += (o, e) =>
            {
                _commandManager.Do(new ReloadCommand());
            };

            #endregion

			//_windowManager.MainPanel = new Control();
			//_windowManager.MainPanel.Resize += (o, e) => _windowManager.MainPanel.Invalidate();
			//_windowManager.MainPanel.Paint += (o, e) =>
			//    {
			////        RotateColorFactory colorFactory = new RotateColorFactory();

			////        int a = 50;

			////        float w = _windowManager.MainPanel.ClientSize.Width / a / 2;
			////        float h = _windowManager.MainPanel.ClientSize.Height / a;

			////        for (int j = 0; j < a; j++)
			////        {
			////            for (int i = 0; i < a; i++ )
			////            {
			////                Color c = colorFactory.RamdomColor();
			////                float x = w * i;
			////                float y = h * j;
			////                e.Graphics.FillRectangle(new SolidBrush(c), x, y, w, h);
			////            }
			////        }

			////        colorFactory.Saturation = 100;
			////        colorFactory.Value = 100;

			////        for (int j = 0; j < a; j++)
			////        {
			////            for (int i = 0; i < a; i++)
			////            {
			////                Color c = colorFactory.RotateColor();
			////                float x = w * i + (_windowManager.MainPanel.ClientSize.Width / 2);
			////                float y = h * j;
			////                e.Graphics.FillRectangle(new SolidBrush(c), x, y, w, h);
			////            }
			////        }

			////    if (ApplicationData.FileContext.IsOpened)
			////    {
			////        float i = 0.0f;
			////        foreach (Shapes ss in ApplicationData.FileContext.Data.VisualizeData.Shapes)
			////        {
			////            float w = (float)_windowManager.MainPanel.ClientSize.Width / (float)ApplicationData.FileContext.Data.VisualizeData.Shapes.Count;

			////            e.Graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF(w * i, 0.0f, w, w));
			////            e.Graphics.DrawRectangle(new System.Drawing.Pen(Color.Black), new Rectangle((int)(w * i), 0, (int)w, (int)w));

			////            foreach (Shape s in ss)
			////            {
			////                e.Graphics.DrawShape(s, new RectangleF(w * i, 0.0f, w, w));
			////            }
			////            i++;
			////        }
			////    }
			////};
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
			{
				try
				{
					Directory.Delete(ApplicationData.Setting.TemporaryDirectoryPath, true);
				}
				catch(Exception _e)
				{
					MessageBox.Show("エラーが発生しました．\n一時ディレクトリ：[" + ApplicationData.Setting.TemporaryDirectoryPath + "]\n強制終了します．\n----\n" + _e.Message, "エラーが発生しました", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);
            ApplicationFactory.BlackBoard.dragFlag = true;
            string[] s = ((string[])(drgevent.Data.GetData(DataFormats.FileDrop)));

            if((
				(s.Length == 1
				&& (Path.GetExtension(s[0]).Contains(Properties.Resources.StandardFormatTraceLogFileExtension)
				|| Path.GetExtension(s[0]).Contains(Properties.Resources.TraceLogFileExtension)
				|| Path.GetExtension(s[0]).Contains(Properties.Resources.ResourceFileExtension)))
			||	(s.Length == 2
				&& ((Path.GetExtension(s[0]).Contains(Properties.Resources.ResourceFileExtension) && Path.GetExtension(s[1]).Contains(Properties.Resources.TraceLogFileExtension))
				|| (Path.GetExtension(s[1]).Contains(Properties.Resources.ResourceFileExtension) && Path.GetExtension(s[0]).Contains(Properties.Resources.TraceLogFileExtension)))
			))
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

                string[] s = ((string[])(drgevent.Data.GetData(DataFormats.FileDrop)));

				if (s.Length == 1 && Path.GetExtension(s[0]).Contains(Properties.Resources.StandardFormatTraceLogFileExtension))
				{
					_commandManager.Do(new OpenCommand(s[0]));
				}
				else if (s.Length == 1 && Path.GetExtension(s[0]).Contains(Properties.Resources.TraceLogFileExtension))
				{
					_commandManager.Do(new NewCommand(null, s[0]));
				}
				else if (s.Length == 1 && Path.GetExtension(s[0]).Contains(Properties.Resources.ResourceFileExtension))
				{
					_commandManager.Do(new NewCommand(s[0], null));
				}
				else if (s.Length == 2)
				{
					string resFilePath = Path.GetExtension(s[0]).Contains(Properties.Resources.ResourceFileExtension) ? s[0] : s[1];
					string logFilePath = Path.GetExtension(s[1]).Contains(Properties.Resources.TraceLogFileExtension) ? s[1] : s[0];
                    _commandManager.Do(new NewCommand(resFilePath, logFilePath));
				}

            }
            ApplicationFactory.BlackBoard.dragFlag = false;
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
                {
                    Text += "新規トレースログ";

                    if (ApplicationData.FileContext.Data.TraceLogData != null)
                    {
                        Text += "(" +
                                    Path.GetFileName(ApplicationData.FileContext.Data.TraceLogData.Path) +
                                    "と" +
                                    Path.GetFileName(ApplicationData.FileContext.Data.ResourceData.Path) +
                                 "を表示中)";
                    }
                }
                else
                {
                    Text += Path.GetFileNameWithoutExtension(ApplicationData.FileContext.Path);
                }

                if (!ApplicationData.FileContext.IsSaved)
                    Text += " *";

                Text += " - ";
            }
            Text += ApplicationData.Name + " " + ApplicationData.Version;
        }

        private void reloadToolStripButton_Click(object sender, EventArgs e)
        {

        }

        
    }
}
