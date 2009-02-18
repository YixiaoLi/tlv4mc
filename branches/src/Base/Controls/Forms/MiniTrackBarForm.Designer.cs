namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	partial class MiniTrackBarForm
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
			this.resizeBar = new System.Windows.Forms.Panel();
			this.miniTrackBar = new NU.OJL.MPRTOS.TLV.Base.Controls.MiniTrackBar();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.miniTrackBar);
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(149, 23);
			this.panel1.TabIndex = 1;
			// 
			// resizeBar
			// 
			this.resizeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.resizeBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.resizeBar.Location = new System.Drawing.Point(151, 1);
			this.resizeBar.Name = "resizeBar";
			this.resizeBar.Size = new System.Drawing.Size(3, 23);
			this.resizeBar.TabIndex = 2;
			// 
			// miniTrackBar
			// 
			this.miniTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.miniTrackBar.Location = new System.Drawing.Point(0, 0);
			this.miniTrackBar.Maximum = 100;
			this.miniTrackBar.Minimum = 0;
			this.miniTrackBar.Name = "miniTrackBar";
			this.miniTrackBar.Size = new System.Drawing.Size(149, 23);
			this.miniTrackBar.TabIndex = 0;
			this.miniTrackBar.TickFrequency = 25;
			this.miniTrackBar.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
			this.miniTrackBar.Value = 0;
			// 
			// MiniTrackBarForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(155, 25);
			this.ControlBox = false;
			this.Controls.Add(this.resizeBar);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MiniTrackBarForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "MiniTrackBarForm";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private MiniTrackBar miniTrackBar;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel resizeBar;
	}
}