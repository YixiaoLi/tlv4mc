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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public enum ScrollBarType
	{
		Vertical,
		Horizontal
	}

    public class NativeScrollBar : NativeWindow
    {
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int SBS_HORZ = 0x0000;
		private const int SBS_VERT = 0x0001;

		public NativeScrollBar(Control parent)
			:this(parent, ScrollBarType.Vertical)
		{

		}

        public NativeScrollBar(Control parent, ScrollBarType type)
        {

            CreateParams cp = new CreateParams();

            cp.Caption = String.Empty;
            cp.ClassName = "SCROLLBAR";

            cp.X = 0;
            cp.Y = 0;
            cp.Height = 0;
            cp.Width = 0;

            cp.Parent = parent.Handle;
			switch (type)
			{
				case ScrollBarType.Horizontal:
					cp.Style = WS_VISIBLE | SBS_HORZ | WS_CHILD;
					break;
				case ScrollBarType.Vertical:
					cp.Style = WS_VISIBLE | WS_CHILD | SBS_VERT;
					break;
			}

            IntPtr modHandle = NativeScrollBar.GetModuleHandle(null);
			int lastWin32Error = 0;
			IntPtr handleCreated = IntPtr.Zero;

            try
            {
                handleCreated = NativeScrollBar.CreateWindowEx(
                    cp.ExStyle,
                    cp.ClassName,
                    cp.Caption,
                    cp.Style,
                    cp.X, cp.Y,
                    cp.Width, cp.Height,
                    new HandleRef(cp, cp.Parent),
                    new HandleRef(null, IntPtr.Zero),
                    new HandleRef(null, modHandle), cp.Param);

                lastWin32Error = Marshal.GetLastWin32Error();
            }
            catch (NullReferenceException e)
            {
                throw new OutOfMemoryException("Could not create Native Window : " + e.Message);
            }
            if (handleCreated == IntPtr.Zero)
            {
                throw new Win32Exception(lastWin32Error, "System error creating Native Window");
            }

			this.AssignHandle(handleCreated);
        }

        [DllImport("USER32.DLL", EntryPoint = "CreateWindowEx", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateWindowEx(
            int dwExStyle,
            string lpszClassName,
            string lpszWindowName,
            int style,
            int x, int y,
            int width, int height,
            HandleRef hWndParent,
            HandleRef hMenu,
            HandleRef hInst,
            [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string modName);

    }

}
