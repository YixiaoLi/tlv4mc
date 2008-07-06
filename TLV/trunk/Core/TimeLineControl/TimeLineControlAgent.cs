using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineScrollBar;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlAgent : Agent<TimeLineControlP, TimeLineControlA, TimeLineControlC>
    {
        private TimeLineGridAgent timeLineGridAgent = new TimeLineGridAgent("TimeLineGrid");
        private TimeLineAgent topTimeLineAgent = new TimeLineAgent("TopTimeLineAgent");
        private TimeLineAgent bottomTimeLineAgent = new TimeLineAgent("BottomTimeLineAgent");
        private TimeLineScrollBarAgent timeLineScrollBarAgent = new TimeLineScrollBarAgent("TimeLineScrollBarAgent");

        public TimeLineControlAgent(string name)
            : base(name, new TimeLineControlC(name, new TimeLineControlP(name), new TimeLineControlA(name)))
        {
            this.Add(timeLineGridAgent);
            this.Add(topTimeLineAgent);
            this.Add(bottomTimeLineAgent);
            this.Add(timeLineScrollBarAgent);

            topTimeLineAgent.P.Location = new Point(1, 1);
            topTimeLineAgent.P.Width = this.P.ClientSize.Width - (topTimeLineAgent.P.Location.X * 2);
            topTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineGridAgent.P.Margin = new Padding(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height, 1, bottomTimeLineAgent.P.Height + timeLineScrollBarAgent.P.Height);
            timeLineGridAgent.P.Location = new Point(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height);
            timeLineGridAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            bottomTimeLineAgent.P.Width = this.P.ClientSize.Width - (bottomTimeLineAgent.P.Location.X * 2);
            bottomTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineScrollBarAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, bottomTimeLineAgent.P.Location.Y + bottomTimeLineAgent.P.Height);
            timeLineScrollBarAgent.P.Width = timeLineGridAgent.P.Width - timeLineGridAgent.P.TimeLineX;
            timeLineScrollBarAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineGridAgent.P.SizeChanged += new EventHandler(timeLineGridPSizeChanged);

            timeLineGridAgent.P.DataSource = new SortableBindingList<TestObject>(new List<TestObject>()
            {
                new TestObject("test"),
                new TestObject("test1"),
                new TestObject("test2"),
                new TestObject("test3"),
                new TestObject("test4"),
                new TestObject("test"),
                new TestObject("test1"),
                new TestObject("test2"),
                new TestObject("test3"),
                new TestObject("test4"),
                new TestObject("test"),
                new TestObject("test1"),
                new TestObject("test2"),
                new TestObject("test3"),
                new TestObject("test4"),
                new TestObject("test"),
            });
        }

        private void timeLineGridPSizeChanged(object sender, EventArgs e)
        {
            int timeLineWidth = timeLineGridAgent.P.Width - timeLineGridAgent.P.VerticalScrollBarWidth;
            topTimeLineAgent.P.Width = timeLineWidth;
            bottomTimeLineAgent.P.Width = timeLineWidth;
            timeLineScrollBarAgent.P.Width = timeLineWidth - timeLineGridAgent.P.TimeLineX;
            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            timeLineScrollBarAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, bottomTimeLineAgent.P.Location.Y + bottomTimeLineAgent.P.Height);
        }

    }

    [Serializable]
    public class TestObject
    {
        protected static int Count = 0;

        public string Name { get; protected set; }
        public string Test { get; protected set; }
        public Log Value { get; set; }

        public TestObject(string name)
        {
            this.Name = name;
            this.Test = "Test";
            this.Value = new Log(this.Name + this.Test + Count.ToString());

            Count++;
        }

        public TestObject DeepClone()
        {
            TestObject target = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;

                target = (TestObject)formatter.Deserialize(stream);
            }

            return target;
        }
    }

    public class Log
    {
        public string Value { get; set; }

        public Log(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
