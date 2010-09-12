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
            this.TimeValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AddSubConditionButton = new System.Windows.Forms.Button();
            this.TimingValueForm = new System.Windows.Forms.ComboBox();
            this.TimingForm = new System.Windows.Forms.ComboBox();
            this.TimingSetting = new System.Windows.Forms.Label();
            this.SubEventDetailForm = new System.Windows.Forms.ComboBox();
            this.SubEventForm = new System.Windows.Forms.ComboBox();
            this.SubRuleForm = new System.Windows.Forms.ComboBox();
            this.SubResourceForm = new System.Windows.Forms.ComboBox();
            this.SubConditionSettingLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MainConditionSettingLabel = new System.Windows.Forms.Label();
            this.MakeMainConditionButton = new System.Windows.Forms.Button();
            this.MainEventDetailForm = new System.Windows.Forms.ComboBox();
            this.MainEventForm = new System.Windows.Forms.ComboBox();
            this.MainRuleForm = new System.Windows.Forms.ComboBox();
            this.MainResourceForm = new System.Windows.Forms.ComboBox();
            this.MainConditionDisplay = new System.Windows.Forms.Panel();
            this.DeleteMainConditionButton = new System.Windows.Forms.Button();
            this.MainConditionLabel = new System.Windows.Forms.Label();
            this.MainConditionBox = new System.Windows.Forms.TextBox();
            this.SubConditionDisplay = new System.Windows.Forms.Panel();
            this.BackwardSearchButton = new System.Windows.Forms.Button();
            this.ForwardSearchButton = new System.Windows.Forms.Button();
            this.WholeSearchButton = new System.Windows.Forms.Button();
            this.ConditionOpratior.SuspendLayout();
            this.MainConditionDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConditionOpratior
            // 
            this.ConditionOpratior.BackColor = System.Drawing.Color.SeaShell;
            this.ConditionOpratior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ConditionOpratior.Controls.Add(this.WholeSearchButton);
            this.ConditionOpratior.Controls.Add(this.ForwardSearchButton);
            this.ConditionOpratior.Controls.Add(this.BackwardSearchButton);
            this.ConditionOpratior.Controls.Add(this.TimeValue);
            this.ConditionOpratior.Controls.Add(this.label2);
            this.ConditionOpratior.Controls.Add(this.AddSubConditionButton);
            this.ConditionOpratior.Controls.Add(this.TimingValueForm);
            this.ConditionOpratior.Controls.Add(this.TimingForm);
            this.ConditionOpratior.Controls.Add(this.TimingSetting);
            this.ConditionOpratior.Controls.Add(this.SubEventDetailForm);
            this.ConditionOpratior.Controls.Add(this.SubEventForm);
            this.ConditionOpratior.Controls.Add(this.SubRuleForm);
            this.ConditionOpratior.Controls.Add(this.SubResourceForm);
            this.ConditionOpratior.Controls.Add(this.SubConditionSettingLabel);
            this.ConditionOpratior.Controls.Add(this.label1);
            this.ConditionOpratior.Controls.Add(this.MainConditionSettingLabel);
            this.ConditionOpratior.Controls.Add(this.MakeMainConditionButton);
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
            // AddSubConditionButton
            // 
            this.AddSubConditionButton.Enabled = false;
            this.AddSubConditionButton.Location = new System.Drawing.Point(447, 128);
            this.AddSubConditionButton.Name = "AddSubConditionButton";
            this.AddSubConditionButton.Size = new System.Drawing.Size(29, 23);
            this.AddSubConditionButton.TabIndex = 15;
            this.AddSubConditionButton.Text = "+";
            this.AddSubConditionButton.UseVisualStyleBackColor = true;
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
            // SubEventDetailForm
            // 
            this.SubEventDetailForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubEventDetailForm.Enabled = false;
            this.SubEventDetailForm.FormattingEnabled = true;
            this.SubEventDetailForm.Location = new System.Drawing.Point(327, 97);
            this.SubEventDetailForm.Name = "SubEventDetailForm";
            this.SubEventDetailForm.Size = new System.Drawing.Size(79, 20);
            this.SubEventDetailForm.TabIndex = 11;
            // 
            // SubEventForm
            // 
            this.SubEventForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubEventForm.Enabled = false;
            this.SubEventForm.FormattingEnabled = true;
            this.SubEventForm.Location = new System.Drawing.Point(232, 98);
            this.SubEventForm.Name = "SubEventForm";
            this.SubEventForm.Size = new System.Drawing.Size(79, 20);
            this.SubEventForm.TabIndex = 10;
            // 
            // SubRuleForm
            // 
            this.SubRuleForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubRuleForm.Enabled = false;
            this.SubRuleForm.FormattingEnabled = true;
            this.SubRuleForm.Location = new System.Drawing.Point(140, 98);
            this.SubRuleForm.Name = "SubRuleForm";
            this.SubRuleForm.Size = new System.Drawing.Size(77, 20);
            this.SubRuleForm.TabIndex = 9;
            // 
            // SubResourceForm
            // 
            this.SubResourceForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubResourceForm.FormattingEnabled = true;
            this.SubResourceForm.Location = new System.Drawing.Point(44, 98);
            this.SubResourceForm.Name = "SubResourceForm";
            this.SubResourceForm.Size = new System.Drawing.Size(83, 20);
            this.SubResourceForm.TabIndex = 8;
            // 
            // SubConditionSettingLabel
            // 
            this.SubConditionSettingLabel.AutoSize = true;
            this.SubConditionSettingLabel.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubConditionSettingLabel.Location = new System.Drawing.Point(28, 70);
            this.SubConditionSettingLabel.Name = "SubConditionSettingLabel";
            this.SubConditionSettingLabel.Size = new System.Drawing.Size(184, 18);
            this.SubConditionSettingLabel.TabIndex = 7;
            this.SubConditionSettingLabel.Text = "Sub Condition Setting";
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
            this.MainConditionSettingLabel.Location = new System.Drawing.Point(27, 6);
            this.MainConditionSettingLabel.Name = "MainConditionSettingLabel";
            this.MainConditionSettingLabel.Size = new System.Drawing.Size(195, 18);
            this.MainConditionSettingLabel.TabIndex = 5;
            this.MainConditionSettingLabel.Text = "Main Condition Setting";
            // 
            // MakeMainConditionButton
            // 
            this.MakeMainConditionButton.Enabled = false;
            this.MakeMainConditionButton.Location = new System.Drawing.Point(447, 27);
            this.MakeMainConditionButton.Name = "MakeMainConditionButton";
            this.MakeMainConditionButton.Size = new System.Drawing.Size(29, 23);
            this.MakeMainConditionButton.TabIndex = 4;
            this.MakeMainConditionButton.Text = "+";
            this.MakeMainConditionButton.UseVisualStyleBackColor = true;
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
            // MainConditionDisplay
            // 
            this.MainConditionDisplay.AutoScroll = true;
            this.MainConditionDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainConditionDisplay.Controls.Add(this.DeleteMainConditionButton);
            this.MainConditionDisplay.Controls.Add(this.MainConditionLabel);
            this.MainConditionDisplay.Controls.Add(this.MainConditionBox);
            this.MainConditionDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainConditionDisplay.Location = new System.Drawing.Point(0, 209);
            this.MainConditionDisplay.Name = "MainConditionDisplay";
            this.MainConditionDisplay.Size = new System.Drawing.Size(604, 142);
            this.MainConditionDisplay.TabIndex = 1;
            // 
            // DeleteMainConditionButton
            // 
            this.DeleteMainConditionButton.Enabled = false;
            this.DeleteMainConditionButton.Location = new System.Drawing.Point(164, 10);
            this.DeleteMainConditionButton.Name = "DeleteMainConditionButton";
            this.DeleteMainConditionButton.Size = new System.Drawing.Size(32, 23);
            this.DeleteMainConditionButton.TabIndex = 2;
            this.DeleteMainConditionButton.Text = "-";
            this.DeleteMainConditionButton.UseVisualStyleBackColor = true;
            // 
            // MainConditionLabel
            // 
            this.MainConditionLabel.AutoSize = true;
            this.MainConditionLabel.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainConditionLabel.Location = new System.Drawing.Point(30, 16);
            this.MainConditionLabel.Name = "MainConditionLabel";
            this.MainConditionLabel.Size = new System.Drawing.Size(109, 16);
            this.MainConditionLabel.TabIndex = 1;
            this.MainConditionLabel.Text = "MainCondition";
            // 
            // MainConditionBox
            // 
            this.MainConditionBox.Location = new System.Drawing.Point(27, 36);
            this.MainConditionBox.Multiline = true;
            this.MainConditionBox.Name = "MainConditionBox";
            this.MainConditionBox.ReadOnly = true;
            this.MainConditionBox.Size = new System.Drawing.Size(392, 86);
            this.MainConditionBox.TabIndex = 0;
            // 
            // SubConditionDisplay
            // 
            this.SubConditionDisplay.AutoScroll = true;
            this.SubConditionDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SubConditionDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubConditionDisplay.Location = new System.Drawing.Point(0, 351);
            this.SubConditionDisplay.Name = "SubConditionDisplay";
            this.SubConditionDisplay.Size = new System.Drawing.Size(604, 259);
            this.SubConditionDisplay.TabIndex = 2;
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
            // DetailSearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(604, 610);
            this.Controls.Add(this.SubConditionDisplay);
            this.Controls.Add(this.MainConditionDisplay);
            this.Controls.Add(this.ConditionOpratior);
            this.Name = "DetailSearchPanel";
            this.Text = "DetailSearchPanel";
            this.ConditionOpratior.ResumeLayout(false);
            this.ConditionOpratior.PerformLayout();
            this.MainConditionDisplay.ResumeLayout(false);
            this.MainConditionDisplay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ConditionOpratior;
        private System.Windows.Forms.Panel MainConditionDisplay;
        private System.Windows.Forms.Button MakeMainConditionButton;
        private System.Windows.Forms.ComboBox MainEventDetailForm;
        private System.Windows.Forms.ComboBox MainEventForm;
        private System.Windows.Forms.ComboBox MainRuleForm;
        private System.Windows.Forms.ComboBox MainResourceForm;
        private System.Windows.Forms.TextBox MainConditionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MainConditionSettingLabel;
        private System.Windows.Forms.Label SubConditionSettingLabel;
        private System.Windows.Forms.Label MainConditionLabel;
        private System.Windows.Forms.ComboBox TimingValueForm;
        private System.Windows.Forms.ComboBox TimingForm;
        private System.Windows.Forms.Label TimingSetting;
        private System.Windows.Forms.ComboBox SubEventDetailForm;
        private System.Windows.Forms.ComboBox SubEventForm;
        private System.Windows.Forms.ComboBox SubRuleForm;
        private System.Windows.Forms.ComboBox SubResourceForm;
        private System.Windows.Forms.Button AddSubConditionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TimeValue;
        private System.Windows.Forms.Panel SubConditionDisplay;
        private System.Windows.Forms.Button DeleteMainConditionButton;
        private System.Windows.Forms.Button WholeSearchButton;
        private System.Windows.Forms.Button ForwardSearchButton;
        private System.Windows.Forms.Button BackwardSearchButton;



    }
}