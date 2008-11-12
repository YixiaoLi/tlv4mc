using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public enum LogType
	{
		AttributeChange,
		BehaviorCall
	}

	public static class LogTypeExtension
	{
		public static string ToDisplayText(this LogType logType)
		{
			switch (logType)
			{
				case LogType.AttributeChange:
					return "メンバ値変更";
				case LogType.BehaviorCall:
					return "メソッド呼出し";
				default:
					return "その他";
			}
		}
	}
}
