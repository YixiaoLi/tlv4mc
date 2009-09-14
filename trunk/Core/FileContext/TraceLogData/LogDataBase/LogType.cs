
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public enum TraceLogType
	{
		AttributeChange,
		BehaviorHappen,
		None
	}

	public static class LogTypeExtension
	{
		public static string ToDisplayText(this TraceLogType logType)
		{
			switch (logType)
			{
				case TraceLogType.AttributeChange:
					return "メンバ値変更";
				case TraceLogType.BehaviorHappen:
					return "メソッド呼出し";
				default:
					return "その他";
			}
		}
	}
}
