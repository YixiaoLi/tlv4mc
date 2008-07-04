using System;
using System.IO;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceExplorer
{
    public enum ResourceData
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

    public class ResourceFileManager
    {
        private const string RESFILE_COMENT = "#";
        private const string RESFILE_NULL = "";
        private const char RESFILE_COMMA = ',';

        public bool ReadResourceFile(string filePath, out ResourceList resList, out ViewTypeList viewTypeList)
        {
            bool ret = false;

            resList = new ResourceList();
            viewTypeList = new ViewTypeList();


            try
            {
                //ファイルエラーチェック
                if (filePath == string.Empty)
                {
                    return false;
                }

                StreamReader objResorceFile = new StreamReader(filePath);
                string resLine = string.Empty;

                while (resLine != null)
                {
                    resLine = objResorceFile.ReadLine();

                    if (!(resLine.Equals(RESFILE_NULL)) && !((resLine.Substring(0, 1)).Equals(RESFILE_COMENT)))
                    {
                        char[] split = { RESFILE_COMMA };
                        string[] resArray = resLine.Split(split);

                        string type = resArray[(int)ResourceData.TYPE];

                        switch ((ResourceType)Enum.Parse(ResourceType.TSK.GetType(), type))
                        {
                            case ResourceType.TSK:
                                resList.TskInfoList.Add(setTskInfo(resArray));
                                break;
                            case ResourceType.SEM:
                                resList.SemInfoList.Add(setSemInfo(resArray));
                                break;
                            case ResourceType.FLG:
                                resList.FlgInfoList.Add(setFlgInfo(resArray));
                                break;
                            case ResourceType.DTQ:
                                resList.DtqInfoList.Add(setDtqInfo(resArray));
                                break;
                            case ResourceType.PDTQ:
                                resList.PdtqInfoList.Add(setPdtqInfo(resArray));
                                break;
                            case ResourceType.MBX:
                                resList.MbxInfoList.Add(setMbxInfo(resArray));
                                break;
                            case ResourceType.MPF:
                                resList.MpfInfoList.Add(setMpfInfo(resArray));
                                break;
                            case ResourceType.CYC:
                                resList.CycInfoList.Add(setCycInfo(resArray));
                                break;
                            case ResourceType.ALM:
                                resList.AlmInfoList.Add(setAlmInfo(resArray));
                                break;
                            case ResourceType.SPN:
                                resList.SpnInfoList.Add(setSpnInfo(resArray));
                                break;
                            case ResourceType.INH:
                                resList.InhInfoList.Add(setInhInfo(resArray));
                                break;
                        }

                        if (type == ResourceType.TSK.ToString() || type == ResourceType.CYC.ToString() || type == ResourceType.ALM.ToString())
                        {
                            setPrcViewType(resArray[(int)ResourceData.PRCID], ref viewTypeList);
                        }
                        setClsViewType(resArray[(int)ResourceData.CLASS], ref viewTypeList);
                        setResViewType(resArray[(int)ResourceData.TYPE], ref viewTypeList);

                    }

                }

                ret = true;

            }
            catch (Exception e)
            {
                Console.WriteLine("[ResourceFileManager::ReadResourceFile] " + e.Message);
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
            tskInfo.Type = array[(int)ResourceData.TYPE];
            tskInfo.Id = covertInt(array[(int)ResourceData.ID]);
            tskInfo.Name = array[(int)ResourceData.NAME];
            tskInfo.Atr = array[(int)ResourceData.ATR];
            tskInfo.Class = array[(int)ResourceData.CLASS];

            tskInfo.Pri = covertInt(array[(int)ResourceData.PRI]);
            tskInfo.Exinf = array[(int)ResourceData.EXINF];
            tskInfo.PrcID = array[(int)ResourceData.PRCID];
            tskInfo.AffinityMask = array[(int)ResourceData.AFFINTYMASK];
            tskInfo.StkSize = covertInt(array[(int)ResourceData.STKSIZE]);
            tskInfo.Task = array[(int)ResourceData.TASK];
            
            return tskInfo;
        }

        private SemInfo setSemInfo(string[] array)
        {
            SemInfo semInfo = new SemInfo();

            // TYPE ID NAME ATR CLASS SEMCNT MAXSEM
            //  0   1   2    3    4     5      6
            semInfo.Type = array[(int)ResourceData.TYPE];
            semInfo.Id = covertInt(array[(int)ResourceData.ID]);
            semInfo.Name = array[(int)ResourceData.NAME];
            semInfo.Atr = array[(int)ResourceData.ATR];
            semInfo.Class = array[(int)ResourceData.CLASS];
            
            semInfo.SemCount = covertInt(array[(int)ResourceData.SEMCNT]);
            semInfo.MaxSem = covertInt(array[(int)ResourceData.MAXSEM]);

            return semInfo;
        }

        private FlgInfo setFlgInfo(string[] array)
        {
            FlgInfo flgInfo = new FlgInfo();

            // TYPE ID NAME ATR CLASS FLGPTN
            //  0   1   2    3    4     5
            flgInfo.Type = array[(int)ResourceData.TYPE];
            flgInfo.Id = covertInt(array[(int)ResourceData.ID]);
            flgInfo.Name = array[(int)ResourceData.NAME];
            flgInfo.Atr = array[(int)ResourceData.ATR];
            flgInfo.Class = array[(int)ResourceData.CLASS];
           
            flgInfo.FlgPtn = covertInt(array[(int)ResourceData.CLASS]);

            return flgInfo;
        }

        private DtqInfo setDtqInfo(string[] array)
        {
            DtqInfo dtqInfo = new DtqInfo();

            // TYPE ID NAME ATR CLASS DTQCNT
            //  0   1   2    3    4     5
            dtqInfo.Type = array[(int)ResourceData.TYPE];
            dtqInfo.Id = covertInt(array[(int)ResourceData.ID]);
            dtqInfo.Name = array[(int)ResourceData.NAME];
            dtqInfo.Atr = array[(int)ResourceData.ATR];
            dtqInfo.Class = array[(int)ResourceData.CLASS];
            
            dtqInfo.DtqCount = covertInt(array[(int)ResourceData.DTQCNT]);

            return dtqInfo;
        }

        private PdtqInfo setPdtqInfo(string[] array)
        {
            PdtqInfo pdtqInfo = new PdtqInfo();

            // TYPE ID NAME ATR CLASS PDQCNT MAXDPRI
            //  0   1   2    3    4     5       6
            pdtqInfo.Type = array[(int)ResourceData.TYPE];
            pdtqInfo.Id = covertInt(array[(int)ResourceData.ID]);
            pdtqInfo.Name = array[(int)ResourceData.NAME];
            pdtqInfo.Atr = array[(int)ResourceData.ATR];
            pdtqInfo.Class = array[(int)ResourceData.CLASS];

            pdtqInfo.PdqCount = covertInt(array[(int)ResourceData.PDQCNT]);
            pdtqInfo.MaxDPri = covertInt(array[(int)ResourceData.MAXDPRI]);

            return pdtqInfo;
        }

        private MbxInfo setMbxInfo(string[] array)
        {
            MbxInfo mbxInfo = new MbxInfo();

            // TYPE ID NAME ATR CLASS MAXMPRI
            //  0   1   2    3    4      5   
            mbxInfo.Type = array[(int)ResourceData.TYPE];
            mbxInfo.Id = covertInt(array[(int)ResourceData.ID]);
            mbxInfo.Name = array[(int)ResourceData.NAME];
            mbxInfo.Atr = array[(int)ResourceData.ATR];
            mbxInfo.Class = array[(int)ResourceData.CLASS];

            mbxInfo.MaxMPri = covertInt(array[(int)ResourceData.MAXMPRI]);

            return mbxInfo;
        }

        private MpfInfo setMpfInfo(string[] array)
        {
            MpfInfo mpfInfo = new MpfInfo();

            // TYPE ID NAME ATR CLASS BLKCNT BLKSIZE
            //  0   1   2    3    4     5       6
            mpfInfo.Type = array[(int)ResourceData.TYPE];
            mpfInfo.Id = covertInt(array[(int)ResourceData.ID]);
            mpfInfo.Name = array[(int)ResourceData.NAME];
            mpfInfo.Atr = array[(int)ResourceData.ATR];
            mpfInfo.Class = array[(int)ResourceData.CLASS];

            mpfInfo.BlkCount = covertInt(array[(int)ResourceData.BLKCNT]);
            mpfInfo.BlkSize = covertInt(array[(int)ResourceData.BLKSIZE]);
           
            return mpfInfo;
        }

        private CycInfo setCycInfo(string[] array)
        {
            CycInfo cycInfo = new CycInfo();

            // TYPE ID NAME ATR CLASS CYCHDR EXINF PRCID AFFINTYMASK CYCTIM CYCPHS
            //  0   1   2    3    4     5      6     7        8        9      10
            cycInfo.Type = array[(int)ResourceData.TYPE];
            cycInfo.Id = covertInt(array[(int)ResourceData.ID]);
            cycInfo.Name = array[(int)ResourceData.NAME];
            cycInfo.Atr = array[(int)ResourceData.ATR];
            cycInfo.Class = array[(int)ResourceData.CLASS];
           
            cycInfo.CycHdr = array[(int)ResourceData.CYCHDR];
            cycInfo.Exinf = array[(int)ResourceData.EXINF];
            cycInfo.PrcID = array[(int)ResourceData.PRCID];
            cycInfo.AffinityMask = array[(int)ResourceData.AFFINTYMASK];
            cycInfo.CycTim = covertInt(array[(int)ResourceData.CYCTIM]);
            cycInfo.CycPhs = covertInt(array[(int)ResourceData.CYCPHS]);
           
            return cycInfo;
        }

        private AlmInfo setAlmInfo(string[] array)
        {
            AlmInfo almInfo = new AlmInfo();

            // TYPE ID NAME ATR CLASS ALMHDR EXINF PRCID AFFINTYMASK 
            //  0   1   2    3    4     5      6     7        8
            almInfo.Type = array[(int)ResourceData.TYPE];
            almInfo.Id = covertInt(array[(int)ResourceData.ID]);
            almInfo.Name = array[(int)ResourceData.NAME];
            almInfo.Atr = array[(int)ResourceData.ATR];
            almInfo.Class = array[(int)ResourceData.CLASS];
           
            almInfo.AlmHdr = array[(int)ResourceData.ALMHDR];
            almInfo.Exinf = array[(int)ResourceData.EXINF];
            almInfo.PrcID = array[(int)ResourceData.PRCID];
            almInfo.AffinityMask = array[(int)ResourceData.AFFINTYMASK];

            return almInfo;
        }

        private SpnInfo setSpnInfo(string[] array)
        {
            SpnInfo spnInfo = new SpnInfo();

            // TYPE ID NAME ATR CLASS
            //  0   1   2    3    4
            spnInfo.Type = array[(int)ResourceData.TYPE];
            spnInfo.Id = covertInt(array[(int)ResourceData.ID]);
            spnInfo.Name = array[(int)ResourceData.NAME];
            spnInfo.Atr = array[(int)ResourceData.ATR];
            spnInfo.Class = array[(int)ResourceData.CLASS];

            return spnInfo;
        }

        private InhInfo setInhInfo(string[] array)
        {
            InhInfo inhInfo = new InhInfo();

            // TYPE ID NAME ATR CLASS PRI INHHDR
            //  0   1   2    3    4    5    6
            inhInfo.Type = array[(int)ResourceData.TYPE];
            inhInfo.Id = covertInt(array[(int)ResourceData.ID]);
            inhInfo.Name = array[(int)ResourceData.NAME];
            inhInfo.Atr = array[(int)ResourceData.ATR];
            inhInfo.Class = array[(int)ResourceData.CLASS];

            inhInfo.Pri = covertInt(array[(int)ResourceData.PRI]);
            inhInfo.InhHdr = array[(int)ResourceData.INHHDR];
            
            return inhInfo;
        }

        private ExcInfo setExcInfo(string[] array)
        {
            ExcInfo excInfo = new ExcInfo();

            // TYPE ID NAME ATR CLASS EXCHDR
            //  0   1   2    3    4     5
            excInfo.Type = array[(int)ResourceData.TYPE];
            excInfo.Id = covertInt(array[(int)ResourceData.ID]);
            excInfo.Name = array[(int)ResourceData.NAME];
            excInfo.Atr = array[(int)ResourceData.ATR];
            excInfo.Class = array[(int)ResourceData.CLASS];
            
            excInfo.ExcHdr = array[(int)ResourceData.EXCHDR];

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
