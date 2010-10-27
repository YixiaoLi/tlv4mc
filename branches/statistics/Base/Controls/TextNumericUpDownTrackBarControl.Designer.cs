/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textBox.Location = new System.Drawing.Point(1, 9);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(38, 12);
			this.textBox.TabIndex = 2;
			this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// vScrollBar
			// 
			this.vScrollBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.vScrollBar.Location = new System.Drawing.Point(40, 8);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(14, 14);
			this.vScrollBar.TabIndex = 3;
			this.vScrollBar.Value = 50;
			// 
			// trackBarButton
			// 
			this.trackBarButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.trackBarButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.trackBarButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trackBarButton.BackgroundImage")));
			this.trackBarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.trackBarButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.trackBarButton.Location = new System.Drawing.Point(54, 8);
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
			this.Size = new System.Drawing.Size(68, 30);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.CheckBox trackBarButton;
	}
}
