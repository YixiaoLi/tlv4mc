using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Mode
{
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
