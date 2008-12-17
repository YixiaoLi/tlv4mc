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

	public class TimeLineScale : TimeLine
	{
		public ScaleMarkDirection ScaleMarkDirection { get; set; }
		private Time _maxTime;
		private Time _minTime;
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
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);
			_maxTime = _data.TraceLogData.MaxTime;
			_minTime = _data.TraceLogData.MinTime;
			_pPs = _data.SettingData.TraceLogDisplayPanelSetting.PixelPerScaleMark;
		}

		public override void Draw(PaintEventArgs e)
		{
			base.Draw(e);

			if (_from == Time.NaN
				|| _to == Time.NaN
				|| _maxTime == Time.NaN
				|| _minTime == Time.NaN
				|| (_maxTime - _minTime).Value == 0
				|| _pPs == 0
				|| e.ClipRectangle.Width == 0)
				return;

			decimal tPp = (decimal)((_to - _from).Value) / (decimal)e.ClipRectangle.Width;
			decimal tPs = tPp * (decimal)_pPs;

			if (tPp == 0)
				return;

			int i = (int)(Math.Truncate((decimal)(_from.Value - _minTime.Value) / tPs));

			float lastLabelX = float.MinValue;

			decimal t = i > 0 ? _minTime.Value + (i - _padding) * tPs : _from.Value;

			for (; t < _to.Value + (_padding * tPs); t += tPs, i++)
			{
				float h = i % 10 == 0 ? _scaleHeight * 3f : i % 5 == 0 ? _scaleHeight * 2f : _scaleHeight;
				float x = ((float)((float)t - (float)(_from.Value)) / (float)(_to.Value - _from.Value)) * (float)e.ClipRectangle.Width;

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
					long _it = (long)Math.Truncate(Math.Round(t, 2, MidpointRounding.AwayFromZero));

					string tm = Convert.ToString(_it, _data.ResourceData.TimeRadix);

					SizeF tsz = e.Graphics.MeasureString(tm + "_", Font);

					if (x - (tsz.Width / 2f) > lastLabelX)
					{
						RectangleF rect; ;
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
