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

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ApplicationBlackBoard
	{
        public EventHandler<GeneralChangedEventArgs<Time>> CursorTimeChanged;
        public EventHandler<GeneralChangedEventArgs<Pair<Time, Time>>> SelectedTimeRangeChanged;
        public EventHandler<GeneralChangedEventArgs<List<Time>>> SearchTimeChanged;
        public EventHandler<GeneralChangedEventArgs<Boolean>> DetailSearchFlagChanged;
        public EventHandler<GeneralChangedEventArgs<int>> DeletedSearchConditionNumChanged;
        
		private Time _cursorTime;
        public Time CursorTime { get { return _cursorTime; } set { ApplicationMethod.SetValue<Time>(ref _cursorTime, value, CursorTimeChanged, this); } }

        //検索時刻を記録する。これが変化するとTraceLogViewerにおいて表示ログのフォーカス位置が更新される
        private List<Time> _searchTime; 
        public List<Time> SearchTime { get { return _searchTime; } set { ApplicationMethod.SetValue<List<Time>>(ref _searchTime, value, SearchTimeChanged, this);} }

		private Pair<Time, Time> _selectedTimeChange;
		public Pair<Time, Time> SelectedTimeChange { get { return _selectedTimeChange; } set { ApplicationMethod.SetValue<Pair<Time, Time>>(ref _selectedTimeChange, value, SelectedTimeRangeChanged, this); } }

        //ログファイルとリソースファイルをドラッグしていることを示すフラグ
        //これが1のときは、TraceLogDisplayPanel, MacroViewer,TraceLogViewer において、
        //カーソルが動かないようにする
        public int dragFlag = 0;

        private Boolean _detailSearchFlag = false; //これがfalseのときは詳細検索フォームが出現していることを表す
                                                   //（詳細検索フォームが出現中は、TraceLogDisplayPanelを操作不能にするためのフラグ）
        public Boolean DetailSearchFlag
        {
            get { return _detailSearchFlag; }
            set { ApplicationMethod.SetValue<Boolean>(ref _detailSearchFlag, value, DetailSearchFlagChanged, this); }
        }

        //消去された検索条件セットの番号。検索条件セットが消去されるたびに値が更新される。
        //（検索条件セットとは基本条件と絞込み条件のセットのこと）
        private int _deletedSearchConditionNum = -1;

        public int DeletedSearchConditionNum
        {
            get { return _deletedSearchConditionNum; }
            set { ApplicationMethod.SetValue<int>(ref _deletedSearchConditionNum, value, DeletedSearchConditionNumChanged, this); }
        }

		public ApplicationBlackBoard()
		{
			_cursorTime = Time.Empty;
            _searchTime = new List<Time>();
			_selectedTimeChange = null;
            DetailSearchFlag = _detailSearchFlag;
		}
	}
}
