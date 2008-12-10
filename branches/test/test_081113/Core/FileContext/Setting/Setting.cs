using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Setting : Json, IFileContextData
	{
		public event EventHandler<GeneralEventArgs<bool>> IsDirtyChanged;

		public Setting()
		{
			Value = new Dictionary<string, Json>(); 
		}

		private bool _isDirty = false;
		public bool IsDirty
		{
			get { return _isDirty; }
			set
			{
				if (_isDirty != value)
				{
					_isDirty = value;

					if (IsDirtyChanged != null)
						IsDirtyChanged(this, new GeneralEventArgs<bool>(_isDirty));
				}
			}
		}
		public void Serialize(string path)
		{
			File.WriteAllText(path, ApplicationFactory.JsonSerializer.Serialize(Value));
		}

		public void Deserialize(string path)
		{
			Value = ((Json)ApplicationFactory.JsonSerializer.Deserialize(File.ReadAllText(path), typeof(Json))).Value;
		}

	}
}
