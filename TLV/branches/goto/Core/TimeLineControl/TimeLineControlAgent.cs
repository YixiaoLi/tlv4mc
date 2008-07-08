using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;
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
            topTimeLineAgent.P.ScaleMarkDirection = ScaleMarkDirection.Bottom;

            timeLineGridAgent.P.Margin = new Padding(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height, 1, bottomTimeLineAgent.P.Height + timeLineScrollBarAgent.P.Height);
            timeLineGridAgent.P.Location = new Point(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height);
            timeLineGridAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            timeLineGridAgent.P.HScrollBar = timeLineScrollBarAgent.P;

            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            bottomTimeLineAgent.P.Width = this.P.ClientSize.Width - (bottomTimeLineAgent.P.Location.X * 2);
            bottomTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            bottomTimeLineAgent.P.ScaleMarkDirection = ScaleMarkDirection.Top;

            timeLineScrollBarAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, bottomTimeLineAgent.P.Location.Y + bottomTimeLineAgent.P.Height);
            timeLineScrollBarAgent.P.Width = timeLineGridAgent.P.Width - timeLineGridAgent.P.TimeLineX;
            timeLineScrollBarAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineGridAgent.P.SizeChanged += timeLineGridPSizeChanged;
            timeLineScrollBarAgent.P.Scroll += new ScrollEventHandler(timeLineScrollBarPScroll);
           
        }

        public override void Init()
        {
            base.Init();

            timeLineGridAgent.P.DataSource = new SortableBindingList<TestObject>(new List<TestObject>()
            {
                new TestObject("task1", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(200000, Verb.RUN),
                        new TimeLineEvent(400000, Verb.DORMANT),
                    })),
                new TestObject("task2", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(250000, Verb.RUNNABLE),
                        new TimeLineEvent(400000, Verb.RUN),
                        new TimeLineEvent(500000, Verb.RUNNABLE),
                        new TimeLineEvent(600000, Verb.RUNNABLE),
                        new TimeLineEvent(700000, Verb.RUN),
                        new TimeLineEvent(719284, Verb.DORMANT),
                    })),
                new TestObject("task3", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(500000, Verb.RUN),
                        new TimeLineEvent(550000, Verb.WAITING),
                        new TimeLineEvent(600000, Verb.RUN),
                        new TimeLineEvent(700000, Verb.DORMANT),
                    })),
                new TestObject("task4", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task5", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task6", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task7", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task8", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task9", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task10", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task11", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task12", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task13", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task14", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task15", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task16", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task17", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task18", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task19", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task20", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task21", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task22", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task23", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task24", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task25", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task26", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
                new TestObject("task27", new TimeLineEvents(new List<TimeLineEvent>()
                    {
                        new TimeLineEvent(100239, Verb.DORMANT),
                        new TimeLineEvent(800000, Verb.DORMANT),
                    })),
            });

            timeLineGridAgent.P.MinRowHeight = 15;
            timeLineGridAgent.P.NowRowHeight = 25;
        }

        private void timeLineGridPSizeChanged(object sender, EventArgs e)
        {
            int timeLineWidth = timeLineGridAgent.P.Width - timeLineGridAgent.P.VerticalScrollBarWidth;
            topTimeLineAgent.P.Width = timeLineWidth;
            bottomTimeLineAgent.P.Width = timeLineWidth;
            timeLineScrollBarAgent.P.Width = timeLineWidth - (timeLineGridAgent.P.TimeLineX - timeLineGridAgent.P.Location.X);
            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            timeLineScrollBarAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, bottomTimeLineAgent.P.Location.Y + bottomTimeLineAgent.P.Height);
        }

        private void timeLineScrollBarPScroll(object sender, ScrollEventArgs e)
        {
            if (timeLineGridAgent.P.Edited == false)
            {
                timeLineGridAgent.P.Edited = true;
            }
        }

    }

    [Serializable]
    public class TestObject : ITimeLineViewable
    {
        public string Name { get; protected set; }
        public TimeLineEvents TimeLineEvents { get; protected set; }

        public TestObject(string name, TimeLineEvents timeLineEvents)
        {
            this.Name = name;
            this.TimeLineEvents = timeLineEvents;
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

}
