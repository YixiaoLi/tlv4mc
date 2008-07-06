using System;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Core.Properties;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{

    public partial class TimeLineControlP : DockContent, IPresentation
    {
        private RowSizeMode rowSizeMode;

        public event PropertyChangedEventHandler PropertyChanged;

        public ToolStripContentPanel ContentPanel { get { return this.toolStripContainer.ContentPanel; } }
        public RowSizeMode RowSizeMode
        {
            get { return rowSizeMode; }
            set
            {
                if(rowSizeMode != value)
                {
                    rowSizeMode = value;
                    fillFixRowButtonIconChange();
                    NotifyPropertyChanged("RowSizeMode");
                }
            }
        }

        public TimeLineControlP(string name)
        {
            InitializeComponent();

            this.Name = name;
            this.TabText = "タイムライン";
            this.RowSizeMode = RowSizeMode.Fill;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
        }

        public void Add(IPresentation presentation)
        {
            this.ContentPanel.Controls.Add((Control)presentation);
        }

        private void fillFixRowButtonClick(object sender, EventArgs e)
        {
            if (RowSizeMode == RowSizeMode.Fill)
            {
                RowSizeMode = RowSizeMode.Fix;
            }
            else
            {
                RowSizeMode = RowSizeMode.Fill;
            }
        }

        private void fillFixRowButtonIconChange()
        {
            if (RowSizeMode == RowSizeMode.Fill)
            {
                this.fillFixRowButton.Image = Resources.fixRowButton;
                this.fillFixRowButton.Text = "行サイズを固定にする";
                this.fillFixRowButton.ToolTipText = this.fillFixRowButton.Text;
            }
            else
            {
                this.fillFixRowButton.Image = Resources.fillRowButton;
                this.fillFixRowButton.Text = "行サイズを可変にする";
                this.fillFixRowButton.ToolTipText = this.fillFixRowButton.Text;
            }
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }

}
