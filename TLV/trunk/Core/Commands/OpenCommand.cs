﻿using System;
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
        private string _path = string.Empty;

        public OpenCommand():this(string.Empty)
        {

        }

        public OpenCommand(string path)
        {
            _path = path;
            Text = "共通形式トレースログファイルを開く";
        }

        protected override void action()
        {

            BackGroundWorkForm bw = new BackGroundWorkForm() { Text = "共通形式トレースログファイルを展開中", CanCancel = false };
            
            bw.DoWork += (o, e) =>
                {
                    ApplicationDatas.ActiveFileContext.Close();
                    try
                    {
                        ApplicationDatas.ActiveFileContext.Open(_path);
                    }
                    catch
                    {
                        MessageBox.Show("サポートされないファイル形式です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ApplicationDatas.ActiveFileContext.Close();
                    }
                    bw.ReportProgress(100);
                };


            if (_path == string.Empty)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = Properties.Resources.CommonFormatTraceLogFileExtension;
                ofd.Filter = "Common Format TraceLog File (*." + ofd.DefaultExt + ")|*." + ofd.DefaultExt;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _path = ofd.FileName;
                }
            }

            bw.RunWorkerAsync();
        }

    }
}
