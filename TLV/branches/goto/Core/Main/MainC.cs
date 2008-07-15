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
                            using (StreamReader resourceFile = new StreamReader(A.ResourceFilePath))
                            {
                                string resLine = "";
                                Dictionary<Type, List<TaskInfo>> vol = new Dictionary<Type, List<TaskInfo>>();

                                while ((resLine = resourceFile.ReadLine()) != null)
                                {
                                    resLine = resLine.Trim();
                                    ResourceFileLineParser resParser = new ResourceFileLineParser();

                                    if (!(resLine.Equals(String.Empty)) && !((resLine.Substring(0, 1)).Equals(resParser.CommentStr)))
                                    {
                                        TimeLineViewableObjectType type = resParser.GetObjectType(resLine);
                                        if (type.GetObjectType().IsSubclassOf(typeof(TimeLineViewableObject)) || type.GetObjectType() == typeof(TimeLineViewableObject))
                                        {
                                            if (!vol.ContainsKey(type.GetObjectType()))
                                            {
                                                vol.Add(type.GetObjectType(), new List<TaskInfo>());
                                            }

                                            TimeLineEvents tles = new TimeLineEvents();

                                            using (StreamReader TraceLogFile = new StreamReader(A.TraceLogFilePath))
                                            {
                                                string logLine = "";
                                                TraceLogFileLineParser logParser = new TraceLogFileLineParser();
                                                while ((logLine = TraceLogFile.ReadLine()) != null)
                                                {
                                                    logLine = logLine.Trim();

                                                    if (!(logLine.Equals(String.Empty)) && logParser.ContainLog(logLine, resLine))
                                                    {
                                                        foreach (TimeLineEvent tle in logParser.GetTimeLineEvent(logLine, resLine))
                                                        {
                                                            tles.Add(tle);
                                                        }
                                                    }
                                                }
                                            }

                                            TaskInfo to = (TaskInfo)Activator.CreateInstance(type.GetObjectType(), new object[] { resLine, tles });

                                            vol[type.GetObjectType()].Add(to);

                                        }
                                    }
                                }

                                SetPropertyToA(typeof(Dictionary<Type, List<TaskInfo>>), "ViewableObjectList", vol, SearchAFlags.Self);

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
