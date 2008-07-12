using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Properties;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public enum CursorMode
    {
        Default,
        ZoomIn,
        ZoomOut,
        ZoomSelect,
        Hand
    }

    public static class CursorModeExtension
    {
        public static Cursor Cursor(this CursorMode mode)
        {
            return Cursor(mode, false);
        }

        public static Cursor Cursor(this CursorMode mode, bool mouseDowned)
        {   
            switch (mode)
            {
                case CursorMode.Default:
                    return Cursors.Cross;
                case CursorMode.ZoomIn:
                    return new Cursor(Resources.zoomIn.Handle);
                case CursorMode.ZoomOut:
                    return new Cursor(Resources.zoomOut.Handle);
                case CursorMode.ZoomSelect:
                    return new Cursor(Resources.zoomSelect.Handle);
                case CursorMode.Hand:
                    if (mouseDowned)
                    {
                        return new Cursor(Resources.handHold.Handle);
                    }
                    else
                    {
                        return new Cursor(Resources.hand.Handle);
                    }
                default:
                    return Cursors.Default;
            }
        }

    }

}
