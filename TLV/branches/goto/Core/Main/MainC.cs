using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public class MainC : Control<MainP, MainA>
    {
        public MainC(string name, MainP presentation, MainA absrtaction)
            : base(name, presentation, absrtaction)
        {

        }

        public override void InitParentFirst()
        {
            BindPToA("ResourceFilePath", typeof(string), "ResourceFilePath", SearchAFlags.Self);
            BindPToA("TraceLogFilePath", typeof(string), "TraceLogFilePath", SearchAFlags.Self);
            A.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(APropertyChanged);
        }

        private void APropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "ResourceFilePath":
                case "TraceLogFilePath":
                    if (A.ResourceFilePath != String.Empty && A.TraceLogFilePath != String.Empty)
                    {
                        try
                        {

                            TimeLineViewableObjectList<TaskInfo> tlvol = new TimeLineViewableObjectList<TaskInfo>();
                            LogList logList = new LogList();

                            using (StreamReader resourceFile = new StreamReader(A.ResourceFilePath))
                            {
                                string resLine = "";

                                while ((resLine = resourceFile.ReadLine()) != null)
                                {
                                    resLine = resLine.Trim();
                                    ResourceFileLineParser resParser = new ResourceFileLineParser();

                                    if (!(resLine.Equals(String.Empty)) && !((resLine.Substring(0, 1)).Equals(resParser.CommentStr)))
                                    {
                                        TimeLineViewableObjectType type = resParser.GetObjectType(resLine);
                                        if (type.GetObjectType().IsSubclassOf(typeof(TimeLineViewableObject)) || type.GetObjectType() == typeof(TimeLineViewableObject))
                                        {

                                            tlvol.Add((TaskInfo)Activator.CreateInstance(type.GetObjectType(), new object[] { resLine }));

                                        }
                                    }
                                }
                            }

                            using (StreamReader TraceLogFile = new StreamReader(A.TraceLogFilePath))
                            {
                                string logLine = "";

                                while ((logLine = TraceLogFile.ReadLine()) != null)
                                {
                                    logLine = logLine.Trim();
                                    TraceLogFileLineParser<TaskInfo> logParser = new TraceLogFileLineParser<TaskInfo>();

                                    if (!(logLine.Equals(String.Empty)))
                                    {
                                        Log log = logParser.Parse(logLine, tlvol);
                                        if(log != null)
                                        {
                                            logList.Add(log);
                                        }
                                    }
                                }
                            }

                            A.ViewableObjectList = tlvol;
                            A.LogList = logList;

                            foreach (TaskInfo ti in A.ViewableObjectList.List)
                            {
                                ti.TimeLineEvents = A.LogList[ti.MetaId];
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        this.P.MainContentPanel.Show();
                    }
                    else
                    {
                        this.P.MainContentPanel.Hide();
                    }
                    break;
            }
        }

    }

}
