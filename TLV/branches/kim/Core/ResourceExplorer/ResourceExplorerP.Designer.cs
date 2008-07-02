namespace NU.OJL.MPRTOS.TLV.Core.ResourceExplorer
{
    partial class ResourceExplorerP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceExplorerP));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPrcPage = new System.Windows.Forms.TabPage();
            this.prcView = new System.Windows.Forms.TreeView();
            this.tabClsPage = new System.Windows.Forms.TabPage();
            this.clsView = new System.Windows.Forms.TreeView();
            this.tabResPage = new System.Windows.Forms.TabPage();
            this.resView = new System.Windows.Forms.TreeView();
            this.tabControl.SuspendLayout();
            this.tabPrcPage.SuspendLayout();
            this.tabClsPage.SuspendLayout();
            this.tabResPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPrcPage);
            this.tabControl.Controls.Add(this.tabClsPage);
            this.tabControl.Controls.Add(this.tabResPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(299, 353);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabPrcPage
            // 
            this.tabPrcPage.Controls.Add(this.prcView);
            this.tabPrcPage.Location = new System.Drawing.Point(4, 21);
            this.tabPrcPage.Name = "tabPrcPage";
            this.tabPrcPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPrcPage.Size = new System.Drawing.Size(291, 328);
            this.tabPrcPage.TabIndex = 0;
            this.tabPrcPage.Text = "プロセッサ";
            this.tabPrcPage.UseVisualStyleBackColor = true;
            // 
            // prcView
            // 
            this.prcView.CheckBoxes = true;
            this.prcView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prcView.Location = new System.Drawing.Point(3, 3);
            this.prcView.Name = "prcView";
            this.prcView.Size = new System.Drawing.Size(285, 322);
            this.prcView.TabIndex = 0;
            this.prcView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.prcView_AfterCheck);
            this.prcView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.prcView_AfterSelect);
            // 
            // tabClsPage
            // 
            this.tabClsPage.Controls.Add(this.clsView);
            this.tabClsPage.Location = new System.Drawing.Point(4, 21);
            this.tabClsPage.Name = "tabClsPage";
            this.tabClsPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabClsPage.Size = new System.Drawing.Size(291, 328);
            this.tabClsPage.TabIndex = 1;
            this.tabClsPage.Text = "クラス";
            this.tabClsPage.UseVisualStyleBackColor = true;
            // 
            // clsView
            // 
            this.clsView.CheckBoxes = true;
            this.clsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clsView.Location = new System.Drawing.Point(3, 3);
            this.clsView.Name = "clsView";
            this.clsView.Size = new System.Drawing.Size(285, 322);
            this.clsView.TabIndex = 0;
            this.clsView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.clsView_AfterCheck);
            this.clsView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.clsView_AfterSelect);
            // 
            // tabResPage
            // 
            this.tabResPage.Controls.Add(this.resView);
            this.tabResPage.Location = new System.Drawing.Point(4, 21);
            this.tabResPage.Name = "tabResPage";
            this.tabResPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabResPage.Size = new System.Drawing.Size(291, 328);
            this.tabResPage.TabIndex = 2;
            this.tabResPage.Text = "リソース";
            this.tabResPage.UseVisualStyleBackColor = true;
            // 
            // resView
            // 
            this.resView.CheckBoxes = true;
            this.resView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resView.Location = new System.Drawing.Point(3, 3);
            this.resView.Name = "resView";
            this.resView.Size = new System.Drawing.Size(285, 322);
            this.resView.TabIndex = 0;
            this.resView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.resView_AfterCheck);
            this.resView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.resView_AfterSelect);
            // 
            // ResExplorer
            // 
            this.ClientSize = new System.Drawing.Size(299, 353);
            this.Controls.Add(this.tabControl);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResExplorer";
            this.TabText = "リソース エクスプローラ";
            this.Text = "リソース エクスプローラ";
            this.tabControl.ResumeLayout(false);
            this.tabPrcPage.ResumeLayout(false);
            this.tabClsPage.ResumeLayout(false);
            this.tabResPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPrcPage;
        private System.Windows.Forms.TreeView prcView;
        private System.Windows.Forms.TabPage tabClsPage;
        private System.Windows.Forms.TreeView clsView;
        private System.Windows.Forms.TabPage tabResPage;
        private System.Windows.Forms.TreeView resView;
    }
}
