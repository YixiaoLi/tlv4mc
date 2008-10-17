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
            this.formatComboBox = new System.Windows.Forms.ComboBox();
            this.messageBox = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.traceLogFileRefButton = new System.Windows.Forms.Button();
            this.traceLogFilePathTextBox = new System.Windows.Forms.TextBox();
            this.resourceFileRefButton = new System.Windows.Forms.Button();
            this.resourceFilePathTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(294, 257);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "フォーマット";
            // 
            // resourceFileOpenFileDialog
            // 
            this.resourceFileOpenFileDialog.DefaultExt = "res";
            this.resourceFileOpenFileDialog.Filter = "リソースファイル(*.res)|*.res";
            // 
            // traceLogFileOpenFileDialog
            // 
            this.traceLogFileOpenFileDialog.DefaultExt = "log";
            this.traceLogFileOpenFileDialog.Filter = "トレースログファイル(*.log)|*.log";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(375, 257);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // formatComboBox
            // 
            this.formatComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.formatComboBox.FormattingEnabled = true;
            this.formatComboBox.Location = new System.Drawing.Point(111, 60);
            this.formatComboBox.Name = "formatComboBox";
            this.formatComboBox.Size = new System.Drawing.Size(258, 20);
            this.formatComboBox.TabIndex = 4;
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.messageBox.Location = new System.Drawing.Point(12, 83);
            this.messageBox.Name = "messageBox";
            this.messageBox.Padding = new System.Windows.Forms.Padding(5);
            this.messageBox.Size = new System.Drawing.Size(438, 171);
            this.messageBox.TabIndex = 17;
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
            this.traceLogFileRefButton.Location = new System.Drawing.Point(375, 33);
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
            this.traceLogFilePathTextBox.Location = new System.Drawing.Point(111, 35);
            this.traceLogFilePathTextBox.Name = "traceLogFilePathTextBox";
            this.traceLogFilePathTextBox.Size = new System.Drawing.Size(258, 19);
            this.traceLogFilePathTextBox.TabIndex = 2;
            // 
            // resourceFileRefButton
            // 
            this.resourceFileRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceFileRefButton.Location = new System.Drawing.Point(375, 4);
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
            this.resourceFilePathTextBox.Location = new System.Drawing.Point(111, 6);
            this.resourceFilePathTextBox.Name = "resourceFilePathTextBox";
            this.resourceFilePathTextBox.Size = new System.Drawing.Size(258, 19);
            this.resourceFilePathTextBox.TabIndex = 0;
            // 
            // OpenResourceFileAndTraceLogFileOpenForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(462, 292);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.formatComboBox);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.traceLogFileRefButton);
            this.Controls.Add(this.traceLogFilePathTextBox);
            this.Controls.Add(this.resourceFileRefButton);
            this.Controls.Add(this.resourceFilePathTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(245, 170);
            this.Name = "OpenResourceFileAndTraceLogFileOpenForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ファイルを開く";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog resourceFileOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog traceLogFileOpenFileDialog;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox formatComboBox;
        private System.Windows.Forms.Label messageBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button traceLogFileRefButton;
        private System.Windows.Forms.TextBox traceLogFilePathTextBox;
        private System.Windows.Forms.Button resourceFileRefButton;
        private System.Windows.Forms.TextBox resourceFilePathTextBox;
    }
}