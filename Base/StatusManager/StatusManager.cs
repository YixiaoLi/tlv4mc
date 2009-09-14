/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
