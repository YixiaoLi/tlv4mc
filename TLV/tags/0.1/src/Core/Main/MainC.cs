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

                            foreach(TaskInfo ti in tlvol.List)
                            {
                                logList.Add(new Log(0, ti.MetaId, "DORMANT"));
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
                                            if(log.Verb == "RUN")
                                            {
                                                // 一つ前に実行状態だったやつをみつける
                                                Log l1 = logList.List.FindLast(l => l.Verb == "RUN");
                                                if(l1 != null)
                                                {
                                                    // そのタスクの最後に休止状態だったときを見つける
                                                    Log l2 = logList.List.FindLast(l => (l.MetaId == l1.MetaId && l.Verb == "DORMANT"));
                                                    // まだ休止になっていなくてRUNなら実行可能状態にする
                                                    if(l2 != null && l1.Time >= l2.Time)
                                                    {
                                                        Log l3 = logList.List.FindLast(l => (l.MetaId == l1.MetaId));
                                                        if(l3.Verb == "RUN")
                                                        {
                                                            logList.Add(new Log(log.Time, l1.MetaId, "RUNNABLE"));
                                                        }
                                                    }
                                                }
                                            }

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
