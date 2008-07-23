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
            // formatListの最初の要素は"ObjectType"にすること
            switch (type)
            {
                case TimeLineViewableObjectType.TSK:
                    return new List<string>() { "ObjectType", "Id", "Name", "Atr", "Pri", "Exinf", "Stksize", "Task" };
                default:
                    return new List<string>() { "ObjectType" };
            }
        }

        public static Type GetObjectType(this TimeLineViewableObjectType type)
        {
            switch (type)
            {
                case TimeLineViewableObjectType.TSK:
                    return typeof(TaskInfo);
                default:
                    return typeof(TimeLineViewableObject);
            }
        }
    }

}
