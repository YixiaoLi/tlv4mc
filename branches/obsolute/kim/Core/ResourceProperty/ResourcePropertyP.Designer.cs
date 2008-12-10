namespace NU.OJL.MPRTOS.TLV.Core.ResourceProperty
{
    partial class ResourcePropertyP
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.proptyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // proptyGrid
            // 
            this.proptyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proptyGrid.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.proptyGrid.HelpVisible = false;
            this.proptyGrid.Location = new System.Drawing.Point(0, 0);
            this.proptyGrid.Name = "proptyGrid";
            this.proptyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.proptyGrid.Size = new System.Drawing.Size(292, 266);
            this.proptyGrid.TabIndex = 0;
            this.proptyGrid.ToolbarVisible = false;
            // 
            // ResProperty
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.proptyGrid);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.HideOnClose = true;
            this.Name = "ResProperty";
            this.TabText = "プロパティ";
            this.Text = "プロパティ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid proptyGrid;
    }
}
