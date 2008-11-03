using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class ConditionExpression
	{
		public string Left { get; set; }
		public string Ope { get; set; }
		public string Right { get; set; }

		public ConditionExpression(string condition)
		{
			Match m = Regex.Match(condition, @"(?<left>\w+)\s*(?<ope>(&&|\|\|))\s*(?<right>\w+)");
			Left = m.Groups["left"].Value;
			Ope = m.Groups["ope"].Value;
			Right = m.Groups["right"].Value;
		}

		public ConditionExpression(string left, string ope, string right)
		{
			Left = left;
			Ope = ope;
			Right = right;
		}

		public bool Result()
		{
			switch (Ope)
			{
				case "&&":
					return bool.Parse(Left) && bool.Parse(Right);
				case "||":
					return bool.Parse(Left) || bool.Parse(Right);
				default:
					return false;
			}
		}

	}
}
