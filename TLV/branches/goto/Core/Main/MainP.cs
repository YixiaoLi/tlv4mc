using System;
using System.ComponentModel;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public partial class MainP : Form, IPresentation
    {
        public string resourceFilePath = String.Empty;
        public string traceLogFilePath = String.Empty;
        public string ResourceFilePath
        {
            get { return resourceFilePath; }
            set
            {
                if (value != resourceFilePath)
                {
                    resourceFilePath = value;
                    NotifyPropertyChanged("ResourceFilePath");
                }
            }
        }
        public string TraceLogFilePath
        {
            get { return traceLogFilePath; }
            set
            {
                if (value != traceLogFilePath)
                {
                    traceLogFilePath = value;
                    NotifyPropertyChanged("TraceLogFilePath");
                }
            }
        }
        public ToolStripContentPanel MainContentPanel
        {
            get { return this.toolStripContainer.ContentPanel; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainP(string name)
        {
            InitializeComponent();

            this.Name = name;

            openToolStripMenuItem.Click += new EventHandler(openToolStripMenuItemClick);

            MainContentPanel.Hide();
        }

        public void Add(IPresentation presentation)
        {
            this.toolStripContainer.ContentPanel.Controls.Add((Control)presentation);
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected void openToolStripMenuItemClick(object sender, EventArgs e)
        {
            FileOpenForm fileOpenForm = new FileOpenForm();
            if (fileOpenForm.ShowDialog() == DialogResult.OK)
            {
                ResourceFilePath = fileOpenForm.ResourceFilePath;
                TraceLogFilePath = fileOpenForm.TraceLogFilePath;
            }
        }

    }

}
