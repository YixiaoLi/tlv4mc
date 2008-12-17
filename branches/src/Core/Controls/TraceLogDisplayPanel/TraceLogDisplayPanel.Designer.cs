namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class TraceLogDisplayPanel
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
			this.components = new System.ComponentModel.Container();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.treeGridView = new NU.OJL.MPRTOS.TLV.Third.TreeGridView();
			this.viewingTimeRangeToolStrip = new System.Windows.Forms.ToolStrip();
			this.viewingTimeRangeLabel = new System.Windows.Forms.ToolStripLabel();
			this.viewingTimeRangeFromTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.viewingTimeRangeFromScaleLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.viewingTimeRangeToTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.viewingTimeRangeToScaleLabel = new System.Windows.Forms.ToolStripLabel();
			this.topTimeLineScale = new NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineScale();
			this.bottomTimeLineScale = new NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineScale();
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.viewingTimeRangeToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// hScrollBar
			// 
			this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar.Location = new System.Drawing.Point(1, 85);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(404, 16);
			this.hScrollBar.TabIndex = 3;
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.BottomToolStripPanel
			// 
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.bottomTimeLineScale);
			this.toolStripContainer.ContentPanel.Controls.Add(this.topTimeLineScale);
			this.toolStripContainer.ContentPanel.Controls.Add(this.hScrollBar);
			this.toolStripContainer.ContentPanel.Controls.Add(this.treeGridView);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(406, 351);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(406, 398);
			this.toolStripContainer.TabIndex = 4;
			this.toolStripContainer.Text = "toolStripContainer";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.viewingTimeRangeToolStrip);
			// 
			// statusStrip
			// 
			this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip.Location = new System.Drawing.Point(0, 0);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(406, 22);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 4;
			// 
			// treeGridView
			// 
			this.treeGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeGridView.Location = new System.Drawing.Point(1, 31);
			this.treeGridView.Name = "treeGridView";
			this.treeGridView.Size = new System.Drawing.Size(404, 23);
			this.treeGridView.TabIndex = 0;
			// 
			// viewingTimeRangeToolStrip
			// 
			this.viewingTimeRangeToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.viewingTimeRangeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewingTimeRangeLabel,
            this.viewingTimeRangeFromTextBox,
            this.viewingTimeRangeFromScaleLabel,
            this.toolStripLabel4,
            this.viewingTimeRangeToTextBox,
            this.viewingTimeRangeToScaleLabel});
			this.viewingTimeRangeToolStrip.Location = new System.Drawing.Point(3, 0);
			this.viewingTimeRangeToolStrip.Name = "viewingTimeRangeToolStrip";
			this.viewingTimeRangeToolStrip.Size = new System.Drawing.Size(183, 25);
			this.viewingTimeRangeToolStrip.TabIndex = 1;
			// 
			// viewingTimeRangeLabel
			// 
			this.viewingTimeRangeLabel.Name = "viewingTimeRangeLabel";
			this.viewingTimeRangeLabel.Size = new System.Drawing.Size(51, 22);
			this.viewingTimeRangeLabel.Text = "表示領域";
			// 
			// viewingTimeRangeFromTextBox
			// 
			this.viewingTimeRangeFromTextBox.AutoSize = false;
			this.viewingTimeRangeFromTextBox.Enabled = false;
			this.viewingTimeRangeFromTextBox.Name = "viewingTimeRangeFromTextBox";
			this.viewingTimeRangeFromTextBox.Size = new System.Drawing.Size(50, 25);
			// 
			// viewingTimeRangeFromScaleLabel
			// 
			this.viewingTimeRangeFromScaleLabel.Name = "viewingTimeRangeFromScaleLabel";
			this.viewingTimeRangeFromScaleLabel.Size = new System.Drawing.Size(0, 22);
			// 
			// toolStripLabel4
			// 
			this.toolStripLabel4.Name = "toolStripLabel4";
			this.toolStripLabel4.Size = new System.Drawing.Size(18, 22);
			this.toolStripLabel4.Text = "～";
			// 
			// viewingTimeRangeToTextBox
			// 
			this.viewingTimeRangeToTextBox.AutoSize = false;
			this.viewingTimeRangeToTextBox.Enabled = false;
			this.viewingTimeRangeToTextBox.Name = "viewingTimeRangeToTextBox";
			this.viewingTimeRangeToTextBox.Size = new System.Drawing.Size(50, 25);
			// 
			// viewingTimeRangeToScaleLabel
			// 
			this.viewingTimeRangeToScaleLabel.Name = "viewingTimeRangeToScaleLabel";
			this.viewingTimeRangeToScaleLabel.Size = new System.Drawing.Size(0, 22);
			// 
			// topTimeLineScale
			// 
			this.topTimeLineScale.BackColor = System.Drawing.Color.Black;
			this.topTimeLineScale.DynamicTimeRangeChange = true;
			this.topTimeLineScale.Font = new System.Drawing.Font("Courier New", 8F);
			this.topTimeLineScale.Location = new System.Drawing.Point(1, 1);
			this.topTimeLineScale.Name = "topTimeLineScale";
			this.topTimeLineScale.ScaleMarkDirection = NU.OJL.MPRTOS.TLV.Core.Controls.ScaleMarkDirection.Bottom;
			this.topTimeLineScale.Size = new System.Drawing.Size(404, 30);
			this.topTimeLineScale.TabIndex = 4;
			// 
			// bottomTimeLineScale
			// 
			this.bottomTimeLineScale.BackColor = System.Drawing.Color.Black;
			this.bottomTimeLineScale.DynamicTimeRangeChange = true;
			this.bottomTimeLineScale.Font = new System.Drawing.Font("Courier New", 8F);
			this.bottomTimeLineScale.Location = new System.Drawing.Point(1, 54);
			this.bottomTimeLineScale.Name = "bottomTimeLineScale";
			this.bottomTimeLineScale.ScaleMarkDirection = NU.OJL.MPRTOS.TLV.Core.Controls.ScaleMarkDirection.Top;
			this.bottomTimeLineScale.Size = new System.Drawing.Size(404, 30);
			this.bottomTimeLineScale.TabIndex = 5;
			// 
			// TraceLogDisplayPanel
			// 
			this.Controls.Add(this.toolStripContainer);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Name = "TraceLogDisplayPanel";
			this.Size = new System.Drawing.Size(406, 398);
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.viewingTimeRangeToolStrip.ResumeLayout(false);
			this.viewingTimeRangeToolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private NU.OJL.MPRTOS.TLV.Third.TreeGridView treeGridView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStrip viewingTimeRangeToolStrip;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeLabel;
		private System.Windows.Forms.ToolStripTextBox viewingTimeRangeFromTextBox;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeFromScaleLabel;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private System.Windows.Forms.ToolStripTextBox viewingTimeRangeToTextBox;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeToScaleLabel;
		private TimeLineScale bottomTimeLineScale;
		private TimeLineScale topTimeLineScale;


	}
}
