namespace NU.OJL.MPRTOS.TLV.Core.Controls.Forms
{
    partial class AboutForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Enabled = true;
            this.titleLabel.Font = new System.Drawing.Font("MS UI Gothic", 20F);
            this.titleLabel.Location = new System.Drawing.Point(25, 21);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(244, 27);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Trace Log Visualizer";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Enabled = true;
            this.versionLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.versionLabel.Location = new System.Drawing.Point(48, 58);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(111, 16);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "バージョン " + ApplicationData.Version;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(193, 118);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 148);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "AboutForm";
            this.Text = "AboutForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button okButton;
    }
}