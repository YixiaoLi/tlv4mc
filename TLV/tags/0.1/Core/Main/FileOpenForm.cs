using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public partial class FileOpenForm : Form
    {
        public string ResourceFilePath { get; protected set; }
        public string TraceLogFilePath { get; protected set; }

        public FileOpenForm()
        {
            InitializeComponent();
            ResourceFilePath = string.Empty;
            TraceLogFilePath = string.Empty;
        }

        private void resourceRefButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "resource files (*.res)|*.res";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.resourceTextBox.Text = openFileDialog.FileName;
            }
        }

        private void traceLogRefButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "resource files (*.log)|*.log";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.traceLogTextBox.Text = openFileDialog.FileName;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (traceLogTextBox.Text != string.Empty && resourceTextBox.Text != string.Empty)
            {
                ResourceFilePath = resourceTextBox.Text;
                TraceLogFilePath = traceLogTextBox.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("ファイルを選択してください。");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
