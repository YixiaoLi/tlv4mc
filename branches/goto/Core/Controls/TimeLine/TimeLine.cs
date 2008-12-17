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
	public partial class TimeLine : UserControl, ITimeLine
	{

		protected Time _from;
		protected Time _to;
		protected TraceLogVisualizerData _data;

		public Time FromTime { get { return _from; } set { if (_from != value) { _from = value; } } }
		public Time ToTime { get { return _to; } set { if (_to != value) { _to = value; } } }
		public bool DynamicTimeRangeChange { get; set; }

		public TimeLine()
		{
			DoubleBuffered = true;
			DynamicTimeRangeChange = true;
			InitializeComponent();
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
						SetData(ApplicationData.FileContext.Data);
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

		public virtual void SetData(TraceLogVisualizerData data)
		{
			_data = data;

			ClearData();

			FromTime = new Time(_data.SettingData.TraceLogDisplayPanelSetting.FromTime, data.ResourceData.TimeRadix);
			ToTime = new Time(_data.SettingData.TraceLogDisplayPanelSetting.ToTime, data.ResourceData.TimeRadix);

			_data.SettingData.TraceLogDisplayPanelSetting.BecameDirty += (_o, p) =>
			{
				settingDataBecomeDirty(p);
			};
		}

		private void settingDataBecomeDirty(string p)
		{
			switch (p)
			{
				case "FromTime":
					if (DynamicTimeRangeChange)
						FromTime = new Time(_data.SettingData.TraceLogDisplayPanelSetting.FromTime, _data.ResourceData.TimeRadix); ;
					break;
				case "ToTime":
					if (DynamicTimeRangeChange)
						ToTime = new Time(_data.SettingData.TraceLogDisplayPanelSetting.ToTime, _data.ResourceData.TimeRadix); ;
					break;
			}
		}

		public void ClearData()
		{
			FromTime = new Time();
			ToTime = new Time();
		}
	}
}
