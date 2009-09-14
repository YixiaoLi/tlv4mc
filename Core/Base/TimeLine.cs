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
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLine : ICloneable
	{
		protected Time _from = Time.Empty;
		protected Time _to = Time.Empty;
		protected Time _min = Time.Empty;
		protected Time _max = Time.Empty;

		public event EventHandler<GeneralChangedEventArgs<TimeLine>> ViewingAreaChanged;
		
		public virtual Time FromTime { get { return _from; } private set { if (_from != value)_from = value; } }
		public virtual Time ToTime { get { return _to; } private set { if (_to != value)_to = value; } }
		public virtual Time MinTime { get { return _min; } set { if (_min != value)_min = value; } }
		public virtual Time MaxTime { get { return _max; } set { if (_max != value)_max = value; } }
		public virtual Time ViewableSpan { get { return _max - _min; } }
		public virtual Time ViewingSpan { get { return _to - _from; } }

		public TimeLine(Time from, Time to)
			: this(from, to, from, to) { }

		public TimeLine(Time from, Time to, Time min, Time max)
		{
			FromTime = from;
			ToTime = to;
			MinTime = min;
			MaxTime = max;
		}

		public virtual void SetTime(Time from, Time to)
		{
			TimeLine old = (TimeLine)this.Clone();

			Time span = ViewingSpan;

			if (!from.IsEmpty && from < MinTime)
				from = MinTime;
			if (!to.IsEmpty && to < MinTime)
				to = MinTime;

			if (!to.IsEmpty && to > MaxTime)
				to = MaxTime;
			if (!from.IsEmpty && from > MaxTime)
				from = MaxTime;

			if (!from.IsEmpty && !to.IsEmpty && from > to)
			{
				Time s = to;
				to = from;
				from = to;
			}
			if (!from.IsEmpty && !to.IsEmpty && from == to)
			{
				return;
			}

			if (from.IsEmpty && !to.IsEmpty && to <= FromTime)
				throw new ArgumentException("toはFromTimeより大きくなければなりません。");

			if (to.IsEmpty && !from.IsEmpty && from >= ToTime)
				throw new ArgumentException("fromはToTimeより小さくなければなりません。");

			if (from > ToTime)
			{
				if (!to.IsEmpty)
					ToTime = to;
				if (!from.IsEmpty)
					FromTime = from;
			}
			else
			{
				if (!from.IsEmpty)
					FromTime = from;
				if (!to.IsEmpty)
					ToTime = to;
			}
            if (ViewingAreaChanged != null)
            {
                ViewingAreaChanged(this, new GeneralChangedEventArgs<TimeLine>(old, this));
            }
		}

		public virtual void MoveBySettingToTime(Time time)
		{
			if (time.IsEmpty)
				return;

			if (time == _to)
				return;

			if (time > _max)
			{
				time = _max;
			}
			if (time - ViewingSpan < _min)
			{
				time = _min + ViewingSpan;
			}

			SetTime(time - ViewingSpan, time);
		}

		public virtual void MoveBySettingFromTime(Time time)
		{
			if (time.IsEmpty)
				return;

			if (time == _from)
				return;

			if (time < _min)
			{
				time = _min;
			}
			if (time + ViewingSpan > _max)
			{
				time = _max - ViewingSpan;
			}

			SetTime(time, time + ViewingSpan);

		}

		public override string ToString()
		{
			return "[" + _from.ToString() + "〜" + _to.ToString() + "] in (" + _min.ToString() + "〜" + _max.ToString() + ")";
		}

		public object Clone()
		{
			return new TimeLine(FromTime, ToTime, MinTime, MaxTime);
		}

	}
}
