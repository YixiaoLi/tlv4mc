using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions
{
    public class ErrorCondition
    {
        private int _panelNum;
        private int _conditionNum;
        private string _errorMessage;
        public int PanelNum { set { _panelNum = value; } get { return _panelNum; } }
        public int ConditionNum { set { _conditionNum = value; } get { return _conditionNum; } }
        public string ErrorMessage { set { _errorMessage = value; } get { return _errorMessage; } }

        public ErrorCondition()
        {
            _panelNum = 0;
            _conditionNum = 0;
            _errorMessage = "";
        }
    }
}
