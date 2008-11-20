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
			this.treeGridView = new NU.OJL.MPRTOS.TLV.Third.TreeGridView();
			this.SuspendLayout();
			// 
			// treeGridView
			// 
			this.treeGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeGridView.Location = new System.Drawing.Point(1, 1);
			this.treeGridView.Name = "treeGridView";
			this.treeGridView.Size = new System.Drawing.Size(404, 23);
			this.treeGridView.TabIndex = 0;
			this.treeGridView.Text = "treeGridView1";
			// 
			// TraceLogDisplayPanel
			// 
			this.Controls.Add(this.treeGridView);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Name = "TraceLogDisplayPanel";
			this.Size = new System.Drawing.Size(406, 398);
			this.ResumeLayout(false);

		}

		#endregion

		private NU.OJL.MPRTOS.TLV.Third.TreeGridView treeGridView;


	}
}
