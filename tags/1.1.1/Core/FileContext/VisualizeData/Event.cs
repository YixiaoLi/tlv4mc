/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
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
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Event : INamed
	{
		private string _ruleName = string.Empty;
		private string _name = string.Empty;
		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				if (DisplayName == null)
					DisplayName = value;
			}
		}
		public string DisplayName { get; set; }
		public TraceLog From { get; set; }
		public TraceLog To { get; set; }
		public TraceLog When { get; set; }
		public Figures Figures { get; set; }

		public void SetVisualizeRuleName(string name)
		{
			_ruleName = name;
		}

		public string GetVisualizeRuleName()
		{
			return _ruleName;
		}

		public string getImageKey()
		{
			string imgId = "warning";

			if (When != null)
			{
				if (When.Attribute != null && When.Behavior == null)
					imgId = "attribute";
				else if (When.Attribute == null && When.Behavior != null)
					imgId = "behavior";
			}
			else if (From != null && To != null)
			{
				string from = string.Empty;
				string to = string.Empty;

				if (From.Attribute != null && From.Behavior == null)
					from = "atr";
				else if (From.Attribute == null && From.Behavior != null)
					from = "bhr";


				if (To.Attribute != null && To.Behavior == null)
					to = "atr";
				else if (To.Attribute == null && To.Behavior != null)
					to = "bhr";

				imgId = from + "2" + to;
			}

			return imgId;

		}
	}
}
