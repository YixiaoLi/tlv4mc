﻿using System;
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
			if (hue < 0)
			{
				hue += -1;
			}

			if (hue > 360)
			{
				hue %= 360;
			}

			if (saturation < 0)
			{
				saturation = 0;
			}
			else if (saturation > 100)
			{
				saturation = 100;
			}

			if (value < 0)
			{
				value = 0;
			}
			else if(value > 100)
			{
				value = 100;
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

		public static Color ComplementaryColor(this Color color)
		{
			return new Color().FromHsv(((int)color.GetHue() + 180) % 360, (int)(color.GetSaturation() * 100), (int)(color.GetBrightness() * 100));
		}

		public static Color Random(this Color color)
		{
			Random r = new Random(DateTime.Now.Millisecond);

			int hue =  r.Next(0, 360);
			int saturation = r.Next(80, 100);
			int value = r.Next(80, 100);

			return color.FromHsv(hue, saturation, value);

		}

	}
}