using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using WinForms = System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlAgent : Agent<TimeLineControlP, TimeLineControlA, TimeLineControlC>
    {
        private TimeLineGridAgent timeLineGridAgent = new TimeLineGridAgent("TimeLineGrid");
        private TimeLineAgent topTimeLineAgent = new TimeLineAgent("TopTimeLineAgent");
        private TimeLineAgent bottomTimeLineAgent = new TimeLineAgent("BottomTimeLineAgent");

        public TimeLineControlAgent(string name)
            : base(name, new TimeLineControlC(name, new TimeLineControlP(name), new TimeLineControlA(name)))
        {
            this.Add(timeLineGridAgent);
            this.Add(topTimeLineAgent);
            this.Add(bottomTimeLineAgent);

            topTimeLineAgent.P.Location = new Point(1, 1);
            topTimeLineAgent.P.Width = this.P.ClientSize.Width - (topTimeLineAgent.P.Location.X * 2);
            topTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineGridAgent.P.Margin = new Padding(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height, 1, bottomTimeLineAgent.P.Height);
            timeLineGridAgent.P.Location = new Point(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height);
            timeLineGridAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            bottomTimeLineAgent.P.Width = this.P.ClientSize.Width - (bottomTimeLineAgent.P.Location.X * 2);
            bottomTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineGridAgent.P.SizeChanged += new EventHandler(timeLineGridPSizeChanged);
            topTimeLineAgent.P.TimeLineXResizing += timeLineGridAgent.P.TimeLineXResizing;
            bottomTimeLineAgent.P.TimeLineXResizing += timeLineGridAgent.P.TimeLineXResizing;
            topTimeLineAgent.P.TimeLineXResized += (object o, MouseEventArgs e) => { timeLineGridAgent.P.Refresh(); };
            bottomTimeLineAgent.P.TimeLineXResized += (object o, MouseEventArgs e) => { timeLineGridAgent.P.Refresh(); };

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
            topTimeLineAgent.P.Width = this.P.ClientSize.Width - (topTimeLineAgent.P.Location.X * 2);
            bottomTimeLineAgent.P.Width = this.P.ClientSize.Width - (bottomTimeLineAgent.P.Location.X * 2);
            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
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
