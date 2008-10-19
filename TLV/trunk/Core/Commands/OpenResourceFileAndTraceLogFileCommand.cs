using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class OpenResourceFileAndTraceLogFileCommand : ICommand
    {

        public string Text
        {
            get;
            set;
        }

        public bool CanUndo { get { return false; } }

        public void Do()
        {
            ApplicationFactory.CommandManager.Do(new FileChangeCommand(() =>
            {
                var f = new OpenResourceFileAndTraceLogFileOpenForm();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    CommonFormatConverter cfc = CommonFormatConverter.GetInstance(f.ConvertRuleFilePath);
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
                }
            }));
        }

        public void Undo()
        {
        }

        public OpenResourceFileAndTraceLogFileCommand()
        {
            Text = "リソースファイルとトレースログファイルを開く";
        }

    }
}
