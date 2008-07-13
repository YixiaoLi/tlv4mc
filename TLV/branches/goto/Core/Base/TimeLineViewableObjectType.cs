using System.Collections.Generic;
using System;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public enum TimeLineViewableObjectType
    {
        NONE,
        TSK,
        SEM,
        FLG,
        DTQ,
        PDTQ,
        MBX,
        MPF,
        CYC,
        ALM,
        SPN,
        INH,
        EXC
    }

    static class TimeLineViewableObjectTypeExtension
    {
        public static List<string> GetFormat(this TimeLineViewableObjectType type)
        {
            List<string> formatList = null;

            // formatListの最初の要素は"ObjectType"にすること
            switch (type)
            {
                case TimeLineViewableObjectType.TSK:
                    formatList = new List<string>() { "ObjectType", "ID", "Name", "Atr", "Pri", "Exinf", "Stksize", "Task" };
                    break;
                default :
                    break;
            }

            return formatList;
        }

        public static Type GetObjectType(this TimeLineViewableObjectType type)
        {
            Type t = null;

            // formatListの最初の要素は"ObjectType"にすること
            switch (type)
            {
                case TimeLineViewableObjectType.TSK:
                    t = typeof(TaskInfo);
                    break;
                default:
                    break;
            }

            return t;
        }
    }

}
