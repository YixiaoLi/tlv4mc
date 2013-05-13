using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Mode
{
    public class ScriptExtension
    {
        /// <summary>
        /// 対象とするファイル
        /// <para>　standard: 標準形式トレースログ</para>
        /// <para>　nonstandard: 変換前のトレースログ</para>
        /// <para>　ファイルパス: 統計情報に関する上記以外のファイル</para>
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// スクリプト処理系のファイルパス
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 統計情報を生成するスクリプトのファイルパス
        /// </summary>
        public string Arguments { get; set; }
        /// <summary>
        /// スクリプト文　文法は指定されたスクリプト処理系に依存する
        /// </summary>
        public string Script { get; set; }
    }
}
