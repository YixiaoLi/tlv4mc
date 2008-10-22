namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    partial class OpenResourceFileAndTraceLogFileOpenForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.resourceFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.traceLogFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.cancelButton = new System.Windows.Forms.Button();
            this.convertRuleComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.traceLogFileRefButton = new System.Windows.Forms.Button();
            this.traceLogFilePathTextBox = new System.Windows.Forms.TextBox();
            this.resourceFileRefButton = new System.Windows.Forms.Button();
            this.resourceFilePathTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.savePathRefButton = new System.Windows.Forms.Button();
            this.savePathTextBox = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.convertRuleMessageBox = new System.Windows.Forms.TextBox();
            this.errorMessageBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(274, 248);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "共通形式変換ルール";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(355, 248);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // convertRuleComboBox
            // 
            this.convertRuleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.convertRuleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.convertRuleComboBox.FormattingEnabled = true;
            this.convertRuleComboBox.Location = new System.Drawing.Point(126, 62);
            this.convertRuleComboBox.Name = "convertRuleComboBox";
            this.convertRuleComboBox.Size = new System.Drawing.Size(304, 20);
            this.convertRuleComboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "トレースログファイル";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "リソースファイル";
            // 
            // traceLogFileRefButton
            // 
            this.traceLogFileRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.traceLogFileRefButton.Location = new System.Drawing.Point(355, 33);
            this.traceLogFileRefButton.Name = "traceLogFileRefButton";
            this.traceLogFileRefButton.Size = new System.Drawing.Size(75, 23);
            this.traceLogFileRefButton.TabIndex = 3;
            this.traceLogFileRefButton.Text = "参照";
            this.traceLogFileRefButton.UseVisualStyleBackColor = true;
            // 
            // traceLogFilePathTextBox
            // 
            this.traceLogFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.traceLogFilePathTextBox.Location = new System.Drawing.Point(126, 35);
            this.traceLogFilePathTextBox.Name = "traceLogFilePathTextBox";
            this.traceLogFilePathTextBox.Size = new System.Drawing.Size(223, 19);
            this.traceLogFilePathTextBox.TabIndex = 2;
            // 
            // resourceFileRefButton
            // 
            this.resourceFileRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceFileRefButton.Location = new System.Drawing.Point(355, 4);
            this.resourceFileRefButton.Name = "resourceFileRefButton";
            this.resourceFileRefButton.Size = new System.Drawing.Size(75, 23);
            this.resourceFileRefButton.TabIndex = 1;
            this.resourceFileRefButton.Text = "参照";
            this.resourceFileRefButton.UseVisualStyleBackColor = true;
            // 
            // resourceFilePathTextBox
            // 
            this.resourceFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceFilePathTextBox.Location = new System.Drawing.Point(126, 6);
            this.resourceFilePathTextBox.Name = "resourceFilePathTextBox";
            this.resourceFilePathTextBox.Size = new System.Drawing.Size(223, 19);
            this.resourceFilePathTextBox.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "共通形式変換ルールの説明";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "保存先";
            // 
            // savePathRefButton
            // 
            this.savePathRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.savePathRefButton.Location = new System.Drawing.Point(355, 86);
            this.savePathRefButton.Name = "savePathRefButton";
            this.savePathRefButton.Size = new System.Drawing.Size(75, 23);
            this.savePathRefButton.TabIndex = 6;
            this.savePathRefButton.Text = "参照";
            this.savePathRefButton.UseVisualStyleBackColor = true;
            // 
            // savePathTextBox
            // 
            this.savePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.savePathTextBox.Location = new System.Drawing.Point(126, 88);
            this.savePathTextBox.Name = "savePathTextBox";
            this.savePathTextBox.Size = new System.Drawing.Size(223, 19);
            this.savePathTextBox.TabIndex = 5;
            // 
            // convertRuleMessageBox
            // 
            this.convertRuleMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.convertRuleMessageBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.convertRuleMessageBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.convertRuleMessageBox.Location = new System.Drawing.Point(14, 132);
            this.convertRuleMessageBox.Multiline = true;
            this.convertRuleMessageBox.Name = "convertRuleMessageBox";
            this.convertRuleMessageBox.ReadOnly = true;
            this.convertRuleMessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.convertRuleMessageBox.Size = new System.Drawing.Size(416, 52);
            this.convertRuleMessageBox.TabIndex = 25;
            // 
            // errorMessageBox
            // 
            this.errorMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.errorMessageBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.errorMessageBox.Location = new System.Drawing.Point(14, 190);
            this.errorMessageBox.Multiline = true;
            this.errorMessageBox.Name = "errorMessageBox";
            this.errorMessageBox.ReadOnly = true;
            this.errorMessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.errorMessageBox.Size = new System.Drawing.Size(416, 52);
            this.errorMessageBox.TabIndex = 26;
            // 
            // OpenResourceFileAndTraceLogFileOpenForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(442, 283);
            this.Controls.Add(this.errorMessageBox);
            this.Controls.Add(this.convertRuleMessageBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.savePathRefButton);
            this.Controls.Add(this.savePathTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.convertRuleComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.traceLogFileRefButton);
            this.Controls.Add(this.traceLogFilePathTextBox);
            this.Controls.Add(this.resourceFileRefButton);
            this.Controls.Add(this.resourceFilePathTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(240, 280);
            this.Name = "OpenResourceFileAndTraceLogFileOpenForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "新規作成ウィザード";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog resourceFileOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog traceLogFileOpenFileDialog;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox convertRuleComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button traceLogFileRefButton;
        private System.Windows.Forms.TextBox traceLogFilePathTextBox;
        private System.Windows.Forms.Button resourceFileRefButton;
        private System.Windows.Forms.TextBox resourceFilePathTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button savePathRefButton;
        private System.Windows.Forms.TextBox savePathTextBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox convertRuleMessageBox;
        private System.Windows.Forms.TextBox errorMessageBox;
    }
}