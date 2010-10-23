using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search.Filters
{
    class TimingFilter : DetailFilter
    {
        public override Boolean checkSearchCondition(VisualizeLog visLog, SearchCondition refiningCondition, decimal normTime)
        {
            filter = new SimpleFilter();

            if (refiningCondition.timing.Equals("以内に発生(基準時以前)"))
            {

                if (filter.checkSearchCondition(visLog, refiningCondition, normTime)) // SimpleFilterを使ってrefiningCondition の「リソース名」～「イベント詳細」
                {                                                                     // までが visLog とマッチするかを調べる

                    // 時間制約による判定
                    if ((Math.Abs(visLog.fromTime - normTime) < decimal.Parse(refiningCondition.timingValue)) && (visLog.fromTime < normTime))
                    {
                        if (refiningCondition.denyCondition) //この絞込み条件の否定がとられていた場合
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

            }
            else if (refiningCondition.timing.Equals("以内に発生(基準時以後)"))
            {
                if (filter.checkSearchCondition(visLog, refiningCondition, normTime))
                {

                    if ((Math.Abs(visLog.fromTime - normTime) < decimal.Parse(refiningCondition.timingValue)) && (visLog.fromTime > normTime))
                    {
                        if (refiningCondition.denyCondition)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

            }
            else if (refiningCondition.timing.Equals("以上前に発生"))
            {
                if (filter.checkSearchCondition(visLog, refiningCondition, normTime))
                {
                    if (normTime - visLog.fromTime > decimal.Parse(refiningCondition.timingValue))
                    {
                        if (refiningCondition.denyCondition)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

            }
            else if (refiningCondition.timing.Equals("以上後に発生"))
            {
                if (filter.checkSearchCondition(visLog, refiningCondition, normTime))
                {
                    if (visLog.fromTime - normTime > decimal.Parse(refiningCondition.timingValue))
                    {
                        if (refiningCondition.denyCondition)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            return false;
        }

    }
}
