/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
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
using System.Linq.Parallel;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class EventShapes : IJsonable<EventShapes>
	{
		public Dictionary<string, List<EventShape>> List { get { return _list; } set { _list = value; } }

		private Dictionary<string, List<EventShape>> _list = new Dictionary<string, List<EventShape>>();

		public IEnumerable<EventShape> GetShapes(Time from, Time to)
		{
			if (ApplicationData.FileContext.Data != null)
			{
				//foreach (EventShape esp in this.AsParallel().OrderBy(l => l.From).Where(l =>
				//    ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(l.Event.GetVisualizeRuleName(), l.Event.Name)
				//    && ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(l.Event.GetVisualizeRuleName(), l.Event.Name)
				//    && l.From < to
				//    && l.To > from))
				//{
				//    yield return esp;
				//}

				foreach (KeyValuePair<string, List<EventShape>> kvp in _list)
				{
					string key1 = kvp.Key.Split(':')[0];
					string key2 = kvp.Key.Split(':')[1];

					if (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(key1, key2)
						&& ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(key1, key2)
					)
					{
						foreach (EventShape es in kvp.Value.AsParallel().Where(l => l.From < to))
						{
							if (es.To > from)
								yield return es;
						}
					}
				}
			}
		}

		public void Add(EventShape eventShape)
		{
			string p = eventShape.Event.GetVisualizeRuleName() + ":" + eventShape.Event.Name;
			if (!_list.ContainsKey(p))
				_list.Add(p, new List<EventShape>());
			_list[p].Add(eventShape);
		}

		public void Optimize()
		{
		    Dictionary<string, List<EventShape>> list = new Dictionary<string, List<EventShape>>();
		    foreach (KeyValuePair<string, List<EventShape>> kvp in _list)
		    {
		        list.Add(kvp.Key, kvp.Value.OrderBy(l => l.From).ToList());
		    }
		    _list = list;
		}

		public void Clear()
		{
			_list.Clear();
		}

        public EventShapes Parse(string traceLogData)
        {
            return ApplicationFactory.JsonSerializer.Deserialize<EventShapes>(traceLogData);
        }

        public string ToJson()
        {
            this.Optimize();
            return ApplicationFactory.JsonSerializer.Serialize(_list);
        }

	}
}
