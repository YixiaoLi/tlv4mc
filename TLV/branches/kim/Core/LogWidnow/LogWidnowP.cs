using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;


namespace NU.OJL.MPRTOS.TLV.Core.LogWindow
{
    public partial class LogWindowP : WeifenLuo.WinFormsUI.Docking.DockContent, IPresentation
    {
        private LogList logList;
        private ListViewColumnSorter lvwColumnSorter;

        public LogWindowP(string name)
        {
            InitializeComponent();

            this.Name = name;

            lvwColumnSorter = new ListViewColumnSorter();
            this.listView.ListViewItemSorter = lvwColumnSorter;

        }

        public void AddChild(Control control, object args)
        {

        }

        public void InitLogWindow(string filePath)
        {
            LogFileManager logFile = new LogFileManager();

            logFile.ReadLogFile(filePath, out logList);

            this.listView.Clear();

            initListViewHeader();
            initListViewItem();
            
            //ソート初期化
            lvwColumnSorter.SortColumn = 0;
            lvwColumnSorter.Order = SortOrder.Ascending;
            this.listView.Sort();

        }

        private void initListViewHeader()
        {
            this.listView.View = View.Details;
            this.listView.Columns.Add("時間",100, HorizontalAlignment.Right);
            this.listView.Columns.Add("プロセッサID", 100, HorizontalAlignment.Center);
            this.listView.Columns.Add("リソースタイプ", 100, HorizontalAlignment.Center);
            this.listView.Columns.Add("リソースID", 100, HorizontalAlignment.Center);
            this.listView.Columns.Add("状態", 200, HorizontalAlignment.Left);
        }

        private void initListViewItem()
        {

            foreach (Log log in this.logList.List)
            {
                string[] itemList = new string[] {log.Time.ToString(), log.PrcID.ToString(), 
                    log.Subject.Type.ToString(), log.Subject.Id.ToString(), log.Verb.ToString()};

                this.listView.Items.Add(new ListViewItem(itemList));
            }

        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            this.listView.Sort();

        }

    }

    public class ListViewColumnSorter : IComparer
    {
　      private int ColumnToSort;
        private SortOrder OrderOfSort;
        private CaseInsensitiveComparer ObjectCompare;

        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }

    }
}
