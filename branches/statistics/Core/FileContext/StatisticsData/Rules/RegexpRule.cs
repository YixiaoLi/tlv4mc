using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Rules
{
    public class RegexpRule
    {
        /// <summary>
        /// 対象とするファイル
        /// <para></para>
        /// <para>　standard: 標準形式トレースログ</para>
        /// <para>　nonstandard: 変換前のトレースログ</para>
        /// <para>　ファイルパス: 統計情報に関する上記以外のファイル</para>
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// マッチさせる正規表現とマッチした場合のDataPointへの格納方法の組み合わせのリスト
        /// <para></para>
        /// <para>　Key: 正規表現</para>
        /// <para>　Val: マッチした場合のDataPointの各プロパティへ格納するデータ(または置換パターン(例：${置換}))</para>
        /// </summary>
        public RegexpReplacePatternPairList Regexps { get; set; }
    }

    /// <summary>
    /// マッチした場合のDataPointの各プロパティへ格納するデータ(または置換パターン(例：${置換}))を表すクラス
    /// </summary>
    public class DataPointReplacePattern
    {
        /// <summary>
        /// DataPoint.XLabelに格納する文字列または置換パターン
        /// </summary>
        public string XLabel { get; set; }
        /// <summary>
        /// DataPoint.XValueに格納する文字列または置換パターン
        /// </summary>
        public string XValue { get; set; }
        /// <summary>
        /// DataPoint.YValueに格納する文字列または置換パターン
        /// </summary>
        public string YValue { get; set; }
        /// <summary>
        /// DataPoint.Colorに格納する文字列または置換パターン
        /// </summary>
        public string Color { get; set; }
    }

    /// <summary>
    /// マッチさせる正規表現とマッチした場合のDataPointへの格納方法の組み合わせのリスト
    /// <para></para>
    /// <para>　Key: 正規表現</para>
    /// <para>　Val: マッチした場合のDataPointの各プロパティへ格納するデータ(またはその方法(例：${置換}))</para>
    /// </summary>
    public class RegexpReplacePatternPairList : GeneralKeyedJsonableCollection<string, DataPointReplacePattern, RegexpReplacePatternPairList>
    {
    }
}
