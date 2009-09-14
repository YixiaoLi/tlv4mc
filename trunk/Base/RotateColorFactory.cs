
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class RotateColorFactory
	{
		private uint _i = 0;
		public int Hue { get; set; }
		public int Saturation { get; set; }
		public int Value { get; set; }
		public Random Random { get; set; }
		public int RotateRange { get; set; }
		public int IncrementRange { get; set; }

		public RotateColorFactory()
		{
			Random = new Random(DateTime.Now.Millisecond);
			HueRandomSet();
			Saturation = 100;
			Value = 100;
			RotateRange = 120;
			IncrementRange = RotateRange / 2;
		}

		public Color RamdomColor(int hueFrom, int hueTo, int valueFrom, int valueTo, int saturationFrom, int saturationTo)
		{
			HueRandomSet(hueFrom, hueTo);
			ValueRandomSet(valueFrom, valueTo);
			SaturationRandomSet(saturationFrom, saturationTo);
			return new Color().FromHsv(Hue, Saturation, Value);
		}

		public Color RamdomColor()
		{
			return RamdomColor(0,360,80,100,80,100);
		}

		public Color RotateColor()
		{
			Color Result = new Color().FromHsv(Hue, Saturation, Value);

			if ((_i % (360 / RotateRange)) == 0)
			{
				Hue += IncrementRange;
				IncrementRange /= 2;
				if (IncrementRange == 1)
				{
					IncrementRange = RotateRange / 2;
					HueRandomSet();
				}
			}

			Hue += RotateRange;
			if (Hue > 360)
				Hue %= 360;

			_i++;
			if (_i == uint.MaxValue)
				_i = 0;

			return Result;
		}

		public void Reset()
		{
			_i = 0;
			Hue = 0;
			Saturation = 100;
			Value = 100;
			IncrementRange = RotateRange / 2;
		}

		public void HueRandomSet()
		{
			HueRandomSet(0, 360);
		}

		public void SaturationRandomSet()
		{
			SaturationRandomSet(0, 100);
		}

		public void ValueRandomSet()
		{
			ValueRandomSet(0, 100);
		}

		public void HueRandomSet(int from, int to)
		{
			int h = Hue;
			while(Math.Abs(h - Hue) < 30)
				h = Random.Next(from, to);
			Hue = h;
		}

		public void SaturationRandomSet(int from, int to)
		{
			Saturation = Random.Next(from, to);
		}

		public void ValueRandomSet(int from, int to)
		{
			Value = Random.Next(from, to);
		}
	}
}
