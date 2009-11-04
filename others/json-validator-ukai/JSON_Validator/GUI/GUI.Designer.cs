namespace GUI
{
    partial class JSON_GUI_Validator
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
            this.JSONFile = new System.Windows.Forms.TextBox();
            this.JSONSchemaFile = new System.Windows.Forms.TextBox();
            this.JSONButton = new System.Windows.Forms.Button();
            this.SchemaButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ValidateButton = new System.Windows.Forms.Button();
            this.ResultReport = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // JSONFile
            // 
            this.JSONFile.Location = new System.Drawing.Point(12, 94);
            this.JSONFile.Name = "JSONFile";
            this.JSONFile.Size = new System.Drawing.Size(378, 19);
            this.JSONFile.TabIndex = 1;
            this.JSONFile.TextChanged += new System.EventHandler(this.JSONFile_TextChanged);
            // 
            // JSONSchemaFile
            // 
            this.JSONSchemaFile.Location = new System.Drawing.Point(12, 32);
            this.JSONSchemaFile.Name = "JSONSchemaFile";
            this.JSONSchemaFile.Size = new System.Drawing.Size(378, 19);
            this.JSONSchemaFile.TabIndex = 4;
            this.JSONSchemaFile.TextChanged += new System.EventHandler(this.JSONSchemaFile_TextChanged);
            // 
            // JSONButton
            // 
            this.JSONButton.Location = new System.Drawing.Point(396, 92);
            this.JSONButton.Name = "JSONButton";
            this.JSONButton.Size = new System.Drawing.Size(66, 23);
            this.JSONButton.TabIndex = 5;
            this.JSONButton.Text = "参照";
            this.JSONButton.UseVisualStyleBackColor = true;
            this.JSONButton.Click += new System.EventHandler(this.JSONButton_Click);
            // 
            // SchemaButton
            // 
            this.SchemaButton.Location = new System.Drawing.Point(396, 30);
            this.SchemaButton.Name = "SchemaButton";
            this.SchemaButton.Size = new System.Drawing.Size(66, 23);
            this.SchemaButton.TabIndex = 6;
            this.SchemaButton.Text = "参照";
            this.SchemaButton.UseVisualStyleBackColor = true;
            this.SchemaButton.Click += new System.EventHandler(this.SchemaButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "JSONファイル";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "JSONスキーマファイル";
            // 
            // ValidateButton
            // 
            this.ValidateButton.Location = new System.Drawing.Point(212, 119);
            this.ValidateButton.Name = "ValidateButton";
            this.ValidateButton.Size = new System.Drawing.Size(67, 25);
            this.ValidateButton.TabIndex = 9;
            this.ValidateButton.Text = "検証開始";
            this.ValidateButton.UseVisualStyleBackColor = true;
            this.ValidateButton.Click += new System.EventHandler(this.ValidateButton_Click);
            // 
            // ResultReport
            // 
            this.ResultReport.Location = new System.Drawing.Point(14, 196);
            this.ResultReport.Name = "ResultReport";
            this.ResultReport.Size = new System.Drawing.Size(474, 137);
            this.ResultReport.TabIndex = 10;
            this.ResultReport.Text = "";
            this.ResultReport.TextChanged += new System.EventHandler(this.ResultReport_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(12, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "検証結果";
            // 
            // JSON_GUI_Validator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 345);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ResultReport);
            this.Controls.Add(this.ValidateButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SchemaButton);
            this.Controls.Add(this.JSONButton);
            this.Controls.Add(this.JSONSchemaFile);
            this.Controls.Add(this.JSONFile);
            this.Name = "JSON_GUI_Validator";
            this.Text = "JSON_Validator";
            this.Load += new System.EventHandler(this.JSON_GUI_Validator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox JSONFile;
        private System.Windows.Forms.TextBox JSONSchemaFile;
        private System.Windows.Forms.Button JSONButton;
        private System.Windows.Forms.Button SchemaButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ValidateButton;
        private System.Windows.Forms.RichTextBox ResultReport;
        private System.Windows.Forms.Label label3;


    }
}

