/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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
		/// �������̤���Ǻ������Υǡ������֤�
		/// </summary>
		protected LogData ceilingTime(Time time)
		{
			return _list.FindLast(l => l.Time < time);
		}

		/// <summary>
		/// �������Ķ��ǺǾ�����Υǡ������֤�
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
