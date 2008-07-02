using System;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceExplorer
{
    public class FileManager
    {
        private const string RESFILE_COMENT = "#";
        private const string RESFILE_NULL = "";
        private const string KEY_SPACE = "_";
        private const char RESFILE_COMMA = ',';

        public enum ResData : int
        {
            /* 0 */
            TYPE,
            /* 1 */
            ID,
            /* 2 */
            NAME,
            /* 3 */
            ATR,
            /* 4 */
            CLASS,

            /* 5 */
            PRI,
            SEMCNT = PRI,
            FLGPTN = PRI,
            DTQCNT = PRI,
            PDQCNT = PRI,
            MAXMPRI = PRI,
            BLKCNT = PRI,
            CYCHDR = PRI,
            ALMHDR = PRI,
            EXCHDR = PRI,

            /* 6 */
            EXINF,
            MAXSEM = EXINF,
            MAXDPRI = EXINF,
            BLKSIZE = EXINF,
            INHHDR = EXINF,

            /* 7 */
            PRCID,

            /* 8 */
            AFFINTYMASK,

            /* 9 */
            STKSIZE,
            CYCTIM = STKSIZE,

            /* 10 */
            TASK,
            CYCPHS = TASK  
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
            public const string EXC = "EXC";
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

                        string type = resArray[(int)ResData.TYPE];

                        switch (type)
                        {
                            case ResType.TSK:
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

                        if (type == ResType.TSK || type == ResType.CYC || type == ResType.ALM)
                        {
                            setPrcViewType(resArray[(int)ResData.PRCID], ref viewTypeList);
                        }
                        setClsViewType(resArray[(int)ResData.CLASS], ref viewTypeList);
                        setResViewType(resArray[(int)ResData.TYPE], ref viewTypeList);

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

        private TskInfo setTskInfo(string[] array)
        {
            TskInfo tskInfo = new TskInfo();

            // TYPE ID NAME ATR CLASS PRI EXINF PRCID AFFINTYMASK STKSIZE TASK
            //  0   1   2    3    4    5    6      7       8         9     10  
            tskInfo.Type = array[(int)ResData.TYPE];
            tskInfo.Id = covertInt(array[(int)ResData.ID]);
            tskInfo.Name = array[(int)ResData.NAME];
            tskInfo.Atr = array[(int)ResData.ATR];
            tskInfo.Class = array[(int)ResData.CLASS];

            tskInfo.Pri = covertInt(array[(int)ResData.PRI]);
            tskInfo.Exinf = array[(int)ResData.EXINF];
            tskInfo.PrcID = array[(int)ResData.PRCID];
            tskInfo.AffinityMask = array[(int)ResData.AFFINTYMASK];
            tskInfo.StkSize = covertInt(array[(int)ResData.STKSIZE]);
            tskInfo.Task = array[(int)ResData.TASK];
            
            tskInfo.Key = tskInfo.Type + KEY_SPACE + tskInfo.Id.ToString();

            return tskInfo;
        }

        private SemInfo setSemInfo(string[] array)
        {
            SemInfo semInfo = new SemInfo();

            // TYPE ID NAME ATR CLASS SEMCNT MAXSEM
            //  0   1   2    3    4     5      6
            semInfo.Type = array[(int)ResData.TYPE];
            semInfo.Id = covertInt(array[(int)ResData.ID]);
            semInfo.Name = array[(int)ResData.NAME];
            semInfo.Atr = array[(int)ResData.ATR];
            semInfo.Class = array[(int)ResData.CLASS];
            
            semInfo.SemCount = covertInt(array[(int)ResData.SEMCNT]);
            semInfo.MaxSem = covertInt(array[(int)ResData.MAXSEM]);

            semInfo.Key = semInfo.Type + KEY_SPACE + semInfo.Id.ToString();

            return semInfo;
        }

        private FlgInfo setFlgInfo(string[] array)
        {
            FlgInfo flgInfo = new FlgInfo();

            // TYPE ID NAME ATR CLASS FLGPTN
            //  0   1   2    3    4     5
            flgInfo.Type = array[(int)ResData.TYPE];
            flgInfo.Id = covertInt(array[(int)ResData.ID]);
            flgInfo.Name = array[(int)ResData.NAME];
            flgInfo.Atr = array[(int)ResData.ATR];
            flgInfo.Class = array[(int)ResData.CLASS];
           
            flgInfo.FlgPtn = covertInt(array[(int)ResData.CLASS]);

            flgInfo.Key = flgInfo.Type + KEY_SPACE + flgInfo.Id.ToString();

            return flgInfo;
        }

        private DtqInfo setDtqInfo(string[] array)
        {
            DtqInfo dtqInfo = new DtqInfo();

            // TYPE ID NAME ATR CLASS DTQCNT
            //  0   1   2    3    4     5
            dtqInfo.Type = array[(int)ResData.TYPE];
            dtqInfo.Id = covertInt(array[(int)ResData.ID]);
            dtqInfo.Name = array[(int)ResData.NAME];
            dtqInfo.Atr = array[(int)ResData.ATR];
            dtqInfo.Class = array[(int)ResData.CLASS];
            
            dtqInfo.DtqCount = covertInt(array[(int)ResData.DTQCNT]);

            dtqInfo.Key = dtqInfo.Type + KEY_SPACE + dtqInfo.Id.ToString();

            return dtqInfo;
        }

        private PdtqInfo setPdtqInfo(string[] array)
        {
            PdtqInfo pdtqInfo = new PdtqInfo();

            // TYPE ID NAME ATR CLASS PDQCNT MAXDPRI
            //  0   1   2    3    4     5       6
            pdtqInfo.Type = array[(int)ResData.TYPE];
            pdtqInfo.Id = covertInt(array[(int)ResData.ID]);
            pdtqInfo.Name = array[(int)ResData.NAME];
            pdtqInfo.Atr = array[(int)ResData.ATR];
            pdtqInfo.Class = array[(int)ResData.CLASS];

            pdtqInfo.PdqCount = covertInt(array[(int)ResData.PDQCNT]);
            pdtqInfo.MaxDPri = covertInt(array[(int)ResData.MAXDPRI]);

            pdtqInfo.Key = pdtqInfo.Type + KEY_SPACE + pdtqInfo.Id.ToString();

            return pdtqInfo;
        }

        private MbxInfo setMbxInfo(string[] array)
        {
            MbxInfo mbxInfo = new MbxInfo();

            // TYPE ID NAME ATR CLASS MAXMPRI
            //  0   1   2    3    4      5   
            mbxInfo.Type = array[(int)ResData.TYPE];
            mbxInfo.Id = covertInt(array[(int)ResData.ID]);
            mbxInfo.Name = array[(int)ResData.NAME];
            mbxInfo.Atr = array[(int)ResData.ATR];
            mbxInfo.Class = array[(int)ResData.CLASS];

            mbxInfo.MaxMPri = covertInt(array[(int)ResData.MAXMPRI]);

            mbxInfo.Key = mbxInfo.Type + KEY_SPACE + mbxInfo.Id.ToString();

            return mbxInfo;
        }

        private MpfInfo setMpfInfo(string[] array)
        {
            MpfInfo mpfInfo = new MpfInfo();

            // TYPE ID NAME ATR CLASS BLKCNT BLKSIZE
            //  0   1   2    3    4     5       6
            mpfInfo.Type = array[(int)ResData.TYPE];
            mpfInfo.Id = covertInt(array[(int)ResData.ID]);
            mpfInfo.Name = array[(int)ResData.NAME];
            mpfInfo.Atr = array[(int)ResData.ATR];
            mpfInfo.Class = array[(int)ResData.CLASS];

            mpfInfo.BlkCount = covertInt(array[(int)ResData.BLKCNT]);
            mpfInfo.BlkSize = covertInt(array[(int)ResData.BLKSIZE]);
           
            mpfInfo.Key = mpfInfo.Type + KEY_SPACE + mpfInfo.Id.ToString();

            return mpfInfo;
        }

        private CycInfo setCycInfo(string[] array)
        {
            CycInfo cycInfo = new CycInfo();

            // TYPE ID NAME ATR CLASS CYCHDR EXINF PRCID AFFINTYMASK CYCTIM CYCPHS
            //  0   1   2    3    4     5      6     7        8        9      10
            cycInfo.Type = array[(int)ResData.TYPE];
            cycInfo.Id = covertInt(array[(int)ResData.ID]);
            cycInfo.Name = array[(int)ResData.NAME];
            cycInfo.Atr = array[(int)ResData.ATR];
            cycInfo.Class = array[(int)ResData.CLASS];
           
            cycInfo.CycHdr = array[(int)ResData.CYCHDR];
            cycInfo.Exinf = array[(int)ResData.EXINF];
            cycInfo.PrcID = array[(int)ResData.PRCID];
            cycInfo.AffinityMask = array[(int)ResData.AFFINTYMASK];
            cycInfo.CycTim = covertInt(array[(int)ResData.CYCTIM]);
            cycInfo.CycPhs = covertInt(array[(int)ResData.CYCPHS]);
           
            cycInfo.Key = cycInfo.Type + KEY_SPACE + cycInfo.Id.ToString();

            return cycInfo;
        }

        private AlmInfo setAlmInfo(string[] array)
        {
            AlmInfo almInfo = new AlmInfo();

            // TYPE ID NAME ATR CLASS ALMHDR EXINF PRCID AFFINTYMASK 
            //  0   1   2    3    4     5      6     7        8
            almInfo.Type = array[(int)ResData.TYPE];
            almInfo.Id = covertInt(array[(int)ResData.ID]);
            almInfo.Name = array[(int)ResData.NAME];
            almInfo.Atr = array[(int)ResData.ATR];
            almInfo.Class = array[(int)ResData.CLASS];
           
            almInfo.AlmHdr = array[(int)ResData.ALMHDR];
            almInfo.Exinf = array[(int)ResData.EXINF];
            almInfo.PrcID = array[(int)ResData.PRCID];
            almInfo.AffinityMask = array[(int)ResData.AFFINTYMASK];

            almInfo.Key = almInfo.Type + KEY_SPACE + almInfo.Id.ToString();

            return almInfo;
        }

        private SpnInfo setSpnInfo(string[] array)
        {
            SpnInfo spnInfo = new SpnInfo();

            // TYPE ID NAME ATR CLASS
            //  0   1   2    3    4
            spnInfo.Type = array[(int)ResData.TYPE];
            spnInfo.Id = covertInt(array[(int)ResData.ID]);
            spnInfo.Name = array[(int)ResData.NAME];
            spnInfo.Atr = array[(int)ResData.ATR];
            spnInfo.Class = array[(int)ResData.CLASS];

            spnInfo.Key = spnInfo.Type + KEY_SPACE + spnInfo.Id.ToString();

            return spnInfo;
        }

        private InhInfo setInhInfo(string[] array)
        {
            InhInfo inhInfo = new InhInfo();

            // TYPE ID NAME ATR CLASS PRI INHHDR
            //  0   1   2    3    4    5    6
            inhInfo.Type = array[(int)ResData.TYPE];
            inhInfo.Id = covertInt(array[(int)ResData.ID]);
            inhInfo.Name = array[(int)ResData.NAME];
            inhInfo.Atr = array[(int)ResData.ATR];
            inhInfo.Class = array[(int)ResData.CLASS];

            inhInfo.Pri = covertInt(array[(int)ResData.PRI]);
            inhInfo.InhHdr = array[(int)ResData.INHHDR];
            
            inhInfo.Key = inhInfo.Type + KEY_SPACE + inhInfo.Id.ToString();

            return inhInfo;
        }

        private ExcInfo setExcInfo(string[] array)
        {
            ExcInfo excInfo = new ExcInfo();

            // TYPE ID NAME ATR CLASS EXCHDR
            //  0   1   2    3    4     5
            excInfo.Type = array[(int)ResData.TYPE];
            excInfo.Id = covertInt(array[(int)ResData.ID]);
            excInfo.Name = array[(int)ResData.NAME];
            excInfo.Atr = array[(int)ResData.ATR];
            excInfo.Class = array[(int)ResData.CLASS];
            
            excInfo.ExcHdr = array[(int)ResData.EXCHDR];

            excInfo.Key = excInfo.Type + KEY_SPACE + excInfo.Id.ToString();

            return excInfo;
        }

        private int covertInt(string data)
        {
            int ret;

            int.TryParse(data, out ret);

            return ret;
        }

    }
}
