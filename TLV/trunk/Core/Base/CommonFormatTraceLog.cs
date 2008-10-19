using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class CommonFormatTraceLog : IFileContextData
    {
        private bool _isDirty = false;

        public event EventHandler BecameDirty = null;

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if(_isDirty != value)
                {
                    _isDirty = value;
                    if (BecameDirty != null)
                        BecameDirty(this, EventArgs.Empty);
                }
            }
        }
        public ResourceList ResourceList { get; private set; }
        public TraceLogList TraceLogList { get; private set; }

        public CommonFormatTraceLog()
        {
        }

        public CommonFormatTraceLog(string resourceData, string traceLogData)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ResourceList));
            ResourceList = xs.Deserialize(new StringReader(resourceData)) as ResourceList;
            TraceLogList = new TraceLogList(traceLogData);
        }

        public void Serialize(string path)
        {
            CommonFormatTraceLogSerializer.Serialize(path, this);
        }

        public void Deserialize(string path)
        {
            CommonFormatTraceLog c = CommonFormatTraceLogSerializer.Deserialize(path);
            ResourceList = c.ResourceList;
            TraceLogList = c.TraceLogList;
        }
    }
}
