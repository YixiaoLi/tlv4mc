using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class StatisticsExplorer : UserControl
    {
        /// <summary>
        /// _dataGridViewのデータソース
        /// </summary>
        private BindingList<StatisticsExplorerRowData> _dataSource = new BindingList<StatisticsExplorerRowData>();
        /// <summary>
        /// 統計情報ビューアの親フォーム　Focus移動でビューアが隠れることを防ぎたいときに使用
        /// </summary>
        private Form _ownedViewer = null;

        public StatisticsExplorer()
        {
            InitializeComponent();
        }

        public StatisticsExplorer(Form owner)
            : this()
        {
            _ownedViewer = owner;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _dataGridView.MouseEnter += (o, _e) =>
                {
                    if (_dataGridView.DataSource != null)
                    {
                        ApplicationFactory.StatusManager.ShowHint(this.GetType().ToString() + ":changeVisible", "統計情報ビューアの表示/非表示", "ボタンを左クリック");
                    }
                };
            _dataGridView.MouseLeave += (o, _e) =>
                {            
                    ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":changeVisible");
                };
            _dataGridView.LostFocus += (o, _e) =>
                {
                    ApplicationFactory.StatusManager.HideHint(this.GetType().ToString() + ":changeVisible");
                };

            ApplicationData.FileContext.DataChanged += new EventHandler<NU.OJL.MPRTOS.TLV.Base.GeneralEventArgs<TraceLogVisualizerData>>(fileContextDataChanged);            
        }

        private void fileContextDataChanged(object sender, GeneralEventArgs<TraceLogVisualizerData> e)
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
        }

        public void SetData(TraceLogVisualizerData data)
        {
            if (data.StatisticsData == null)
            {
                return;
            }

            ClearData();

            foreach (Statistics s in data.StatisticsData.Statisticses)
            {
                var obj = new StatisticsExplorerRowData(s);
                obj.Viewer.FormClosing += (o, e) => 
                {
                    if (e.CloseReason == CloseReason.UserClosing)
                    {
                        e.Cancel = true;
                        ((StatisticsViewer)o).Visible = false;
                        this.Refresh();
                    }
                };
                if (_ownedViewer != null)
                {
                    obj.Viewer.Owner = _ownedViewer;
                }
                _dataSource.Add(obj);
            }

            _dataGridView.Columns["NameColumn"].DataPropertyName = "DisplayName"; // 2度目の生成で例外
            _dataGridView.Columns["VisibleColumn"].DataPropertyName = "Visible";

            _dataGridView.DataSource = _dataSource;
            // "NameColumn", "VisibleColumn"以外は表示させない
            _dataGridView.Columns["Id"].Visible = false;
            _dataGridView.Columns["Viewer"].Visible = false;
            //_dataGridView.Columns["VisibleButtonText"].Visible = false;

            _dataGridView.ClearSelection();
        }

        public void ClearData()
        {
            foreach (StatisticsExplorerRowData serd in _dataSource)
            {
                serd.Viewer.Visible = false;
                serd.Viewer.Dispose();
            }
            _dataSource = new BindingList<StatisticsExplorerRowData>();
        }

        private void _dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // チェックボックスが押された場合
            if (_dataGridView.Columns[e.ColumnIndex].Name == "VisibleColumn")
            {
                _dataGridView.BindingContext[_dataSource].SuspendBinding();

                _dataSource[e.RowIndex].Visible = !_dataSource[e.RowIndex].Visible; 
                
                _dataGridView.BindingContext[_dataSource].ResumeBinding();

                _dataSource[e.RowIndex].Viewer.Visible = _dataSource[e.RowIndex].Visible;
            }
        }
    }

    /// <summary>
    /// _dataGridViewの行データ
    /// </summary>
    class StatisticsExplorerRowData
    {
        /// <summary>
        /// 統計情報の識別子
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// 一覧に表示する統計情報名
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 統計情報ビューア　表示非表示のコントロールに使い、行には表示されない
        /// </summary>
        public StatisticsViewer Viewer { get; set; }
        /// <summary>
        /// 統計情報ビューアが表示されているかどうか
        /// </summary>
        public bool Visible { get { return Viewer.Visible; } set { Viewer.Visible = value; } }

        public StatisticsExplorerRowData(Statistics data)
        {
            Id = data.Name;
            DisplayName = data.Setting.Title;
            Viewer = new StatisticsViewer(data);
            Visible = false;
        }
    }
}
