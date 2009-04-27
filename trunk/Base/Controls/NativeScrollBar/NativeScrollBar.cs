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
