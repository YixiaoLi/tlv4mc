using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class TimeLine : UserControl, ITimeLine
	{

		protected Time _from;
		protected Time _to;
		protected TraceLogVisualizerData _data;

		public Time FromTime { get { return _from; } set { if (_from != value) { _from = value; } } }
		public Time ToTime { get { return _to; } set { if (_to != value) { _to = value; } } }

		public TimeLine()
		{
			DoubleBuffered = true;
			InitializeComponent();
		}
		public TimeLine(TraceLogVisualizerData data)
			:this()
		{
			_data = data;
			SetData();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			ApplicationData.FileContext.DataChanged += (o, _e) =>
			{
				Invoke((MethodInvoker)(() =>
				{
					if (ApplicationData.FileContext.Data == null)
					{
						ClearData();
					}
					else
					{
						_data = ApplicationData.FileContext.Data;
						SetData();
					}
				}));
			};
		}

		public virtual void Draw(PaintEventArgs e)
		{

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Draw(e);
		}

		public void SetData()
		{
			ClearData();

			FromTime = _data.SettingData.TraceLogDisplayPanelSetting.FromTime;
			ToTime = _data.SettingData.TraceLogDisplayPanelSetting.ToTime;

			_data.SettingData.TraceLogDisplayPanelSetting.BecameDirty += (_o, p) =>
			{
				switch (p)
				{
					case "FromTime":
						FromTime = _data.SettingData.TraceLogDisplayPanelSetting.FromTime;
						break;
					case "ToTime":
						ToTime = _data.SettingData.TraceLogDisplayPanelSetting.ToTime;
						break;
				}
			};
		}

		public void ClearData()
		{
			FromTime = null;
			ToTime = null;
		}
	}
}
