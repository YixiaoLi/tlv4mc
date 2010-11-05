using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    //詳細検索画面に張り付ける検索条件用のパネル
    class ConditionSettingPanel : Panel
    {
        private TraceLogVisualizerData _data = null;
        private TestForm _parentForm = null;
        private BaseConditionComponents _baseConditionComponent = null;
        private List<RefiningConditionComponents> _refiningConditionComponents = null;
        private int _panelID = 0;
        private string _timeScale; //タイムラインの時間単位（s, ms, μsなど）

        public ConditionSettingPanel(TraceLogVisualizerData data, TestForm parentForm, int panelID)
        {
            _data = data;
            _parentForm = parentForm;
            _panelID = panelID;
            this.Location = _parentForm.Location;
            this.Width = _parentForm.Width;
            this.AutoScroll = true;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Size = new System.Drawing.Size(this.Width,200);

            _baseConditionComponent = new BaseConditionComponents(_data, this);
            this.Controls.Add(_baseConditionComponent.getDisplayLabel());
            this.Controls.Add(_baseConditionComponent.getTargetResourceForm());
            this.Controls.Add(_baseConditionComponent.getTargetRuleForm());
            this.Controls.Add(_baseConditionComponent.getTargetEventForm());
            this.Controls.Add(_baseConditionComponent.getTargetEventDetailForm());
            this.Controls.Add(_baseConditionComponent.getDeleteButton());
            _refiningConditionComponents = new List<RefiningConditionComponents>();
        }

        public void updatePanelSize(System.Drawing.Size parentPanelSize)
        {
            this.Size = new System.Drawing.Size(parentPanelSize.Width,200);
            _baseConditionComponent.updateComponentsSize(this.Size.Width); //パネルサイズ変更に伴う条件指定ボックスのサイズ変更
        }

        public void deleteBaseCondition()
        {
            _parentForm.deleteBaseCondition(_panelID);
        }

        public void deleteRefiningCondition()
        {
        }

        public void setPanelID(int panelID)
        {
            _panelID = panelID;
            _baseConditionComponent.changeComponentID(_panelID);
        }

        public int getPanelID()
        {
            return _panelID;
        }

        private void updateComponents()
        {
        }

        private void updateSize()
        {
        }

        private void updateLocation()
        {
        }
    }
}
