namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    partial class TestForm
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
            this.searchOperationArea = new System.Windows.Forms.Panel();
            this.conditionSettingArea = new System.Windows.Forms.Panel();
            this.addConditionButton = new System.Windows.Forms.Button();
            this.searchOperationArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchOperationArea
            // 
            this.searchOperationArea.BackColor = System.Drawing.SystemColors.Window;
            this.searchOperationArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchOperationArea.Controls.Add(this.addConditionButton);
            this.searchOperationArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchOperationArea.Location = new System.Drawing.Point(0, 0);
            this.searchOperationArea.Name = "searchOperationArea";
            this.searchOperationArea.Size = new System.Drawing.Size(541, 66);
            this.searchOperationArea.TabIndex = 0;
            // 
            // conditionSettingArea
            // 
            this.conditionSettingArea.AutoScroll = true;
            this.conditionSettingArea.AutoSize = true;
            this.conditionSettingArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.conditionSettingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionSettingArea.Location = new System.Drawing.Point(0, 66);
            this.conditionSettingArea.Name = "conditionSettingArea";
            this.conditionSettingArea.Size = new System.Drawing.Size(541, 404);
            this.conditionSettingArea.TabIndex = 1;
            // 
            // addConditionButton
            // 
            this.addConditionButton.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.addConditionButton.Location = new System.Drawing.Point(406, 35);
            this.addConditionButton.Name = "addConditionButton";
            this.addConditionButton.Size = new System.Drawing.Size(110, 23);
            this.addConditionButton.TabIndex = 0;
            this.addConditionButton.Text = "基本条件を追加";
            this.addConditionButton.UseVisualStyleBackColor = true;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 470);
            this.Controls.Add(this.conditionSettingArea);
            this.Controls.Add(this.searchOperationArea);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.searchOperationArea.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel searchOperationArea;
        private System.Windows.Forms.Panel conditionSettingArea;
        private System.Windows.Forms.Button addConditionButton;
    }
}