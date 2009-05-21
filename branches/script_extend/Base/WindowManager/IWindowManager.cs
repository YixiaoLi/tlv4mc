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
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// IWindowManagerのハンドラ
    /// IWindowManagerの実装クラスから処理を委譲されるクラスがもつべきインターフェイス
    /// サブウィンドウの管理に必須な機能を定義する
    /// </summary>
    public interface IWindowManager
    {
        event EventHandler<GeneralEventArgs<SubWindow>> SubWindowAdded;
        event EventHandler<GeneralChangedEventArgs<DockState>> SubWindowDockStateChanged;
        event EventHandler<GeneralChangedEventArgs<bool>> SubWindowVisibleChanged;
        Control Parent { get; set; }
        Control MainPanel { get; set; }
        int SubWindowCount { get; }
        IEnumerable<SubWindow> SubWindows { get; }
        SubWindow GetSubWindow(string name);
        bool ContainSubWindow(string name);
        void AddSubWindow(params SubWindow[] subWindows);
        void ShowAllSubWindows();
        void AutoHideAllSubWindows();
        void HideAllSubWindows();
        void ShowSubWindow(string name);
        void HideSubWindow(string name);
        void AutoHideSubWindow(string name);
		void Show();

    }

    public enum DockState
    {
        Unknown,
        Float,
        DockTopAutoHide,
        DockLeftAutoHide,
        DockBottomAutoHide,
        DockRightAutoHide,
        DockTop,
        DockLeft,
        DockBottom,
        DockRight
    }

    public static class DockStateExtension
    {
        public static string ToText(this DockState dockState)
        {
            switch (dockState)
            {
                case DockState.DockBottom:
                    return "下";
                case DockState.DockBottomAutoHide:
                    return "下（オートハイド）";
                case DockState.DockLeft:
                    return "左";
                case DockState.DockLeftAutoHide:
                    return "左（オートハイド）";
                case DockState.DockRight:
                    return "右";
                case DockState.DockRightAutoHide:
                    return "右（オートハイド）";
                case DockState.DockTop:
                    return "上";
                case DockState.DockTopAutoHide:
                    return "上（オートハイド）";
                case DockState.Float:
                    return "フローティング";
                default:
                    return "状態不正";
            }
        }
    }
}
