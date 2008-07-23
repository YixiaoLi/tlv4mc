namespace NU.OJL.MPRTOS.TLV.Base
{
    partial class LabeledTrackBar
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_now = new System.Windows.Forms.Label();
            this.label_max = new System.Windows.Forms.Label();
            this.label_min = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.trackBar, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(200, 36);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // trackBar
            // 
            this.trackBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar.Location = new System.Drawing.Point(0, 0);
            this.trackBar.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(200, 20);
            this.trackBar.TabIndex = 1;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label_max);
            this.panel1.Controls.Add(this.label_min);
            this.panel1.Controls.Add(this.label_now);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 16);
            this.panel1.TabIndex = 2;
            // 
            // label_now
            // 
            this.label_now.BackColor = System.Drawing.Color.Transparent;
            this.label_now.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_now.Location = new System.Drawing.Point(0, 0);
            this.label_now.Margin = new System.Windows.Forms.Padding(0);
            this.label_now.Name = "label_now";
            this.label_now.Size = new System.Drawing.Size(200, 16);
            this.label_now.TabIndex = 2;
            this.label_now.Text = "nowValue";
            this.label_now.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_max
            // 
            this.label_max.AutoSize = true;
            this.label_max.BackColor = System.Drawing.Color.Transparent;
            this.label_max.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_max.Location = new System.Drawing.Point(148, 0);
            this.label_max.Margin = new System.Windows.Forms.Padding(0);
            this.label_max.Name = "label_max";
            this.label_max.Size = new System.Drawing.Size(52, 12);
            this.label_max.TabIndex = 1;
            this.label_max.Text = "minValue";
            this.label_max.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_min
            // 
            this.label_min.AutoSize = true;
            this.label_min.BackColor = System.Drawing.Color.Transparent;
            this.label_min.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_min.Location = new System.Drawing.Point(0, 0);
            this.label_min.Margin = new System.Windows.Forms.Padding(0);
            this.label_min.Name = "label_min";
            this.label_min.Size = new System.Drawing.Size(52, 12);
            this.label_min.TabIndex = 0;
            this.label_min.Text = "minValue";
            this.label_min.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabeledTrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(175, 36);
            this.Name = "LabeledTrackBar";
            this.Size = new System.Drawing.Size(200, 36);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_max;
        private System.Windows.Forms.Label label_now;
        private System.Windows.Forms.Label label_min;
    }
}
