using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    public partial class TestForm : Form
    {
        private TraceLogVisualizerData _data=null;
        private List<ConditionSettingPanel> _conditionSettingPanels; //検索条件設定パネルのリスト
        private int _nextPanelID = 0;
        private int _nextPanelLocationY;

        public TestForm(TraceLogVisualizerData data)
        {
            InitializeComponent();
            _data = data;
            _nextPanelID = 1;
            _conditionSettingPanels = new List<ConditionSettingPanel>();
            _nextPanelLocationY = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            addConditionButton.Click += (o, _e) =>
            {
                addConditionSettingPanel();
            };

            this.SizeChanged += (o, _e) =>
            {
                foreach(ConditionSettingPanel panel in _conditionSettingPanels)
                {
                    panel.updatePanelSize(conditionSettingArea.Size);
                }
            };
        }

        private void addConditionSettingPanel()
        {
            ConditionSettingPanel conditionSettingPanel = new ConditionSettingPanel(_data, _nextPanelID, new System.Drawing.Point(0, _nextPanelLocationY), conditionSettingArea.Width);
            
            _conditionSettingPanels.Add(conditionSettingPanel);
            conditionSettingArea.Controls.Add(conditionSettingPanel);
            _nextPanelID++;
            _nextPanelLocationY += conditionSettingPanel.Height + 5;
        }
    }
}
