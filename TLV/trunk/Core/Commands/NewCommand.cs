using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Controls;

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
            if (f.ShowDialog() == DialogResult.OK)
            {
                CommonFormatConverter cfc = CommonFormatConverter.GetInstance(f.ConvertRuleDirPath);
                string res = "";
                string log = "";
                try
                {
                    res = cfc.ConvertResourceFile(f.ResourceFilePath);
                    log = cfc.ConvertTraceLogFile(f.TraceLogFilePath);
                }
                catch (ResourceFileValidationException e)
                {
                    MessageBox.Show(e.Message, "リソースファイルの共通形式への変換に失敗しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (TraceLogFileValidationException e)
                {
                    MessageBox.Show(e.Message, "トレースログファイルの共通形式への変換に失敗しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ApplicationDatas.ActiveFileContext.Data = new CommonFormatTraceLog(res, log);
                if (f.SaveFilePath != string.Empty)
                {
                    ApplicationDatas.ActiveFileContext.Path = f.SaveFilePath;
                    ApplicationDatas.ActiveFileContext.Save();
                }
            }
        }
    }
}
