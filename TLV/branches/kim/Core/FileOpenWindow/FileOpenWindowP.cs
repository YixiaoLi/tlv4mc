using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.FileOpenWindow
{
    public partial class FileOpenWindowP : WeifenLuo.WinFormsUI.Docking.DockContent, IPresentation
    {
        public FileOpenWindowP(string name)
        {
            InitializeComponent();

            this.Name = name;
        }

        public void AddChild(Control control, object args)
        {

        }

        public void getFilePath(out string resFilePath, out string logFilePath)
        {
            resFilePath = txtResourceFilePath.Text;
            logFilePath = txtLogFilePath.Text;            
        }
               

        private void btnOpenResource_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "resource files (*.log)|*.log";
            openFileDialog.FilterIndex = 0;


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //リソースファイル名取得
                txtResourceFilePath.Text = openFileDialog.FileName;

            }


        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "log files (*.log)|*.log";
            openFileDialog.FilterIndex = 0;

            string openSrc = string.Empty;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //リソースファイル名取得
                txtLogFilePath.Text = openFileDialog.FileName;

            }


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtResourceFilePath.Text != string.Empty && txtLogFilePath.Text != string.Empty)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("ファイルを選択してください。");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
