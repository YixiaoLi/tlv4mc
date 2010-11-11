using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Controls;


namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    //詳細検索画面に張り付ける検索条件のパネル
    class ConditionSettingPanel : Panel
    {
        private TraceLogVisualizerData _data = null;
        private System.Drawing.Size _parentFormSize;
        private BaseConditionPanel _baseConditionPanel = null;
        public BaseConditionPanel baseConditionPanel { set { _baseConditionPanel = value;} get{return _baseConditionPanel;}}
        private Button _searchBackwardButton = null;
        public Button SearchBackwardButton { set { _searchBackwardButton = value; } get { return _searchBackwardButton; } }
        private Button _searchForwardButton = null;
        public Button SearchForwardButton { set { _searchForwardButton = value; } get { return _searchForwardButton; } }
        private Button _searchWholeButton = null;
        public Button SearchWholeButton { set { _searchWholeButton = value; } get { return _searchWholeButton; } }
        
        private Button _addRefiningConditionButton = null;
        private RadioButton _andRadioButton = null;
        private RadioButton _orRadioButton = null;


        private List<RefiningConditionPanel> _refiningConditionPanels = null;
        private int _panelID = 0;
        private string _timeScale; //タイムラインの時間単位（s, ms, μsなど）
        private int _nextComponentLocationY = 0;

        public ConditionSettingPanel(TraceLogVisualizerData data, int panelID, System.Drawing.Size parentFormSize)
        {
            _data = data;
            _panelID = panelID;
            _parentFormSize = parentFormSize;
            _timeScale = _data.ResourceData.TimeScale;
            _refiningConditionPanels = new List<RefiningConditionPanel>();
            initializeCompoent();
        }

        private void initializeCompoent()
        {
            this.Width = _parentFormSize.Width;
            this.AutoScroll = true;
            this.HScroll = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new System.Drawing.Size(575, 300);
            setBaseConditionPanel();
            setSearchButton();
            setAddRefiningConditionButton();
            setConditionRelationRadioButton();
        }

        private void setBaseConditionPanel()
        {
            _baseConditionPanel = new BaseConditionPanel(_data, _panelID, this.Size);
            _baseConditionPanel.Location = new System.Drawing.Point(0, 0); //基本条件パネルは条件設定パネルの一番上へ配置
            _baseConditionPanel.SizeChanged += (o, e) =>
            {
                if (this.Width < _baseConditionPanel.Width)
                {
                    this.Width = _baseConditionPanel.Width;
                }
            };
            this.Controls.Add(_baseConditionPanel);
            _nextComponentLocationY = _baseConditionPanel.Height + 2;
        }

        private void setSearchButton()
        {
            _searchBackwardButton = new Button();
            _searchBackwardButton.Name = "searchBackwardButton:" + _panelID;
            _searchBackwardButton.Text = "後ろを検索";
            _searchBackwardButton.Width = 80;
            _searchBackwardButton.Location = new System.Drawing.Point(//
               _baseConditionPanel.DeleteButton.Location.X + _baseConditionPanel.DeleteButton.Width + 40, //
                                                             _baseConditionPanel.DeleteButton.Location.Y);
            _searchForwardButton = new Button();
            _searchForwardButton.Name = "searchForwardButton:" + _panelID;
            _searchForwardButton.Text = "次を検索";
            _searchForwardButton.Width = 80;
            _searchForwardButton.Location = new System.Drawing.Point(//
                                     _searchBackwardButton.Location.X + _searchBackwardButton.Width + 5, _searchBackwardButton.Location.Y);
            _searchWholeButton = new Button();
            _searchWholeButton.Name = "searchForwardButton:" + _panelID;
            _searchWholeButton.Text = "全体検索";
            _searchWholeButton.Width = 80;
            _searchWholeButton.Location = new System.Drawing.Point(//
                                     _searchForwardButton.Location.X + _searchForwardButton.Width + 5, _searchForwardButton.Location.Y);
            _baseConditionPanel.Controls.Add(_searchBackwardButton);
            _baseConditionPanel.Controls.Add(_searchForwardButton);
            _baseConditionPanel.Controls.Add(_searchWholeButton);
        }

        private void setAddRefiningConditionButton()
        {
            _addRefiningConditionButton = new Button();
            _addRefiningConditionButton.Name = "addRefiningConditionButton:" + _panelID;
            _addRefiningConditionButton.Text = "絞込み条件の追加";
            _addRefiningConditionButton.Location = new System.Drawing.Point(10, _nextComponentLocationY);
            _addRefiningConditionButton.Width = 120;
            _addRefiningConditionButton.Click += (o, _e) =>
            {
                addRefiningConditionPanel();
            };
            this.Controls.Add(_addRefiningConditionButton);
        }

        private void addRefiningConditionPanel()
        {
            RefiningConditionPanel refiningConditionPanel = new RefiningConditionPanel(_data,  _panelID, _refiningConditionPanels.Count + 1, this.Size, _timeScale);
            refiningConditionPanel.Location = new System.Drawing.Point(_baseConditionPanel.Location.X + 20, _nextComponentLocationY);
            refiningConditionPanel.DeleteButton.Click += (o, _e) =>
            {
                deleteRefiningCondition((int)refiningConditionPanel.getConditionID());
            };

            _refiningConditionPanels.Add(refiningConditionPanel);
            this.Controls.Add(refiningConditionPanel);
            _nextComponentLocationY += refiningConditionPanel.Height;

            if (_refiningConditionPanels.Count > 1)
            {
                this.Controls.Add(_andRadioButton);
                this.Controls.Add(_orRadioButton);
            }
        }

        private void setConditionRelationRadioButton()
        {
            _andRadioButton = new RadioButton();
            _andRadioButton.Name = "andRadioButton:" + _panelID;
            _andRadioButton.Text = "全ての条件に一致";
            _andRadioButton.Width = 120;
            _andRadioButton.Checked = true;
            _andRadioButton.Location = new System.Drawing.Point(_addRefiningConditionButton.Location.X + _addRefiningConditionButton.Width + 20, _addRefiningConditionButton.Location.Y);

            _orRadioButton = new RadioButton();
            _orRadioButton.Name = "andRadioButton:" + _panelID;
            _orRadioButton.Text = "いずれかの条件に一致";
            _orRadioButton.Width = 140;
            _orRadioButton.Checked = false;
            _orRadioButton.Location = new System.Drawing.Point(_andRadioButton.Location.X + _andRadioButton.Width + 5, _andRadioButton.Location.Y);

            _nextComponentLocationY += _orRadioButton.Height + 5;
        }

        private void deleteRefiningCondition(int conditionID)
        {
            _refiningConditionPanels.RemoveAt(conditionID-1);
            updatePanel();
        }

        public void setPanelID(int panelID)
        {
            _panelID = panelID;
            _baseConditionPanel.setParentPanelID(_panelID);
            foreach(RefiningConditionPanel panel in _refiningConditionPanels )
            {
                panel.setParentPanelID(_panelID);
            }
        }

        public int getPanelID()
        {
            return _panelID;
        }

        //すべてのコンポーネントをパネル上から消去し、再配置する
        private void updatePanel()
        {
            this.AutoScrollPosition = new System.Drawing.Point(0,0);
            this.Focus();
            this.Controls.Clear();
            _nextComponentLocationY = 0;
            int conditionID = 1;

            //基本条件と条件追加ボタンの追加
            this.Controls.Add(_baseConditionPanel);
            this.Controls.Add(_addRefiningConditionButton);
            _nextComponentLocationY = _addRefiningConditionButton.Location.Y + _addRefiningConditionButton.Height + 5;

            if (_refiningConditionPanels.Count > 1)
            {
                this.Controls.Add(_andRadioButton);
                this.Controls.Add(_orRadioButton);
            }


            //絞込み条件の追加
            foreach (RefiningConditionPanel panel in _refiningConditionPanels)
            {
                panel.Location = new System.Drawing.Point(this.Location.X + 20, _nextComponentLocationY);
                panel.setConditionID(conditionID);
                this.Controls.Add(panel);
                _nextComponentLocationY += panel.Height+1;
                conditionID++;
            }
        }

        public SearchCondition getBaseCondition()
        {
            return _baseConditionPanel.getSearchCondition();
        }

        public List<SearchCondition> getRefiningConditions()
        {
            List<SearchCondition> refiningConditions = new List<SearchCondition>();
            foreach (RefiningConditionPanel panel in _refiningConditionPanels)
            {
                refiningConditions.Add(panel.getSearchCondition());
            }
            return refiningConditions;
        }

        public Boolean isAnd()
        {
            return _andRadioButton.Checked;
        }
    }
}
