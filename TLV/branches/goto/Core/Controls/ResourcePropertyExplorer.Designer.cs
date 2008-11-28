namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class ResourcePropertyExplorer
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
			this._treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// _treeView
			// 
			this._treeView.CheckBoxes = true;
			this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._treeView.ImageIndex = 0;
			this._treeView.ImageList = this.imageList;
			this._treeView.Location = new System.Drawing.Point(0, 0);
			this._treeView.Name = "_treeView";
			this._treeView.SelectedImageIndex = 0;
			this._treeView.Size = new System.Drawing.Size(238, 264);
			this._treeView.TabIndex = 0;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ResourceTypeExplorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._treeView);
			this.Name = "ResourceTypeExplorer";
			this.Size = new System.Drawing.Size(238, 264);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView _treeView;
		private System.Windows.Forms.ImageList imageList;


	}
}
