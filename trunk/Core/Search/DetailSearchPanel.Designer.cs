namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    partial class DetailSearchPanel
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.resourceName = new System.Windows.Forms.ComboBox();
            this.ruleName = new System.Windows.Forms.ComboBox();
            this.eventName = new System.Windows.Forms.ComboBox();
            this.eventDetail = new System.Windows.Forms.ComboBox();
            this.addConditionButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.Controls.Add(this.addConditionButton);
            this.panel1.Controls.Add(this.eventDetail);
            this.panel1.Controls.Add(this.eventName);
            this.panel1.Controls.Add(this.ruleName);
            this.panel1.Controls.Add(this.resourceName);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 166);
            this.panel1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(604, 3);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 166);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(604, 251);
            this.panel2.TabIndex = 1;
            // 
            // resourceName
            // 
            this.resourceName.FormattingEnabled = true;
            this.resourceName.Items.AddRange(new object[] {
            "TASK1",
            "TASK2",
            "TASK3"});
            this.resourceName.Location = new System.Drawing.Point(44, 37);
            this.resourceName.Name = "resourceName";
            this.resourceName.Size = new System.Drawing.Size(83, 20);
            this.resourceName.TabIndex = 1;
            // 
            // ruleName
            // 
            this.ruleName.FormattingEnabled = true;
            this.ruleName.Items.AddRange(new object[] {
            "状態遷移",
            "システムコール"});
            this.ruleName.Location = new System.Drawing.Point(138, 37);
            this.ruleName.Name = "ruleName";
            this.ruleName.Size = new System.Drawing.Size(79, 20);
            this.ruleName.TabIndex = 0;
            // 
            // eventName
            // 
            this.eventName.FormattingEnabled = true;
            this.eventName.Items.AddRange(new object[] {
            "状態"});
            this.eventName.Location = new System.Drawing.Point(232, 37);
            this.eventName.Name = "eventName";
            this.eventName.Size = new System.Drawing.Size(79, 20);
            this.eventName.TabIndex = 2;
            // 
            // eventDetail
            // 
            this.eventDetail.FormattingEnabled = true;
            this.eventDetail.Items.AddRange(new object[] {
            "RUNNING",
            "RUNNABLE",
            "WAITING"});
            this.eventDetail.Location = new System.Drawing.Point(321, 37);
            this.eventDetail.Name = "eventDetail";
            this.eventDetail.Size = new System.Drawing.Size(79, 20);
            this.eventDetail.TabIndex = 3;
            // 
            // addConditionButton
            // 
            this.addConditionButton.Location = new System.Drawing.Point(447, 35);
            this.addConditionButton.Name = "addConditionButton";
            this.addConditionButton.Size = new System.Drawing.Size(29, 23);
            this.addConditionButton.TabIndex = 4;
            this.addConditionButton.Text = "+";
            this.addConditionButton.UseVisualStyleBackColor = true;
            // 
            // DetailSearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(604, 417);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DetailSearchPanel";
            this.Text = "DetailSearchPanel";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button addConditionButton;
        private System.Windows.Forms.ComboBox eventDetail;
        private System.Windows.Forms.ComboBox eventName;
        private System.Windows.Forms.ComboBox ruleName;
        private System.Windows.Forms.ComboBox resourceName;



    }
}