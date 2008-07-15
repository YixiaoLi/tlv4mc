namespace NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo
{
    public enum TaskVerb
    {
        RUN,
        RUNNABLE,
        DORMANT,
        WAITING,
        SUSPENDED,
        WAITING_SUSPENDED,
        unknown_state
    }

    static class TimeLineViewableObjectTypeExtension
    {

        public static string GetLogFormat(this TaskVerb verbType)
        {
            switch(verbType)
            {
                case TaskVerb.RUN:
                    return "dispatch to";
                case TaskVerb.RUNNABLE:
                    return "becomes RUNNABLE";
                case TaskVerb.WAITING:
                    return "becomes WAITING";
                case TaskVerb.DORMANT:
                    return "becomes DORMANT";
                case TaskVerb.SUSPENDED:
                    return "becomes SUSPENDED";
                case TaskVerb.WAITING_SUSPENDED:
                    return "becomes WAITING_SUSPENDED";
                default:
                    return "";
            }
        }

    }
}
