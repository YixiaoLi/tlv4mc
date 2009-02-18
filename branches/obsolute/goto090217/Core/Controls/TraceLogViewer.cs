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
using System.Collections;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TraceLogViewer : UserControl
	{
		private const int _alpha = 40;

		private SortableBindingList<TraceLogViewerRowData> _dataSource = new SortableBindingList<TraceLogViewerRowData>();

		private TraceLogVisualizerData _data;

		public TraceLogViewer()
		{
			InitializeComponent();
		}

		public void SetData(TraceLogVisualizerData data)
		{
			ClearData();

			_data = data;

			_data.SettingData.TraceLogViewerSetting.BecameDirty += traceLogViewerSettingBecameDirty;
			_data.SettingData.ResourceExplorerSetting.BecameDirty += resourceExplorerSettingBecameDirty;

			if (_data.ResourceData != null)
			{
				dataGridView.Columns["time"].HeaderText = "時間[" + _data.ResourceData.TimeScale + "]";

				setDataGridViewDataSource();
			}
			else
			{
				dataGridView.Columns["time"].HeaderText = "時間";
			}

			if (!_data.SettingData.TraceLogViewerSetting.FirstDisplayedTime.IsEmpty)
				setDisplayedRowIndexByTime(_data.SettingData.TraceLogViewerSetting.FirstDisplayedTime);
			else
				setDisplayedRowIndexByTime(_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.FromTime);

			foreach (KeyValuePair<string, bool> kvp in (IList)_data.SettingData.ResourceExplorerSetting.ResourceVisibility)
			{
				setResourceVisibleChange(kvp.Key, kvp.Value);
			}

		}

		private void resourceExplorerSettingBecameDirty(object sender, string propertyName)
		{
			foreach (KeyValuePair<string, bool> kvp in (IList)sender)
			{
				setResourceVisibleChange(kvp.Key, kvp.Value);
			}
		}

		private void setResourceVisibleChange(string resName, bool value)
		{
			foreach (TraceLogViewerRowData d in _dataSource.Where(l=>l.ResourceDisplayName == _data.ResourceData.Resources[resName].DisplayName))
			{
				dataGridView.BindingContext[_dataSource].SuspendBinding();
				dataGridView.Rows[_dataSource.IndexOf(d)].Visible = value;
				dataGridView.BindingContext[_dataSource].ResumeBinding();
			}
		}

		private void traceLogViewerSettingBecameDirty(object sender, string propertyName)
		{
			switch (propertyName)
			{
				case "FirstDisplayedTime":
					setDisplayedRowIndexByTime(_data.SettingData.TraceLogViewerSetting.FirstDisplayedTime);
					break;
			}
		}

		private void setDisplayedRowIndexByTime(Time time)
		{
			int i = _dataSource.IndexOf(_dataSource.First(l => l.Time >= time));
			while (!dataGridView.Rows[i].Visible)
			{
				i++;

				if (dataGridView.Rows.Count <= i)
					return;
			}
			dataGridView.ClearSelection();
			dataGridView.Rows[i].Selected = true;
			dataGridView.FirstDisplayedScrollingRowIndex = i;

		}

		public void ClearData()
		{
			_data = null;
			_dataSource = new SortableBindingList<TraceLogViewerRowData>();
			dataGridView.DataSource = null;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			addColumn("eventType", "", "アイコン", "EventType", typeof(DataGridViewImageColumn));
			addColumn("time", "時間", "Time", typeof(DataGridViewTextBoxColumn));
			addColumn("resourceType", "リソースタイプ", "ResourceType", typeof(DataGridViewTextBoxColumn), false);
			addColumn("resource", "リソース", "ResourceDisplayName", typeof(DataGridViewTextBoxColumn));
			addColumn("event", "イベント", "Event", typeof(DataGridViewTextBoxColumn));

			dataGridView.Columns["eventType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			dataGridView.Columns["eventType"].Width = 22;
			dataGridView.ApplyNativeScroll();
			dataGridView.AutoGenerateColumns = false;
			dataGridView.RowPrePaint += dataGridViewRowPrePaint;
			dataGridView.CellPainting += dataGridViewCellPainting;
			dataGridView.MouseWheel += dataGridViewMouseWheel;
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

						if (dataGridView.HitTest(_e.X, _e.Y).RowIndex != -1)
						{
							Time time = _dataSource[dataGridView.HitTest(_e.X, _e.Y).RowIndex].Time;
							ApplicationFactory.BlackBoard.CursorTime = time;
						}
					}
				};
			dataGridView.MouseDoubleClick += new MouseEventHandler(dataGridViewMouseDoubleClick);
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
						SetData(ApplicationData.FileContext.Data);
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
			ApplicationFactory.CommandManager.Do(new GeneralCommand(Text + " 並び替え",
				() =>
				{
					dataGridView.Sort(sortColumn, sortDirection); 			//セルの色設定を変更する
				},
				() =>
				{
					dataGridView.Sort(sortColumn, sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending);			//セルの色設定を変更する
				}));

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

				int h = TextRenderer.MeasureText("A", dataGridView.DefaultCellStyle.Font).Height + 4;

				foreach (DataGridViewRow row in dataGridView.Rows)
				{
					row.Height = h;
				}

				dataGridView.RowTemplate.Height = h;

				if (e.GetType() == typeof(ExMouseEventArgs))
					((ExMouseEventArgs)e).Handled = true;
			}
		}

		private void dataGridViewRowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			e.Graphics.FillRectangle(new SolidBrush(dataGridView.DefaultCellStyle.BackColor), e.RowBounds);

			e.PaintCellsBackground(e.RowBounds, false);

			e.PaintParts &= ~DataGridViewPaintParts.Background & ~DataGridViewPaintParts.Focus;
		}

		private void dataGridViewCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (dataGridView.DataSource == null || e.RowIndex == -1)
				return;

			LogData ld = ((SortableBindingList<TraceLogViewerRowData>)dataGridView.DataSource)[e.RowIndex].LogData;
			Color color = dataGridView.DefaultCellStyle.BackColor;
			if (dataGridView.Columns[e.ColumnIndex].Name == "resourceType")
			{
				color = Color.FromArgb(150, _data.ResourceData.ResourceHeaders[ld.Object.Type].Color.Value);
			}
			if (dataGridView.Columns[e.ColumnIndex].Name == "resource")
			{
				color = Color.FromArgb(150, ld.Object.Color.Value);
			}
			if (dataGridView.Columns[e.ColumnIndex].Name == "event")
			{
				if (ld.Type == TraceLogType.AttributeChange)
				{
					color = Color.FromArgb(150, _data.ResourceData.ResourceHeaders[ld.Object.Type].Attributes[((AttributeChangeLogData)ld).Attribute.Name].Color.Value);
				}
				else if (ld.Type == TraceLogType.BehaviorHappen)
				{
					color = Color.FromArgb(150, _data.ResourceData.ResourceHeaders[ld.Object.Type].Behaviors[((BehaviorHappenLogData)ld).Behavior.Name].Color.Value);
				}
			}
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, color)), e.CellBounds);

			if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None)
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, dataGridView.DefaultCellStyle.SelectionBackColor)), e.CellBounds);
		}

		private void dataGridViewMouseDoubleClick(object sender, MouseEventArgs e)
		{
			Time time = _dataSource[dataGridView.HitTest(e.X, e.Y).RowIndex].Time;

			Time span = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine.ViewingSpan / 2;

			_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.SetTime(time - span, time + span);
			ApplicationFactory.BlackBoard.CursorTime = time;
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
			if (_data.TraceLogData != null)
			{
				_dataSource = new SortableBindingList<TraceLogViewerRowData>(_data.TraceLogData.LogDataBase.Select(ld => new TraceLogViewerRowData(ld)).ToList());

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
							ApplicationFactory.StatusManager.HideProcessing(this.GetType().ToString() + ":sorting");
							Refresh();
						}));
					};
				dataGridView.DataSource = _dataSource;
			}
		}

	}

	class TraceLogViewerRowData
	{
		public long Id { get; private set; }
		public Image EventType { get; set; }
		public Time Time { get; set; }
		public string ResourceType { get; set; }
		public string ResourceDisplayName { get; set; }
		public string Event { get; set; }
		public LogData LogData { get; private set; }

		public TraceLogViewerRowData(LogData ld)
		{
			Id = _id++;
			LogData = ld;

			Time = ld.Time;
			ResourceType = ld.Object.Type;
			ResourceDisplayName = ld.Object.DisplayName;

			switch (ld.Type)
			{
				case TraceLogType.AttributeChange:
					EventType = Properties.Resources.attribute;
					EventType.Tag = "attribute";
					Event = ((AttributeChangeLogData)ld).Attribute.Name + " = " + ((AttributeChangeLogData)ld).Attribute.Value.ToString();
					break;
				case TraceLogType.BehaviorHappen:
					EventType = Properties.Resources.behavior;
					EventType.Tag = "behavior";
					Event = ((BehaviorHappenLogData)ld).Behavior.Name + "(" + ((BehaviorHappenLogData)ld).Behavior.Arguments.ToString() + ")";
					break;
				default:
					EventType = Properties.Resources.warning;
					EventType.Tag = "undefined";
					Event = string.Empty;
					break;
			}
		}

		static long _id = 0;
	}
}
