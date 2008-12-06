using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text;
using System.ComponentModel;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TraceLogViewer : UserControl
	{
		private Dictionary<string, Color> _eventColorCache = new Dictionary<string, Color>();
		private Dictionary<string, Color> _resColorCache = new Dictionary<string, Color>();
		private Dictionary<string, Color> _resTypeColorCache = new Dictionary<string, Color>();
		private Dictionary<string, KeyValuePair<string[], Color>> _valTypeColorCache = new Dictionary<string, KeyValuePair<string[], Color>>();
		private const int _alpha = 40;

		private SortableBindingList<TraceLog> _dataSource = new SortableBindingList<TraceLog>();

		private TraceLogData _traceLogData;
		private ResourceData _resourceData;

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

			addColumn("eventType", "", "アイコン", "EventType", typeof(DataGridViewImageColumn));
			addColumn("time", "時間", "Time", typeof(DataGridViewTextBoxColumn));
			addColumn("resourceType", "リソースタイプ", "ResourceType", typeof(DataGridViewTextBoxColumn), false);
			addColumn("resource", "リソース", "Resource", typeof(DataGridViewTextBoxColumn));
			addColumn("event", "イベント", "Event", typeof(DataGridViewTextBoxColumn));

			dataGridView.Columns["eventType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			dataGridView.Columns["eventType"].Width = 22;
			dataGridView.ApplyNativeScroll();
			dataGridView.AutoGenerateColumns = false;
			dataGridView.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridViewCellPainting);
			dataGridView.MouseWheel += new MouseEventHandler(dataGridViewMouseWheel);
			dataGridView.MouseEnter += (o, _e) =>
				{
					if (dataGridView.DataSource != null)
					{
						ApplicationFactory.StatusManager.ShowHint(this.GetType().ToString() + ":zoomString", "文字サイズ変更", new[] { "Ctrl", "ホイール" });
						if (dataGridView.SelectedRows.Count != 0)
						{
							ApplicationFactory.StatusManager.ShowHint(this.GetType().ToString() + ":copy", "クリップボードへコピー", new[] { "Ctrl", "C" });
						}
						else
						{
							ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":copy");
						}
					}
				};
			dataGridView.MouseLeave += (o, _e) =>
				{
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":zoomString");
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":copy");
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":rightClickMenu");
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":sortInfo");
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":dragInfo");
				};
			dataGridView.LostFocus += (o, _e) =>
				{
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":zoomString");
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":copy");
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":rightClickMenu");
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":sortInfo");
					ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":dragInfo");
				};
			dataGridView.MouseMove += (o, _e) =>
				{
					if (dataGridView.DataSource != null)
					{
						if (_e.X > 0 && _e.X < dataGridView.Width && _e.Y > 0 && _e.Y < dataGridView.ColumnHeadersHeight)
						{
							ApplicationFactory.StatusManager.ShowHint(this.GetType().ToString() + ":sortInfo", "ソート", "左クリック");
							ApplicationFactory.StatusManager.ShowHint(this.GetType().ToString() + ":dragInfo", "列入れ替え", "ドラッグ");
						}
						else
						{
							ApplicationFactory.StatusManager.ShowHint(this.GetType().ToString() + ":rightClickMenu", "メニュー", "右クリック");
							ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":sortInfo");
							ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":dragInfo");
						}
					}
				};
			dataGridView.ColumnHeaderMouseClick += (o, _e) =>
				{
					DataGridViewColumn clickedColumn = dataGridView.Columns[_e.ColumnIndex];
					if (clickedColumn.SortMode != DataGridViewColumnSortMode.Automatic)
						sortDataGridViewRows(clickedColumn, true);
				};
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
						SetData(ApplicationData.FileContext.Data.TraceLogData, ApplicationData.FileContext.Data.ResourceData);
					}
				}));
			};
		}

		/// <summary>
		/// 指定された列を基準にして並び替えを行う
		/// </summary>
		/// <param name="sortColumn">基準にする列</param>
		/// <param name="orderToggle">並び替えの方向をトグルで変更する</param>
		private void sortDataGridViewRows(DataGridViewColumn sortColumn, bool orderToggle)
		{
			if (sortColumn == null)
				return;

			//今までの並び替えグリフを消す
			if (sortColumn.SortMode == DataGridViewColumnSortMode.Programmatic
				&& dataGridView.SortedColumn != null
				&& !dataGridView.SortedColumn.Equals(sortColumn))
			{
				dataGridView.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
			}

			ListSortDirection sortDirection;

			//並び替えの方向（昇順か降順か）を決める
			if (orderToggle)
				sortDirection = dataGridView.SortOrder == SortOrder.Descending ? ListSortDirection.Ascending : ListSortDirection.Descending;
			else
				sortDirection = dataGridView.SortOrder == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending;

			SortOrder sortOrder = sortDirection == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;

			//並び替えグリフを変更
			if (sortColumn.SortMode == DataGridViewColumnSortMode.Programmatic)
				sortColumn.HeaderCell.SortGlyphDirection = sortOrder;

			//並び替えを行う
			dataGridView.Sort(sortColumn, sortDirection);
		}

		private void dataGridViewMouseWheel(object sender, MouseEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
			{
				int v = e.Delta > 0 ? 1 : -1;
				if (dataGridView.DefaultCellStyle.Font.Size + v > 1 && dataGridView.DefaultCellStyle.Font.Size + v < 100)
				{
					dataGridView.DefaultCellStyle.Font = new System.Drawing.Font(dataGridView.DefaultCellStyle.Font.FontFamily, dataGridView.DefaultCellStyle.Font.Size + v);
				}
				if (dataGridView.ColumnHeadersDefaultCellStyle.Font.Size + v > 1 && dataGridView.ColumnHeadersDefaultCellStyle.Font.Size + v < 100)
				{
					dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dataGridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, dataGridView.ColumnHeadersDefaultCellStyle.Font.Size + v);
				}
			}
		}

		private void dataGridViewCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			bool isResourceColumn = e.ColumnIndex == dataGridView.Columns["Resource"].Index;
			bool isResourceTypeColumn = e.ColumnIndex == dataGridView.Columns["ResourceType"].Index;
			bool isEventColumn = e.ColumnIndex == dataGridView.Columns["Event"].Index;

			DataGridViewPaintParts dgvpp = e.PaintParts;

			e.PaintBackground(e.ClipBounds, true);

			if (e.RowIndex >= 0
				&& (isResourceColumn || isResourceTypeColumn || isEventColumn)
				&& (dgvpp & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
			{
				string value = (string)e.Value;
				KeyValuePair<string[], Color>?  valTextColor = null;
				Color color = Color.White;
				RotateColorFactory colorFactory = new RotateColorFactory();
				string attr = null;
				string val = null;
				string attr_val = null;

				if (isResourceColumn)
				{
					if (_resColorCache.ContainsKey(value))
					{
						color = _resColorCache[value];
					}
					else
					{
						if (ApplicationData.FileContext.Data.SettingData.ColorSetting.ResourceColors.ContainsKey(value))
						{
							color = ApplicationData.FileContext.Data.SettingData.ColorSetting.ResourceColors[value];
						}
						else
						{
							color = Color.FromArgb(_alpha, colorFactory.RotateColor());
							ApplicationData.FileContext.Data.SettingData.ColorSetting.ResourceColors.Add(value, color);
						}
						_resColorCache.Add(value, color);
					}
				}
				else if (isResourceTypeColumn)
				{
					if (_resTypeColorCache.ContainsKey(value))
					{
						color = _resTypeColorCache[value];
					}
					else
					{
						if (ApplicationData.FileContext.Data.SettingData.ColorSetting.ResourceTypeColors.ContainsKey(value))
						{
							color = ApplicationData.FileContext.Data.SettingData.ColorSetting.ResourceTypeColors[value];
						}
						else
						{
							color = Color.FromArgb(_alpha, colorFactory.RotateColor());
							ApplicationData.FileContext.Data.SettingData.ColorSetting.ResourceTypeColors.Add(value, color);
						}
						_resTypeColorCache.Add(value, color);
					}
				}
				else if (isEventColumn)
				{
					if (_eventColorCache.ContainsKey(value) && _valTypeColorCache.ContainsKey(value))
					{
						color = _eventColorCache[value];
						valTextColor = _valTypeColorCache[value];
					}
					else
					{
						Match m = Regex.Match(value, @"((\s*(?<attr>[^=\s]+)(?<attr_val>\s*=\s*)(?<val>[^\s]+))|(\s*(?<bhvr>[^\(\s]+)\((?<args>[^\)]*)\)))");

						if (m.Groups["attr"].Success)
						{
							Color txtColor = Color.Empty;
							attr = m.Groups["attr"].Value;
							attr_val = m.Groups["attr_val"].Value;
							val = m.Groups["val"].Value;

							if (_eventColorCache.ContainsKey(value))
							{
								color = _eventColorCache[value];
							}
							else
							{
								if (ApplicationData.FileContext.Data.SettingData.ColorSetting.AttributeColors.ContainsKey(attr))
								{
									color = ApplicationData.FileContext.Data.SettingData.ColorSetting.AttributeColors[attr];
								}
								else
								{
									color = Color.FromArgb(_alpha, colorFactory.RotateColor());
									ApplicationData.FileContext.Data.SettingData.ColorSetting.AttributeColors.Add(attr, color);
								}
								_eventColorCache.Add(value, color);
							}

							if (_valTypeColorCache.ContainsKey(value))
							{
								valTextColor = _valTypeColorCache[value];
							}
							else
							{
								if (ApplicationData.FileContext.Data.SettingData.ColorSetting.ValueColors.ContainsKey(attr + val))
								{
									txtColor = ApplicationData.FileContext.Data.SettingData.ColorSetting.ValueColors[attr + val];
								}
								else
								{
									txtColor = colorFactory.RotateColor();
									ApplicationData.FileContext.Data.SettingData.ColorSetting.ValueColors.Add(attr + val, txtColor);
								}

								valTextColor = new KeyValuePair<string[], Color>(new[] { attr, attr_val, val }, txtColor);

								_valTypeColorCache.Add(value, valTextColor.Value);
							}
						}
						if (m.Groups["bhvr"].Success)
						{
							string bhvr = m.Groups["bhvr"].Value;
							if (ApplicationData.FileContext.Data.SettingData.ColorSetting.BehaviorColors.ContainsKey(bhvr))
							{
								color = ApplicationData.FileContext.Data.SettingData.ColorSetting.BehaviorColors[bhvr];
							}
							else
							{
								color = Color.FromArgb(_alpha, colorFactory.RotateColor());
								ApplicationData.FileContext.Data.SettingData.ColorSetting.BehaviorColors.Add(bhvr, color);
							}
						}
					}
				}

				e.Graphics.FillRectangle(new SolidBrush(color), e.CellBounds);

				if (valTextColor.HasValue)
				{
					Color foreColor = Color.Empty;
					Color valForeColor = Color.Empty;
					if (!dataGridView.Rows[e.RowIndex].Selected)
					{
						foreColor = e.CellStyle.ForeColor;
						valForeColor = valTextColor.Value.Value;
					}
					else
					{
						foreColor = e.CellStyle.SelectionForeColor;
						valForeColor = valTextColor.Value.Value.ComplementaryColor();
					}

					dgvpp &= ~DataGridViewPaintParts.ContentForeground;

					int strheight = TextRenderer.MeasureText(e.Graphics, valTextColor.Value.Key[0], e.CellStyle.Font).Height;
					int topPadding = (e.CellBounds.Height - strheight) / 2;
					int attrWidth = TextRenderer.MeasureText(e.Graphics, valTextColor.Value.Key[0], e.CellStyle.Font).Width;
					int attr_valWidth = TextRenderer.MeasureText(e.Graphics, valTextColor.Value.Key[1], e.CellStyle.Font).Width;

					Rectangle attrRect = new Rectangle(e.CellBounds.X, e.CellBounds.Y + topPadding, e.CellBounds.Width, strheight);
					Rectangle attr_valRect = new Rectangle(e.CellBounds.X + attrWidth, e.CellBounds.Y + topPadding, e.CellBounds.Width - attrWidth, strheight);
					Rectangle valRect = new Rectangle(e.CellBounds.X + attrWidth + attr_valWidth, e.CellBounds.Y + topPadding, e.CellBounds.Width - attrWidth - attr_valWidth, strheight);
					TextFormatFlags tf = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;

					TextRenderer.DrawText(e.Graphics, valTextColor.Value.Key[0], e.CellStyle.Font, attrRect, foreColor, tf);
					TextRenderer.DrawText(e.Graphics, valTextColor.Value.Key[1], e.CellStyle.Font, attr_valRect, foreColor, tf);
					TextRenderer.DrawText(e.Graphics, valTextColor.Value.Key[2], e.CellStyle.Font, valRect, valForeColor, tf);
				}
			}

			e.Paint(e.ClipBounds, dgvpp & ~DataGridViewPaintParts.Background & ~DataGridViewPaintParts.Focus);

			e.Handled = true;
		}

		private void addColumn(string name, string displayName, string propertyName, Type columnType, bool visibility)
		{
			addColumn(name, displayName, displayName, propertyName, columnType, visibility);
		}
		private void addColumn(string name, string displayName, string propertyName, Type columnType)
		{
			addColumn(name, displayName, displayName, propertyName, columnType, true);
		}
		private void addColumn(string name, string displayName, string contextMenuDisplayName, string propertyName, Type columnType)
		{
			addColumn(name, displayName, contextMenuDisplayName, propertyName, columnType, true);
		}
		private void addColumn(string name, string displayName, string contextMenuDisplayName, string propertyName, Type columnType, bool visibility)
		{
			DataGridViewColumn dgvc = (DataGridViewColumn)Activator.CreateInstance(columnType);
			dgvc.Name = name;
			dgvc.HeaderText = displayName;
			dgvc.DataPropertyName = propertyName;
			dgvc.SortMode = DataGridViewColumnSortMode.Programmatic;
			dgvc.Visible = visibility;

			dataGridView.Columns.Add(dgvc);

			ToolStripMenuItem item = new ToolStripMenuItem(contextMenuDisplayName) { CheckOnClick = true, Checked = visibility };

			item.CheckedChanged += (o, e) =>
			{
				if (item.Checked)
					dgvc.Visible = true;
				else
					dgvc.Visible = false;
			};

			dataGridView.ContextMenuStrip.Items.Add(item);
		}

		private void setDataGridViewDataSource()
		{
			if (_traceLogData != null)
			{
				_dataSource = new SortableBindingList<TraceLog>(_traceLogData.LogDataBase.Select(ld =>
				{
					if (ld.Type == LogType.AttributeChange)
					{
						Image image = Properties.Resources.attribute;
						image.Tag = "attribute";
						return new TraceLog(image, ld.Time, _resourceData.ResourceHeaders[ld.Object.Type].Name, ld.Object.Name, _resourceData.ResourceHeaders[ld.Object.Type].Attributes[((AttributeChangeLogData)ld).Attribute].Name + " = " + ((AttributeChangeLogData)ld).Value.ToString());
					}
					else if (ld.Type == LogType.BehaviorCall)
					{
						Image image = Properties.Resources.behavior;
						image.Tag = "behavior";
						return new TraceLog(image, ld.Time, _resourceData.ResourceHeaders[ld.Object.Type].Name, ld.Object.Name, _resourceData.ResourceHeaders[ld.Object.Type].Behaviors[((BehaviorCallLogData)ld).Behavior].Name + "(" + ((BehaviorCallLogData)ld).Arguments.ToString() + ")");
					}
					else
					{
						Image image = Properties.Resources.warning;
						image.Tag = "undefined";
						return new TraceLog(image, ld.Time, ld.Object.Type, ld.Object.Name, "undefined");
					}
				}).ToList());
				_dataSource.BasePropertyName = "Id";
				_dataSource.Comparisoins.Add("EventType", (t1, t2) =>
					{
						return t1.EventType.Tag.ToString().CompareTo(t2.EventType.Tag.ToString());
					});
				_dataSource.Comparisoins.Add("Time", (t1, t2) =>
					{
						return t1.Id.CompareTo(t2.Id);
					});

				_dataSource.Sorting += (o, e) =>
					{
						this.Invoke(new MethodInvoker(() =>
						{
							ApplicationFactory.StatusManager.ShowProcessing(this.GetType().ToString() + ":sorting", "ソート中");
						}));
					};
				_dataSource.Sorted += (o, e) =>
					{
						this.Invoke(new MethodInvoker(() =>
						{
							dataGridView.Refresh();
							ApplicationFactory.StatusManager.HideProcessing(this.GetType().ToString() + ":sorting");
						}));
					};
				dataGridView.DataSource = _dataSource;
			}
		}

	}

	class TraceLog
	{
		public long Id { get; private set; }
		public Image EventType { get; set; }
		public Time Time { get; set; }
		public string ResourceType { get; set; }
		public string Resource { get; set; }
		public string Event { get; set; }

		public TraceLog(Image eventType, Time time, string resType, string res, string evnt)
		{
			Id = _id++;
			EventType = eventType;
			Time = time;
			ResourceType = resType;
			Resource = res;
			Event = evnt;
		}

		static long _id = 0;
	}
}
