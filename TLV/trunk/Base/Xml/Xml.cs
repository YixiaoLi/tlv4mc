using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.Data;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class Xml
    {
        public static bool IsValid(string xml, string xslt, TextWriter result)
        {
            XmlReaderSettings xs = new XmlReaderSettings();
            xs.Schemas.Add(XmlSchema.Read(new XmlTextReader(new StringReader(xml)), null));
            xs.ValidationType = ValidationType.Schema;
            XmlReader xmlr = XmlReader.Create(new XmlTextReader(new StringReader(xslt)), xs);
            try { while (xmlr.Read()) { } }
            catch (XmlSchemaValidationException e)
            {
                result.WriteLine(e.Message);
                return false;
            }
            finally { xmlr.Close(); }
            return true;
        }

        public static string Transform(string xml, string xslt)
        {
            StringBuilder sb = new StringBuilder();
            XslCompiledTransform _xslt = new XslCompiledTransform();
            _xslt.Load(new XmlTextReader(new StringReader(xslt)));
            _xslt.Transform(new XmlTextReader(new StringReader(xml)), new XmlTextWriter(new StringWriter(sb)));
            return sb.ToString();
        }

        public static string AutoIndent(string xml)
        {
            StringBuilder sb = new StringBuilder();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xd.InnerXml));
            ds.WriteXml(new StringWriter(sb));
            return sb.ToString();
        }
    }
}
