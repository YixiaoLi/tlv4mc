using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class RefiningConditionPanel : BaseConditionPanel
    {
        private int _conditionID;
        private TextBox _timingValueBox;
        private ComboBox _timingForm;
        private Label _timingExpressionLabel;
        private Label _timeScaleLabel;
        private int _refiningConditionPanelID;


        public RefiningConditionPanel(TraceLogVisualizerData data, int parentPanelID, int conditionID, System.Drawing.Size parentPanelSize, string timeScale)
        {
            _data = data;
            _conditionID = conditionID;
            _parentPanelID = parentPanelID;
            _parentPanelSize = parentPanelSize;
            _refiningConditionPanelID = conditionID;
            _searchCondition = new SearchCondition();
            _timeScaleLabel = new Label();
            _timeScaleLabel.Text = timeScale;
            initializeComponents();
        }

        private void initializeComponents()
        {
            _displayLabel = new Label();
            _displayLabel.Name = "displayLabel:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _displayLabel.Font = new System.Drawing.Font("Century", 10, System.Drawing.FontStyle.Underline);
            _displayLabel.Text = "絞込み条件" + _refiningConditionPanelID;
            _targetResourceForm = new ComboBox();
            _targetResourceForm.Name = "refiningResourceForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _targetResourceForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetRuleForm = new ComboBox();
            _targetRuleForm.Name = "refiningRuleForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _targetRuleForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetEventForm = new ComboBox();
            _targetEventForm.Name = "refiningEventForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _targetEventForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetEventDetailForm = new ComboBox();
            _targetEventDetailForm.Name = "refiningEventDetailForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _targetEventDetailForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _timingValueBox = new TextBox();
            _timingValueBox.Name = "timingValueTextBox:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _timingForm = new ComboBox();
            _timingForm.Name = "timingForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _timingForm.DropDownStyle = ComboBoxStyle.DropDownList;
            makeTimingForm();
            _timingExpressionLabel = new Label();
            _timingExpressionLabel.Name = "timingExpressionLabel:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _timingExpressionLabel.Text = "基本条件のイベント発生時刻に対して";
            _timingExpressionLabel.Width = 180;
            _timeScaleLabel.Name = "timeScaleLabel:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _timeScaleLabel.Width = 20;
            _deleteButton = new Button();
            _deleteButton.Name = "refiningConditionDeleteButton" + _parentPanelID + "_" + _refiningConditionPanelID;
            _deleteButton.Text = "削除";
            _deleteButton.Size = new System.Drawing.Size(37, 25);

            _targetResourceForm.Enabled = false;
            _targetRuleForm.Enabled = false;
            _targetEventForm.Enabled = false;
            _targetEventDetailForm.Enabled = false;
            _timingValueBox.Enabled = false;
            _timingForm.Enabled = false;

            arrangeComboBoxSize(_parentPanelSize.Width);
            arrangeLocations();
            makeResourceForm();
            this.setEventHandler();

            this.Controls.Add(_displayLabel);
            this.Controls.Add(_targetResourceForm);
            this.Controls.Add(_targetRuleForm);
            this.Controls.Add(_targetEventForm);
            this.Controls.Add(_targetEventDetailForm);
            this.Controls.Add(_deleteButton);
            this.Controls.Add(_timingExpressionLabel);
            this.Controls.Add(_timingValueBox);
            this.Controls.Add(_timeScaleLabel);
            this.Controls.Add(_timingForm);
            this.Size = new System.Drawing.Size(_parentPanelSize.Width - 25, _timingExpressionLabel.Location.Y + _timingExpressionLabel.Height + 1);
        }

        protected void setEventHandler()
        {
            base.setEventHandler();
            _targetResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                _timingValueBox.Enabled = true;
                _timingForm.Enabled = true;
            };

            _timingForm.SelectedIndexChanged += (o, _e) =>
            {
                _timingForm.Width = getComponentLength(_timingForm.Font, (string)_timingForm.SelectedItem);
                _searchCondition.timing = (string)_timingForm.SelectedItem;
                arrangeLocations();
                changePanelSize(_timingForm.Location.X + _timingForm.Width);
            };

            _timingValueBox.TextChanged += (o, _e) =>
            {
                _searchCondition.timingValue = _timingValueBox.Text;
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
            base.arrangeLocations();
            _timingExpressionLabel.Location = new System.Drawing.Point(_targetResourceForm.Location.X, _targetResourceForm.Location.Y + _targetResourceForm.Height + 5);
            _timingValueBox.Location = new System.Drawing.Point(_timingExpressionLabel.Location.X + _timingExpressionLabel.Width + 5, _timingExpressionLabel.Location.Y);
            _timeScaleLabel.Location = new System.Drawing.Point(_timingValueBox.Location.X + _timingValueBox.Width + 5, _timingValueBox.Location.Y);
            _timingForm.Location = new System.Drawing.Point(_timeScaleLabel.Location.X + _timeScaleLabel.Width + 5, _timeScaleLabel.Location.Y);
        }

        public void updateSize(int parentPanelWidth)
        {
            arrangeComboBoxSize(parentPanelWidth);
            arrangeLocations();
        }

        public void setParentPanelID(int ID)
        {
            _refiningConditionPanelID = ID;
            setComponentName(ID);
        }

        public void setConditionID(int ID)
        {
            _conditionID = ID;
            setComponentName(ID);
        }

        private void setComponentName(int ID)
        {
            _displayLabel.Name = "displayLabel:" + _parentPanelID + _refiningConditionPanelID;
            _displayLabel.Text = "絞込み条件:" + _refiningConditionPanelID;
            _targetResourceForm.Name = "refiningResourceForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _targetRuleForm.Name = "refiningRuleForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _targetEventForm.Name = "refiningEventForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _targetEventDetailForm.Name = "refiningEventDetailForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _timingValueBox.Name = "timingValueTextBox:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _timingForm.Name = "timingForm:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _timingExpressionLabel.Name = "timingExpressionLabel:" + _parentPanelID + "_" + _refiningConditionPanelID;
            _deleteButton.Name = "refiningConditionDeleteButton" + _parentPanelID + "_" + _refiningConditionPanelID;
        }

        public int getConditionID()
        {
            return _conditionID;
        }
    }
}
