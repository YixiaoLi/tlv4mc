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
        public static bool Validate(XmlReader xml, XmlReader xslt, TextWriter result)
        {
            XmlReaderSettings xs = new XmlReaderSettings();
            xs.Schemas.Add(XmlSchema.Read(xml, null));
            xs.ValidationType = ValidationType.Schema;
            XmlReader xmlr = XmlReader.Create(xslt, xs);
            try { while (xmlr.Read()) { } }
            catch (XmlSchemaValidationException e)
            {
                result.WriteLine(e.Message);
                return false;
            }
            finally { xmlr.Close(); }
            return true;
        }

        public static void Transform(XmlReader xml, XmlReader xslt, XmlWriter result)
        {
            XslCompiledTransform _xslt = new XslCompiledTransform();
            _xslt.Load(xslt);
            StringBuilder sb = new StringBuilder();
            _xslt.Transform(xml, result);
        }

        public static void AutoIndent(string xml, TextWriter result)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xd.InnerXml));
            ds.WriteXml(result);
        }
    }
}
