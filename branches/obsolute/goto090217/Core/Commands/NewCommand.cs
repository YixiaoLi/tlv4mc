using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class NewCommand : AbstractFileChangeCommand
	{
		private OpenResourceFileAndTraceLogFileOpenForm _fileOpenDialog = new OpenResourceFileAndTraceLogFileOpenForm() { StartPosition = FormStartPosition.CenterParent};
		private BackGroundWorkForm _convertBw = new BackGroundWorkForm() { Text = "共通形式トレースログへ変換中", StartPosition = FormStartPosition.CenterParent };
		private BackGroundWorkForm _setDataBw = new BackGroundWorkForm() { Text = "初期化中", ProgressBarText = "", Message = "データを設定中", Style = ProgressBarStyle.Marquee, CanCancel = false, StartPosition = FormStartPosition.CenterParent };
		private TraceLogVisualizerData _cftl = null;
		private string _resFilePath;
		private string _logFilePath;

		public NewCommand(string resFilePath, string logFilePath)
		{
			_resFilePath = resFilePath;
			_logFilePath = logFilePath;

			Text = "リソースファイルとトレースログファイルを開く";

			_setDataBw.DoWork += (_o, _e) =>
			{
				ApplicationData.FileContext.Close();
				ApplicationData.FileContext.Data = _cftl;
				if (_fileOpenDialog.SaveFilePath != string.Empty)
				{
					ApplicationData.FileContext.Path = _fileOpenDialog.SaveFilePath;
					ApplicationData.FileContext.Save();
				}
			};

			_convertBw.RunWorkerCompleted += (o, e) =>
			{
				if (!e.Cancelled)
				{
					_setDataBw.RunWorkerAsync();
				}
			};

			_convertBw.DoWork += (o, _e) =>
			{
				try
				{
					string[] visualizeRuleFilePaths = Directory.GetFiles(ApplicationData.Setting.VisualizeRulesDirectoryPath, "*." + Properties.Resources.VisualizeRuleFileExtension);

					StandartFormatConverter cfc = new StandartFormatConverter(
						_resFilePath,
						_logFilePath,
						visualizeRuleFilePaths,
						(p, s) =>
						{
							if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
							_convertBw.ReportProgress((int)((double)p * 0.8));
							_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = s; }));
						});

					if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
					_convertBw.ReportProgress(90);
					_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = "共通形式データを生成中"; }));

					_cftl = new TraceLogVisualizerData(cfc.ResourceData, cfc.TraceLogData, cfc.VisualizeData, cfc.SettingData);

					if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
					_convertBw.ReportProgress(100);
					_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = "完了"; }));
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "共通形式への変換に失敗しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
					_e.Cancel = true;
					return;
				}
			};
		}

        public NewCommand()
			:this(null, null)
        {


        }

        protected override void action()
        {
			if (_resFilePath == null || _logFilePath == null)
			{
				if (_resFilePath != null)
				{
					_fileOpenDialog.ResourceFilePath = _resFilePath;
				}
				if (_logFilePath != null)
				{
					_fileOpenDialog.TraceLogFilePath = _logFilePath;
				}
				if (_fileOpenDialog.ShowDialog() == DialogResult.OK)
				{
					_resFilePath = _fileOpenDialog.ResourceFilePath;
					_logFilePath = _fileOpenDialog.TraceLogFilePath;

					_convertBw.RunWorkerAsync();
				}
			}
			else
			{
				_convertBw.RunWorkerAsync();
			}
        }
    }
}
