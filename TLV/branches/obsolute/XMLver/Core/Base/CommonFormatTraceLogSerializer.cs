using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// <c>CommonFormatTraceLog</c>のシリアライス、デシリアライズを行うサポート静的クラス
    /// </summary>
    public static class CommonFormatTraceLogSerializer
    {
        /// <summary>
        /// パスを指定してデシリアライズする
        /// </summary>
        /// <param name="path">読み込むパス</param>
        /// <returns>デシリアライズした<c>CommonFormatTraceLog</c></returns>
        public static CommonFormatTraceLog Deserialize(string path)
        {
            // 一時ディレクトリ作成
            string tmpDirPath = Path.GetTempPath() + "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\";
            Directory.CreateDirectory(tmpDirPath);

            IZip zip = ApplicationFactory.Zip;

            zip.Extract(path, tmpDirPath);

            string name = Path.GetFileNameWithoutExtension(path);

            string res = File.ReadAllText(tmpDirPath + name + "." + Properties.Resources.ResourceFileExtension);
            string log = File.ReadAllText(tmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension);

            return new CommonFormatTraceLog(res, log);
        }

        /// <summary>
        /// 保存するパスを指定して<c>CommonFormatTraceLog</c>をシリアライズする
        /// </summary>
        /// <param name="path">保存する先のパス</param>
        /// <param name="data">シリアライズする<c>CommonFormatTraceLog</c></param>
        public static void Serialize(string path, CommonFormatTraceLog data)
        {
            // 一時ディレクトリ作成
            string tmpDirPath = Path.GetTempPath() + "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\";
            Directory.CreateDirectory(tmpDirPath);
            
            IZip zip = ApplicationFactory.Zip;

            string name = Path.GetFileNameWithoutExtension(path);

            FileStream fs = new FileStream(tmpDirPath + name + "." + Properties.Resources.ResourceFileExtension, FileMode.Create);
            new XmlSerializer(typeof(ResourceList)).Serialize(fs, data.ResourceList);
            File.WriteAllText(tmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension, data.TraceLogList.ToString());
            fs.Close();

            zip.Compress(path, tmpDirPath);

            Directory.Delete(tmpDirPath, true);
        }
    }
}
