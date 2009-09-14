
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public interface ITraceLogVisualizerControl
	{
		void SetData(TraceLogVisualizerData data);
		void ClearData();
	}
}
