/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
        public static bool IsValid(string xml, string xslt)
        {
            return IsValid(xml, xslt, null);
        }

        public static bool IsValid(string xml, string xslt, TextWriter result)
        {
            XmlReaderSettings xs = new XmlReaderSettings();
            xs.Schemas.Add(XmlSchema.Read(new XmlTextReader(new StringReader(xml)), null));
            xs.ValidationType = ValidationType.Schema;
            XmlReader xmlr = XmlReader.Create(new XmlTextReader(new StringReader(xslt)), xs);
            try { while (xmlr.Read()) { } }
            catch (XmlSchemaValidationException e)
            {
                if (result != null)
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
