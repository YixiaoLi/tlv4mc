using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core.Controls.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            //バージョン情報
            this.versionLabel.Text = "バージョン " + ApplicationData.Version;
            //AssemblyInfo.csで設定したAssemblyVersionを取得
            Assembly asm = Assembly.GetExecutingAssembly();
            Version ver = asm.GetName().Version;
            this.buildLabel.Text = "ビルド番号 " + ver;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void titleLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
