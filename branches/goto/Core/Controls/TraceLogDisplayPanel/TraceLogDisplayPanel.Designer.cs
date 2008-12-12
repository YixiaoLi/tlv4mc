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
			this.treeGridView = new NU.OJL.MPRTOS.TLV.Third.TreeGridView();
			this.bottomTimeLineScale = new NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineScale();
			this.topTimeLineScale = new NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineScale();
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
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
			// bottomTimeLineScale
			// 
			this.bottomTimeLineScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.bottomTimeLineScale.BackColor = System.Drawing.Color.Black;
			this.bottomTimeLineScale.Location = new System.Drawing.Point(1, 54);
			this.bottomTimeLineScale.Name = "bottomTimeLineScale";
			this.bottomTimeLineScale.Size = new System.Drawing.Size(404, 30);
			this.bottomTimeLineScale.TabIndex = 2;
			// 
			// topTimeLineScale
			// 
			this.topTimeLineScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.topTimeLineScale.BackColor = System.Drawing.Color.Black;
			this.topTimeLineScale.Location = new System.Drawing.Point(1, 1);
			this.topTimeLineScale.Name = "topTimeLineScale";
			this.topTimeLineScale.Size = new System.Drawing.Size(404, 30);
			this.topTimeLineScale.TabIndex = 1;
			// 
			// hScrollBar
			// 
			this.hScrollBar.Location = new System.Drawing.Point(1, 84);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(404, 16);
			this.hScrollBar.TabIndex = 3;
			// 
			// TraceLogDisplayPanel
			// 
			this.Controls.Add(this.hScrollBar);
			this.Controls.Add(this.bottomTimeLineScale);
			this.Controls.Add(this.topTimeLineScale);
			this.Controls.Add(this.treeGridView);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Name = "TraceLogDisplayPanel";
			this.Size = new System.Drawing.Size(406, 398);
			this.ResumeLayout(false);

		}

		#endregion

		private NU.OJL.MPRTOS.TLV.Third.TreeGridView treeGridView;
		private System.Windows.Forms.ImageList imageList;
		private TimeLineScale topTimeLineScale;
		private TimeLineScale bottomTimeLineScale;
		private System.Windows.Forms.HScrollBar hScrollBar;


	}
}
