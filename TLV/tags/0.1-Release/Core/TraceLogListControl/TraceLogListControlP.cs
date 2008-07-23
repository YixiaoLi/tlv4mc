using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;
using System.Linq;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.TraceLogListControl
{
    public partial class TraceLogListControlP : DockContent, IPresentation
    {
        private ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
        private DataGridViewTextBoxColumn timeColumn = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn eventColumn = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn subjectColumn = new DataGridViewTextBoxColumn();
        private DataGridViewColumn timeLineColumnPreviousColumn = new DataGridViewColumn();
        private LogList logList = new LogList();
        private TimeLineViewableObjectList<TaskInfo> viewableObjectList = new TimeLineViewableObjectList<TaskInfo>();

        public LogList LogList
        {
            get { return logList; }
            set
            {
                if (value != logList && value != null)
                {
                    logList = value;
                    NotifyPropertyChanged("ViewableObjectList");

                    foreach (Log log in logList.List)
                    {
                        log.Subject = viewableObjectList[log.MetaId].Name;
                    }

                    dataGridView.DataSource = logList.List;

                    dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
        }
        public TimeLineViewableObjectList<TaskInfo> ViewableObjectList
        {
            get { return viewableObjectList; }
            set
            {
                if (value != viewableObjectList)
                {
                    viewableObjectList = value;
                    NotifyPropertyChanged("ViewableObjectList");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TraceLogListControlP(string name)
        {
            InitializeComponent();
            this.Name = name;
            dataGridView.ContextMenuStrip = contextMenuStrip;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dataGridView.ColumnWidthChanged += new DataGridViewColumnEventHandler(dataGridViewColumnWidthChanged);

            eventColumn.Name = "eventColumn";
            eventColumn.HeaderText = "ログ";
            eventColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            eventColumn.DataPropertyName = "Verb";

            subjectColumn.Name = "subjectColumn";
            subjectColumn.HeaderText = "タスク";
            subjectColumn.DataPropertyName = "Subject";

            timeColumn.Name = "timeColumn";
            timeColumn.HeaderText = "時間[ns]";
            timeColumn.DataPropertyName = "Time";

            dataGridView.Columns.Add(timeColumn);
            dataGridView.Columns.Add(subjectColumn);
            dataGridView.Columns.Add(eventColumn);

            subjectColumn.Frozen = true;

            //constructColumns();
        }

        public void Add(IPresentation presentation)
        {

        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void constructColumns()
        {
            PropertyDescriptorCollection viewableObjectTypePdc = PropertyDescriptorCollectionUtils.ConvertToPropertyDisplayPropertyDescriptor(TypeDescriptor.GetProperties(typeof(TaskInfo)));

            List<DataGridViewColumn> columns = new List<DataGridViewColumn>();
            Dictionary<DataGridViewColumn, bool> browsables = new Dictionary<DataGridViewColumn, bool>();

            columns.Add(timeColumn);

            foreach (PropertyDisplayPropertyDescriptor pd in viewableObjectTypePdc)
            {
                Type type = pd.PropertyType;
                string name = pd.Name;
                string headerText = pd.DisplayName;
                bool browsable = pd.DefaultBrowsable;

                if (type != typeof(TimeLineEvents))
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.Name = name;
                    column.ValueType = type;
                    column.DataPropertyName = name;
                    column.HeaderText = headerText;
                    column.CellTemplate = new DataGridViewTextBoxCell();

                    columns.Add(column);

                    ToolStripMenuItem newcontitem = new ToolStripMenuItem();
                    newcontitem.Text = headerText;
                    newcontitem.Name = name + "ContextMenuStrip";
                    newcontitem.Checked = browsable;
                    dataGridView.ContextMenuStrip.Items.Add(newcontitem);
                    browsables[column] = browsable;
                    newcontitem.Click += delegate
                    {
                        if (dataGridView.Columns[column.Name].Visible)
                        {
                            dataGridView.Columns[column.Name].Visible = false;
                            newcontitem.Checked = false;
                        }
                        else
                        {
                            dataGridView.Columns[column.Name].Visible = true;
                            newcontitem.Checked = true;
                        }
                    };
                }

            }

            columns.Add(eventColumn);

            dataGridView.Columns.AddRange(columns.ToArray());

            timeLineColumnPreviousColumn = dataGridView.Columns[columns.Count - 2];
            timeLineColumnPreviousColumn.Frozen = true;

            foreach (KeyValuePair<DataGridViewColumn, bool> kvp in browsables)
            {
                kvp.Key.Visible = kvp.Value;
            }
        }

        private void dataGridViewColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int timeLineColumnPreviousColumnIndex = 0;
            for (int i = 2; i <= dataGridView.Columns.Count; i++)
            {
                if (dataGridView.Columns[dataGridView.Columns.Count - i].Visible)
                {
                    if (timeLineColumnPreviousColumnIndex < dataGridView.Columns[dataGridView.Columns.Count - i].DisplayIndex)
                    {
                        timeLineColumnPreviousColumn = dataGridView.Columns[dataGridView.Columns.Count - i];
                        timeLineColumnPreviousColumnIndex = dataGridView.Columns[dataGridView.Columns.Count - i].DisplayIndex;
                    }
                }
            }
        }

    }
}
