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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.titleLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.buildLabel = new System.Windows.Forms.Label();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.copyrightLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("MS UI Gothic", 20F);
            this.titleLabel.Location = new System.Drawing.Point(4, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(228, 27);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "TraceLogVisualizer";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.versionLabel.Location = new System.Drawing.Point(14, 47);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(106, 16);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "バージョン 1.0rc";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(240, 150);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // buildLabel
            // 
            this.buildLabel.AutoSize = true;
            this.buildLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.buildLabel.Location = new System.Drawing.Point(14, 71);
            this.buildLabel.Name = "buildLabel";
            this.buildLabel.Size = new System.Drawing.Size(81, 16);
            this.buildLabel.TabIndex = 3;
            this.buildLabel.Text = "ビルド番号 ";
            // 
            // logoPicture
            // 
            this.logoPicture.Image = ((System.Drawing.Image)(resources.GetObject("logoPicture.Image")));
            this.logoPicture.Location = new System.Drawing.Point(240, 9);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(286, 88);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logoPicture.TabIndex = 4;
            this.logoPicture.TabStop = false;
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.AutoSize = true;
            this.copyrightLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.copyrightLabel.Location = new System.Drawing.Point(14, 100);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(512, 32);
            this.copyrightLabel.TabIndex = 5;
            this.copyrightLabel.Text = "Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory\r\n           " +
                " Graduate School of Information Science, Nagoya Univ., JAPAN";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 185);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.logoPicture);
            this.Controls.Add(this.buildLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TLVについて";
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label buildLabel;
        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.Label copyrightLabel;
    }
}