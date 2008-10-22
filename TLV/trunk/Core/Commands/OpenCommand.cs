using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using System.ComponentModel;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class OpenCommand : AbstractFileChangeCommand
    {
        public OpenCommand()
        {
            Text = "共通形式トレースログファイルを開く";
        }

        protected override void action()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = Properties.Resources.CommonFormatTraceLogFileExtension;
            ofd.Filter = "Common Format TraceLog File (*." + ofd.DefaultExt + ")|*." + ofd.DefaultExt;

            BackGroundWorkForm bw = new BackGroundWorkForm() { Text = "共通形式トレースログファイルを展開中", CanCancel = false };
            
            bw.DoWork += (o, e) =>
                {
                    ApplicationDatas.ActiveFileContext.Open(ofd.FileName);
                    bw.ReportProgress(100);
                };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bw.RunWorkerAsync();
            }
        }

    }
}
