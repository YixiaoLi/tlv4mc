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
        private Dictionary<Type, List<TaskInfo>> viewableObjectList = new Dictionary<Type, List<TaskInfo>>();

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
        public Dictionary<Type, List<TaskInfo>> ViewableObjectList
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

        public MainA(string name)
            :base(name)
        {

        }
    }
}
