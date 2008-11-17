namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class ResourceTypeExplorer
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
			this._treeView = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// _treeView
			// 
			this._treeView.CheckBoxes = true;
			this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._treeView.Location = new System.Drawing.Point(0, 0);
			this._treeView.Name = "_treeView";
			this._treeView.Size = new System.Drawing.Size(238, 264);
			this._treeView.TabIndex = 0;
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


	}
}
