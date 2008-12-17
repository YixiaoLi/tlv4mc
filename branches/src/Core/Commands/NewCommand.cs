using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using System.ComponentModel;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class NewCommand : AbstractFileChangeCommand
    {

        public NewCommand()
        {
            Text = "リソースファイルとトレースログファイルを開く";
        }

        protected override void action()
        {
            var f = new OpenResourceFileAndTraceLogFileOpenForm();
            BackGroundWorkForm bw = new BackGroundWorkForm() { Text = "共通形式トレースログへ変換中" };

            TraceLogVisualizerData cftl = null;

            bw.RunWorkerCompleted += (o, e) =>
                {
                    if (!e.Cancelled)
                    {
                        ApplicationData.FileContext.Close();
                        ApplicationData.FileContext.Data = cftl;
                        if (f.SaveFilePath != string.Empty)
                        {
                            ApplicationData.FileContext.Path = f.SaveFilePath;
                            ApplicationData.FileContext.Save();
                        }
                    }
                };

            bw.DoWork += (o, _e) =>
			{
				try
				{
					string[] visualizeRuleFilePaths = Directory.GetFiles(ApplicationData.Setting.VisualizeRulesDirectoryPath, "*." + Properties.Resources.VisualizeRuleFileExtension);

					StandartFormatConverter cfc = new StandartFormatConverter(
						f.ResourceFilePath,
						f.TraceLogFilePath, 
						visualizeRuleFilePaths,
						(p,s) =>
						{
							if (bw.CancellationPending) { _e.Cancel = true; return; }
							bw.ReportProgress((int)((double)p * 0.8));
							bw.Invoke(new MethodInvoker(() => { bw.Message = s; }));
						});

					if (bw.CancellationPending) { _e.Cancel = true; return; }
					bw.ReportProgress(90);
					bw.Invoke(new MethodInvoker(() => { bw.Message = "共通形式データを生成中"; }));

					cftl = new TraceLogVisualizerData(cfc.ResourceData, cfc.TraceLogData, cfc.VisualizeData, cfc.SettingData);

					if (bw.CancellationPending) { _e.Cancel = true; return; }
					bw.ReportProgress(100);
					bw.Invoke(new MethodInvoker(() => { bw.Message = "完了"; }));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "共通形式への変換に失敗しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _e.Cancel = true;
					return;
                }
			};

            if (f.ShowDialog() == DialogResult.OK)
            {
                bw.RunWorkerAsync();
            }
        }
    }
}
