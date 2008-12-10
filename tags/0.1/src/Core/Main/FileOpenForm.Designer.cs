namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    partial class FileOpenForm
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
            this.resourceTextBox = new System.Windows.Forms.TextBox();
            this.resourceRefButton = new System.Windows.Forms.Button();
            this.traceLogTextBox = new System.Windows.Forms.TextBox();
            this.traceLogRefButton = new System.Windows.Forms.Button();
            this.resourceLabel = new System.Windows.Forms.Label();
            this.traceLogLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resourceTextBox
            // 
            this.resourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceTextBox.Location = new System.Drawing.Point(111, 13);
            this.resourceTextBox.Name = "resourceTextBox";
            this.resourceTextBox.Size = new System.Drawing.Size(324, 19);
            this.resourceTextBox.TabIndex = 0;
            // 
            // resourceRefButton
            // 
            this.resourceRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceRefButton.Location = new System.Drawing.Point(441, 11);
            this.resourceRefButton.Name = "resourceRefButton";
            this.resourceRefButton.Size = new System.Drawing.Size(75, 23);
            this.resourceRefButton.TabIndex = 1;
            this.resourceRefButton.Text = "参照";
            this.resourceRefButton.UseVisualStyleBackColor = true;
            this.resourceRefButton.Click += new System.EventHandler(this.resourceRefButton_Click);
            // 
            // traceLogTextBox
            // 
            this.traceLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.traceLogTextBox.Location = new System.Drawing.Point(111, 43);
            this.traceLogTextBox.Name = "traceLogTextBox";
            this.traceLogTextBox.Size = new System.Drawing.Size(324, 19);
            this.traceLogTextBox.TabIndex = 2;
            // 
            // traceLogRefButton
            // 
            this.traceLogRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.traceLogRefButton.Location = new System.Drawing.Point(441, 41);
            this.traceLogRefButton.Name = "traceLogRefButton";
            this.traceLogRefButton.Size = new System.Drawing.Size(75, 23);
            this.traceLogRefButton.TabIndex = 3;
            this.traceLogRefButton.Text = "参照";
            this.traceLogRefButton.UseVisualStyleBackColor = true;
            this.traceLogRefButton.Click += new System.EventHandler(this.traceLogRefButton_Click);
            // 
            // resourceLabel
            // 
            this.resourceLabel.AutoSize = true;
            this.resourceLabel.Location = new System.Drawing.Point(12, 16);
            this.resourceLabel.Name = "resourceLabel";
            this.resourceLabel.Size = new System.Drawing.Size(74, 12);
            this.resourceLabel.TabIndex = 100;
            this.resourceLabel.Text = "リソースファイル";
            // 
            // traceLogLabel
            // 
            this.traceLogLabel.AutoSize = true;
            this.traceLogLabel.Location = new System.Drawing.Point(12, 46);
            this.traceLogLabel.Name = "traceLogLabel";
            this.traceLogLabel.Size = new System.Drawing.Size(93, 12);
            this.traceLogLabel.TabIndex = 100;
            this.traceLogLabel.Text = "トレースログファイル";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(441, 77);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(360, 77);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // FileOpenForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(528, 112);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.traceLogLabel);
            this.Controls.Add(this.resourceLabel);
            this.Controls.Add(this.traceLogRefButton);
            this.Controls.Add(this.traceLogTextBox);
            this.Controls.Add(this.resourceRefButton);
            this.Controls.Add(this.resourceTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileOpenForm";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ファイルを選択して下さい";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox resourceTextBox;
        private System.Windows.Forms.Button resourceRefButton;
        private System.Windows.Forms.TextBox traceLogTextBox;
        private System.Windows.Forms.Button traceLogRefButton;
        private System.Windows.Forms.Label resourceLabel;
        private System.Windows.Forms.Label traceLogLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
    }
}