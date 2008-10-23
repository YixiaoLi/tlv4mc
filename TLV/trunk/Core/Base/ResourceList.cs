using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースリスト
    /// </summary>
    [XmlRoot("resources", Namespace="http://133.6.51.8/svn/ojl-mprtos/TLV/Resource")]
    public class ResourceList : IXmlSerializable, IEnumerable<Resource>
    {
        private Dictionary<string, Resource> _list = new Dictionary<string, Resource>();
        public Resource this[string name] { get { return _list[name]; } }

        public XmlSchema GetSchema()
        {
            return XmlSchema.Read(new XmlTextReader(ApplicationDatas.ResourceSchemaFilePath), null);
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer s = new XmlSerializer(typeof(Resource));

            reader.Read();  // <resources>
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                Resource r = s.Deserialize(reader) as Resource; // <resource>～</reaource>
                _list.Add(r.Name, r);
            }
            reader.Read();  // </resources>
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer s = new XmlSerializer(typeof(Resource));
            foreach (Resource val in _list.Values)
            {
                s.Serialize(writer, val);
            }
        }

        public IEnumerator<Resource> GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

    }
}
