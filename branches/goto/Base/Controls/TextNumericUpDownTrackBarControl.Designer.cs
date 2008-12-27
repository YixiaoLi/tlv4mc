namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	partial class TextNumericUpDownTrackBarControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextNumericUpDownTrackBarControl));
			this.textBox = new System.Windows.Forms.TextBox();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.trackBarButton = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// textBox
			// 
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textBox.Location = new System.Drawing.Point(1, 1);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(42, 12);
			this.textBox.TabIndex = 2;
			this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// vScrollBar
			// 
			this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar.Location = new System.Drawing.Point(44, 0);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(14, 14);
			this.vScrollBar.TabIndex = 3;
			this.vScrollBar.Value = 50;
			// 
			// trackBarButton
			// 
			this.trackBarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.trackBarButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trackBarButton.BackgroundImage")));
			this.trackBarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.trackBarButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.trackBarButton.Location = new System.Drawing.Point(58, 0);
			this.trackBarButton.Name = "trackBarButton";
			this.trackBarButton.Size = new System.Drawing.Size(14, 14);
			this.trackBarButton.TabIndex = 4;
			this.trackBarButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.trackBarButton.UseVisualStyleBackColor = true;
			// 
			// TextNumericUpDownTrackBarControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.trackBarButton);
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.textBox);
			this.Name = "TextNumericUpDownTrackBarControl";
			this.Size = new System.Drawing.Size(72, 14);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.CheckBox trackBarButton;
	}
}
