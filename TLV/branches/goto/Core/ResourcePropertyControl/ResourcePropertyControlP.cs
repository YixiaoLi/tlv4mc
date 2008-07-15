using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Reflection;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.ResourcePropertyControl
{
    public partial class ResourcePropertyControlP : DockContent, IPresentation
    {
        public object SelectedObject
        {
            get { return dataGridView.DataSource; }
            set
            {
                PropertyDescriptorCollection viewableObjectTypePdc = PropertyDescriptorCollectionUtils.ConvertToPropertyDisplayPropertyDescriptor(TypeDescriptor.GetProperties(value.GetType()));

                List<object> list = new List<object>();

                foreach (PropertyDisplayPropertyDescriptor pd in viewableObjectTypePdc)
                {
                    if(pd.PropertyType != typeof(TimeLineEvents))
                    {
                        list.Add(new { Name = pd.DisplayName, Value = pd.GetValue(value).ToString()});
                    }
                }

                dataGridView.DataSource = list;
                dataGridView.AutoResizeColumns();
                dataGridView.DefaultCellStyle.SelectionForeColor = dataGridView.DefaultCellStyle.ForeColor;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ResourcePropertyControlP(string name)
        {
            InitializeComponent();
            this.Name = name;
            this.TabText = "リソース情報";
            dataGridView.AutoGenerateColumns = false;
            dataGridView.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridViewRowPrePaint);
        }

        protected void dataGridViewRowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
            e.PaintParts &= ~DataGridViewPaintParts.SelectionBackground;
            e.PaintParts &= ~DataGridViewPaintParts.ContentBackground;
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

    }
}
