namespace NU.OJL.MPRTOS.TLV.Core.Search
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
            this.TargetConditionLabel = new System.Windows.Forms.Label();
            this.TargetConditionForm = new System.Windows.Forms.ComboBox();
            this.WholeSearchButton = new System.Windows.Forms.Button();
            this.ForwardSearchButton = new System.Windows.Forms.Button();
            this.BackwardSearchButton = new System.Windows.Forms.Button();
            this.TimeValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AddRefiningConditionButton = new System.Windows.Forms.Button();
            this.TimingValueForm = new System.Windows.Forms.ComboBox();
            this.TimingForm = new System.Windows.Forms.ComboBox();
            this.TimingSetting = new System.Windows.Forms.Label();
            this.RefiningConditionEventDetailForm = new System.Windows.Forms.ComboBox();
            this.RefiningConditionEventForm = new System.Windows.Forms.ComboBox();
            this.RefiningConditionRuleForm = new System.Windows.Forms.ComboBox();
            this.RefiningConditionResourceForm = new System.Windows.Forms.ComboBox();
            this.RefiningConditionSettingLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MainConditionSettingLabel = new System.Windows.Forms.Label();
            this.AddMainConditionButton = new System.Windows.Forms.Button();
            this.MainEventDetailForm = new System.Windows.Forms.ComboBox();
            this.MainEventForm = new System.Windows.Forms.ComboBox();
            this.MainRuleForm = new System.Windows.Forms.ComboBox();
            this.MainResourceForm = new System.Windows.Forms.ComboBox();
            this.ConditionDisplayPanel = new System.Windows.Forms.Panel();
            this.ConditionOpratior.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConditionOpratior
            // 
            this.ConditionOpratior.BackColor = System.Drawing.Color.SeaShell;
            this.ConditionOpratior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ConditionOpratior.Controls.Add(this.TargetConditionLabel);
            this.ConditionOpratior.Controls.Add(this.TargetConditionForm);
            this.ConditionOpratior.Controls.Add(this.WholeSearchButton);
            this.ConditionOpratior.Controls.Add(this.ForwardSearchButton);
            this.ConditionOpratior.Controls.Add(this.BackwardSearchButton);
            this.ConditionOpratior.Controls.Add(this.TimeValue);
            this.ConditionOpratior.Controls.Add(this.label2);
            this.ConditionOpratior.Controls.Add(this.AddRefiningConditionButton);
            this.ConditionOpratior.Controls.Add(this.TimingValueForm);
            this.ConditionOpratior.Controls.Add(this.TimingForm);
            this.ConditionOpratior.Controls.Add(this.TimingSetting);
            this.ConditionOpratior.Controls.Add(this.RefiningConditionEventDetailForm);
            this.ConditionOpratior.Controls.Add(this.RefiningConditionEventForm);
            this.ConditionOpratior.Controls.Add(this.RefiningConditionRuleForm);
            this.ConditionOpratior.Controls.Add(this.RefiningConditionResourceForm);
            this.ConditionOpratior.Controls.Add(this.RefiningConditionSettingLabel);
            this.ConditionOpratior.Controls.Add(this.label1);
            this.ConditionOpratior.Controls.Add(this.MainConditionSettingLabel);
            this.ConditionOpratior.Controls.Add(this.AddMainConditionButton);
            this.ConditionOpratior.Controls.Add(this.MainEventDetailForm);
            this.ConditionOpratior.Controls.Add(this.MainEventForm);
            this.ConditionOpratior.Controls.Add(this.MainRuleForm);
            this.ConditionOpratior.Controls.Add(this.MainResourceForm);
            this.ConditionOpratior.Dock = System.Windows.Forms.DockStyle.Top;
            this.ConditionOpratior.Location = new System.Drawing.Point(0, 0);
            this.ConditionOpratior.Name = "ConditionOpratior";
            this.ConditionOpratior.Size = new System.Drawing.Size(604, 209);
            this.ConditionOpratior.TabIndex = 0;
            // 
            // TargetConditionLabel
            // 
            this.TargetConditionLabel.AutoSize = true;
            this.TargetConditionLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TargetConditionLabel.Location = new System.Drawing.Point(140, 74);
            this.TargetConditionLabel.Name = "TargetConditionLabel";
            this.TargetConditionLabel.Size = new System.Drawing.Size(92, 13);
            this.TargetConditionLabel.TabIndex = 22;
            this.TargetConditionLabel.Text = "対象条件番号：";
            // 
            // TargetConditionForm
            // 
            this.TargetConditionForm.Enabled = false;
            this.TargetConditionForm.FormattingEnabled = true;
            this.TargetConditionForm.Location = new System.Drawing.Point(232, 70);
            this.TargetConditionForm.Name = "TargetConditionForm";
            this.TargetConditionForm.Size = new System.Drawing.Size(79, 20);
            this.TargetConditionForm.TabIndex = 21;
            // 
            // WholeSearchButton
            // 
            this.WholeSearchButton.Enabled = false;
            this.WholeSearchButton.Location = new System.Drawing.Point(513, 181);
            this.WholeSearchButton.Name = "WholeSearchButton";
            this.WholeSearchButton.Size = new System.Drawing.Size(75, 23);
            this.WholeSearchButton.TabIndex = 20;
            this.WholeSearchButton.Text = "全体を検索";
            this.WholeSearchButton.UseVisualStyleBackColor = true;
            // 
            // ForwardSearchButton
            // 
            this.ForwardSearchButton.Enabled = false;
            this.ForwardSearchButton.Location = new System.Drawing.Point(421, 181);
            this.ForwardSearchButton.Name = "ForwardSearchButton";
            this.ForwardSearchButton.Size = new System.Drawing.Size(75, 23);
            this.ForwardSearchButton.TabIndex = 19;
            this.ForwardSearchButton.Text = "前を検索";
            this.ForwardSearchButton.UseVisualStyleBackColor = true;
            // 
            // BackwardSearchButton
            // 
            this.BackwardSearchButton.Enabled = false;
            this.BackwardSearchButton.Location = new System.Drawing.Point(330, 181);
            this.BackwardSearchButton.Name = "BackwardSearchButton";
            this.BackwardSearchButton.Size = new System.Drawing.Size(75, 23);
            this.BackwardSearchButton.TabIndex = 18;
            this.BackwardSearchButton.Text = "後ろを検索";
            this.BackwardSearchButton.UseVisualStyleBackColor = true;
            // 
            // TimeValue
            // 
            this.TimeValue.AutoSize = true;
            this.TimeValue.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TimeValue.Location = new System.Drawing.Point(317, 140);
            this.TimeValue.Name = "TimeValue";
            this.TimeValue.Size = new System.Drawing.Size(29, 12);
            this.TimeValue.TabIndex = 17;
            this.TimeValue.Text = "(us)";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(31, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(451, 1);
            this.label2.TabIndex = 16;
            // 
            // AddRefiningConditionButton
            // 
            this.AddRefiningConditionButton.Enabled = false;
            this.AddRefiningConditionButton.Location = new System.Drawing.Point(447, 128);
            this.AddRefiningConditionButton.Name = "AddRefiningConditionButton";
            this.AddRefiningConditionButton.Size = new System.Drawing.Size(29, 23);
            this.AddRefiningConditionButton.TabIndex = 15;
            this.AddRefiningConditionButton.Text = "+";
            this.AddRefiningConditionButton.UseVisualStyleBackColor = true;
            // 
            // TimingValueForm
            // 
            this.TimingValueForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.TimingValueForm.Enabled = false;
            this.TimingValueForm.FormattingEnabled = true;
            this.TimingValueForm.Location = new System.Drawing.Point(233, 134);
            this.TimingValueForm.Name = "TimingValueForm";
            this.TimingValueForm.Size = new System.Drawing.Size(79, 20);
            this.TimingValueForm.TabIndex = 14;
            // 
            // TimingForm
            // 
            this.TimingForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimingForm.Enabled = false;
            this.TimingForm.FormattingEnabled = true;
            this.TimingForm.Items.AddRange(new object[] {
            "以内",
            "以前",
            "以後",
            "直後",
            "直前"});
            this.TimingForm.Location = new System.Drawing.Point(140, 134);
            this.TimingForm.Name = "TimingForm";
            this.TimingForm.Size = new System.Drawing.Size(78, 20);
            this.TimingForm.TabIndex = 13;
            // 
            // TimingSetting
            // 
            this.TimingSetting.AutoSize = true;
            this.TimingSetting.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimingSetting.Location = new System.Drawing.Point(73, 135);
            this.TimingSetting.Name = "TimingSetting";
            this.TimingSetting.Size = new System.Drawing.Size(67, 16);
            this.TimingSetting.TabIndex = 12;
            this.TimingSetting.Text = "Timing：";
            // 
            // RefiningConditionEventDetailForm
            // 
            this.RefiningConditionEventDetailForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RefiningConditionEventDetailForm.Enabled = false;
            this.RefiningConditionEventDetailForm.FormattingEnabled = true;
            this.RefiningConditionEventDetailForm.Location = new System.Drawing.Point(327, 97);
            this.RefiningConditionEventDetailForm.Name = "RefiningConditionEventDetailForm";
            this.RefiningConditionEventDetailForm.Size = new System.Drawing.Size(79, 20);
            this.RefiningConditionEventDetailForm.TabIndex = 11;
            // 
            // RefiningConditionEventForm
            // 
            this.RefiningConditionEventForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RefiningConditionEventForm.Enabled = false;
            this.RefiningConditionEventForm.FormattingEnabled = true;
            this.RefiningConditionEventForm.Location = new System.Drawing.Point(232, 98);
            this.RefiningConditionEventForm.Name = "RefiningConditionEventForm";
            this.RefiningConditionEventForm.Size = new System.Drawing.Size(79, 20);
            this.RefiningConditionEventForm.TabIndex = 10;
            // 
            // RefiningConditionRuleForm
            // 
            this.RefiningConditionRuleForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RefiningConditionRuleForm.Enabled = false;
            this.RefiningConditionRuleForm.FormattingEnabled = true;
            this.RefiningConditionRuleForm.Location = new System.Drawing.Point(140, 98);
            this.RefiningConditionRuleForm.Name = "RefiningConditionRuleForm";
            this.RefiningConditionRuleForm.Size = new System.Drawing.Size(77, 20);
            this.RefiningConditionRuleForm.TabIndex = 9;
            // 
            // RefiningConditionResourceForm
            // 
            this.RefiningConditionResourceForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RefiningConditionResourceForm.Enabled = false;
            this.RefiningConditionResourceForm.FormattingEnabled = true;
            this.RefiningConditionResourceForm.Location = new System.Drawing.Point(44, 98);
            this.RefiningConditionResourceForm.Name = "RefiningConditionResourceForm";
            this.RefiningConditionResourceForm.Size = new System.Drawing.Size(83, 20);
            this.RefiningConditionResourceForm.TabIndex = 8;
            // 
            // RefiningConditionSettingLabel
            // 
            this.RefiningConditionSettingLabel.AutoSize = true;
            this.RefiningConditionSettingLabel.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefiningConditionSettingLabel.Location = new System.Drawing.Point(28, 70);
            this.RefiningConditionSettingLabel.Name = "RefiningConditionSettingLabel";
            this.RefiningConditionSettingLabel.Size = new System.Drawing.Size(96, 18);
            this.RefiningConditionSettingLabel.TabIndex = 7;
            this.RefiningConditionSettingLabel.Text = "絞り込み条件";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(29, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(453, 1);
            this.label1.TabIndex = 6;
            // 
            // MainConditionSettingLabel
            // 
            this.MainConditionSettingLabel.AutoSize = true;
            this.MainConditionSettingLabel.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainConditionSettingLabel.Location = new System.Drawing.Point(28, 8);
            this.MainConditionSettingLabel.Name = "MainConditionSettingLabel";
            this.MainConditionSettingLabel.Size = new System.Drawing.Size(72, 18);
            this.MainConditionSettingLabel.TabIndex = 5;
            this.MainConditionSettingLabel.Text = "検索条件";
            // 
            // AddMainConditionButton
            // 
            this.AddMainConditionButton.Enabled = false;
            this.AddMainConditionButton.Location = new System.Drawing.Point(447, 27);
            this.AddMainConditionButton.Name = "AddMainConditionButton";
            this.AddMainConditionButton.Size = new System.Drawing.Size(29, 23);
            this.AddMainConditionButton.TabIndex = 4;
            this.AddMainConditionButton.Text = "+";
            this.AddMainConditionButton.UseVisualStyleBackColor = true;
            // 
            // MainEventDetailForm
            // 
            this.MainEventDetailForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainEventDetailForm.FormattingEnabled = true;
            this.MainEventDetailForm.Location = new System.Drawing.Point(327, 29);
            this.MainEventDetailForm.Name = "MainEventDetailForm";
            this.MainEventDetailForm.Size = new System.Drawing.Size(79, 20);
            this.MainEventDetailForm.TabIndex = 3;
            // 
            // MainEventForm
            // 
            this.MainEventForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainEventForm.FormattingEnabled = true;
            this.MainEventForm.Location = new System.Drawing.Point(232, 29);
            this.MainEventForm.Name = "MainEventForm";
            this.MainEventForm.Size = new System.Drawing.Size(79, 20);
            this.MainEventForm.TabIndex = 2;
            // 
            // MainRuleForm
            // 
            this.MainRuleForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainRuleForm.FormattingEnabled = true;
            this.MainRuleForm.Location = new System.Drawing.Point(138, 29);
            this.MainRuleForm.Name = "MainRuleForm";
            this.MainRuleForm.Size = new System.Drawing.Size(79, 20);
            this.MainRuleForm.TabIndex = 0;
            // 
            // MainResourceForm
            // 
            this.MainResourceForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainResourceForm.FormattingEnabled = true;
            this.MainResourceForm.Location = new System.Drawing.Point(44, 29);
            this.MainResourceForm.Name = "MainResourceForm";
            this.MainResourceForm.Size = new System.Drawing.Size(83, 20);
            this.MainResourceForm.TabIndex = 1;
            // 
            // ConditionDisplayPanel
            // 
            this.ConditionDisplayPanel.AutoScroll = true;
            this.ConditionDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConditionDisplayPanel.Location = new System.Drawing.Point(0, 209);
            this.ConditionDisplayPanel.Name = "ConditionDisplayPanel";
            this.ConditionDisplayPanel.Size = new System.Drawing.Size(604, 401);
            this.ConditionDisplayPanel.TabIndex = 1;
            // 
            // DetailSearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(604, 610);
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
        private System.Windows.Forms.Button AddMainConditionButton;
        private System.Windows.Forms.ComboBox MainEventDetailForm;
        private System.Windows.Forms.ComboBox MainEventForm;
        private System.Windows.Forms.ComboBox MainRuleForm;
        private System.Windows.Forms.ComboBox MainResourceForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MainConditionSettingLabel;
        private System.Windows.Forms.Label RefiningConditionSettingLabel;
        private System.Windows.Forms.ComboBox TimingValueForm;
        private System.Windows.Forms.ComboBox TimingForm;
        private System.Windows.Forms.Label TimingSetting;
        private System.Windows.Forms.ComboBox RefiningConditionEventDetailForm;
        private System.Windows.Forms.ComboBox RefiningConditionEventForm;
        private System.Windows.Forms.ComboBox RefiningConditionRuleForm;
        private System.Windows.Forms.ComboBox RefiningConditionResourceForm;
        private System.Windows.Forms.Button AddRefiningConditionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TimeValue;
        private System.Windows.Forms.Button WholeSearchButton;
        private System.Windows.Forms.Button ForwardSearchButton;
        private System.Windows.Forms.Button BackwardSearchButton;
        private System.Windows.Forms.ComboBox TargetConditionForm;
        private System.Windows.Forms.Label TargetConditionLabel;
        private System.Windows.Forms.Panel ConditionDisplayPanel;



    }
}