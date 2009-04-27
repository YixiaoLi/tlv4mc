/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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

            #region ApplicationDatas���٥������
            ApplicationData.FileContext.PathChanged += (o, e) =>
			{
				invoke((MethodInvoker)(() => 
                {
                    textReflesh();
                }));
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
                    textReflesh();
                }));
            };
			ApplicationData.FileContext.Saving += (o, e) =>
			{
				invoke((MethodInvoker)(() => 
				{
					Cursor.Current = Cursors.WaitCursor;
					_statusManager.ShowProcessing(this.GetType().ToString() + ":saving", "��¸��");
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

            #region ���ޥ�ɴ��������
            undoToolStripMenuItem.SetCommandManagerAsUndo(_commandManager);
            redoToolStripMenuItem.SetCommandManagerAsRedo(_commandManager);
            undoToolStripButton.SetCommandManagerAsUndo(_commandManager);
            redoToolStripButton.SetCommandManagerAsRedo(_commandManager);
            #endregion

            #region ���֥�����ɥ����������
            _windowManager.Parent = this.toolStripContainer.ContentPanel;
			_windowManager.MainPanel = new TraceLogDisplayPanel();
			SubWindow[] sws = new[]
            {
                new SubWindow("macroViewer", new TimeLineMacroViewer(){ Text = "�ޥ���ӥ塼��" }, DockState.DockBottom) { Text = "�ޥ���ӥ塼��" },
                new SubWindow("traceLogViewer", new TraceLogViewer(){ Text = "�ȥ졼�����ӥ塼��" }, DockState.DockRight) { Text = "�ȥ졼�����ӥ塼��" },
                new SubWindow("resourceExplorer", new ResourceExplorer(){ Text = "�꥽�����������ץ���" }, DockState.DockLeft) { Text = "�꥽�����������ץ���" },
                new SubWindow("visualizeRuleExplorer", new VisualizeRuleExplorer(){ Text = "�Ļ벽�롼�륨�����ץ���" }, DockState.DockLeft) { Text = "�Ļ벽�롼�륨�����ץ���" },
            };
			_windowManager.AddSubWindow(sws);
			_windowManager.Load();
			_windowManager.Show();
            _windowManager.SubWindowDockStateChanged += (o, e) => { _commandManager.Done(new ChangeSubWindowDockStateCommand(((SubWindow)o), e.Old, e.New)); };
            EventHandler<GeneralChangedEventArgs<bool>> v = (o, e) => { _commandManager.Done(new ChangeSubWindowVisiblityCommand(((SubWindow)o), e.New)); };
            _windowManager.SubWindowVisibleChanged += v;
            viewToolStripMenuItem.SetWindowManager(_windowManager);
            #endregion

            #region ��˥塼�С����٥������

            #region ɽ����˥塼
            showAllToolStripMenuItem.Click += (o, e) =>
            {
                // ��ɽ�����֤Υ�����ɥ���õ�����ޥ�ɤ���������
                var cswvc = from sw in sws
                         where !sw.Visible && sw.Enabled
                         select (ICommand)(new ChangeSubWindowVisiblityCommand(sw, true));
                if (cswvc.Count() != 0)
                {
                    // SubWindowVisibleChanged��̵���ˤ��Ƥ����ʤ���undo�����å��ˤ��٤ƤΥ�����ɥ���ɽ�����ޥ�ɤ��ɲä���Ƥ��ޤ�
                    _windowManager.SubWindowVisibleChanged -= v;
                    _commandManager.Do(new MacroCommand(cswvc) { Text="���٤ƤΥ�����ɥ���ɽ������"});
                    _windowManager.SubWindowVisibleChanged += v;
                }
            };
            hideAllToolStripMenuItem.Click += (o, e) =>
            {
                // ɽ�����֤Υ�����ɥ���õ�����ޥ�ɤ���������
                var cswvc = from sw in sws
                            where sw.Visible && sw.Enabled
                            select (ICommand)(new ChangeSubWindowVisiblityCommand(sw, false));
                if (cswvc.Count() != 0)
                {
                    // SubWindowVisibleChanged��̵���ˤ��Ƥ����ʤ���undo�����å��ˤ��٤ƤΥ�����ɥ�����ɽ�����ޥ�ɤ��ɲä���Ƥ��ޤ�
                    _windowManager.SubWindowVisibleChanged -= v;
                    _commandManager.Do(new MacroCommand(cswvc) { Text = "���٤ƤΥ�����ɥ�����ɽ���ˤ���" });
                    _windowManager.SubWindowVisibleChanged += v;
                }
            };
            #endregion

            #region �ե������˥塼

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

            #endregion

            #endregion

			#region ���ơ������С�����

			_statusManager = ApplicationFactory.StatusManager;
			_statusManager.StatusStrip = statusStrip;

			#endregion

			#region �ġ���С����٥������

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
					MessageBox.Show("���顼��ȯ�����ޤ�����\n����ǥ��쥯�ȥꡧ[" + ApplicationData.Setting.TemporaryDirectoryPath + "]\n������λ���ޤ���\n----\n" + _e.Message, "���顼��ȯ�����ޤ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

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
                    Text += "�����ȥ졼����";
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
