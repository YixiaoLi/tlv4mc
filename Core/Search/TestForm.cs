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
        private List<ConditionSettingPanel> _conditionSettingPanels = null; //検索条件設定パネルのリスト
        private int _nextPanelID = 1;
        private int _nextPanelLocationY = 0;

        public TestForm(TraceLogVisualizerData data)
        {
            InitializeComponent();
            _data = data;
            _conditionSettingPanels = new List<ConditionSettingPanel>();
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

            this.FormClosing += (o, _e) =>
            {
                ApplicationFactory.BlackBoard.DetailSearchFlag = false;
            };
        }

        private void addConditionSettingPanel()
        {
            ConditionSettingPanel conditionSettingPanel = new ConditionSettingPanel(_data, this, _nextPanelID);
            conditionSettingPanel.Location = new System.Drawing.Point(0, _nextPanelLocationY);
            _conditionSettingPanels.Add(conditionSettingPanel);
            _nextPanelLocationY += conditionSettingPanel.Height + 5;
            _nextPanelID++;
            conditionSettingArea.Controls.Add(conditionSettingPanel);
        }

        public void deleteBaseCondition(int panelID)
        {
            _conditionSettingPanels.RemoveAt(panelID-1);
            updatePanel();
        }

        private void updatePanel() 
        {
            //一度 conditionSettingPanel 上から全てのパネルを消去し、その後あらためてパネル配置する
            conditionSettingArea.Controls.Clear();
            _nextPanelLocationY = 0;
            _nextPanelID = 1;
            foreach(ConditionSettingPanel panel in _conditionSettingPanels)
            {
                panel.Location = new System.Drawing.Point(conditionSettingArea.Location.X, _nextPanelLocationY);
                panel.setPanelID(_nextPanelID);
                conditionSettingArea.Controls.Add(panel);
                _nextPanelLocationY += panel.Height + 5;
                _nextPanelID++;
            }
        }
    }
}
