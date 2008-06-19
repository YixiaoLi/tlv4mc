using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using NagoyaUniv.OjlMpRtos.TraceLogVisualizer.Models;

namespace NagoyaUniv.OjlMpRtos.TraceLogVisualizer.Forms
{
    public partial class MainForm : Form
    {
        private ApplicationModel appModel;

        public MainForm()
        {
            InitializeComponent();
            appModel = new ApplicationModel();
        }

    }
}
