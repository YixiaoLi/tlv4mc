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
 */﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class SimpleSearch : TraceLogSearcher
    {
        private List<VisualizeLog> _visLogs;
        private decimal _normTime;
        private SearchCondition _condition;

        public SimpleSearch()
        {
            _normTime = 0;
        }

        public override void setSearchData(List<VisualizeLog> visLogs, SearchCondition condition, List<SearchCondition> refiningCondition)
        {
            _visLogs = visLogs;
            _condition = condition;
        }

        // 簡易検索では不要なコード（インタフェースを用いた弊害）
        public override void setSearchData(List<VisualizeLog> visLogs, SearchCondition condition, List<SearchCondition> refiningCondition, Boolean isAnd)
        {
            _visLogs = visLogs;
            _condition = condition;
        }

        public override VisualizeLog searchForward(decimal normTime)
        {
            _normTime = normTime;
            VisualizeLog hitLog = null;
            foreach (VisualizeLog visLog in _visLogs)
            {
                if (checkSearchCondition(visLog, _condition, _normTime))
                {
                    if (visLog.fromTime > _normTime)
                    {
                        hitLog = visLog;
                        break;
                    }
                }
            }
            return hitLog;
        }


        public override VisualizeLog searchBackward(decimal normTime)
        {
            _normTime = normTime;
            VisualizeLog hitLog = null ;
            for(int i = _visLogs.Count -1  ; i>0; i--)
            {
                VisualizeLog visLog = _visLogs[i];
                if (checkSearchCondition(visLog, _condition, _normTime))
                {
                    if (visLog.fromTime < _normTime)
                    {
                        hitLog  = visLog;
                        break;
                    }
                }
            }
            return hitLog;
        }


        public override List<VisualizeLog> searchWhole()
        {
            List<VisualizeLog> hitLogs = new List<VisualizeLog>();
            foreach (VisualizeLog visLog in _visLogs)
            {
                if (checkSearchCondition(visLog, _condition, _normTime))
                {
                    hitLogs.Add(visLog);
                }
            }

            return hitLogs;
        }

        private Boolean checkSearchCondition(VisualizeLog visLog, SearchCondition condition, decimal normTime)
        {
            if (!visLog.resourceName.Equals(condition.resourceName))
                return false;

            if (condition.ruleName != null)
            {
                if (!visLog.ruleName.Equals(condition.ruleName))
                    return false;
            }

            if (condition.eventName != null)
            {
                if (!visLog.evntName.Equals(condition.eventName))
                    return false;
            }

            if (condition.eventDetail != null)  // イベント詳細が指定されているかを確認
            {
                if (!visLog.evntDetail.Equals(condition.eventDetail))
                    return false;
            }

            return true;
        }

    }
}
