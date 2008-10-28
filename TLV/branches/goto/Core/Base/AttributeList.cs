using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// <c>Attribute</c>のリスト
    /// </summary>
    [XmlRoot("attributes", Namespace = "http://133.6.51.8/svn/ojl-mprtos/TLV/Attribute")]
    public class AttributeList : IXmlSerializable, IEnumerable<Attribute>
    {
        private Dictionary<string, Attribute> _list = new Dictionary<string, Attribute>();
        public Attribute this[string name] { get { return _list[name]; } }

        public XmlSchema GetSchema()
        {
            return XmlSchema.Read(new XmlTextReader(ApplicationDatas.ResourceSchemaFilePath), null);
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer s = new XmlSerializer(typeof(Attribute));

            reader.Read();  // <attributes>
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                Attribute a = s.Deserialize(reader) as Attribute;   // <attribute>～</attribute>
                _list.Add(a.Name, a);
            }
            reader.Read();  // </attributes>
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer s = new XmlSerializer(typeof(Attribute));
            foreach (Attribute val in _list.Values)
            {
                s.Serialize(writer, val);
            }
        }

        public IEnumerator<Attribute> GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

    }
}
