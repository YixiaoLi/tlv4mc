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
			this.infoPanel = new System.Windows.Forms.Panel();
			this.viewableSpanLabel = new System.Windows.Forms.Label();
			this.viewableSpanTextBox = new System.Windows.Forms.TextBox();
			this.bottomTimeLineScale = new NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineScale();
			this.topTimeLineScale = new NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineScale();
			this.treeGridView = new NU.OJL.MPRTOS.TLV.Third.TreeGridView();
			this.viewingTimeRangeToolStrip = new System.Windows.Forms.ToolStrip();
			this.viewingTimeRangeLabel = new System.Windows.Forms.ToolStripLabel();
			this.viewingTimeRangeFromTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.viewingTimeRangeFromScaleLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.viewingTimeRangeToTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.viewingTimeRangeToScaleLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.infoPanel.SuspendLayout();
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
			this.hScrollBar.Location = new System.Drawing.Point(245, 54);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(525, 16);
			this.hScrollBar.TabIndex = 3;
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.infoPanel);
			this.toolStripContainer.ContentPanel.Controls.Add(this.bottomTimeLineScale);
			this.toolStripContainer.ContentPanel.Controls.Add(this.topTimeLineScale);
			this.toolStripContainer.ContentPanel.Controls.Add(this.hScrollBar);
			this.toolStripContainer.ContentPanel.Controls.Add(this.treeGridView);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(773, 373);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(773, 398);
			this.toolStripContainer.TabIndex = 4;
			this.toolStripContainer.Text = "toolStripContainer";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.viewingTimeRangeToolStrip);
			// 
			// infoPanel
			// 
			this.infoPanel.Controls.Add(this.viewableSpanLabel);
			this.infoPanel.Controls.Add(this.viewableSpanTextBox);
			this.infoPanel.Location = new System.Drawing.Point(1, 1);
			this.infoPanel.Name = "infoPanel";
			this.infoPanel.Size = new System.Drawing.Size(244, 20);
			this.infoPanel.TabIndex = 6;
			// 
			// viewableSpanLabel
			// 
			this.viewableSpanLabel.AutoSize = true;
			this.viewableSpanLabel.Location = new System.Drawing.Point(4, 4);
			this.viewableSpanLabel.Name = "viewableSpanLabel";
			this.viewableSpanLabel.Size = new System.Drawing.Size(87, 12);
			this.viewableSpanLabel.TabIndex = 1;
			this.viewableSpanLabel.Text = "表示可能領域 : ";
			// 
			// viewableSpanTextBox
			// 
			this.viewableSpanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.viewableSpanTextBox.Location = new System.Drawing.Point(92, 4);
			this.viewableSpanTextBox.Name = "viewableSpanTextBox";
			this.viewableSpanTextBox.ReadOnly = true;
			this.viewableSpanTextBox.Size = new System.Drawing.Size(45, 12);
			this.viewableSpanTextBox.TabIndex = 0;
			// 
			// bottomTimeLineScale
			// 
			this.bottomTimeLineScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.bottomTimeLineScale.BackColor = System.Drawing.Color.Black;
			this.bottomTimeLineScale.Font = new System.Drawing.Font("Courier New", 8F);
			this.bottomTimeLineScale.Location = new System.Drawing.Point(245, 34);
			this.bottomTimeLineScale.Name = "bottomTimeLineScale";
			this.bottomTimeLineScale.ScaleMarkDirection = NU.OJL.MPRTOS.TLV.Core.Controls.ScaleMarkDirection.Top;
			this.bottomTimeLineScale.Size = new System.Drawing.Size(527, 20);
			this.bottomTimeLineScale.TabIndex = 5;
			this.bottomTimeLineScale.TimeLine = null;
			// 
			// topTimeLineScale
			// 
			this.topTimeLineScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.topTimeLineScale.BackColor = System.Drawing.Color.Black;
			this.topTimeLineScale.Font = new System.Drawing.Font("Courier New", 8F);
			this.topTimeLineScale.Location = new System.Drawing.Point(245, 1);
			this.topTimeLineScale.Name = "topTimeLineScale";
			this.topTimeLineScale.ScaleMarkDirection = NU.OJL.MPRTOS.TLV.Core.Controls.ScaleMarkDirection.Bottom;
			this.topTimeLineScale.Size = new System.Drawing.Size(527, 20);
			this.topTimeLineScale.TabIndex = 4;
			this.topTimeLineScale.TimeLine = null;
			// 
			// treeGridView
			// 
			this.treeGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeGridView.Location = new System.Drawing.Point(1, 21);
			this.treeGridView.Name = "treeGridView";
			this.treeGridView.Size = new System.Drawing.Size(771, 13);
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
			// TraceLogDisplayPanel
			// 
			this.Controls.Add(this.toolStripContainer);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Name = "TraceLogDisplayPanel";
			this.Size = new System.Drawing.Size(773, 398);
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.infoPanel.ResumeLayout(false);
			this.infoPanel.PerformLayout();
			this.viewingTimeRangeToolStrip.ResumeLayout(false);
			this.viewingTimeRangeToolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private NU.OJL.MPRTOS.TLV.Third.TreeGridView treeGridView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.ToolStrip viewingTimeRangeToolStrip;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeLabel;
		private System.Windows.Forms.ToolStripTextBox viewingTimeRangeFromTextBox;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeFromScaleLabel;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private System.Windows.Forms.ToolStripTextBox viewingTimeRangeToTextBox;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeToScaleLabel;
		private TimeLineScale bottomTimeLineScale;
		private TimeLineScale topTimeLineScale;
		private System.Windows.Forms.Panel infoPanel;
		private System.Windows.Forms.TextBox viewableSpanTextBox;
		private System.Windows.Forms.Label viewableSpanLabel;


	}
}
