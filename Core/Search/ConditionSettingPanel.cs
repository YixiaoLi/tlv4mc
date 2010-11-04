using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    //詳細検索画面に張り付ける検索条件用のパネル
    //検索条件ごとにこのクラスを割り当てる
    class ConditionSettingPanel : Panel
    {
        private TraceLogVisualizerData _data = null;
        private BaseConditionComponents _baseConditionComponent = null;
        private List<RefiningConditionComponents> _refiningConditionComponents = null;
        private int _conditionNumber;
        public int conditionNumber
        { 
            set{_conditionNumber = value;}
            get{return _conditionNumber;}
        }

        private string _timeScale; //タイムラインの時間単位（s, ms, μsなど）

        public ConditionSettingPanel(TraceLogVisualizerData data, int conditionNumber, System.Drawing.Point formLocation, int formWidth)
        {
            _data = data;
            _conditionNumber = conditionNumber;
            this.Location = formLocation;
            this.Width = formWidth;
            this.AutoScroll = true;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Size = new System.Drawing.Size(formWidth,200);

            _baseConditionComponent = new BaseConditionComponents(data, _conditionNumber, this.Width);
            this.Controls.Add(_baseConditionComponent.getDisplayLabel());
            this.Controls.Add(_baseConditionComponent.getTargetResourceForm());
            this.Controls.Add(_baseConditionComponent.getTargetRuleForm());
            this.Controls.Add(_baseConditionComponent.getTargetEventForm());
            this.Controls.Add(_baseConditionComponent.getTargetEventDetailForm());
            _refiningConditionComponents = new List<RefiningConditionComponents>();
        }

        public void updatePanelSize(System.Drawing.Size parentPanelSize)
        {
            this.Size = new System.Drawing.Size(parentPanelSize.Width,200);
            _baseConditionComponent.updateComponentsSize(this.Size.Width); //パネルサイズ変更に伴う条件指定ボックスのサイズ変更
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
