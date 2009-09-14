
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLineMacroViewerSetting : ISetting
	{
		private Color _selectedAreaColor = Color.Purple;

		public Color SelectedAreaColor { get { return _selectedAreaColor; } set { if (_selectedAreaColor != value) { _selectedAreaColor = value; onBecameDirty("SelectedAreaColor"); } } }

		public event SettingChangeEventHandler BecameDirty;

		private void onBecameDirty(string propertyName)
		{
			if (BecameDirty != null)
				BecameDirty(this, propertyName);
		}
	}
}
