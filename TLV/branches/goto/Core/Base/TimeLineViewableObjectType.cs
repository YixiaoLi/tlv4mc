using System.Collections.Generic;
using System;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;
using NU.OJL.MPRTOS.TLV.Base;

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

        public static List<string> GetResourceFormat(this TimeLineViewableObjectType type)
        {
            List<string> formatList = null;

            // formatListの最初の要素は"ObjectType"にすること
            switch (type)
            {
                case TimeLineViewableObjectType.TSK:
                    formatList = new List<string>() { "ObjectType", "Id", "Name", "Atr", "Pri", "Exinf", "Stksize", "Task" };
                    break;
                default:
                    formatList = new List<string>() { "ObjectType" };
                    break;
            }

            return formatList;
        }

        public static Type GetObjectType(this TimeLineViewableObjectType type)
        {
            Type t = null;

            switch (type)
            {
                case TimeLineViewableObjectType.TSK:
                    t = typeof(TaskInfo);
                    break;
                default:
                    t = typeof(TimeLineViewableObject);
                    break;
            }

            return t;
        }
    }

}
