﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
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

			ResourceData res = new ResourceData().Parse(File.ReadAllText(tmpDirPath + name + "." + Properties.Resources.ResourceFileExtension));
            TraceLogList log = new TraceLogList().Parse(File.ReadAllText(tmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension));

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

            File.WriteAllText(tmpDirPath + name + "." + Properties.Resources.ResourceFileExtension, data.ResourceData.ToJson());
			File.WriteAllText(tmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension, data.TraceLogList.ToJson());

            zip.Compress(path, tmpDirPath);

            Directory.Delete(tmpDirPath, true);
        }
    }
}
