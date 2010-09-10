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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.addSubConditionButton = new System.Windows.Forms.Button();
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
            this.makeMainConditionButton = new System.Windows.Forms.Button();
            this.MainEventDetailForm = new System.Windows.Forms.ComboBox();
            this.MainEventForm = new System.Windows.Forms.ComboBox();
            this.MainRuleForm = new System.Windows.Forms.ComboBox();
            this.MainResourceForm = new System.Windows.Forms.ComboBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SubConditionDisplay = new System.Windows.Forms.Label();
            this.MainConditionDisplay = new System.Windows.Forms.Label();
            this.MainConditionBox = new System.Windows.Forms.TextBox();
            this.TimeValue = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.TimeValue);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.addSubConditionButton);
            this.panel1.Controls.Add(this.TimingValueForm);
            this.panel1.Controls.Add(this.TimingForm);
            this.panel1.Controls.Add(this.TimingSetting);
            this.panel1.Controls.Add(this.SubEventDetailForm);
            this.panel1.Controls.Add(this.SubEventForm);
            this.panel1.Controls.Add(this.SubRuleForm);
            this.panel1.Controls.Add(this.SubResourceForm);
            this.panel1.Controls.Add(this.SubConditionSettingLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.MainConditionSettingLabel);
            this.panel1.Controls.Add(this.makeMainConditionButton);
            this.panel1.Controls.Add(this.MainEventDetailForm);
            this.panel1.Controls.Add(this.MainEventForm);
            this.panel1.Controls.Add(this.MainRuleForm);
            this.panel1.Controls.Add(this.MainResourceForm);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 215);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(31, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(451, 1);
            this.label2.TabIndex = 16;
            // 
            // addSubConditionButton
            // 
            this.addSubConditionButton.Location = new System.Drawing.Point(447, 157);
            this.addSubConditionButton.Name = "addSubConditionButton";
            this.addSubConditionButton.Size = new System.Drawing.Size(29, 23);
            this.addSubConditionButton.TabIndex = 15;
            this.addSubConditionButton.Text = "+";
            this.addSubConditionButton.UseVisualStyleBackColor = true;
            // 
            // TimingValueForm
            // 
            this.TimingValueForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.TimingValueForm.Enabled = false;
            this.TimingValueForm.FormattingEnabled = true;
            this.TimingValueForm.Location = new System.Drawing.Point(233, 163);
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
            this.TimingForm.Location = new System.Drawing.Point(140, 163);
            this.TimingForm.Name = "TimingForm";
            this.TimingForm.Size = new System.Drawing.Size(78, 20);
            this.TimingForm.TabIndex = 13;
            // 
            // TimingSetting
            // 
            this.TimingSetting.AutoSize = true;
            this.TimingSetting.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimingSetting.Location = new System.Drawing.Point(73, 164);
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
            this.SubEventDetailForm.Location = new System.Drawing.Point(327, 118);
            this.SubEventDetailForm.Name = "SubEventDetailForm";
            this.SubEventDetailForm.Size = new System.Drawing.Size(79, 20);
            this.SubEventDetailForm.TabIndex = 11;
            // 
            // SubEventForm
            // 
            this.SubEventForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubEventForm.Enabled = false;
            this.SubEventForm.FormattingEnabled = true;
            this.SubEventForm.Location = new System.Drawing.Point(232, 119);
            this.SubEventForm.Name = "SubEventForm";
            this.SubEventForm.Size = new System.Drawing.Size(79, 20);
            this.SubEventForm.TabIndex = 10;
            // 
            // SubRuleForm
            // 
            this.SubRuleForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubRuleForm.Enabled = false;
            this.SubRuleForm.FormattingEnabled = true;
            this.SubRuleForm.Location = new System.Drawing.Point(140, 119);
            this.SubRuleForm.Name = "SubRuleForm";
            this.SubRuleForm.Size = new System.Drawing.Size(77, 20);
            this.SubRuleForm.TabIndex = 9;
            // 
            // SubResourceForm
            // 
            this.SubResourceForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubResourceForm.FormattingEnabled = true;
            this.SubResourceForm.Location = new System.Drawing.Point(44, 119);
            this.SubResourceForm.Name = "SubResourceForm";
            this.SubResourceForm.Size = new System.Drawing.Size(83, 20);
            this.SubResourceForm.TabIndex = 8;
            // 
            // SubConditionSettingLabel
            // 
            this.SubConditionSettingLabel.AutoSize = true;
            this.SubConditionSettingLabel.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubConditionSettingLabel.Location = new System.Drawing.Point(28, 91);
            this.SubConditionSettingLabel.Name = "SubConditionSettingLabel";
            this.SubConditionSettingLabel.Size = new System.Drawing.Size(184, 18);
            this.SubConditionSettingLabel.TabIndex = 7;
            this.SubConditionSettingLabel.Text = "Sub Condition Setting";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(29, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(453, 1);
            this.label1.TabIndex = 6;
            // 
            // MainConditionSettingLabel
            // 
            this.MainConditionSettingLabel.AutoSize = true;
            this.MainConditionSettingLabel.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainConditionSettingLabel.Location = new System.Drawing.Point(27, 14);
            this.MainConditionSettingLabel.Name = "MainConditionSettingLabel";
            this.MainConditionSettingLabel.Size = new System.Drawing.Size(195, 18);
            this.MainConditionSettingLabel.TabIndex = 5;
            this.MainConditionSettingLabel.Text = "Main Condition Setting";
            // 
            // addMainConditionButton
            // 
            this.makeMainConditionButton.Location = new System.Drawing.Point(447, 35);
            this.makeMainConditionButton.Name = "addMainConditionButton";
            this.makeMainConditionButton.Size = new System.Drawing.Size(29, 23);
            this.makeMainConditionButton.TabIndex = 4;
            this.makeMainConditionButton.Text = "+";
            this.makeMainConditionButton.UseVisualStyleBackColor = true;
            // 
            // MainEventDetailForm
            // 
            this.MainEventDetailForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainEventDetailForm.FormattingEnabled = true;
            this.MainEventDetailForm.Location = new System.Drawing.Point(327, 37);
            this.MainEventDetailForm.Name = "MainEventDetailForm";
            this.MainEventDetailForm.Size = new System.Drawing.Size(79, 20);
            this.MainEventDetailForm.TabIndex = 3;
            // 
            // MainEventForm
            // 
            this.MainEventForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainEventForm.FormattingEnabled = true;
            this.MainEventForm.Location = new System.Drawing.Point(232, 37);
            this.MainEventForm.Name = "MainEventForm";
            this.MainEventForm.Size = new System.Drawing.Size(79, 20);
            this.MainEventForm.TabIndex = 2;
            // 
            // MainRuleForm
            // 
            this.MainRuleForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainRuleForm.FormattingEnabled = true;
            this.MainRuleForm.Location = new System.Drawing.Point(138, 37);
            this.MainRuleForm.Name = "MainRuleForm";
            this.MainRuleForm.Size = new System.Drawing.Size(79, 20);
            this.MainRuleForm.TabIndex = 0;
            // 
            // MainResourceForm
            // 
            this.MainResourceForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainResourceForm.FormattingEnabled = true;
            this.MainResourceForm.Location = new System.Drawing.Point(44, 37);
            this.MainResourceForm.Name = "MainResourceForm";
            this.MainResourceForm.Size = new System.Drawing.Size(83, 20);
            this.MainResourceForm.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(600, 3);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.SubConditionDisplay);
            this.panel2.Controls.Add(this.MainConditionDisplay);
            this.panel2.Controls.Add(this.MainConditionBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 215);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(604, 327);
            this.panel2.TabIndex = 1;
            // 
            // SubConditionDisplay
            // 
            this.SubConditionDisplay.AutoSize = true;
            this.SubConditionDisplay.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubConditionDisplay.Location = new System.Drawing.Point(31, 182);
            this.SubConditionDisplay.Name = "SubConditionDisplay";
            this.SubConditionDisplay.Size = new System.Drawing.Size(116, 18);
            this.SubConditionDisplay.TabIndex = 2;
            this.SubConditionDisplay.Text = "SubCondition";
            // 
            // MainConditionDisplay
            // 
            this.MainConditionDisplay.AutoSize = true;
            this.MainConditionDisplay.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainConditionDisplay.Location = new System.Drawing.Point(27, 52);
            this.MainConditionDisplay.Name = "MainConditionDisplay";
            this.MainConditionDisplay.Size = new System.Drawing.Size(127, 18);
            this.MainConditionDisplay.TabIndex = 1;
            this.MainConditionDisplay.Text = "MainCondition";
            // 
            // MainConditionBox
            // 
            this.MainConditionBox.Location = new System.Drawing.Point(33, 76);
            this.MainConditionBox.Multiline = true;
            this.MainConditionBox.Name = "MainConditionBox";
            this.MainConditionBox.ReadOnly = true;
            this.MainConditionBox.Size = new System.Drawing.Size(392, 73);
            this.MainConditionBox.TabIndex = 0;
            // 
            // TimeValue
            // 
            this.TimeValue.AutoSize = true;
            this.TimeValue.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TimeValue.Location = new System.Drawing.Point(317, 169);
            this.TimeValue.Name = "TimeValue";
            this.TimeValue.Size = new System.Drawing.Size(29, 12);
            this.TimeValue.TabIndex = 17;
            this.TimeValue.Text = "(us)";
            // 
            // DetailSearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(604, 542);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DetailSearchPanel";
            this.Text = "DetailSearchPanel";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button makeMainConditionButton;
        private System.Windows.Forms.ComboBox MainEventDetailForm;
        private System.Windows.Forms.ComboBox MainEventForm;
        private System.Windows.Forms.ComboBox MainRuleForm;
        private System.Windows.Forms.ComboBox MainResourceForm;
        private System.Windows.Forms.TextBox MainConditionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MainConditionSettingLabel;
        private System.Windows.Forms.Label SubConditionSettingLabel;
        private System.Windows.Forms.Label MainConditionDisplay;
        private System.Windows.Forms.Label SubConditionDisplay;
        private System.Windows.Forms.ComboBox TimingValueForm;
        private System.Windows.Forms.ComboBox TimingForm;
        private System.Windows.Forms.Label TimingSetting;
        private System.Windows.Forms.ComboBox SubEventDetailForm;
        private System.Windows.Forms.ComboBox SubEventForm;
        private System.Windows.Forms.ComboBox SubRuleForm;
        private System.Windows.Forms.ComboBox SubResourceForm;
        private System.Windows.Forms.Button addSubConditionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TimeValue;



    }
}