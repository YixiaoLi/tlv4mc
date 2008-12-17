using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public enum ScaleMarkDirection
	{
		Top,
		Bottom
	}

	partial class TimeLineScale : TimeLine
	{
		public ScaleMarkDirection ScaleMarkDirection { get; private set; }

		public TimeLineScale(ScaleMarkDirection markDirection)
			:base()
		{
			ScaleMarkDirection = markDirection;
			Font = new System.Drawing.Font(FontFamily.GenericMonospace, 8);

			InitializeComponent();
		}

		public override void Draw(PaintEventArgs e)
		{
			base.Draw(e);

			if (_data.SettingData.TraceLogDisplayPanelSetting.FromTime == null
				|| _data.SettingData.TraceLogDisplayPanelSetting.ToTime == null
				|| _data.TraceLogData.MaxTime == null
				|| _data.TraceLogData.MinTime == null
				|| _data.TraceLogData.MaxTime.Value - _data.TraceLogData.MinTime.Value == 0)
				return;

			int w = e.ClipRectangle.Width;
			long max = _data.TraceLogData.MaxTime.Value;
			long min = _data.TraceLogData.MinTime.Value;
			long from = _data.SettingData.TraceLogDisplayPanelSetting.FromTime.Value;
			long to = _data.SettingData.TraceLogDisplayPanelSetting.ToTime.Value;
			int pxPsm = _data.SettingData.TraceLogDisplayPanelSetting.PixelPerScaleMark;

			if (pxPsm == 0 || w == 0)
				return;

			int tmPsm = (int)((decimal)((to - from)) / ((decimal)w / (decimal)pxPsm));
			int tmPpx = (int)((decimal)tmPsm / (decimal)pxPsm);

			if (tmPpx == 0)
				return;

			int offset = (int)((((decimal)(from - min)) / (decimal)tmPpx) % (decimal)pxPsm);
			int i = (int)Math.Floor((((double)(from - min)) / (double)tmPpx) / (double)pxPsm);
			int ie = (int)Math.Floor((((double)(to - min)) / (double)tmPpx) / (double)pxPsm);
			int h = 2;
			int j = 0;
			float lastLabelX = float.MinValue;

			for (long t = i * tmPsm + min; i < ie ; t += tmPsm, i++, j++)
			{
				System.Drawing.Point fp;
				System.Drawing.Point tp;
				int _h = i % 10 == 0 ? h * 3 : i % 5 == 0 ? h * 2: h;
				int x = offset + j * pxPsm;

				switch (ScaleMarkDirection)
				{
					case ScaleMarkDirection.Bottom:
						fp = new System.Drawing.Point(x, Height - _h);
						tp = new System.Drawing.Point(x, Height);
						break;
					case ScaleMarkDirection.Top:
						fp = new System.Drawing.Point(x, 0);
						tp = new System.Drawing.Point(x, _h - 1);
						break;
					default:
						return;
				}

				e.Graphics.DrawLine(Pens.White, fp, tp);

				if (i % 10 == 0)
				{
					string tm = Convert.ToString(t, _data.ResourceData.TimeRadix);
					SizeF tsz = e.Graphics.MeasureString(tm, Font);

					if (x - (tsz.Width / 2f) > lastLabelX)
					{
						RectangleF rect = new RectangleF();
						float tx = (float)(x - (tsz.Width / 2f));

						switch (ScaleMarkDirection)
						{
							case ScaleMarkDirection.Bottom:
								rect = new RectangleF(tx, Height - _h - tsz.Height, tsz.Width, tsz.Height);
								break;
							case ScaleMarkDirection.Top:
								rect = new RectangleF(tx, _h - 1, tsz.Width, tsz.Height);
								break;
							default:
								return;
						}
						e.Graphics.DrawString(tm, Font, new SolidBrush(Color.White), rect);
						lastLabelX = tx + tsz.Width;
					}
				}
			}

		}
	}
}
