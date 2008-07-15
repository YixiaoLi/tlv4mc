using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceSelectControl
{
    public partial class ResourceSelectControlP : DockContent, IPresentation
    {
        private Dictionary<Type, List<TaskInfo>> viewableObjectList = new Dictionary<Type, List<TaskInfo>>();
        private SortableBindingList<TaskInfo> viewableObjectDataSource;
        public object selectedObject;

        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<Type, List<TaskInfo>> ViewableObjectList
        {
            get { return viewableObjectList; }
            set
            {
                if (value != viewableObjectList && value != null)
                {
                    viewableObjectList = value;
                    NotifyPropertyChanged("ViewableObjectList");

                    foreach (KeyValuePair<Type, List<TaskInfo>> vo in viewableObjectList)
                    {
                        var pis = from pi in vo.Key.GetProperties()
                                             where pi.IsDefined(typeof(PropertyDisplayNameAttribute), true)
                                             && ((PropertyDisplayNameAttribute)(pi.GetCustomAttributes(typeof(PropertyDisplayNameAttribute), true)[0])).Categorizable
                                             select pi;

                        foreach(var pi in pis)
                        {
                            PropertyDisplayNameAttribute pdna = (PropertyDisplayNameAttribute)(pi.GetCustomAttributes(typeof(PropertyDisplayNameAttribute), true)[0]);

                            if (! tabControl.TabPages.ContainsKey(pi.Name))
                            {
                                TabPage tab = new TabPage(pdna.PropertyDisplayName + "別");
                                tab.Name = pi.Name;
                                tabControl.TabPages.Add(tab);
                                TreeView tv = new TreeView();
                                tv.Dock = DockStyle.Fill;
                                tv.Name = "treeView";
                                tv.CheckBoxes = true;
                                tv.AfterCheck += new TreeViewEventHandler(tvAfterCheck);
                                tv.AfterSelect += new TreeViewEventHandler(tvAfterSelect);
                                tabControl.TabPages[pi.Name].Controls.Add(tv);
                            }

                            foreach(TimeLineViewableObject to in vo.Value)
                            {
                                string str = to.GetType().GetProperty(pi.Name).GetValue(to, null).ToString();

                                if (! ((TreeView)tabControl.TabPages[pi.Name].Controls["treeView"]).Nodes.ContainsKey(str))
                                {
                                    TreeNode tn = new TreeNode(str);
                                    tn.Name = str;
                                    ((TreeView)tabControl.TabPages[pi.Name].Controls["treeView"]).Nodes.Add(tn);
                                }

                                TreeNode n = new TreeNode(to.ToString());
                                n.Name = to.ToString();

                                ((TreeView)tabControl.TabPages[pi.Name].Controls["treeView"]).Nodes[str].Nodes.Add(n);

                            }
                            ((TreeView)tabControl.TabPages[pi.Name].Controls["treeView"]).ExpandAll();
                        }
                    }

                }
            }
        }
        public SortableBindingList<TaskInfo> ViewableObjectDataSource
        {
            get { return viewableObjectDataSource; }
            set
            {
                if (viewableObjectDataSource != value)
                {
                    viewableObjectDataSource = value;
                    NotifyPropertyChanged("ViewableObjectDataSource");
                }
            }
        }
        public object SelectedObject
        {
            get { return selectedObject; }
            set
            {
                if (selectedObject != value)
                {
                    selectedObject = value;
                    NotifyPropertyChanged("SelectedObject");
                }
            }
        }

        public ResourceSelectControlP(string name)
        {
            InitializeComponent();
            this.Name = name;
            this.TabText = "リソース一覧";
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

        protected void tvAfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    if (tn.Checked != e.Node.Checked)
                    {
                        tn.Checked = e.Node.Checked;
                    }
                }

                if (e.Node.Checked == false)
                {

                }
            }
            else
            {
                Type type = ((TimeLineViewableObjectType)(TypeDescriptor.GetConverter(typeof(TimeLineViewableObjectType)).ConvertFromString(e.Node.Parent.Name))).GetObjectType();

                if (e.Node.Checked)
                {

                    ViewableObjectDataSource.Add(viewableObjectList[type][e.Node.Index]);

                    bool allChecked = true;
                    foreach (TreeNode tn in e.Node.Parent.Nodes)
                    {
                        allChecked = tn.Checked;
                        if (!tn.Checked)
                        {
                            break;
                        }
                    }
                    if (allChecked)
                    {
                        e.Node.Parent.Checked = true;
                    }
                }
                else
                {

                    ViewableObjectDataSource.Remove(viewableObjectList[type][e.Node.Index]);

                    bool allChecked = false;
                    foreach (TreeNode tn in e.Node.Parent.Nodes)
                    {
                        allChecked = tn.Checked;
                        if (tn.Checked)
                        {
                            break;
                        }
                    }
                    if (! allChecked)
                    {
                        e.Node.Parent.Checked = false;
                    }
                }
            }
        }

        protected void tvAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 0)
            {
                Type type = ((TimeLineViewableObjectType)(TypeDescriptor.GetConverter(typeof(TimeLineViewableObjectType)).ConvertFromString(e.Node.Parent.Name))).GetObjectType();
                SelectedObject = viewableObjectList[type][e.Node.Index];
            }
        }
    }
}
