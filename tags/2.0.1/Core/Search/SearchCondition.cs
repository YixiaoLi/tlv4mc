/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
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

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    //検索条件
    class SearchCondition
    {
        private string _resourceName;
        private string _resourceType;
        private string _ruleName;
        private string _ruleDisplayName;
        private string _eventName;
        private string _eventDisplayName;
        private string _eventDetail;
        private string _timing;
        private string _timingValue;
        private Boolean _denyCondition;

        public string resourceName { set { this._resourceName = value; } get { return _resourceName; } }
        public string resourceType { set { this._resourceType = value; } get { return _resourceType; } }
        public string ruleName { set { this._ruleName = value; } get { return _ruleName; } }
        public string ruleDisplayName { set { this._ruleDisplayName = value; } get { return _ruleDisplayName; } }
        public string eventName { set { this._eventName = value; } get { return _eventName; } }
        public string eventDisplayName { set { this._eventDisplayName = value; } get { return _eventDisplayName; } }
        public string eventDetail { set { this._eventDetail = value; } get { return _eventDetail; } }
        public string timing { set { this._timing = value; } get { return _timing; } }
        public string timingValue { set { this._timingValue = value; } get { return _timingValue; } }
        public Boolean denyCondition { set { this._denyCondition = value; } get { return _denyCondition; } }


        public SearchCondition()
        {
            _resourceName = null;
            _resourceType = null;
            _ruleName = null;
            _eventName = null;
            _eventDetail = null;
            _timing = null;
            _timingValue = null;
            _denyCondition = false;
        }
    }
}
