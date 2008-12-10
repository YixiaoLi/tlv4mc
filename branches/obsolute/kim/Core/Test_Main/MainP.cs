using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Base;

using NU.OJL.MPRTOS.TLV.Core.FileOpenWindow;
using NU.OJL.MPRTOS.TLV.Core.ResourceExplorer;
using NU.OJL.MPRTOS.TLV.Core.ResourceProperty;
using NU.OJL.MPRTOS.TLV.Core.LogWindow;

namespace NU.OJL.MPRTOS.TLV.Core.Test_Main
{
    public partial class MainP : Form, IPresentation
    {
        private const string DockConfigFile = "TLVDockPanel.config";

        public ResourceExplorerP resExplorer;
        public ResourcePropertyP resProperty;

        public LogWindowP logWindow;

        private DeserializeDockContent deserializeDockContent;
        //private bool m_bSaveLayout = true;

        public MainP(string name)
        {
            InitializeComponent();

            this.Name = name;

            this.resExplorer = new ResourceExplorerP("ResourceExplorer");
            this.resProperty = new ResourcePropertyP("ResourceProperty");
            this.logWindow = new LogWindowP("LogWindow");

            this.resExplorer.SetHandle = this.Handle;

            this.deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

        }

        public void AddChild(Control control, object args)
        {
            //this.toolStripContainer.ContentPanel.Controls.Add(control);
        }


        public void ChangePropty(Object tskInfo)
        {
            this.resProperty.InitPropty(tskInfo);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ResourceExplorerP).ToString())
            {
                this.tsmiDisplayExploer.Checked = true;
                return this.resExplorer;
            }
            else if (persistString == typeof(ResourcePropertyP).ToString())
            {
                this.tsmiDesplayProperty.Checked = true;
                return this.resProperty;
            }
            //else if (persistString == typeof(LogDisplayP).ToString())
            else
            {
                this.tsmiLogWindow.Checked = true;
                return this.logWindow;
            }
        }


        private void MainForm_Load(object sender, System.EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), DockConfigFile);

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, this.deserializeDockContent);
            
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), DockConfigFile);
            //if (m_bSaveLayout)
            dockPanel.SaveAsXml(configFile);
            //else if (File.Exists(configFile))
            //    File.Delete(configFile);
        }


        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            FileOpenWindowP fileOpenWindow = new FileOpenWindowP("FileOpenWidow");
            string resFilePath = string.Empty;
            string logFilePath = string.Empty;

            if (fileOpenWindow.ShowDialog() == DialogResult.OK)
            {
                fileOpenWindow.getFilePath(out resFilePath, out logFilePath);

                this.resExplorer.InitTreeView(resFilePath);

                this.logWindow.InitLogWindow(logFilePath);
            }

        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            //終了
            this.Close();
        }

        private void tsBtnProperty_Click(object sender, EventArgs e)
        {
            if (!this.resProperty.Visible)
            {
                this.resProperty.Show(dockPanel);
            }
            else
            {
                this.resProperty.Hide();
            }

        }

        private void tsmiDisplayExploer_Click(object sender, EventArgs e)
        {
            if (!this.resExplorer.Visible)
            {
                this.tsmiDisplayExploer.Checked = true;
                this.resExplorer.Show(dockPanel);
            }
            else
            {
                this.tsmiDisplayExploer.Checked = false;
                this.resExplorer.Hide();
            }
        }

        private void tsmiDesplayProperty_Click(object sender, EventArgs e)
        {
            if (!this.resProperty.Visible)
            {
                this.tsmiDesplayProperty.Checked = true;
                this.resProperty.Show(dockPanel);
            }
            else
            {
                this.tsmiDesplayProperty.Checked = false;
                this.resProperty.Hide();
            }
        }

        private void tsmiLogWindow_Click(object sender, EventArgs e)
        {
            if (!this.logWindow.Visible)
            {
                this.tsmiLogWindow.Checked = true;
                this.logWindow.Show(dockPanel);
            }
            else
            {
                this.tsmiLogWindow.Checked = false;
                this.logWindow.Hide();
            }


        }

    }
}

