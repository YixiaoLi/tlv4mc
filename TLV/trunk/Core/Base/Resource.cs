using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NU.OJL.MPRTOS.TLV.Core
{
    [XmlRoot("resource", Namespace = "http://133.6.51.8/svn/ojl-mprtos/TLV/Resource")]
    public class Resource
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("attributes")]
        public AttributeList Attributes { get; set; }

        public Resource()
        {
            Name = string.Empty;
            Attributes = new AttributeList();
        }
    }
}
