using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public enum ScaleMarkDirection
	{
		Top,
		Bottom
	}

	public class TimeLineScale : TimeLineControl
	{
		public ScaleMarkDirection ScaleMarkDirection { get; set; }
		private int _padding = 10;
		private float _scaleHeight = 2f;
		private float _pPs;
		private StringFormat _sf;

		public TimeLineScale()
			:base()
		{
			BackColor = System.Drawing.Color.Black;
			Font = new System.Drawing.Font(FontFamily.GenericMonospace, 8);

			_sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
			Height = 20;
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);
			_pPs = _data.SettingData.TraceLogDisplayPanelSetting.PixelPerScaleMark;
			_data.SettingData.TraceLogDisplayPanelSetting.BecameDirty += (o,p)=>
				{
					if (p == "PixelPerScaleMark")
						_pPs = _data.SettingData.TraceLogDisplayPanelSetting.PixelPerScaleMark;
				};
		}

		public override void Draw(PaintEventArgs e)
		{
			base.Draw(e);

			if (TimeLine == null
				|| TimeLine.ViewableSpan.Value == 0
				|| TimeLine.ViewingSpan.Value == 0
				|| _pPs == 0
				|| e.ClipRectangle.Width == 0)
				return;

			decimal tPp = (decimal)(TimeLine.ViewingSpan.Value) / (decimal)e.ClipRectangle.Width;
			decimal tPs = tPp * (decimal)_pPs;

			if (tPp == 0 || tPs == 0)
				return;

			int i = (int)(Math.Truncate((decimal)(TimeLine.FromTime.Value - TimeLine.MinTime.Value) / tPs));

			float lastLabelX = float.MinValue;

			decimal t = i > 0 ? TimeLine.MinTime.Value + (i - _padding) * tPs : TimeLine.FromTime.Value;

			bool dflag = tPs * 10m <= 1;
			int carry = (int)Math.Ceiling(Math.Log10((double)(tPs * 10m)) * -1);

			for (; t < TimeLine.ToTime.Value + (_padding * tPs); t += tPs, i++)
			{
				float h = i % 10 == 0 ? _scaleHeight * 3f : i % 5 == 0 ? _scaleHeight * 2f : _scaleHeight;
				//float x = ((float)((float)t - (float)(TimeLine.FromTime.Value)) / (float)(TimeLine.ToTime.Value - TimeLine.FromTime.Value)) * (float)e.ClipRectangle.Width;
				float x = new Time(t.ToString(_timeRadix), _timeRadix).ToX(TimeLine.FromTime, TimeLine.ToTime, e.ClipRectangle.Width);

				System.Drawing.PointF fp;
				System.Drawing.PointF tp;
				switch (ScaleMarkDirection)
				{
					case ScaleMarkDirection.Bottom:
						fp = new System.Drawing.PointF(x, Height - h);
						tp = new System.Drawing.PointF(x, Height);
						break;
					case ScaleMarkDirection.Top:
						fp = new System.Drawing.PointF(x, 0);
						tp = new System.Drawing.PointF(x, h - 1);
						break;
					default:
						return;
				}

				e.Graphics.DrawLine(Pens.White, fp, tp);

				if (i % 10 == 0)
				{
					decimal _it;
					if (dflag)
					{
						_it = Math.Round(t, carry, MidpointRounding.ToEven);
					}
					else
					{
						_it = Math.Truncate(t);
					}


					string tm = _it.ToString(_timeRadix);

					SizeF tsz = e.Graphics.MeasureString(tm + "_", Font);

					if (x - (tsz.Width / 2f) > lastLabelX)
					{
						RectangleF rect;
						float tx = x - (tsz.Width / 2f);

						switch (ScaleMarkDirection)
						{
							default:
							case ScaleMarkDirection.Bottom:
								rect = new RectangleF(tx, Height - h - tsz.Height, tsz.Width, tsz.Height);
								break;
							case ScaleMarkDirection.Top:
								rect = new RectangleF(tx, h - 1, tsz.Width, tsz.Height);
								break;
						}
						e.Graphics.DrawString(tm, Font, new SolidBrush(Color.White), rect, _sf);
						lastLabelX = tx + tsz.Width;
					}
				}
			}

		}
	}
}
