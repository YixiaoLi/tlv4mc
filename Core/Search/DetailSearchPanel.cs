using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    public partial class DetailSearchPanel : Form
    {


        public DetailSearchPanel()
        {
            InitializeComponent();
            //this.Visible = true;
            ruleName.Enabled = false;
            eventName.Enabled = false;
            eventDetail.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.FormClosed += (o, _e) =>
            {
                ApplicationFactory.BlackBoard.DetailSearchFlag = 0;
            };

            resourceName.SelectedIndexChanged += (o, _e) =>
            {
                ruleName.Enabled = true;
                eventName.Enabled = false;
                eventDetail.Enabled = false;
            };

            ruleName.SelectedIndexChanged += (o, _e) =>
            {
                eventName.Enabled = true;
                eventDetail.Enabled = false;
            };

            eventName.SelectedIndexChanged += (o, _e) =>
            {
                eventDetail.Enabled = true;
                addConditionButton.Enabled = true;
            };
        }
    }
}
