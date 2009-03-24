using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public class ExTreeView : TreeView
	{
		private Dictionary<TreeNode, bool> _lastCheck = new Dictionary<TreeNode, bool>();
		private Dictionary<TreeNode, bool> _childrenCheckChangeGuard = new Dictionary<TreeNode, bool>();
		private Dictionary<TreeNode, bool> _parentCheckChangeGuard = new Dictionary<TreeNode, bool>();

		protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
		{
			base.OnBeforeCheck(e);

			if (!_lastCheck.ContainsKey(e.Node))
				_lastCheck.Add(e.Node, e.Node.Checked);
		}

		protected override void OnAfterCheck(TreeViewEventArgs e)
		{

			base.OnAfterCheck(e);


			if (!(_parentCheckChangeGuard.ContainsKey(e.Node) && _parentCheckChangeGuard[e.Node]))
			{
				if (e.Node.Parent != null)
				{
					bool flag = false;

					foreach (TreeNode n in e.Node.Parent.Nodes)
					{
						if (n.Checked)
						{
							flag = true;
							break;
						}
					}

					if (e.Node.Parent.Checked != flag)
					{
						if (!_childrenCheckChangeGuard.ContainsKey(e.Node.Parent))
							_childrenCheckChangeGuard.Add(e.Node.Parent, true);
						else
							_childrenCheckChangeGuard[e.Node.Parent] = true;

						e.Node.Parent.Checked = flag;

						_childrenCheckChangeGuard[e.Node.Parent] = false;
					}
				}
			}

			if (!(_childrenCheckChangeGuard.ContainsKey(e.Node) && _childrenCheckChangeGuard[e.Node]))
			{

				if (e.Node.Nodes != null || e.Node.Nodes.Count != 0)
				{
					foreach (TreeNode n in e.Node.Nodes)
					{
						if (n.Checked != e.Node.Checked)
						{

							if (!_parentCheckChangeGuard.ContainsKey(n))
								_parentCheckChangeGuard.Add(n, true);
							else
								_parentCheckChangeGuard[n] = true;

							n.Checked = e.Node.Checked;

							_parentCheckChangeGuard[n] = false;
						}
					}
				}
			}
		}
	}
}
