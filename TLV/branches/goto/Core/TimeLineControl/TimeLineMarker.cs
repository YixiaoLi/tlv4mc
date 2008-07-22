using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    static class ColorExtension
    {
        public static Color FromHsv(this Color color, int hue, int saturation, int value)
        {
            if (hue < 0 || hue > 360)
            {
                throw new ArgumentOutOfRangeException("hue", "must be in the range [0, 360]");
            }

            if (saturation < 0 || saturation > 100)
            {
                throw new ArgumentOutOfRangeException("saturation", "must be in the range [0, 100]");
            }

            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException("value", "must be in the range [0, 100]");
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
    }

    public class TimeLineMarker
    {
        private static int i = 0;
        private static int hue = 0;
        private static int saturation = 100;
        private static int value = 100;
        private bool selected = false;

        public string Name { get; set; }
        public ulong Time { get; set; }
        public Color Color { get; protected set; }
        public bool Selected
        {
            get { return selected; }
            set
            {
                if(selected != value)
                {
                    selected = value;
                    if (selected && OnSelected != null)
                    {
                        OnSelected(this, EventArgs.Empty);
                    }
                }
            }
        }

        public event EventHandler OnSelected = null;

        public TimeLineMarker(ulong time)
        {
            Time = time;
            Name = i.ToString();
            Color = Color.FromHsv(hue % 360, saturation, value);
            Selected = false;

            colorRecalc();
        }

        // HSV色空間を色相反転 + 回転しながら色を変える
        private void colorRecalc()
        {
            hue += 45 + (((i % 8) == 0) ? 5 : 0) + 180;

            if (i % (45 / 5) == 0)
            {
                saturation -= 5;
                value -= 5;

                if (saturation < 50)
                {
                    saturation = 100;
                }
                if (value < 50)
                {
                    value = 100;
                }
            }
            i++;
        }
    }

    public class TimeLineMarkers
    {
        private List<TimeLineMarker> list = new List<TimeLineMarker>();

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public event EventHandler ItemAdded = null;
        public event EventHandler ItemRemoved = null;
        public event EventHandler ListChanged = null;
        public event EventHandler SelectChanged = null;
        public event EventHandler SelectCleared = null;

        public TimeLineMarkers(List<TimeLineMarker> list)
        {
            this.list = list;
        }
        public TimeLineMarkers()
        {
        }

        public void SelectClear()
        {
            foreach (TimeLineMarker tlm in list)
            {
                tlm.Selected = false;
            }
            if (SelectCleared != null)
            {
                SelectCleared(this, EventArgs.Empty);
            }
        }

        public void Add(TimeLineMarker tlm)
        {
            tlm.OnSelected += new EventHandler(tlmOnSelected);
            list.Add(tlm);
            list.Sort((t1, t2) => { return (int)((decimal)t1.Time - (decimal)t2.Time); });
            if(ItemAdded != null)
            {
                ItemAdded(this, EventArgs.Empty);
            }
            if(ListChanged != null)
            {
                ListChanged(this, EventArgs.Empty);
            }
        }

        protected void tlmOnSelected(object sender, EventArgs e)
        {
            if (SelectChanged != null)
            {
                SelectChanged(this, EventArgs.Empty);
            }
        }

        public void Remove(TimeLineMarker tlm)
        {
            list.Remove(tlm);
            list.Sort((t1, t2) => { return (int)((decimal)t1.Time - (decimal)t2.Time); });
            if(ItemRemoved != null)
            {
                ItemRemoved(this, EventArgs.Empty);
            }
            if(ListChanged != null)
            {
                ListChanged(this, EventArgs.Empty);
            }
        }
        public bool Exists(Predicate<TimeLineMarker> match)
        {
            return list.Exists(match);
        }
        public TimeLineMarkers GetBetween(ulong from, ulong to)
        {
            List<TimeLineMarker> tlms = list.FindAll(tlm => tlm.Time >= from && tlm.Time <= to);

            return new TimeLineMarkers(tlms);
        }
        public TimeLineMarkers GetBetweenExtended(ulong from, ulong to)
        {
            List<TimeLineMarker> tlms = list.FindAll(tlm => tlm.Time >= from && tlm.Time <= to);
            TimeLineMarker pre = list.FindLast(tlm => tlm.Time < from);
            TimeLineMarker post = list.Find(tlm => tlm.Time > to);
            if(pre != null)
            {
                tlms.Insert(0, pre);
            }
            if(post != null)
            {
                tlms.Add(post);
            }

            return new TimeLineMarkers(tlms);
        }

        public IEnumerator<TimeLineMarker> GetEnumerator()
        {
            foreach (TimeLineMarker element in list)
            {
                yield return element;
            }
        }
    }
}
