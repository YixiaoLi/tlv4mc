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
		private Time _tPp;
		private Time _tPs;
		private bool _dflag;
		private int _carry;
		private int _startI;
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
				_tPp = TimeLine.ViewingSpan / (decimal)Width;
				_tPp = _tPp.Value > 1m ? _tPp.Round(0) : new Time(((decimal)Math.Pow(10, Math.Floor(Math.Log10((double)_tPp.Value)))).ToString(_timeRadix), _timeRadix);
				_tPs = _tPp.Value > 1m ? _tPp * (decimal)_pPs : TimeLine.ViewingSpan / ((decimal)Width / (decimal)_pPs);
				_dflag = _tPs.Value % 1.0m != 0;
				_carry = (int)Math.Ceiling(Math.Log10((double)(_tPs.Value)) * -1);
				_startI = (int)((TimeLine.FromTime - TimeLine.MinTime) / _tPs).Truncate().Value;
				_startT = _startI > 0 ? TimeLine.MinTime + (_startI - _padding) * _tPs : TimeLine.FromTime;
				_endT = TimeLine.ToTime + (_padding * _tPs);
			}
		}

		public override void Draw(PaintEventArgs e)
		{
			base.Draw(e);

			if (TimeLine == null
				|| TimeLine.ViewableSpan.Value == 0
				|| TimeLine.ViewingSpan.Value == 0
				|| _pPs == 0
				|| _tPp.IsEmpty
				|| _tPs.IsEmpty
				|| Width == 0)
				return;

			int i = _startI;
			float lastLabelX = float.MinValue;

			for (Time t = _startT;
				t < _endT;
				t += _tPs, i++ )
			{

				float x = t.ToX(TimeLine.FromTime, TimeLine.ToTime, Width) + e.ClipRectangle.X;

				float h = i % 10 == 0 ? _scaleHeight * 3f : i % 5 == 0 ? _scaleHeight * 2f : _scaleHeight;
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

				e.Graphics.DrawLine(Pens.White, fp, tp);

				if (i % 10 == 0)
				{
					string tmStr = (_dflag ? t.Round(_carry) : t.Truncate()).ToString();

					SizeF tmStrSz = e.Graphics.MeasureString(tmStr + "_", Font);

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

						e.Graphics.DrawString(tmStr, Font, new SolidBrush(Color.White), new RectangleF(tx, y, tmStrSz.Width, tmStrSz.Height), _stringFormat);
					}
				}
			}

		}
	}
}
