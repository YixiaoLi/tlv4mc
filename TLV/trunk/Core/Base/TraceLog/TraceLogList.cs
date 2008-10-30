using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// トレースログリスト
    /// </summary>
	public class TraceLogList : GeneralReadOnlyJsonableCollection<TraceLog, TraceLogList>
	{
		public TraceLogList()
			:base()
		{
			
		}

		public TraceLogList(IList<TraceLog> list)
			: base(list)
		{
		}
	}
}
