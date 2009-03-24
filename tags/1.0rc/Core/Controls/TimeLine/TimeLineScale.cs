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
		public event EventHandler TimePerScaleMarkChanged;

		public bool DisplayCursorTime { get; set; }
		public ScaleMarkDirection ScaleMarkDirection { get; set; }

		public Time TimePerScaleMark
		{
			get { return _tPs; }
			set
			{
				_tPs = value;
				_dflag = _tPs.Value % 1.0m != 0;
				_carry = (int)Math.Ceiling(Math.Log10((double)(_tPs.Value)) * -1);
				_carry = _carry < 0 ? 0 : _carry;
				_startI = ((TimeLine.FromTime - TimeLine.MinTime) / _tPs).Truncate().Value;
				_startT = _startI > 0 ? TimeLine.MinTime + (_startI - _padding) * _tPs : TimeLine.FromTime;
				_endT = TimeLine.ToTime + (_padding * _tPs);

				if (TimePerScaleMarkChanged != null)
					TimePerScaleMarkChanged(this, EventArgs.Empty);
			}
		}

		private int _padding = 10;
		private float _scaleHeight = 2f;
		private float _pPs;
		private Time _tPp;
		private Time _tPs;
		private bool _dflag;
		private int _carry;
		private decimal _startI;
		private Time _startT;
		private Time _endT;
		private StringFormat _stringFormat;

		public TimeLineScale()
			:base()
		{
			Height = 20;
			BackColor = System.Drawing.Color.Black;
			Font = new System.Drawing.Font(FontFamily.GenericMonospace, 8);

			SizeChanged += (o, _e) => { memberUpdate(); };

			_stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			DisplayCursorTime = true;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);
			_pPs = _data.SettingData.TraceLogDisplayPanelSetting.PixelPerScaleMark;
			_data.SettingData.TraceLogDisplayPanelSetting.BecameDirty += (o,p)=>
				{
					if (p == "PixelPerScaleMark")
					{
						_pPs = _data.SettingData.TraceLogDisplayPanelSetting.PixelPerScaleMark;
						memberUpdate();
						Refresh();
					}
				};
		}

		public override TimeLine TimeLine
		{
			get
			{
				return base.TimeLine;
			}
			set
			{
				base.TimeLine = value;
				if (base.TimeLine != null)
				{
					TimeLine.ViewingAreaChanged += (o, e) => { memberUpdate(); };
					memberUpdate();
				}
			}
		}

		private void memberUpdate()
		{
			if (TimeLine != null && Width > 0)
			{
				Time old = _tPp;
				_tPp = TimeLine.ViewingSpan / (decimal)Width;
				_tPp = _tPp.Value > 1m ? _tPp.Round(0) : new Time(((decimal)Math.Pow(10, Math.Floor(Math.Log10((double)_tPp.Value)))).ToString(_timeRadix), _timeRadix);

				if (_tPp.IsEmpty)
				{
					_tPp = old;
				}
				else
				{
					Time t = _tPp.Value > 1m ? _tPp * (decimal)_pPs : TimeLine.ViewingSpan / ((decimal)Width / (decimal)_pPs);
					if (!t.IsEmpty)
						TimePerScaleMark = t;
				}
			}
		}

		public override void Draw(Graphics g, Rectangle rect)
		{
			base.Draw(g, rect);

			if (TimeLine == null
				|| TimeLine.ViewableSpan.Value == 0
				|| TimeLine.ViewingSpan.Value == 0
				|| _pPs == 0
				|| _tPp.IsEmpty
				|| _tPs.IsEmpty
				|| Width == 0)
				return;

			decimal i = _startI;
			float lastLabelX = float.MinValue;

			int bi = (int)((_pPs <= 50 ? 50 : 100) / _pPs);

			for (Time t = _startT;
				t < _endT;
				t += _tPs, i++ )
			{

				float x = t.ToX(TimeLine.FromTime, TimeLine.ToTime, Width) + rect.X;

				int iw = _data.ResourceData.TimeRadix;

				float h = i % iw == 0 ? _scaleHeight * 3f : i % (iw/2) == 0 ? _scaleHeight * 2f : _scaleHeight;
				System.Drawing.PointF fp;
				System.Drawing.PointF tp;
				switch (ScaleMarkDirection)
				{
					default:
					case ScaleMarkDirection.Bottom:
						fp = new System.Drawing.PointF(x, Height - h);
						tp = new System.Drawing.PointF(x, Height);
						break;
					case ScaleMarkDirection.Top:
						fp = new System.Drawing.PointF(x, 0);
						tp = new System.Drawing.PointF(x, h - 1);
						break;
				}

				g.DrawLine(Pens.White, fp, tp);

				if (i % (iw/2) == 0)
				{
					string tmStr = (_dflag ? t.Round(_carry) : t.Truncate()).ToString();

					SizeF tmStrSz = g.MeasureString(tmStr, Font);

					if (x - (tmStrSz.Width / 2f) > Width)
						return;

					if (x - (tmStrSz.Width / 2f) > lastLabelX)
					{
						float tx = x - (tmStrSz.Width / 2f);
						lastLabelX = tx + tmStrSz.Width;

						float y;
						switch (ScaleMarkDirection)
						{
							default:
							case ScaleMarkDirection.Bottom:
								y = Height - h - tmStrSz.Height;
								break;
							case ScaleMarkDirection.Top:
								y = h - 1;
								break;
						}

						g.DrawString(tmStr, Font, new SolidBrush(Color.White), new RectangleF(tx, y, tmStrSz.Width, tmStrSz.Height), _stringFormat);
					}
				}
			}

		}

		public override void DrawCursor(Graphics graphics, Color color, Time time)
		{
			base.DrawCursor(graphics, color, time);

			if (DisplayCursorTime)
			{
				drawCursorLabel(graphics, color, time, false);
			}
		}

		public override void DrawMarker(Graphics g, TimeLineMarker marker)
		{
			base.DrawMarker(g, marker);

			if (DisplayCursorTime)
			{
				drawCursorLabel(g, marker.Color, marker.Time, marker.Selected);
			}
		}

		private void drawCursorLabel(Graphics graphics, Color color, Time time, bool selected)
		{
			if (time.IsEmpty || TimeLine == null)
				return;

			string tmStr = (_dflag ? time.Round(_carry) : time.Truncate()).ToString();
			SizeF tmStrSz = graphics.MeasureString(tmStr, Font);

			float x = time.ToX(TimeLine.FromTime, TimeLine.ToTime, Width);

			if (x - (tmStrSz.Width / 2) > Width)
				return;

			float y;
			float margin = 5;
			switch (ScaleMarkDirection)
			{
				default:
				case ScaleMarkDirection.Bottom:
					y = 0;
					break;
				case ScaleMarkDirection.Top:
					y = margin;
					break;
			}

			PointF[] points;

			if (ScaleMarkDirection == ScaleMarkDirection.Bottom)
			{
				points = new[] {
					new PointF(x - tmStrSz.Width / 2,		y),
					new PointF(x + tmStrSz.Width / 2,		y),
					new PointF(x + tmStrSz.Width / 2,       y + tmStrSz.Height),
					new PointF(x,							y + tmStrSz.Height + margin),
					new PointF(x - tmStrSz.Width / 2,       y + tmStrSz.Height)
				};
			}
			else
			{
				points = new[] {
					new PointF(x + tmStrSz.Width / 2,		y),
					new PointF(x + tmStrSz.Width / 2,		y + tmStrSz.Height),
					new PointF(x - tmStrSz.Width / 2,       y + tmStrSz.Height),
					new PointF(x - tmStrSz.Width / 2,		y),
					new PointF(x,						    y - margin)
				};
			}

			int a = selected ? 255 : 250;
			float w = selected ? 3 : 1;

			using (SolidBrush brush = new SolidBrush(Color.FromArgb(a, Color.White)))
			{
				graphics.FillPolygon(brush, points);
			}
			using (System.Drawing.Pen pen = new System.Drawing.Pen(color, w))
			{
				graphics.DrawPolygon(pen, points);
			}

			graphics.DrawString(tmStr, Font, Brushes.Black, x - (tmStrSz.Width / 2), y);
		}
	}
}
