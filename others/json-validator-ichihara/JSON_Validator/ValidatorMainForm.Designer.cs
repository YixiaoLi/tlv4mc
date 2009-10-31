namespace JSON_Validator
{
    partial class ValidatorMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.schemaFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SchemaAddButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.CheckButton = new System.Windows.Forms.Button();
            this.JsonAddButton = new System.Windows.Forms.Button();
            this.jsonFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // schemaFileDialog
            // 
            this.schemaFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            this.schemaFileDialog.FilterIndex = 2;
            this.schemaFileDialog.Title = "Schema file selection";
            this.schemaFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.schemaFileDialog_FileOk);
            // 
            // SchemaAddButton
            // 
            this.SchemaAddButton.Location = new System.Drawing.Point(205, 59);
            this.SchemaAddButton.Name = "SchemaAddButton";
            this.SchemaAddButton.Size = new System.Drawing.Size(75, 23);
            this.SchemaAddButton.TabIndex = 1;
            this.SchemaAddButton.Text = "add";
            this.SchemaAddButton.UseCompatibleTextRendering = true;
            this.SchemaAddButton.UseVisualStyleBackColor = true;
            this.SchemaAddButton.Click += new System.EventHandler(this.SchemaAddButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "C:\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Schema file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "JSON file";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 114);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(174, 19);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "C:\\";
            // 
            // CheckButton
            // 
            this.CheckButton.Location = new System.Drawing.Point(111, 158);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(75, 23);
            this.CheckButton.TabIndex = 4;
            this.CheckButton.Text = "check";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // JsonAddButton
            // 
            this.JsonAddButton.Location = new System.Drawing.Point(205, 112);
            this.JsonAddButton.Name = "JsonAddButton";
            this.JsonAddButton.Size = new System.Drawing.Size(75, 23);
            this.JsonAddButton.TabIndex = 3;
            this.JsonAddButton.Text = "add";
            this.JsonAddButton.UseVisualStyleBackColor = true;
            this.JsonAddButton.Click += new System.EventHandler(this.JsonAddButton_Click);
            // 
            // jsonFileDialog
            // 
            this.jsonFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            this.jsonFileDialog.FilterIndex = 2;
            this.jsonFileDialog.Title = "JSON file selection";
            this.jsonFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.jsonFileDialog_FileOk);
            // 
            // ValidatorMainForm
            // 
            this.AcceptButton = this.CheckButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 228);
            this.Controls.Add(this.JsonAddButton);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SchemaAddButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ValidatorMainForm";
            this.Text = "JSON Validator - GUI ver";
            this.Load += new System.EventHandler(this.ValidatorMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog schemaFileDialog;
        private System.Windows.Forms.Button SchemaAddButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.Button JsonAddButton;
        private System.Windows.Forms.OpenFileDialog jsonFileDialog;

    }
}