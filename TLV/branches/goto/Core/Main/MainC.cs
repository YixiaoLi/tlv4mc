using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

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
                                Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>> vol = new Dictionary<TimeLineViewableObjectType,List<TimeLineViewableObject>>();

                                while ((resLine = resourceFile.ReadLine()) != null)
                                {
                                    resLine = resLine.Trim();
                                    ResourceFileLineParser resParser = new ResourceFileLineParser();

                                    if (!(resLine.Equals(String.Empty)) && !((resLine.Substring(0, 1)).Equals(resParser.CommentStr)))
                                    {
                                        TimeLineViewableObjectType ot = resParser.GetObjectType(resLine);
                                        if (ot != TimeLineViewableObjectType.NONE)
                                        {
                                            if (!vol.ContainsKey(ot))
                                            {
                                                vol.Add(ot, new List<TimeLineViewableObject>());
                                            }

                                            TimeLineEvents tes = new TimeLineEvents()
                                            {
                                                new TimeLineEvent(123987, 0),
                                                new TimeLineEvent(236272, 0),
                                                new TimeLineEvent(373473, 0),
                                                new TimeLineEvent(456845, 0),
                                                new TimeLineEvent(595695, 0),
                                                new TimeLineEvent(676860, 0),
                                                new TimeLineEvent(789745, 0),
                                                new TimeLineEvent(823562, 0),
                                            };

                                            TimeLineViewableObject to = (TimeLineViewableObject)Activator.CreateInstance(ot.GetObjectType(), new object[] { resLine, tes });

                                            vol[ot].Add(to);
                                        }
                                    }
                                }

                                A.ViewableObjectList = vol;
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
