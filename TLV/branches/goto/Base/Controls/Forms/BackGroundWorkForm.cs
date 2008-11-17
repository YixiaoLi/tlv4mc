using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
    public partial class BackGroundWorkForm : Form
    {
        private BackgroundWorker _backgroundWorker;
        private string _text = "";
        private bool _canCancel = true;

        public event DoWorkEventHandler DoWork
        {
            add { _backgroundWorker.DoWork += value;  }
            remove { _backgroundWorker.DoWork -= value; }
        }
        public event ProgressChangedEventHandler ProgressChanged
        {
            add { _backgroundWorker.ProgressChanged += value; }
            remove { _backgroundWorker.ProgressChanged -= value; }
        }
        public event RunWorkerCompletedEventHandler RunWorkerCompleted
        {
            add { _backgroundWorker.RunWorkerCompleted += value; }
            remove { _backgroundWorker.RunWorkerCompleted -= value; }
        }
        public bool CancellationPending
        {
            get { return _backgroundWorker.CancellationPending; }
        }
        public bool CanCancel
        {
            get { return _canCancel; }
            set
            {
                _canCancel = value;
                cancelButton.Enabled = _canCancel;
            }
        }
		public string Message
		{
			get { return messageLabel.Text; }
			set { messageLabel.Text = value; }
		}

        public BackGroundWorkForm()
        {
            InitializeComponent();
            _backgroundWorker = new BackgroundWorker();
			_backgroundWorker.WorkerReportsProgress = true;
			_backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.ProgressChanged += (o, e) =>
                {
                    Text = _text + " (" + e.ProgressPercentage + " %)";
                    percentageLabel.Text = e.ProgressPercentage + " %";
                };
            cancelButton.Click += (o, e) =>
                {
                    _backgroundWorker.CancelAsync();
                };
        }

        public void ReportProgress(int percentage)
        {
            _backgroundWorker.ReportProgress(percentage);
            Invoke((MethodInvoker)(() =>
            {
                progressBar1.Value = percentage;
            }));
        }

        public void RunWorkerAsync()
        {
            _text = Text;
            ShowDialog();
        }

        protected override void OnShown(EventArgs _e)
        {
            _backgroundWorker.RunWorkerAsync();
            _backgroundWorker.RunWorkerCompleted += (o, e) =>
            {
                DialogResult = DialogResult.OK; 
                Close();
                Dispose();
            };
        }
    }
}
