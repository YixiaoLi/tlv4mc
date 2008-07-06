using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class NativeScrollBar : NativeWindow
    {
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int SBS_HORZ = 0x0000;
        private const int SBS_VERT = 0x0001;

        public NativeScrollBar(Control parent)
        {

            CreateParams cp = new CreateParams();

            cp.Caption = String.Empty;
            cp.ClassName = "SCROLLBAR";

            cp.X = -1000;
            cp.Y = -1000;
            cp.Height = 100;
            cp.Width = 100;

            cp.Parent = parent.Handle;
            cp.Style = WS_VISIBLE | SBS_VERT | WS_CHILD;

            IntPtr modHandle = NativeScrollBar.GetModuleHandle(null);
            IntPtr handleCreated = IntPtr.Zero;
            int lastWin32Error = 0;

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
                throw new OutOfMemoryException("Could not create Native Window");
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
