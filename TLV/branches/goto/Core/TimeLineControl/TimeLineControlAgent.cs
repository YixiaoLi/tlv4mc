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

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlAgent : Agent<TimeLineControlP, TimeLineControlA, TimeLineControlC>
    {
        private TimeLineGridAgent timeLineGridAgent = new TimeLineGridAgent("TimeLineGrid");

        public TimeLineControlAgent(string name)
            : base(name, new TimeLineControlC(name, new TimeLineControlP(name), new TimeLineControlA(name)))
        {
            this.Add(timeLineGridAgent);

            this.P.ContentPanel.ClientSizeChanged += timeLineGridAgent.P.ParentSizeChanged;

            timeLineGridAgent.P.Location = new Point(1, 1);
            timeLineGridAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

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
                new TestObject("test1"),
                new TestObject("test2"),
                new TestObject("test3"),
                new TestObject("test4"),
            });
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
