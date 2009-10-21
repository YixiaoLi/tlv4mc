namespace NU.OJL.MPRTOS.TLV.Core.Controls.Forms
{
    partial class MessageForm
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
            this.detail = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.error = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errroIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errroIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // detail
            // 
            this.detail.Location = new System.Drawing.Point(14, 72);
            this.detail.Multiline = true;
            this.detail.Name = "detail";
            this.detail.ReadOnly = true;
            this.detail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.detail.Size = new System.Drawing.Size(376, 97);
            this.detail.TabIndex = 0;
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(315, 179);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 1;
            this.button.Text = "OK";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button1_Click);
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.error.Location = new System.Drawing.Point(52, 22);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(55, 19);
            this.error.TabIndex = 2;
            this.error.Text = "label1";
            this.error.Click += new System.EventHandler(this.error_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "詳細:";
            // 
            // errroIcon
            // 
            this.errroIcon.Location = new System.Drawing.Point(14, 9);
            this.errroIcon.Name = "errroIcon";
            this.errroIcon.Size = new System.Drawing.Size(32, 32);
            this.errroIcon.TabIndex = 4;
            this.errroIcon.TabStop = false;
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 214);
            this.Controls.Add(this.errroIcon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.error);
            this.Controls.Add(this.button);
            this.Controls.Add(this.detail);
            this.Name = "MessageForm";
            this.Text = "MessageForm";
            this.Load += new System.EventHandler(this.MessageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errroIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox detail;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox errroIcon;
    }
}