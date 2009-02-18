using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class TypeExtension
	{
		public static bool IsCollection(this Type type)
		{
			if (type.IsGenericType && typeof(ICollection).IsAssignableFrom(type))
			{
				return true;
			}
			else if (type != typeof(object))
			{
				return IsCollection(type.BaseType);
			}
			else
			{
				return false;
			}
		}
	}
}
