using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class RefiningConditionComponents
    {
        private TraceLogVisualizerData _data;
        private ComboBox _baseTargetResource;
        private ComboBox _baseTargetRule;
        private ComboBox _baseTargetEvent;
        private ComboBox _baseTargetEventDetail;

        public RefiningConditionComponents(TraceLogVisualizerData data)
        {
            _data = data;
        }
    }
}
