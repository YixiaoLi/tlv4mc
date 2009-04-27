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
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class LogDataEnumeable : IEnumerable<LogData>
	{
		protected List<LogData> _list;

		public List<LogData> List { get { return _list; } }

		public LogDataEnumeable(IEnumerable<LogData> list)
		{
			if (list == null)
				_list = new List<LogData>();
			else
				_list = list.OrderBy<LogData, long>(l => l.Id).ToList();
		}

		public IEnumerator<LogData> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		public static LogDataEnumeable GetFirstAttributeSetLogData(Resource res)
		{
			List<LogData> first = new List<LogData>();

			foreach (KeyValuePair<string, Json> attr in res.Attributes)
			{
				if (!attr.Value.IsEmpty)
					first.Add(new AttributeChangeLogData(Time.MinTime(10), res, attr.Key, attr.Value));
			}
			for (int i = 0; i < first.Count; i++)
			{
				first[i].Id = (first.Count - i) * -1;
			}

			return new LogDataEnumeable(first);
		}

		public IEnumerable<LogData> GetEnumerator(Resource target)
		{
			if (target != null)
			{
				return _list.Where(l => l.Object == target);
			}
			return _list.AsEnumerable();
		}

		public IEnumerable<LogData> GetEnumerator<T>(string value, params string[] args)
			where T : LogData
		{
			if (args == null || (args.Length == 1 && args[0] == null))
				return GetEnumerator<T>(value);

			if (typeof(T) == typeof(BehaviorHappenLogData))
				return _list.Where(l => l is T && ((BehaviorHappenLogData)l).Behavior.Name == value && ((BehaviorHappenLogData)l).Behavior.Arguments.checkArgs(args));
			else if (typeof(T) == typeof(AttributeChangeLogData))
				return _list.Where(l => l is T && ((AttributeChangeLogData)l).Attribute.Name == value && ((AttributeChangeLogData)l).Attribute.Value == args[0]);
			return _list.AsEnumerable();
		}

		public IEnumerable<LogData> GetEnumerator<T>(string value)
			where T : LogData
		{
			if (typeof(T) == typeof(BehaviorHappenLogData))
				return _list.Where(l => l is T && ((BehaviorHappenLogData)l).Behavior.Name == value);
			else if (typeof(T) == typeof(AttributeChangeLogData))
				return _list.Where(l => l is T && ((AttributeChangeLogData)l).Attribute.Name == value);
			return _list.AsEnumerable();
		}

		public IEnumerable<LogData> GetEnumerator(Time from, Time to)
		{ return GetEnumerator(from, to, false, false); }

		public IEnumerable<LogData> GetEnumerator(Time from, Time to, bool prevFlag, bool postFlag)
		{
			LogData prev = null;
			LogData post = null;

			if (prevFlag) prev = ceilingTime(from);
			if (postFlag) post = flooringTime(to);

			filter(from, to);

			yield return prev;

			foreach(LogData d in getEnumerator(from, to))
			{
				yield return d;
			}

			yield return post;
		}

		protected IEnumerable<LogData> getEnumerator(Time from, Time to)
		{
			if (!from.IsEmpty && !to.IsEmpty)
			{
				return _list.Where(l => l.Time >= from && l.Time <= to);
			}
			return _list.AsEnumerable();
		}

		public static LogDataEnumeable operator+(LogDataEnumeable left, LogDataEnumeable right)
		{
			return new LogDataEnumeable(left.Union(right).Distinct(new LogDataIdEqualityComparer()).OrderBy(d=>d.Id));
		}

		class LogDataIdEqualityComparer : IEqualityComparer<LogData>
		{
			public bool Equals(LogData x, LogData y)
			{
				return x.Id == y.Id;
			}

			public int GetHashCode(LogData obj)
			{
				return obj.Id.GetHashCode();
			}

		}

		public IEnumerable<LogData[]> GetPrevPostSetEnumerator()
		{
			for (int i = 0; i < _list.Count; i++)
			{
				LogData prev = null;
				LogData now = null;
				LogData post = null;

				if (i > 0)
					prev = _list[i - 1];

				if (i < _list.Count - 1)
					post = _list[i + 1];

				now = _list[i];

				yield return new LogData[] { prev, now, post };
			}
		}

		public LogDataEnumeable Filter(Resource target)
		{
			if (target != null)
			{
				_list = _list.FindAll(l => l.Object == target);
			}
			return this;
		}

		public LogDataEnumeable Filter<T>(string value)
			where T:LogData
		{
			if(typeof(T) == typeof(BehaviorHappenLogData))
				_list = _list.FindAll(l => l is T && ((BehaviorHappenLogData)l).Behavior.Name == value);
			else if (typeof(T) == typeof(AttributeChangeLogData))
				_list = _list.FindAll(l => l is T && ((AttributeChangeLogData)l).Attribute.Name == value);
			return this;
		}

		public LogDataEnumeable Filter(Time _from, Time _to)
		{ return Filter(_from, _to, false, false); }

		public LogDataEnumeable Filter(Time _from, Time _to, bool prevFlag, bool postFlag)
		{
			LogData prev = null;
			LogData post = null;

			if (prevFlag)	prev = ceilingTime(_from);
			if (postFlag)	post = flooringTime(_to);

			filter(_from, _to);

			if (prev != null)	addPrev(prev);
			if (post != null)	addPost(post);

			return this;
		}

		protected LogDataEnumeable filter(Time from, Time to)
		{
			if (!from.IsEmpty && !to.IsEmpty)
			{
				_list = _list.FindAll(l => l.Time >= from && l.Time <= to);
			}
			return this;
		}

		/// <summary>
		/// 指定時間未満で最大時刻のデータを返す
		/// </summary>
		protected LogData ceilingTime(Time time)
		{
			return _list.FindLast(l => l.Time < time);
		}

		/// <summary>
		/// 指定時間超過で最小時刻のデータを返す
		/// </summary>
		protected LogData flooringTime(Time time)
		{
			return _list.Find(l => l.Time > time);
		}

		protected LogDataEnumeable addPrev(params LogData[] logs)
		{
			_list.InsertRange(0, logs);
			return this;
		}

		protected LogDataEnumeable addPost(params LogData[] logs)
		{
			_list.AddRange(logs);
			return this;
		}
	}
}
