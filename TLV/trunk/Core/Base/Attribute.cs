using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NU.OJL.MPRTOS.TLV.Core
{
    [XmlRoot("attribute", Namespace = "http://133.6.51.8/svn/ojl-mprtos/TLV/Resource")]
    public class Attribute
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("displayName")]
        public string DisplayName { get; set; }
        [XmlElement("value")]
        public string Value { get; set; }
        [XmlElement("variableType")]
        public VariableType VariableType { get; set; }
        [XmlElement("allocationType")]
        public AllocationType AllocationType { get; set; }
        [XmlElement("grouping")]
        public bool Grouping { get; set; }

        public Attribute()
        {
            Name = string.Empty;
            DisplayName = string.Empty;
            Value = string.Empty;
            VariableType = VariableType.String;
            AllocationType = AllocationType.Static;
            Grouping = false;
        }
    }

    public enum AllocationType
    {
        Static,
        Dynamic
    }

    public enum VariableType
    {
        String,
        Decimal,
        Enumeration,
        Boolean
    }
}
