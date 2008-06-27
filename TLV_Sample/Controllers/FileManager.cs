using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TLV.Models;

namespace TLV.Controllers
{
    public class FileManager
    {
        private const string RESFILE_COMENT = "#";
        private const string RESFILE_NULL = "";
        private const string KEY_SPACE = "_";
        private const char RESFILE_COMMA = ',';

        public struct ResData
        {
            public const int TYPE = 0;
            public const int ID = 1;
            public const int NAME = 2;
            public const int CLS = 3;
            public const int PRC = 4;
            public const int PRI = 5;
            public const int STKSIZE = 6;
        }

        public struct ResType
        {
            public const string TSK = "TSK";
            public const string SEM = "SEM";
            public const string FLG = "FLG";
            public const string DTQ = "DTQ";
            public const string PDTQ = "PDTQ";
            public const string MBX = "MBX";
            public const string MPF = "MPF";
            public const string CYC = "CYC";
            public const string ALM = "ALM";
            public const string SPN = "SPN";
            public const string INH = "INH";
        }

        private string FilePath;

        public void SetFilePath(string path)
        {
            FilePath = path;
        }

        public bool ReadResFile(out ResourceList resList, out ViewTypeList viewTypeList)
        {
            bool ret = false;

            resList = new ResourceList();
            viewTypeList = new ViewTypeList();


            try
            {
                //ファイルエラーチェック
                if (FilePath == string.Empty)
                {
                    return false;
                }

                StreamReader objResorceFile = new StreamReader(FilePath);
                string resLine = string.Empty;

                while (resLine != null)
                {
                    resLine = objResorceFile.ReadLine();

                    if (!(resLine.Equals(RESFILE_NULL)) && !((resLine.Substring(0, 1)).Equals(RESFILE_COMENT)))
                    {
                        char[] split = { RESFILE_COMMA };
                        string[] resArray = resLine.Split(split);

                        string type = resArray[ResData.TYPE];

                        switch (type)
                        {
                            case ResType.TSK:
                                setPrcViewType(resArray[ResData.PRC], ref viewTypeList);
                                resList.TskInfoList.Add(setTskInfo(resArray));
                                break;
                            case ResType.SEM:
                                resList.SemInfoList.Add(setSemInfo(resArray));
                                break;
                            case ResType.FLG:
                                resList.FlgInfoList.Add(setFlgInfo(resArray));
                                break;
                            case ResType.DTQ:
                                resList.DtqInfoList.Add(setDtqInfo(resArray));
                                break;
                            case ResType.PDTQ:
                                resList.PdtqInfoList.Add(setPdtqInfo(resArray));
                                break;
                            case ResType.MBX:
                                resList.MbxInfoList.Add(setMbxInfo(resArray));
                                break;
                            case ResType.MPF:
                                resList.MpfInfoList.Add(setMpfInfo(resArray));
                                break;
                            case ResType.CYC:
                                resList.CycInfoList.Add(setCycInfo(resArray));
                                break;
                            case ResType.ALM:
                                resList.AlmInfoList.Add(setAlmInfo(resArray));
                                break;
                            case ResType.SPN:
                                resList.SpnInfoList.Add(setSpnInfo(resArray));
                                break;
                            case ResType.INH:
                                resList.InhInfoList.Add(setInhInfo(resArray));
                                break;
                        }

                        setClsViewType(resArray[ResData.CLS], ref viewTypeList);
                        setResViewType(resArray[ResData.TYPE], ref viewTypeList);

                    }

                }

                ret = true;

            }
            catch (Exception e)
            {
                Console.WriteLine("[FileManager::ResFileRoad] " + e.Message);
                ret = false;
            }
            return ret;
        }

        private void setPrcViewType(string type, ref ViewTypeList viewList)
        {
            bool isSamePrc = false;

            for (int i = 0; i < viewList.Prc.Count; i++)
            {
                if ((viewList.Prc[i].Equals(type)))
                {
                    isSamePrc = true;
                    break;
                }

            }

            if (isSamePrc == false)
            {
                if (!(type.Equals(string.Empty)))
                {
                    viewList.Prc.Add(type);
                }
            }
            
        }

        private void setClsViewType(string type, ref ViewTypeList viewList)
        {
            bool isSameCls = false;

            for (int i = 0; i < viewList.Cls.Count; i++)
            {
                if ((viewList.Cls[i].Equals(type)))
                {
                    isSameCls = true;
                    break;
                }
            }

            if (isSameCls == false)
            {
                if (!(type.Equals(string.Empty)))
                {
                    viewList.Cls.Add(type);
                }
            }

        }

        private void setResViewType(string type, ref ViewTypeList viewList)
        {
            bool isSameRes = false;

            for (int i = 0; i < viewList.Res.Count; i++)
            {

                if ((viewList.Res[i].Equals(type)))
                {
                    isSameRes = true;
                    break;
                }

            }

            if (isSameRes == false)
            {
                if (!(type.Equals(string.Empty)))
                {
                    viewList.Res.Add(type);
                }
            }

        }

        private ResourceList.TskInfo setTskInfo(string[] array)
        {
            ResourceList.TskInfo tskInfo = new ResourceList.TskInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                tskInfo.Id = int.Parse(array[ResData.ID]);
            }
            if (!array[ResData.PRI].Equals(RESFILE_NULL))
            {
                tskInfo.Pri = int.Parse(array[ResData.PRI]);
            }
            if (!array[ResData.STKSIZE].Equals(RESFILE_NULL))
            {
                tskInfo.StkSize = int.Parse(array[ResData.STKSIZE]);
            }

            tskInfo.Type = array[ResData.TYPE];
            tskInfo.Name = array[ResData.NAME];
            tskInfo.Class = array[ResData.CLS];
            tskInfo.Prc = array[ResData.PRC];
            tskInfo.Key = tskInfo.Type + KEY_SPACE + tskInfo.Id.ToString();

            return tskInfo;
        }

        private ResourceList.SemInfo setSemInfo(string[] array)
        {
            ResourceList.SemInfo semInfo = new ResourceList.SemInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                semInfo.Id = int.Parse(array[ResData.ID]);
            }

            semInfo.Type = array[ResData.TYPE];
            semInfo.Name = array[ResData.NAME];
            semInfo.Class = array[ResData.CLS];
            semInfo.Key = semInfo.Type + KEY_SPACE + semInfo.Id.ToString();

            return semInfo;
        }

        private ResourceList.FlgInfo setFlgInfo(string[] array)
        {
            ResourceList.FlgInfo flgInfo = new ResourceList.FlgInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                flgInfo.Id = int.Parse(array[ResData.ID]);
            }

            flgInfo.Type = array[ResData.TYPE];
            flgInfo.Name = array[ResData.NAME];
            flgInfo.Class = array[ResData.CLS];
            flgInfo.Key = flgInfo.Type + KEY_SPACE + flgInfo.Id.ToString();

            return flgInfo;
        }

        private ResourceList.DtqInfo setDtqInfo(string[] array)
        {
            ResourceList.DtqInfo dtqInfo = new ResourceList.DtqInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                dtqInfo.Id = int.Parse(array[ResData.ID]);
            }

            dtqInfo.Type = array[ResData.TYPE];
            dtqInfo.Name = array[ResData.NAME];
            dtqInfo.Class = array[ResData.CLS];
            dtqInfo.Key = dtqInfo.Type + KEY_SPACE + dtqInfo.Id.ToString();

            return dtqInfo;
        }

        private ResourceList.PdtqInfo setPdtqInfo(string[] array)
        {
            ResourceList.PdtqInfo pdtqInfo = new ResourceList.PdtqInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                pdtqInfo.Id = int.Parse(array[ResData.ID]);
            }

            pdtqInfo.Type = array[ResData.TYPE];
            pdtqInfo.Name = array[ResData.NAME];
            pdtqInfo.Class = array[ResData.CLS];
            pdtqInfo.Key = pdtqInfo.Type + KEY_SPACE + pdtqInfo.Id.ToString();

            return pdtqInfo;
        }

        private ResourceList.MbxInfo setMbxInfo(string[] array)
        {
            ResourceList.MbxInfo mbxInfo = new ResourceList.MbxInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                mbxInfo.Id = int.Parse(array[ResData.ID]);
            }

            mbxInfo.Type = array[ResData.TYPE];
            mbxInfo.Name = array[ResData.NAME];
            mbxInfo.Class = array[ResData.CLS];
            mbxInfo.Key = mbxInfo.Type + KEY_SPACE + mbxInfo.Id.ToString();

            return mbxInfo;
        }

        private ResourceList.MpfInfo setMpfInfo(string[] array)
        {
            ResourceList.MpfInfo mpfInfo = new ResourceList.MpfInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                mpfInfo.Id = int.Parse(array[ResData.ID]);
            }

            mpfInfo.Type = array[ResData.TYPE];
            mpfInfo.Name = array[ResData.NAME];
            mpfInfo.Class = array[ResData.CLS];
            mpfInfo.Key = mpfInfo.Type + KEY_SPACE + mpfInfo.Id.ToString();

            return mpfInfo;
        }

        private ResourceList.CycInfo setCycInfo(string[] array)
        {
            ResourceList.CycInfo cycInfo = new ResourceList.CycInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                cycInfo.Id = int.Parse(array[ResData.ID]);
            }

            cycInfo.Type = array[ResData.TYPE];
            cycInfo.Name = array[ResData.NAME];
            cycInfo.Class = array[ResData.CLS];
            cycInfo.Key = cycInfo.Type + KEY_SPACE + cycInfo.Id.ToString();

            return cycInfo;
        }

        private ResourceList.AlmInfo setAlmInfo(string[] array)
        {
            ResourceList.AlmInfo almInfo = new ResourceList.AlmInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                almInfo.Id = int.Parse(array[ResData.ID]);
            }

            almInfo.Type = array[ResData.TYPE];
            almInfo.Name = array[ResData.NAME];
            almInfo.Class = array[ResData.CLS];
            almInfo.Key = almInfo.Type + KEY_SPACE + almInfo.Id.ToString();

            return almInfo;
        }

        private ResourceList.SpnInfo setSpnInfo(string[] array)
        {
            ResourceList.SpnInfo spnInfo = new ResourceList.SpnInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                spnInfo.Id = int.Parse(array[ResData.ID]);
            }

            spnInfo.Type = array[ResData.TYPE];
            spnInfo.Name = array[ResData.NAME];
            spnInfo.Class = array[ResData.CLS];
            spnInfo.Key = spnInfo.Type + KEY_SPACE + spnInfo.Id.ToString();

            return spnInfo;
        }

        private ResourceList.InhInfo setInhInfo(string[] array)
        {
            ResourceList.InhInfo inhInfo = new ResourceList.InhInfo();

            if (!array[ResData.ID].Equals(RESFILE_NULL))
            {
                inhInfo.Id = int.Parse(array[ResData.ID]);
            }
            if (!array[ResData.PRI].Equals(RESFILE_NULL))
            {
                inhInfo.Pri = int.Parse(array[ResData.PRI]);
            }

            inhInfo.Type = array[ResData.TYPE];
            inhInfo.Name = array[ResData.NAME];
            inhInfo.Class = array[ResData.CLS];
            inhInfo.Prc = array[ResData.PRC];
            inhInfo.Key = inhInfo.Type + KEY_SPACE + inhInfo.Id.ToString();

            return inhInfo;
        }


    }
}
