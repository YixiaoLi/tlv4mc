using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class LogDataBase : GeneralJsonableCollection<LogData, LogDataBase>
	{
		public void SetIds()
		{
			long i = 0;
			foreach(LogData log in this)
			{
				log.Id = i;
				i++;
			}
		}
	}
}
