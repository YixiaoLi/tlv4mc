using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class RefiningConditionPanel : BaseConditionPanel
    {
        private int _refiningConditionID;
        private System.Drawing.Size _baseConditionPanelSize;
        private TextBox _timingValueBox;
        private ComboBox _timingForm;
        private Label _timingExpressionLabel;
        private Label _timeScaleLabel;
        private CheckBox _denyConditionBox;

        public RefiningConditionPanel(TraceLogVisualizerData data, int baseConditionID, int conditionID, System.Drawing.Size baseConditionPanelSize, string timeScale)
        {
            _data = data;
            _refiningConditionID = conditionID;
            _baseConditionID = baseConditionID;
            _baseConditionPanelSize = baseConditionPanelSize;
            _refiningConditionID = conditionID;
            _searchCondition = new SearchCondition();
            _timeScaleLabel = new Label();
            _timeScaleLabel.Text = timeScale;
            initializeComponents();
        }

        private void initializeComponents()
        {
            _displayLabel = new Label();
            _displayLabel.Name = "displayLabel:" + _baseConditionID + "_" + _refiningConditionID;
            _displayLabel.Font = new System.Drawing.Font("Century", 10, System.Drawing.FontStyle.Underline);
            _displayLabel.Text = "絞込み条件" + _refiningConditionID;
            _targetResourceForm = new ComboBox();
            _targetResourceForm.Name = "refiningResourceForm:" + _baseConditionID + "_" + _refiningConditionID;
            _targetResourceForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetRuleForm = new ComboBox();
            _targetRuleForm.Name = "refiningRuleForm:" + _baseConditionID + "_" + _refiningConditionID;
            _targetRuleForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetEventForm = new ComboBox();
            _targetEventForm.Name = "refiningEventForm:" + _baseConditionID + "_" + _refiningConditionID;
            _targetEventForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetEventDetailForm = new ComboBox();
            _targetEventDetailForm.Name = "refiningEventDetailForm:" + _baseConditionID + "_" + _refiningConditionID;
            _targetEventDetailForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _timingValueBox = new TextBox();
            _timingValueBox.Name = "timingValueTextBox:" + _baseConditionID + "_" + _refiningConditionID;
            _timingForm = new ComboBox();
            _timingForm.Name = "timingForm:" + _baseConditionID + "_" + _refiningConditionID;
            _timingForm.DropDownStyle = ComboBoxStyle.DropDownList;
            makeTimingForm();
            _timingExpressionLabel = new Label();
            _timingExpressionLabel.Name = "timingExpressionLabel:" + _baseConditionID + "_" + _refiningConditionID;
            _timingExpressionLabel.Text = "基本条件のイベント発生時刻に対して";
            _timingExpressionLabel.Width = 180;
            _timeScaleLabel.Name = "timeScaleLabel:" + _baseConditionID + "_" + _refiningConditionID;
            _timeScaleLabel.Width = 20;
            _denyConditionBox = new CheckBox();
            _denyConditionBox.Name = "denyConditionBox:" + _baseConditionID;
            _denyConditionBox.Text = "条件を否定";
            _denyConditionBox.Width = 100;
            _deleteButton = new Button();
            _deleteButton.Name = "refiningConditionDeleteButton" + _baseConditionID + "_" + _refiningConditionID;
            _deleteButton.Text = "削除";
            _deleteButton.Size = new System.Drawing.Size(37, 25);

            _targetResourceForm.Enabled = false;
            _targetRuleForm.Enabled = false;
            _targetEventForm.Enabled = false;
            _targetEventDetailForm.Enabled = false;
            _timingValueBox.Enabled = false;
            _timingForm.Enabled = false;
            _denyConditionBox.Checked = false;

            arrangeComboBoxSize(_baseConditionPanelSize.Width);
            this.arrangeLocations();
            makeResourceForm();
            this.setEventHandler();

            this.Controls.Add(_displayLabel);
            this.Controls.Add(_targetResourceForm);
            this.Controls.Add(_targetRuleForm);
            this.Controls.Add(_targetEventForm);
            this.Controls.Add(_targetEventDetailForm);
            this.Controls.Add(_denyConditionBox);
            this.Controls.Add(_deleteButton);
            this.Controls.Add(_timingExpressionLabel);
            this.Controls.Add(_timingValueBox);
            this.Controls.Add(_timeScaleLabel);
            this.Controls.Add(_timingForm);
            this.Size = new System.Drawing.Size(_baseConditionPanelSize.Width - 25, _timingExpressionLabel.Location.Y + _timingExpressionLabel.Height + 1);
        }

        protected void setEventHandler()
        {
            base.setEventHandler(); //BaseConditionPanelで定義済みの条件指定ボックスにイベントハンドラを追加する
            _targetResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                if (_timingForm.Enabled == true)
                {
                    _searchCondition.timing = null;
                    _timingForm.SelectedIndex = -1;
                    _timingValueBox.Text = "";
                }
                else
                {
                    _timingValueBox.Enabled = true;
                    _timingForm.Enabled = true;
                }
            };

            _timingForm.SelectedIndexChanged += (o, _e) =>
            {
                _timingForm.Width = getComponentLength(_timingForm.Font, (string)_timingForm.SelectedItem);
                _searchCondition.timing = (string)_timingForm.SelectedItem;
                this.arrangeLocations();
                changePanelSize(_timingForm.Location.X + _timingForm.Width);
            };

            _timingValueBox.TextChanged += (o, _e) =>
            {
                _searchCondition.timingValue = _timingValueBox.Text;
            };

            _denyConditionBox.CheckedChanged += (o, _e) =>
            {
                _searchCondition.denyCondition = _denyConditionBox.Checked;
            };
        }

        private void makeTimingForm()
        {
            _timingForm.Items.Add("以内に発生(基準時以前)");
            _timingForm.Items.Add("以内に発生(基準時以後)");
            _timingForm.Items.Add("以上前に発生");
            _timingForm.Items.Add("以上後に発生");
            arrangeDropDownSize(_timingForm);
        }

        private void arrangeComboBoxSize(int width)
        {
            int boxWidth = width / 6; //初期サイズを width / 6 に固定
            _targetResourceForm.Width = boxWidth;
            _targetRuleForm.Width = boxWidth;
            _targetEventForm.Width = boxWidth;
            _targetEventDetailForm.Width = boxWidth;
            _timingValueBox.Width = boxWidth;
            _timingForm.Width = boxWidth;
        }

        private void arrangeLocations()
        {
            _displayLabel.Location = new System.Drawing.Point(10, 10);
            _targetResourceForm.Location = new System.Drawing.Point(_displayLabel.Location.X, _displayLabel.Location.Y + _displayLabel.Height + 1);
            _targetRuleForm.Location = new System.Drawing.Point(_targetResourceForm.Location.X + _targetResourceForm.Width + 5, _targetResourceForm.Location.Y);
            _targetEventForm.Location = new System.Drawing.Point(_targetRuleForm.Location.X + _targetRuleForm.Width + 5, _targetRuleForm.Location.Y);
            _targetEventDetailForm.Location = new System.Drawing.Point(_targetEventForm.Location.X + _targetEventForm.Width + 5, _targetEventForm.Location.Y);
            _denyConditionBox.Location = new System.Drawing.Point(_displayLabel.Location.X + _displayLabel.Width + 5, _displayLabel.Location.Y);
            _deleteButton.Location = new System.Drawing.Point(_denyConditionBox.Location.X + _denyConditionBox.Width + 10, _denyConditionBox.Location.Y - 2);
            _timingExpressionLabel.Location = new System.Drawing.Point(_targetResourceForm.Location.X, _targetResourceForm.Location.Y + _targetResourceForm.Height + 5);
            _timingValueBox.Location = new System.Drawing.Point(_timingExpressionLabel.Location.X + _timingExpressionLabel.Width + 5, _timingExpressionLabel.Location.Y);
            _timeScaleLabel.Location = new System.Drawing.Point(_timingValueBox.Location.X + _timingValueBox.Width + 5, _timingValueBox.Location.Y);
            _timingForm.Location = new System.Drawing.Point(_timeScaleLabel.Location.X + _timeScaleLabel.Width + 5, _timeScaleLabel.Location.Y);
        }

        public void setBaseConditionID(int ID)
        {
            _baseConditionID = ID;
            setComponentName();
        }

        public void setConditionID(int ID)
        {
            _refiningConditionID = ID;
            setComponentName();
        }

        private void setComponentName()
        {
            _displayLabel.Name = "displayLabel:" + _baseConditionID + _refiningConditionID;
            _displayLabel.Text = "絞込み条件:" + _refiningConditionID;
            _targetResourceForm.Name = "refiningResourceForm:" + _baseConditionID + "_" + _refiningConditionID;
            _targetRuleForm.Name = "refiningRuleForm:" + _baseConditionID + "_" + _refiningConditionID;
            _targetEventForm.Name = "refiningEventForm:" + _baseConditionID + "_" + _refiningConditionID;
            _targetEventDetailForm.Name = "refiningEventDetailForm:" + _baseConditionID + "_" + _refiningConditionID;
            _timingValueBox.Name = "timingValueTextBox:" + _baseConditionID + "_" + _refiningConditionID;
            _timingForm.Name = "timingForm:" + _baseConditionID + "_" + _refiningConditionID;
            _timingExpressionLabel.Name = "timingExpressionLabel:" + _baseConditionID + "_" + _refiningConditionID;
            _deleteButton.Name = "refiningConditionDeleteButton" + _baseConditionID + "_" + _refiningConditionID;
        }

        public int getConditionID()
        {
            return _refiningConditionID;
        }
    }
}
