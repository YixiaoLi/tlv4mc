using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Mode
{
    /// <summary>
    /// 統計情報生成ルールのBasicRuleにおけるWhen、From、Toを表すクラス
    /// </summary>
    public class BaseEvent
    {
        /// <summary>
        /// 対象とするリソース名のリスト
        /// </summary>
        public List<string> ResourceNames { get; set; }
        /// <summary>
        /// 対象とするリソースタイプのリスト
        /// </summary>
        public List<string> ResourceType { get; set; }
        /// <summary>
        /// 対象とする属性遷移イベントにおける属性名
        /// </summary>
        public string AttributeName { get; set; }
        /// <summary>
        /// 対象とする属性遷移イベントにおける属性値
        /// </summary>
        public Json AttributeValue { get; set; }  // Jsonにすることで、数値、文字列など様々な値をルールファイルに記述可能になる
        /// <summary>
        /// 対象とする振る舞いイベントにおける振る舞い名
        /// </summary>
        public string BehaviorName { get; set; }
        /// <summary>
        /// 対象とする振る舞いイベントにおける引数
        /// </summary>
        public string BehaviorArg { get; set; }

        public BaseEvent()
        {
            ResourceNames = new List<string>();
            ResourceType = new List<string>();
            AttributeName = string.Empty;
            AttributeValue = Json.Empty;
            BehaviorName = string.Empty;
            BehaviorArg = string.Empty;
        }

        /// <summary>
        /// 対象とするリソース名のリストを取得する<para></para>
        /// ResourceNamesに加え、ResourceTypeで指定したリソースタイプに属するする
        /// リソースの名前を含めたリストを取得する
        /// </summary>
        /// <param name="data">対象トレースログのリソースファイルデータ</param>
        /// <returns>対象とするすべてのリソース名のList</returns>
        public List<string> GetResourceNameList(ResourceData data)
        {
            List<string> result = new List<string>();

            if (ResourceNames.Any<string>())
            {
                result.AddRange(ResourceNames);
            }
            foreach (string rt in ResourceType)
            {
                foreach (Resource res in data.Resources.Where<Resource>((r) => { return r.Type == rt && !result.Contains(r.Name); }))
                {
                    result.Add(res.Name);
                }
            }

            return result;
        }
    }
}
