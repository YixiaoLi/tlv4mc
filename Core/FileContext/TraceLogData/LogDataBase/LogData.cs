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

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class LogData : IComparer<LogData>, IComparable<LogData>, IEqualityComparer<LogData>, IEquatable<LogData>
	{
		public virtual TraceLogType Type { get; private set; }
		public Time Time { get; private set; }
		public Resource Object { get; private set; }
		public long Id { get; set; }

		public LogData(Time time, Resource obj)
		{
			Time = time;
			Object = obj;
			Type = TraceLogType.None;
		}

		public int Compare(LogData x, LogData y)
		{
			return x.Id == y.Id ? 0 : x.Id < y.Id ? 1 : -1;
		}

		public int CompareTo(LogData other)
		{
			return Compare(this, other);
		}

		public bool Equals(LogData x, LogData y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(LogData obj)
		{
			return obj.Id.GetHashCode();
		}

		public bool Equals(LogData other)
		{
			return Equals(this, other);
		}

		public override string ToString()
		{
			return "[" + Time.ToString() + "]" + Object.Name;
		}

		public bool CheckAttributeOrBehavior(TraceLog log)
		{
			bool result = true;

			if (log.Attribute != null && log.Behavior == null && this is AttributeChangeLogData)
				result &= log.Attribute == ((AttributeChangeLogData)this).Attribute.Name && (log.Value != null ? log.Value == ((AttributeChangeLogData)this).Attribute.Value.ToString() : true);
			else if (log.Attribute == null && log.Behavior != null && this is BehaviorHappenLogData)
				result &= log.Behavior == ((BehaviorHappenLogData)this).Behavior.Name && (log.Arguments != null ? ((BehaviorHappenLogData)this).Behavior.Arguments.checkArgs(log.Arguments.Split(',')) : true);
			else
				result &= false;

			return result;
		}
	}
}
