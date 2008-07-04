using System;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class LogFileManager
    {

        public bool ReadLogFile(string filePath, out LogList logList)
        {
            bool ret = false;
            logList = new LogList();
           
            try
            {
                //ファイルエラーチェック
                if (filePath == string.Empty)
                {
                    return false;
                }

                StreamReader logFile = new StreamReader(filePath);
                string logLine = string.Empty;

                while (logLine != null)
                {
                    logLine = logFile.ReadLine();

                    if (!(string.IsNullOrEmpty(logLine)))
                    {
                        char[] split = { ' ' };
                        string[] logArray = logLine.Split(split);


                        switch (logArray[1])
                        {
                            case "task":
                                setTaskLog(logArray, ref logList);
                                break;
                            case "dispatch":
                                setDispatchLog(logArray, ref logList);
                                break;
                        }

                    }
                }

                ret = true;


            }
            catch(Exception e)
            {
                Console.WriteLine("[LogFileManager::ReadLogFile] " + e.Message);
                ret = false;
            }

            return ret;
        }

        private void setTaskLog(string[] array, ref LogList logList)
        {
            ulong time = 0;
            int prcid = 0;
            int taskid = 0;
            
            Subject subject;
            Verb verb;

            string[] timeArray = getLogTime(array[0]);

            ulong.TryParse(timeArray[0], out time);

            int.TryParse(array[2], out taskid);

            if (array[3] == "becomes")
            {
                subject = new Subject(ResourceType.TSK, taskid);

                verb = (Verb)Enum.Parse(Verb.RUN.GetType(), array[4].TrimEnd('.'));


                if (timeArray.Length == 2)
                {
                    int.TryParse(timeArray[1], out prcid);
                }
                
                logList.Add(new Log(time, prcid, subject, verb));
            }
            else if (array[3] == "phase")
            {

            }
        }

        private void setDispatchLog(string[] array, ref LogList logList)
        {
            ulong time;
            int prcid =0;
            int taskid;

            string[] timeArray = getLogTime(array[0]);

            ulong.TryParse(timeArray[0], out time);

            if (array[2] == "to" && array[3] == "task")
            {
                int.TryParse(array[4].TrimEnd('.'), out taskid);

                if (timeArray.Length == 2)
                {
                    int.TryParse(timeArray[1], out prcid);
                }

                int runId = logList.GetRunTaskId(prcid);

                if (runId == 0)
                {
                    logList.Add(new Log(time, prcid, new Subject(ResourceType.TSK, taskid), Verb.RUN));
                }
                else
                {
                    logList.Add(new Log(time, prcid, new Subject(ResourceType.TSK, runId), Verb.RUNNABLE));
                    logList.Add(new Log(time, prcid, new Subject(ResourceType.TSK, taskid), Verb.RUN));
                }

            }

        }

        private string[] getLogTime(string str)
        {
            string split = "[:]";
            string[] array = str.Split(split.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            return array;
        }


    }
}
