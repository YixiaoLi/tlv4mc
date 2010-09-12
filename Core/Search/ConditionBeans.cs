using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    //詳細検索画面のメイン条件フォーム、サブ条件フォームで選択されている
    //検索条件を記録しておくためのクラス
    class ConditionBeans
    {
        public string mainResourceName;
        public string mainResourceType;
        public string mainRuleName;
        public string mainEventName;
        public string mainEventDetail;

        public string subResourceName;
        public string subResourceType;
        public string subRuleName;
        public string subEventName;
        public string subEventDetail;
        
        public string timing;
        public string  timingValue;

        public ConditionBeans()
        {
            mainResourceName = null;
            mainResourceType = null;
            mainRuleName = null;
            mainEventName = null;
            mainEventDetail = null;

            subResourceName = null;
            subResourceType = null;
            subRuleName = null;
            subEventName = null;
            subEventDetail = null;
            timing = null;
            timingValue = null;

        }

        public void clearMainCondition()
        {
            mainResourceName = null;
            mainResourceType = null;
            mainRuleName = null;
            mainEventName = null;
            mainEventDetail = null;
        }

        public void clearSubCondition()
        {
            subResourceName = null;
            subResourceType = null;
            subRuleName = null;
            subEventName = null;
            subEventDetail = null;

            timing = null;
            timingValue = null;
        }
    }
}
