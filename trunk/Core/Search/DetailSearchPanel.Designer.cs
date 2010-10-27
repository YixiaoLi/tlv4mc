﻿namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    partial class DetailSearchPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConditionOpratior = new System.Windows.Forms.Panel();
            this.goEnd = new System.Windows.Forms.Button();
            this.goStart = new System.Windows.Forms.Button();
            this.denialCheckBox = new System.Windows.Forms.CheckBox();
            this.targetConditionLabel = new System.Windows.Forms.Label();
            this.targetConditionForm = new System.Windows.Forms.ComboBox();
            this.searchWholeButton = new System.Windows.Forms.Button();
            this.searchForwardButton = new System.Windows.Forms.Button();
            this.searchBackwardButton = new System.Windows.Forms.Button();
            this.timeValueLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addRefiningConditionButton = new System.Windows.Forms.Button();
            this.timingValueForm = new System.Windows.Forms.ComboBox();
            this.timingForm = new System.Windows.Forms.ComboBox();
            this.timingSetting = new System.Windows.Forms.Label();
            this.refiningConditionEventDetailForm = new System.Windows.Forms.ComboBox();
            this.refiningConditionEventForm = new System.Windows.Forms.ComboBox();
            this.refiningConditionRuleForm = new System.Windows.Forms.ComboBox();
            this.refiningConditionResourceForm = new System.Windows.Forms.ComboBox();
            this.refiningConditionSettingLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mainConditionSettingLabel = new System.Windows.Forms.Label();
            this.addMainConditionButton = new System.Windows.Forms.Button();
            this.mainEventDetailForm = new System.Windows.Forms.ComboBox();
            this.mainEventForm = new System.Windows.Forms.ComboBox();
            this.mainRuleForm = new System.Windows.Forms.ComboBox();
            this.mainResourceForm = new System.Windows.Forms.ComboBox();
            this.ConditionDisplayPanel = new System.Windows.Forms.Panel();
            this.ConditionOpratior.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConditionOpratior
            // 
            this.ConditionOpratior.BackColor = System.Drawing.Color.SeaShell;
            this.ConditionOpratior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ConditionOpratior.Controls.Add(this.goEnd);
            this.ConditionOpratior.Controls.Add(this.goStart);
            this.ConditionOpratior.Controls.Add(this.denialCheckBox);
            this.ConditionOpratior.Controls.Add(this.targetConditionLabel);
            this.ConditionOpratior.Controls.Add(this.targetConditionForm);
            this.ConditionOpratior.Controls.Add(this.searchWholeButton);
            this.ConditionOpratior.Controls.Add(this.searchForwardButton);
            this.ConditionOpratior.Controls.Add(this.searchBackwardButton);
            this.ConditionOpratior.Controls.Add(this.timeValueLabel);
            this.ConditionOpratior.Controls.Add(this.label2);
            this.ConditionOpratior.Controls.Add(this.addRefiningConditionButton);
            this.ConditionOpratior.Controls.Add(this.timingValueForm);
            this.ConditionOpratior.Controls.Add(this.timingForm);
            this.ConditionOpratior.Controls.Add(this.timingSetting);
            this.ConditionOpratior.Controls.Add(this.refiningConditionEventDetailForm);
            this.ConditionOpratior.Controls.Add(this.refiningConditionEventForm);
            this.ConditionOpratior.Controls.Add(this.refiningConditionRuleForm);
            this.ConditionOpratior.Controls.Add(this.refiningConditionResourceForm);
            this.ConditionOpratior.Controls.Add(this.refiningConditionSettingLabel);
            this.ConditionOpratior.Controls.Add(this.label1);
            this.ConditionOpratior.Controls.Add(this.mainConditionSettingLabel);
            this.ConditionOpratior.Controls.Add(this.addMainConditionButton);
            this.ConditionOpratior.Controls.Add(this.mainEventDetailForm);
            this.ConditionOpratior.Controls.Add(this.mainEventForm);
            this.ConditionOpratior.Controls.Add(this.mainRuleForm);
            this.ConditionOpratior.Controls.Add(this.mainResourceForm);
            this.ConditionOpratior.Dock = System.Windows.Forms.DockStyle.Top;
            this.ConditionOpratior.Location = new System.Drawing.Point(0, 0);
            this.ConditionOpratior.Name = "ConditionOpratior";
            this.ConditionOpratior.Size = new System.Drawing.Size(604, 219);
            this.ConditionOpratior.TabIndex = 0;
            // 
            // goEnd
            // 
            this.goEnd.Location = new System.Drawing.Point(488, 91);
            this.goEnd.Name = "goEnd";
            this.goEnd.Size = new System.Drawing.Size(100, 23);
            this.goEnd.TabIndex = 25;
            this.goEnd.Text = "カーソルをEへ";
            this.goEnd.UseVisualStyleBackColor = true;
            this.goEnd.Click += new System.EventHandler(this.goEnd_Click);
            // 
            // goStart
            // 
            this.goStart.Location = new System.Drawing.Point(488, 66);
            this.goStart.Name = "goStart";
            this.goStart.Size = new System.Drawing.Size(102, 23);
            this.goStart.TabIndex = 24;
            this.goStart.Text = "カーソルをSへ";
            this.goStart.UseVisualStyleBackColor = true;
            this.goStart.Click += new System.EventHandler(this.goStart_Click);
            // 
            // denialCheckBox
            // 
            this.denialCheckBox.AutoSize = true;
            this.denialCheckBox.Enabled = false;
            this.denialCheckBox.Location = new System.Drawing.Point(48, 161);
            this.denialCheckBox.Name = "denialCheckBox";
            this.denialCheckBox.Size = new System.Drawing.Size(126, 16);
            this.denialCheckBox.TabIndex = 23;
            this.denialCheckBox.Text = "この条件の否定をとる";
            this.denialCheckBox.UseVisualStyleBackColor = true;
            // 
            // targetConditionLabel
            // 
            this.targetConditionLabel.AutoSize = true;
            this.targetConditionLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.targetConditionLabel.Location = new System.Drawing.Point(140, 74);
            this.targetConditionLabel.Name = "targetConditionLabel";
            this.targetConditionLabel.Size = new System.Drawing.Size(92, 13);
            this.targetConditionLabel.TabIndex = 22;
            this.targetConditionLabel.Text = "対象条件番号：";
            // 
            // targetConditionForm
            // 
            this.targetConditionForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetConditionForm.Enabled = false;
            this.targetConditionForm.FormattingEnabled = true;
            this.targetConditionForm.Location = new System.Drawing.Point(232, 70);
            this.targetConditionForm.Name = "targetConditionForm";
            this.targetConditionForm.Size = new System.Drawing.Size(79, 20);
            this.targetConditionForm.TabIndex = 21;
            // 
            // searchWholeButton
            // 
            this.searchWholeButton.Enabled = false;
            this.searchWholeButton.Location = new System.Drawing.Point(513, 189);
            this.searchWholeButton.Name = "searchWholeButton";
            this.searchWholeButton.Size = new System.Drawing.Size(75, 23);
            this.searchWholeButton.TabIndex = 20;
            this.searchWholeButton.Text = "全体を検索";
            this.searchWholeButton.UseVisualStyleBackColor = true;
            // 
            // searchForwardButton
            // 
            this.searchForwardButton.Enabled = false;
            this.searchForwardButton.Location = new System.Drawing.Point(421, 189);
            this.searchForwardButton.Name = "searchForwardButton";
            this.searchForwardButton.Size = new System.Drawing.Size(75, 23);
            this.searchForwardButton.TabIndex = 19;
            this.searchForwardButton.Text = "前を検索";
            this.searchForwardButton.UseVisualStyleBackColor = true;
            // 
            // searchBackwardButton
            // 
            this.searchBackwardButton.Enabled = false;
            this.searchBackwardButton.Location = new System.Drawing.Point(330, 189);
            this.searchBackwardButton.Name = "searchBackwardButton";
            this.searchBackwardButton.Size = new System.Drawing.Size(75, 23);
            this.searchBackwardButton.TabIndex = 18;
            this.searchBackwardButton.Text = "後ろを検索";
            this.searchBackwardButton.UseVisualStyleBackColor = true;
            // 
            // timeValueLabel
            // 
            this.timeValueLabel.AutoSize = true;
            this.timeValueLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.timeValueLabel.Location = new System.Drawing.Point(320, 134);
            this.timeValueLabel.Name = "timeValueLabel";
            this.timeValueLabel.Size = new System.Drawing.Size(31, 12);
            this.timeValueLabel.TabIndex = 17;
            this.timeValueLabel.Text = "μ秒";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(31, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(451, 1);
            this.label2.TabIndex = 16;
            // 
            // addRefiningConditionButton
            // 
            this.addRefiningConditionButton.Enabled = false;
            this.addRefiningConditionButton.Location = new System.Drawing.Point(467, 128);
            this.addRefiningConditionButton.Name = "addRefiningConditionButton";
            this.addRefiningConditionButton.Size = new System.Drawing.Size(29, 23);
            this.addRefiningConditionButton.TabIndex = 15;
            this.addRefiningConditionButton.Text = "+";
            this.addRefiningConditionButton.UseVisualStyleBackColor = true;
            // 
            // timingValueForm
            // 
            this.timingValueForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.timingValueForm.Enabled = false;
            this.timingValueForm.FormattingEnabled = true;
            this.timingValueForm.Location = new System.Drawing.Point(234, 128);
            this.timingValueForm.Name = "timingValueForm";
            this.timingValueForm.Size = new System.Drawing.Size(79, 20);
            this.timingValueForm.TabIndex = 14;
            // 
            // timingForm
            // 
            this.timingForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timingForm.DropDownWidth = 125;
            this.timingForm.Enabled = false;
            this.timingForm.FormattingEnabled = true;
            this.timingForm.Items.AddRange(new object[] {
            "以内に発生(基準時以前)",
            "以内に発生(基準時以後)",
            "以上前に発生",
            "以上後に発生"});
            this.timingForm.Location = new System.Drawing.Point(360, 129);
            this.timingForm.Name = "timingForm";
            this.timingForm.Size = new System.Drawing.Size(93, 20);
            this.timingForm.TabIndex = 13;
            // 
            // timingSetting
            // 
            this.timingSetting.AutoSize = true;
            this.timingSetting.Font = new System.Drawing.Font("Century", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timingSetting.Location = new System.Drawing.Point(45, 130);
            this.timingSetting.Name = "timingSetting";
            this.timingSetting.Size = new System.Drawing.Size(188, 15);
            this.timingSetting.TabIndex = 12;
            this.timingSetting.Text = "基本条件のイベント発生時刻に対して";
            // 
            // refiningConditionEventDetailForm
            // 
            this.refiningConditionEventDetailForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.refiningConditionEventDetailForm.Enabled = false;
            this.refiningConditionEventDetailForm.FormattingEnabled = true;
            this.refiningConditionEventDetailForm.Location = new System.Drawing.Point(327, 97);
            this.refiningConditionEventDetailForm.Name = "refiningConditionEventDetailForm";
            this.refiningConditionEventDetailForm.Size = new System.Drawing.Size(79, 20);
            this.refiningConditionEventDetailForm.TabIndex = 11;
            // 
            // refiningConditionEventForm
            // 
            this.refiningConditionEventForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.refiningConditionEventForm.Enabled = false;
            this.refiningConditionEventForm.FormattingEnabled = true;
            this.refiningConditionEventForm.Location = new System.Drawing.Point(232, 98);
            this.refiningConditionEventForm.Name = "refiningConditionEventForm";
            this.refiningConditionEventForm.Size = new System.Drawing.Size(79, 20);
            this.refiningConditionEventForm.TabIndex = 10;
            // 
            // refiningConditionRuleForm
            // 
            this.refiningConditionRuleForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.refiningConditionRuleForm.Enabled = false;
            this.refiningConditionRuleForm.FormattingEnabled = true;
            this.refiningConditionRuleForm.Location = new System.Drawing.Point(140, 98);
            this.refiningConditionRuleForm.Name = "refiningConditionRuleForm";
            this.refiningConditionRuleForm.Size = new System.Drawing.Size(77, 20);
            this.refiningConditionRuleForm.TabIndex = 9;
            // 
            // refiningConditionResourceForm
            // 
            this.refiningConditionResourceForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.refiningConditionResourceForm.Enabled = false;
            this.refiningConditionResourceForm.FormattingEnabled = true;
            this.refiningConditionResourceForm.Location = new System.Drawing.Point(44, 98);
            this.refiningConditionResourceForm.Name = "refiningConditionResourceForm";
            this.refiningConditionResourceForm.Size = new System.Drawing.Size(83, 20);
            this.refiningConditionResourceForm.TabIndex = 8;
            // 
            // refiningConditionSettingLabel
            // 
            this.refiningConditionSettingLabel.AutoSize = true;
            this.refiningConditionSettingLabel.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refiningConditionSettingLabel.Location = new System.Drawing.Point(28, 70);
            this.refiningConditionSettingLabel.Name = "refiningConditionSettingLabel";
            this.refiningConditionSettingLabel.Size = new System.Drawing.Size(96, 18);
            this.refiningConditionSettingLabel.TabIndex = 7;
            this.refiningConditionSettingLabel.Text = "絞り込み条件";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(29, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(453, 1);
            this.label1.TabIndex = 6;
            // 
            // mainConditionSettingLabel
            // 
            this.mainConditionSettingLabel.AutoSize = true;
            this.mainConditionSettingLabel.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainConditionSettingLabel.Location = new System.Drawing.Point(28, 8);
            this.mainConditionSettingLabel.Name = "mainConditionSettingLabel";
            this.mainConditionSettingLabel.Size = new System.Drawing.Size(72, 18);
            this.mainConditionSettingLabel.TabIndex = 5;
            this.mainConditionSettingLabel.Text = "基本条件";
            // 
            // addMainConditionButton
            // 
            this.addMainConditionButton.Enabled = false;
            this.addMainConditionButton.Location = new System.Drawing.Point(463, 27);
            this.addMainConditionButton.Name = "addMainConditionButton";
            this.addMainConditionButton.Size = new System.Drawing.Size(29, 23);
            this.addMainConditionButton.TabIndex = 4;
            this.addMainConditionButton.Text = "+";
            this.addMainConditionButton.UseVisualStyleBackColor = true;
            // 
            // mainEventDetailForm
            // 
            this.mainEventDetailForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainEventDetailForm.FormattingEnabled = true;
            this.mainEventDetailForm.Location = new System.Drawing.Point(327, 29);
            this.mainEventDetailForm.Name = "mainEventDetailForm";
            this.mainEventDetailForm.Size = new System.Drawing.Size(79, 20);
            this.mainEventDetailForm.TabIndex = 3;
            // 
            // mainEventForm
            // 
            this.mainEventForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainEventForm.FormattingEnabled = true;
            this.mainEventForm.Location = new System.Drawing.Point(232, 29);
            this.mainEventForm.Name = "mainEventForm";
            this.mainEventForm.Size = new System.Drawing.Size(79, 20);
            this.mainEventForm.TabIndex = 2;
            // 
            // mainRuleForm
            // 
            this.mainRuleForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainRuleForm.FormattingEnabled = true;
            this.mainRuleForm.Location = new System.Drawing.Point(138, 29);
            this.mainRuleForm.Name = "mainRuleForm";
            this.mainRuleForm.Size = new System.Drawing.Size(79, 20);
            this.mainRuleForm.TabIndex = 0;
            // 
            // mainResourceForm
            // 
            this.mainResourceForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainResourceForm.FormattingEnabled = true;
            this.mainResourceForm.Location = new System.Drawing.Point(44, 29);
            this.mainResourceForm.Name = "mainResourceForm";
            this.mainResourceForm.Size = new System.Drawing.Size(83, 20);
            this.mainResourceForm.TabIndex = 1;
            // 
            // ConditionDisplayPanel
            // 
            this.ConditionDisplayPanel.AutoScroll = true;
            this.ConditionDisplayPanel.BackColor = System.Drawing.Color.LavenderBlush;
            this.ConditionDisplayPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ConditionDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConditionDisplayPanel.Location = new System.Drawing.Point(0, 219);
            this.ConditionDisplayPanel.Name = "ConditionDisplayPanel";
            this.ConditionDisplayPanel.Size = new System.Drawing.Size(604, 415);
            this.ConditionDisplayPanel.TabIndex = 1;
            // 
            // DetailSearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(604, 634);
            this.Controls.Add(this.ConditionDisplayPanel);
            this.Controls.Add(this.ConditionOpratior);
            this.Name = "DetailSearchPanel";
            this.Text = "DetailSearchPanel";
            this.ConditionOpratior.ResumeLayout(false);
            this.ConditionOpratior.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ConditionOpratior;
        private System.Windows.Forms.Button addMainConditionButton;
        private System.Windows.Forms.ComboBox mainEventDetailForm;
        private System.Windows.Forms.ComboBox mainEventForm;
        private System.Windows.Forms.ComboBox mainRuleForm;
        private System.Windows.Forms.ComboBox mainResourceForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label mainConditionSettingLabel;
        private System.Windows.Forms.Label refiningConditionSettingLabel;
        private System.Windows.Forms.ComboBox timingValueForm;
        private System.Windows.Forms.ComboBox timingForm;
        private System.Windows.Forms.Label timingSetting;
        private System.Windows.Forms.ComboBox refiningConditionEventDetailForm;
        private System.Windows.Forms.ComboBox refiningConditionEventForm;
        private System.Windows.Forms.ComboBox refiningConditionRuleForm;
        private System.Windows.Forms.ComboBox refiningConditionResourceForm;
        private System.Windows.Forms.Button addRefiningConditionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label timeValueLabel;
        private System.Windows.Forms.Button searchWholeButton;
        private System.Windows.Forms.Button searchForwardButton;
        private System.Windows.Forms.Button searchBackwardButton;
        private System.Windows.Forms.ComboBox targetConditionForm;
        private System.Windows.Forms.Label targetConditionLabel;
        private System.Windows.Forms.Panel ConditionDisplayPanel;
        private System.Windows.Forms.CheckBox denialCheckBox;
        private System.Windows.Forms.Button goStart;
        private System.Windows.Forms.Button goEnd;



    }
}