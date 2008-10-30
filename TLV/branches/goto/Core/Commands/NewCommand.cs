using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
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

            CommonFormatTraceLog cftl = null;

            bw.RunWorkerCompleted += (o, e) =>
                {
                    if (!e.Cancelled)
                    {
                        ApplicationDatas.ActiveFileContext.Close();
                        ApplicationDatas.ActiveFileContext.Data = cftl;
                        if (f.SaveFilePath != string.Empty)
                        {
                            ApplicationDatas.ActiveFileContext.Path = f.SaveFilePath;
                            ApplicationDatas.ActiveFileContext.Save();
                        }
                    }
                };

            bw.DoWork += (o, _e) =>
			{
				try
				{
					bw.ReportProgress(0);
                    if (bw.CancellationPending) { _e.Cancel = true; return; }

					CommonFormatConverter cfc = new CommonFormatConverter(f.ResourceFilePath, f.TraceLogFilePath);

                    bw.ReportProgress(25);
                    if (bw.CancellationPending) { _e.Cancel = true; return; }

					ResourceData res = cfc.ResourceData;

                    bw.ReportProgress(50);
                    if (bw.CancellationPending) { _e.Cancel = true; return; }

					TraceLogList log = cfc.TraceLogList;

                    bw.ReportProgress(75);
					if (bw.CancellationPending) { _e.Cancel = true; return; }

					cftl = new CommonFormatTraceLog(res, log);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "共通形式への変換に失敗しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _e.Cancel = true;
					return;
                }
                bw.ReportProgress(100);
			};

            if (f.ShowDialog() == DialogResult.OK)
            {
                bw.RunWorkerAsync();
            }
        }
    }
}
