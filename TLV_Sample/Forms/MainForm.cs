using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using TLV.Models;


namespace TLV.Forms
{
    public partial class TLVSample : Form
    {
        private const string DockConfigFile = "TLVDockPanel.config";

        public ResExplorer m_resExplorer;
        public ResProperty m_resProperty;
        private DeserializeDockContent m_deserializeDockContent;
        //private bool m_bSaveLayout = true;

        public TLVSample()
        {
            InitializeComponent();

            m_resExplorer = new ResExplorer();
            m_resProperty = new ResProperty();

            m_resExplorer.SetHandle = this.Handle;

            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

        }

        public void ChangePropty(Object tskInfo)
        {
            m_resProperty.InitPropty(tskInfo);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ResExplorer).ToString())
            {
                return m_resExplorer;
            }
            else
            //else if (persistString == typeof(ResProperty).ToString())
            {
                return m_resProperty;
            }
        }


        private void MainForm_Load(object sender, System.EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), DockConfigFile);

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "resource files (*.log)|*.log";
            openFileDialog.FilterIndex = 0;

            string openSrc = string.Empty;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //リソースファイル名取得
                openSrc = openFileDialog.FileName;

                //TreeView初期化
                m_resExplorer.InitTreeView(openSrc);
            }

        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            //終了
            this.Close();
        }

        private void tsBtnResExplorer_Click(object sender, EventArgs e)
        {
            if (!m_resExplorer.Visible)
            {
                m_resExplorer.Show(dockPanel);
            }
            else
            {
                m_resExplorer.Hide();
            }

        }

        private void tsBtnProperty_Click(object sender, EventArgs e)
        {
            if (!m_resProperty.Visible)
            {
                m_resProperty.Show(dockPanel);
            }
            else
            {
                m_resProperty.Hide();
            }

        }


    }
}

