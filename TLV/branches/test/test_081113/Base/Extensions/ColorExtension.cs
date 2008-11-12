using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class ColorExtension
	{
		public static Color FromHsv(this Color color, int hue, int saturation, int value)
		{
			if (hue < 0 || hue > 360)
			{
				throw new ArgumentOutOfRangeException("hue", "0～360の間でなければなりません。");
			}

			if (saturation < 0 || saturation > 100)
			{
				throw new ArgumentOutOfRangeException("saturation", "0～100の間でなければなりません。");
			}

			if (value < 0 || value > 100)
			{
				throw new ArgumentOutOfRangeException("value", "0～100の間でなければなりません。");
			}

			double h;
			double s;
			double v;

			double r = 0;
			double g = 0;
			double b = 0;

			h = (double)hue % 360;
			s = (double)saturation / 100;
			v = (double)value / 100;

			if (s == 0)
			{
				r = v;
				g = v;
				b = v;
			}
			else
			{
				double p;
				double q;
				double t;

				double fractionalSector;
				int sectorNumber;
				double sectorPos;

				sectorPos = h / 60;
				sectorNumber = (int)(Math.Floor(sectorPos));

				fractionalSector = sectorPos - sectorNumber;

				p = v * (1 - s);
				q = v * (1 - (s * fractionalSector));
				t = v * (1 - (s * (1 - fractionalSector)));

				switch (sectorNumber)
				{
					case 0:
						r = v;
						g = t;
						b = p;
						break;

					case 1:
						r = q;
						g = v;
						b = p;
						break;

					case 2:
						r = p;
						g = v;
						b = t;
						break;

					case 3:
						r = p;
						g = q;
						b = v;
						break;

					case 4:
						r = t;
						g = p;
						b = v;
						break;

					case 5:
						r = v;
						g = p;
						b = q;
						break;
				}
			}

			return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));

		}

		public static Color RandomNextColor(this Color color)
		{
			Color Result = color.FromHsv(hue, saturation, value);

			hue = r.Next(0, 360);
			saturation = r.Next(80, 100);

			return Result;
		}

		public static Color HueRotateNextColor(this Color color)
		{
			Color Result = color.FromHsv(hue, saturation, value);

			hue += rotateRange + (((i % (360 / rotateRange)) == 0) ? incrementRange : 0);

			if (i % (rotateRange / incrementRange) == 0)
			{
				saturation = r.Next(80, 100);

				incrementRange /= 2;

				if (incrementRange < 15)
				{
					hue += new Random().Next(0, 360);
					incrementRange = new Random().Next(15, rotateRange);
				}
			}
			i++;

			if (i == uint.MaxValue)
				i = 0;

			if (hue > 360)
				hue %= 360;

			return Result;
		}

		public static void HueRotateColorReset(this Color color)
		{
			i = 1;
			hue = new Random().Next(0, 360);
			saturation = 100;
			value = 100;
		}

		static uint i = 1;
		static int hue = 0;
		static int saturation = 100;
		static int value = 100;
		static Random r = new Random();

		static int rotateRange = 60;
		static int incrementRange = rotateRange/2;
	}
}
