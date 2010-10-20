namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    partial class StatsViewerTmp
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
            this.statsVewier1 = new NU.OJL.MPRTOS.TLV.Core.Controls.StatsVewier();
            this.SuspendLayout();
            // 
            // statsVewier1
            // 
            this.statsVewier1.Location = new System.Drawing.Point(12, 12);
            this.statsVewier1.Name = "statsVewier1";
            this.statsVewier1.Size = new System.Drawing.Size(284, 290);
            this.statsVewier1.TabIndex = 0;
            // 
            // StatsViewerTmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 310);
            this.Controls.Add(this.statsVewier1);
            this.Name = "StatsViewerTmp";
            this.Text = "StatsViewerTmp";
            this.Load += new System.EventHandler(this.StatsViewerTmp_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StatsViewerTmp_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private StatsVewier statsVewier1;
    }
}