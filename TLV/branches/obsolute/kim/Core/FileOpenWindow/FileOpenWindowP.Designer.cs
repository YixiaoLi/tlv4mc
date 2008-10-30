namespace NU.OJL.MPRTOS.TLV.Core.FileOpenWindow
{
    partial class FileOpenWindowP
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.lblLogFile = new System.Windows.Forms.Label();
            this.lblResourceFile = new System.Windows.Forms.Label();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.txtResourceFilePath = new System.Windows.Forms.TextBox();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.btnOpenResource = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(256, 192);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(347, 192);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.lblLogFile);
            this.groupBox.Controls.Add(this.lblResourceFile);
            this.groupBox.Controls.Add(this.txtLogFilePath);
            this.groupBox.Controls.Add(this.txtResourceFilePath);
            this.groupBox.Controls.Add(this.btnOpenLog);
            this.groupBox.Controls.Add(this.btnOpenResource);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(413, 167);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            // 
            // lblLogFile
            // 
            this.lblLogFile.AutoSize = true;
            this.lblLogFile.Location = new System.Drawing.Point(6, 85);
            this.lblLogFile.Name = "lblLogFile";
            this.lblLogFile.Size = new System.Drawing.Size(93, 12);
            this.lblLogFile.TabIndex = 5;
            this.lblLogFile.Text = "トレースログファイル";
            // 
            // lblResourceFile
            // 
            this.lblResourceFile.AutoSize = true;
            this.lblResourceFile.Location = new System.Drawing.Point(6, 15);
            this.lblResourceFile.Name = "lblResourceFile";
            this.lblResourceFile.Size = new System.Drawing.Size(74, 12);
            this.lblResourceFile.TabIndex = 4;
            this.lblResourceFile.Text = "リソースファイル";
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Location = new System.Drawing.Point(6, 100);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.ReadOnly = true;
            this.txtLogFilePath.Size = new System.Drawing.Size(313, 19);
            this.txtLogFilePath.TabIndex = 3;
            // 
            // txtResourceFilePath
            // 
            this.txtResourceFilePath.Location = new System.Drawing.Point(6, 31);
            this.txtResourceFilePath.Name = "txtResourceFilePath";
            this.txtResourceFilePath.ReadOnly = true;
            this.txtResourceFilePath.Size = new System.Drawing.Size(313, 19);
            this.txtResourceFilePath.TabIndex = 2;
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Location = new System.Drawing.Point(325, 97);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(75, 23);
            this.btnOpenLog.TabIndex = 1;
            this.btnOpenLog.Text = "参照";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            // 
            // btnOpenResource
            // 
            this.btnOpenResource.Location = new System.Drawing.Point(325, 28);
            this.btnOpenResource.Name = "btnOpenResource";
            this.btnOpenResource.Size = new System.Drawing.Size(75, 23);
            this.btnOpenResource.TabIndex = 0;
            this.btnOpenResource.Text = "参照";
            this.btnOpenResource.UseVisualStyleBackColor = true;
            this.btnOpenResource.Click += new System.EventHandler(this.btnOpenResource_Click);
            // 
            // FileOpenWindowP
            // 
            this.ClientSize = new System.Drawing.Size(435, 227);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "FileOpenWindowP";
            this.TabText = "FileOpenWindow";
            this.Text = "FileOpenWindow";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.TextBox txtLogFilePath;
        private System.Windows.Forms.TextBox txtResourceFilePath;
        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.Button btnOpenResource;
        private System.Windows.Forms.Label lblLogFile;
        private System.Windows.Forms.Label lblResourceFile;
    }
}
