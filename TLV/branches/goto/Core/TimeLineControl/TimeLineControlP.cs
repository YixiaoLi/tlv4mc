using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{

    public partial class TimeLineControlP : DockContent, IPresentation
    {
        public ToolStripContentPanel ContentPanel { get { return this.toolStripContainer.ContentPanel; } }

        public TimeLineControlP(string name)
        {
            InitializeComponent();

            this.Name = name;
            this.TabText = "タイムライン";
        }

        public void Add(IPresentation presentation)
        {
            this.toolStripContainer.ContentPanel.Controls.Add((Control)presentation);
        }

    }

}
