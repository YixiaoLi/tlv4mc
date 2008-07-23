using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public class MainA : Abstraction
    {
        private string resourceFilePath = String.Empty;
        private string traceLogFilePath = String.Empty;
        private TimeLineViewableObjectList<TaskInfo> viewableObjectList = new TimeLineViewableObjectList<TaskInfo>();
        private LogList logList = new LogList();

        public string ResourceFilePath
        {
            get { return resourceFilePath; }
            set
            {
                if (value != resourceFilePath)
                {
                    resourceFilePath = value;
                    NotifyPropertyChanged("ResourceFilePath");
                }
            }
        }
        public string TraceLogFilePath
        {
            get { return traceLogFilePath; }
            set
            {
                if (value != traceLogFilePath)
                {
                    traceLogFilePath = value;
                    NotifyPropertyChanged("TraceLogFilePath");
                }
            }
        }
        public TimeLineViewableObjectList<TaskInfo> ViewableObjectList
        {
            get { return viewableObjectList; }
            set
            {
                if (value != viewableObjectList)
                {
                    viewableObjectList = value;
                    NotifyPropertyChanged("ViewableObjectList");
                }
            }
        }
        public LogList LogList
        {
            get { return logList; }
            set
            {
                if (value != logList)
                {
                    logList = value;
                    NotifyPropertyChanged("LogList");
                }
            }
        }

        public MainA(string name)
            :base(name)
        {

        }
    }
}
