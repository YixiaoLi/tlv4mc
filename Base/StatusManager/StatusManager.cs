
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class StatusManager
	{
		public StatusStrip StatusStrip { get; set; }
		private Dictionary<string, ToolStripStatusLabel> _infos = new Dictionary<string, ToolStripStatusLabel>();
		private Dictionary<string, ToolStripStatusLabel> _processings = new Dictionary<string, ToolStripStatusLabel>();
		private Dictionary<string, List<ToolStripStatusLabel>> _hints = new Dictionary<string, List<ToolStripStatusLabel>>();

		public Border3DStyle InfoBorder { get; set; }
		public Border3DStyle HistBorder { get; set; }

		public StatusManager()
		{
			StatusStrip = null;

			InfoBorder = Border3DStyle.SunkenOuter;
			HistBorder = Border3DStyle.Raised;
		}

		public void ShowInfo(string name, string text)
		{
			if (StatusStrip == null)
				throw new NullReferenceException();

			if (_infos.ContainsKey(name))
			{
				_infos[name].Visible = true;
				_infos[name].Text = text;
			}
			else
			{
				ToolStripStatusLabel label = new ToolStripStatusLabel(text);
				label.BorderSides = ToolStripStatusLabelBorderSides.All;
				label.BorderStyle = InfoBorder;
				label.Visible = true;
				_infos.Add(name, label);
				updateStatusStrip();
			}
		}
		public void HideInfo(string name)
		{
			if (_infos.ContainsKey(name))
				_infos[name].Visible = false;
		}
		public bool IsInfoShown(string name)
		{
			if (!_infos.ContainsKey(name))
				return false;

			return _infos[name].Visible;
		}

		public void ShowProcessing(string name, string text)
		{
			if (StatusStrip == null)
				throw new NullReferenceException();

			if (_processings.ContainsKey(name))
			{
				_processings[name].Visible = true;
				_processings[name].Text = text;
			}
			else
			{
				ToolStripStatusLabel label = new ToolStripStatusLabel(text);
				label.BorderSides = ToolStripStatusLabelBorderSides.None;
				label.Image = StatusManagerResource.status_anim;
				label.ImageScaling = ToolStripItemImageScaling.None;
				label.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
				label.TextImageRelation = TextImageRelation.ImageBeforeText;
				label.Visible = true;
				_processings.Add(name, label);
				updateStatusStrip();
			}
		}
		public void HideProcessing(string name)
		{
			if (_processings.ContainsKey(name))
				_processings[name].Visible = false;
		}
		public bool IsProcessingShown(string name)
		{
			if (!_processings.ContainsKey(name))
				return false;

			return _processings[name].Visible;
		}

		public void ShowHint(string name, string discription, params string[] text)
		{
			if (StatusStrip == null)
				throw new NullReferenceException();

			if (_hints.ContainsKey(name))
			{
				_hints[name].ForEach((t) => { t.Visible = true; });
				_hints[name].Last().Text = discription;
			}
			else
			{
				List<ToolStripStatusLabel> modifyKeyLabels = new List<ToolStripStatusLabel>();

				for (int i = 0; i < text.Length; i++)
				{
					string str = text[i];
					string sp = "+";

					if (str[0] == ',')
					{
						str = str.Remove(0, 1);
						sp = "or";
					}

					ToolStripStatusLabel keyLabel = new ToolStripStatusLabel(str);
					keyLabel.BorderSides = ToolStripStatusLabelBorderSides.All;
					keyLabel.BorderStyle = HistBorder;
					keyLabel.Visible = true;

					if (i != 0)
						modifyKeyLabels.Add(new ToolStripStatusLabel(sp) { BorderSides = ToolStripStatusLabelBorderSides.None });

					modifyKeyLabels.Add(keyLabel);
				}

				modifyKeyLabels.Add(new ToolStripStatusLabel(":") { BorderSides = ToolStripStatusLabelBorderSides.None });

				ToolStripStatusLabel label = new ToolStripStatusLabel(discription) { BorderSides = ToolStripStatusLabelBorderSides.None };
				label.Margin = new Padding(label.Margin.Left, label.Margin.Top, label.Margin.Right + 10, label.Margin.Bottom);
				modifyKeyLabels.Add(label);

				_hints.Add(name, modifyKeyLabels);
				updateStatusStrip();
			}
		}
		public void HideHint(string name)
		{
			if (_hints.ContainsKey(name))
			{
				_hints[name].ForEach((t) => { t.Visible = false; });
			}
		}
		public bool IsHintShown(string name)
		{
			if (!_hints.ContainsKey(name))
				return false;

			return _hints[name][0].Visible;
		}

		private void updateStatusStrip()
		{
			StatusStrip.Items.Clear();

			foreach (List<ToolStripStatusLabel> labels in _hints.Values)
			{
				StatusStrip.Items.AddRange(labels.ToArray());
			}

			StatusStrip.Items.Add(new ToolStripStatusLabel() { Spring = true });

			StatusStrip.Items.AddRange(_infos.Values.ToArray());

			StatusStrip.Items.AddRange(_processings.Values.ToArray());
		}

		public void Clear()
		{
			StatusStrip.Items.Clear();
			_infos = new Dictionary<string, ToolStripStatusLabel>();
			_processings = new Dictionary<string, ToolStripStatusLabel>();
			_hints = new Dictionary<string, List<ToolStripStatusLabel>>();
		}
	}
}
