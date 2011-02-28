using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Search.Filters;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions;

namespace NU.OJL.MPRTOS.TLV.Core.Search.ConditionPanels
{
    public abstract class ConditionPanel : Panel
    {
        public Button deleteButton;
        public  int conditionID;
        public  int parentPanelID;
        abstract public SearchFilter getSearchFilter(SearchFilter decorateFilter);
        abstract public void setConditionID(int ID);
        abstract public ErrorCondition checkSearchCondition();
    }
}
