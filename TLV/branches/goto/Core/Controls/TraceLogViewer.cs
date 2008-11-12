using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TraceLogViewer : UserControl
	{
		private Dictionary<string, Color> _eventColorCache = new Dictionary<string, Color>();
		private Dictionary<string, Color> _resColorCache = new Dictionary<string, Color>();
		private Dictionary<string, Color> _resTypeColorCache = new Dictionary<string, Color>();

		private SortableBindingList<TraceLog> _dataSource = new SortableBindingList<TraceLog>();

		private TraceLogData _traceLogData;
		private ResourceData _resourceData;
		private Json _colorTable = new Json(new Dictionary<string, Json>());

		public TraceLogViewer()
		{
			InitializeComponent();
		}

		public void SetData(TraceLogData traceLogData, ResourceData resourceData)
		{
			_traceLogData = traceLogData;
			_resourceData = resourceData;

			if (_resourceData != null)
			{
				dataGridView.Columns["time"].HeaderText = "時間[" + _resourceData.TimeScale + "]";

				setDataGridViewDataSource();

				setupColorTable();
			}
			else
			{
				dataGridView.Columns["time"].HeaderText = "時間";
			}

			if (_traceLogData == null)
				dataGridView.DataSource = null;

		}

		public void ClearData()
		{
			SetData(null, null);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			addColumn("time", "時間", "Time");
			addColumn("resourceType", "リソースタイプ", "ResourceType", false);
			addColumn("resource", "リソース", "Resource");
			addColumn("event", "イベント", "Event");

			dataGridView.AutoGenerateColumns = false;
			dataGridView.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridViewCellPainting);
			dataGridView.MouseWheel += new MouseEventHandler(dataGridViewMouseWheel);
			dataGridView.KeyDown += (o, _e) => hintTextUpdate();
			dataGridView.KeyUp += (o, _e) => hintTextUpdate();
			dataGridView.MouseMove += (o, _e) => hintTextUpdate();
			dataGridView.MouseLeave += (o, _e) => hintTextUpdate();

		}

		private void hintTextUpdate()
		{

			hintStatusLabel.Text = "";

			if (new Rectangle(0, 0, dataGridView.Width, dataGridView.ColumnHeadersHeight).Contains(dataGridView.PointToClient(Control.MousePosition)))
			{
				hintStatusLabel.Text = "列ヘッダクリックでソート";
			}
			else if (new Rectangle(0, dataGridView.ColumnHeadersHeight, dataGridView.Width, dataGridView.Height -  dataGridView.ColumnHeadersHeight).Contains(dataGridView.PointToClient(Control.MousePosition)))
			{
				hintStatusLabel.Text = "右クリックメニューから表示項目の選択";
			}
			
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
			{
				hintStatusLabel.Text = "Ctrl + ホイールで文字サイズの変更";
				enableCtrlKey.BorderStyle = Border3DStyle.SunkenOuter;
			}
			else
			{
				enableCtrlKey.BorderStyle = Border3DStyle.RaisedInner;
			}
		}

		private void dataGridViewMouseWheel(object sender, MouseEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
			{
				int v = e.Delta > 0 ? 1 : -1;
				if (dataGridView.DefaultCellStyle.Font.Size + v > 1 && dataGridView.DefaultCellStyle.Font.Size + v < 100)
					dataGridView.DefaultCellStyle.Font = new Font(dataGridView.DefaultCellStyle.Font.FontFamily, dataGridView.DefaultCellStyle.Font.Size + v);
				if (dataGridView.ColumnHeadersDefaultCellStyle.Font.Size + v > 1 && dataGridView.ColumnHeadersDefaultCellStyle.Font.Size + v < 100)
					dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, dataGridView.ColumnHeadersDefaultCellStyle.Font.Size + v);
			}
		}

		private void dataGridViewCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			bool isResource = e.ColumnIndex == dataGridView.Columns["Resource"].Index;
			bool isResourceType = e.ColumnIndex == dataGridView.Columns["ResourceType"].Index;
			bool isEvent = e.ColumnIndex == dataGridView.Columns["Event"].Index;

			e.PaintBackground(e.ClipBounds, true);

			if (e.RowIndex >= 0
				&& (isResource || isResourceType || isEvent)
				&& (e.PaintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
			{
				string value = (string)e.Value;
				Color color = Color.White;
				if (isResource)
				{
					if (_resColorCache.ContainsKey(value))
					{
						color = _resColorCache[value];
					}
					else
					{
						color = new Color().FromHexString((string)_colorTable["ResourceColors"][value].Value);
						_resColorCache.Add(value, color);
					}
				}
				else if (isResourceType)
				{
					if (_resTypeColorCache.ContainsKey(value))
					{
						color = _resTypeColorCache[value];
					}
					else
					{
						color = new Color().FromHexString((string)_colorTable["ResourceTypeColors"][value].Value);
						_resTypeColorCache.Add(value, color);
					}
				}
				else if (isEvent)
				{
					if (_eventColorCache.ContainsKey(value))
					{
						color = _eventColorCache[value];
					}
					else
					{
						Match m = Regex.Match(value, @"((\s*(?<attr>[^=\s]+)\s*=\s*[^\s]+)|(\s*(?<bhvr>[^\(\s]+)\([^\)]*\)))");

						if (m.Groups["attr"].Success)
							color = new Color().FromHexString((string)_colorTable["AttributeColors"][m.Groups["attr"].Value].Value);
						if (m.Groups["bhvr"].Success)
							color = new Color().FromHexString((string)_colorTable["BehaviorColors"][m.Groups["bhvr"].Value].Value);

						_eventColorCache.Add(value, color);
					}
				}
				e.Graphics.FillRectangle(new SolidBrush(color), e.CellBounds);
			}

			e.Paint(e.ClipBounds, e.PaintParts & ~DataGridViewPaintParts.Background & ~DataGridViewPaintParts.Focus);

			e.Handled = true;
		}

		private void addColumn(string name, string str, string propertyName, bool visibility)
		{
			DataGridViewColumn dgvc = new DataGridViewTextBoxColumn() { Name = name, HeaderText = str, DataPropertyName = propertyName, SortMode = DataGridViewColumnSortMode.Automatic, Visible = visibility };

			dataGridView.Columns.Add(dgvc);

			ToolStripMenuItem item = new ToolStripMenuItem(str) { CheckOnClick = true, Checked = visibility };

			item.CheckedChanged += (o, e) =>
			{
				if (item.Checked)
					dgvc.Visible = true;
				else
					dgvc.Visible = false;
			};

			dataGridView.ContextMenuStrip.Items.Add(item);
		}
		private void addColumn(string name, string str, string propertyName)
		{
			addColumn(name, str, propertyName, true);
		}

		private void setDataGridViewDataSource()
		{
			if (_traceLogData != null)
			{
				_dataSource = new SortableBindingList<TraceLog>(_traceLogData.LogDataBase.Select(ld =>
				{
					if (ld.Type == LogType.AttributeChange)
						return new TraceLog(ld.Time, _resourceData.ResourceHeader[ld.Object.Type].Name, ld.Object.Name, _resourceData.ResourceHeader[ld.Object.Type].Attributes[((AttributeChangeLogData)ld).Attribute].Name + " = " +((AttributeChangeLogData)ld).Value.ToString());
					else if (ld.Type == LogType.BehaviorCall)
						return new TraceLog(ld.Time, _resourceData.ResourceHeader[ld.Object.Type].Name, ld.Object.Name, _resourceData.ResourceHeader[ld.Object.Type].Behaviors[((BehaviorCallLogData)ld).Behavior].Name + "(" + ((BehaviorCallLogData)ld).Arguments.ToString() + ")");
					else
						return new TraceLog(ld.Time, ld.Object.Type, ld.Object.Name, "undefined");
				}).ToList()) { SecondSortPropertyName = "Id" };

				dataGridView.DataSource = _dataSource;

				_dataSource.Sorting += (o, e) =>
					{
						this.Invoke(new MethodInvoker(()=>
						{
							processingImage.Text = "ソート中";
							processingImage.Visible = true;
							hintStatusLabel.Visible = false;
						}));
					};
				_dataSource.Sorted += (o, e) =>
					{
						this.Invoke(new MethodInvoker(() =>
						{
							processingImage.Text = "";
							processingImage.Visible = false;
							hintStatusLabel.Visible = true;
						}));
					};
			}
		}

		private void setupColorTable()
		{
			Color color = new Color();
			int aplpha = 40;

			if (!ApplicationData.Setting.Data.ContainsKey("TraceLogViewerColors"))
				ApplicationData.Setting.Data.AddObject("TraceLogViewerColors");
			if (!ApplicationData.Setting.Data["TraceLogViewerColors"].ContainsKey("ResourceColors"))
				ApplicationData.Setting.Data["TraceLogViewerColors"].AddObject("ResourceColors");
			if (!ApplicationData.Setting.Data["TraceLogViewerColors"].ContainsKey("ResourceTypeColors"))
				ApplicationData.Setting.Data["TraceLogViewerColors"].AddObject("ResourceTypeColors");
			if (!ApplicationData.Setting.Data["TraceLogViewerColors"].ContainsKey("AttributeColors"))
				ApplicationData.Setting.Data["TraceLogViewerColors"].AddObject("AttributeColors");
			if (!ApplicationData.Setting.Data["TraceLogViewerColors"].ContainsKey("BehaviorColors"))
				ApplicationData.Setting.Data["TraceLogViewerColors"].AddObject("BehaviorColors");

			foreach (Resource res in _resourceData.Resources)
			{
				if (!ApplicationData.Setting.Data["TraceLogViewerColors"]["ResourceColors"].ContainsKey(res.Name))
				{
					ApplicationData.Setting.Data["TraceLogViewerColors"]["ResourceColors"].Add(res.Name, Color.FromArgb(aplpha, color.HueRotateNextColor()).ToHexString());
				}
			}
			foreach (ResourceType resType in _resourceData.ResourceHeader.ResourceTypes)
			{
				if (!ApplicationData.Setting.Data["TraceLogViewerColors"]["ResourceTypeColors"].ContainsKey(resType.Name))
				{
					ApplicationData.Setting.Data["TraceLogViewerColors"]["ResourceTypeColors"].Add(resType.Name, Color.FromArgb(aplpha, color.HueRotateNextColor()).ToHexString());
				}
				foreach (AttributeType attrType in resType.Attributes.Where<AttributeType>(a => a.AllocationType == AllocationType.Dynamic))
				{
					if (!ApplicationData.Setting.Data["TraceLogViewerColors"]["AttributeColors"].ContainsKey(attrType.Name))
					{
						ApplicationData.Setting.Data["TraceLogViewerColors"]["AttributeColors"].Add(attrType.Name, Color.FromArgb(aplpha, color.HueRotateNextColor()).ToHexString());
					}
				}
				foreach (Behavior bhvr in resType.Behaviors)
				{
					if (!ApplicationData.Setting.Data["TraceLogViewerColors"]["BehaviorColors"].ContainsKey(bhvr.Name))
					{
						ApplicationData.Setting.Data["TraceLogViewerColors"]["BehaviorColors"].Add(bhvr.Name, Color.FromArgb(aplpha, color.HueRotateNextColor()).ToHexString());
					}
				}
			}

			_colorTable = ApplicationData.Setting.Data["TraceLogViewerColors"];
		}

	}

	class TraceLog
	{
		public long Id { get; private set; }
		public Time Time { get; set; }
		public string ResourceType { get; set; }
		public string Resource { get; set; }
		public string Event { get; set; }

		public TraceLog(Time time, string resType, string res, string evnt)
		{
			Id = _id++;
			Time = time;
			ResourceType = resType;
			Resource = res;
			Event = evnt;
		}

		static long _id = 0;
	}
}
