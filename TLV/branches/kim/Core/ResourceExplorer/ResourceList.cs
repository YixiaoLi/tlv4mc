using System;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceExplorer
{
    public class ResourceList
    {

        private List<TskInfo> tskInfoList;
        private List<SemInfo> semInfoList;
        private List<FlgInfo> flgInfoList;
        private List<DtqInfo> dtqInfoList;
        private List<PdtqInfo> pdtqInfoList;
        private List<MbxInfo> mbxInfoList;
        private List<MpfInfo> mpfInfoList;
        private List<CycInfo> cycInfoList;
        private List<AlmInfo> almInfoList;
        private List<SpnInfo> spnInfoList;
        private List<InhInfo> inhInfoList;
        private List<ExcInfo> excInfoList;

        public List<TskInfo> TskInfoList
        {
            get { return tskInfoList; }

        }
        public List<SemInfo> SemInfoList
        {
            get { return semInfoList; }
        }
        public List<FlgInfo> FlgInfoList
        {
            get { return flgInfoList; }
        }
        public List<DtqInfo> DtqInfoList
        {
            get { return dtqInfoList; }
        }
        public List<PdtqInfo> PdtqInfoList
        {
            get { return pdtqInfoList; }
        }
        public List<MbxInfo> MbxInfoList
        {
            get { return mbxInfoList; }
        }
        public List<MpfInfo> MpfInfoList
        {
            get { return mpfInfoList; }
        }
        public List<CycInfo> CycInfoList
        {
            get { return cycInfoList; }
        }
        public List<AlmInfo> AlmInfoList
        {
            get { return almInfoList; }
        }
        public List<SpnInfo> SpnInfoList
        {
            get { return spnInfoList; }
        }
        public List<InhInfo> InhInfoList
        {
            get { return inhInfoList; }
        }
        public List<ExcInfo> ExcInfoList
        {
            get { return excInfoList; }
        }

        public ResourceList()
        {
            tskInfoList = new List<TskInfo>();
            semInfoList = new List<SemInfo>();
            flgInfoList = new List<FlgInfo>();
            dtqInfoList = new List<DtqInfo>();
            pdtqInfoList = new List<PdtqInfo>();
            mbxInfoList = new List<MbxInfo>();
            mpfInfoList = new List<MpfInfo>();
            cycInfoList = new List<CycInfo>();
            almInfoList = new List<AlmInfo>();
            spnInfoList = new List<SpnInfo>();
            inhInfoList = new List<InhInfo>();
            excInfoList = new List<ExcInfo>();
        }
    }

    public class ObjectBase
    {
        protected int id;
        protected string atr;
        protected ResourceType type;
        protected string name;
        protected string cls;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Atr
        {
            get { return atr; }
            set { atr = value; }
        }
        public ResourceType Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Class
        {
            get { return cls; }
            set { cls = value; }
        }

    }

    public class TskInfo : ObjectBase
    {
        private int tskpri;
        private int stksz;
        private string affinitymask;
        private string task;
        private string prcid;
        private string exinf;

        public int Pri
        {
            get { return tskpri; }
            set { tskpri = value; }
        }
        public int StkSize
        {
            get { return stksz; }
            set { stksz = value; }
        }
        public string AffinityMask
        {
            get { return affinitymask; }
            set { affinitymask = value; }
        }
        public string Task
        {
            get { return task; }
            set { task = value; }
        }
        public string PrcID
        {
            get { return prcid; }
            set { prcid = value; }
        }
        public string Exinf
        {
            get { return exinf; }
            set { exinf = value; }
        }

    }

    public class SemInfo : ObjectBase
    {
        private int semcnt;
        private int maxsem;

        public int SemCount
        {
            get { return semcnt; }
            set { semcnt = value; }
        }
        public int MaxSem
        {
            get { return maxsem; }
            set { maxsem = value; }
        }

    }

    public class FlgInfo : ObjectBase
    {
        private int flgptn;

        public int FlgPtn
        {
            get { return flgptn; }
            set { flgptn = value; }
        }
    }

    public class DtqInfo : ObjectBase
    {
        private int dtqcnt;

        public int DtqCount
        {
            get { return dtqcnt; }
            set { dtqcnt = value; }
        }
    }

    public class PdtqInfo : ObjectBase
    {
        private int pdqcnt;
        private int maxdpri;

        public int PdqCount
        {
            get { return pdqcnt; }
            set { pdqcnt = value; }
        }
        public int MaxDPri
        {
            get { return maxdpri; }
            set { maxdpri = value; }
        }
    }

    public class MbxInfo : ObjectBase
    {
        private int maxmpri;

        public int MaxMPri
        {
            get { return maxmpri; }
            set { maxmpri = value; }
        }
    }

    public class MpfInfo : ObjectBase
    {
        private int blkcnt;
        private int blksz;

        public int BlkCount
        {
            get { return blkcnt; }
            set { blkcnt = value; }
        }
        public int BlkSize
        {
            get { return blksz; }
            set { blksz = value; }
        }
    }

    public class CycInfo : ObjectBase
    {
        private int cyctim;
        private int cycphs;
        private string affinitymask;
        private string prcid;
        private string cychdr;
        private string exinf;

        public int CycTim
        {
            get { return cyctim; }
            set { cyctim = value; }
        }
        public int CycPhs
        {
            get { return cycphs; }
            set { cycphs = value; }
        }
        public string AffinityMask
        {
            get { return affinitymask; }
            set { affinitymask = value; }
        }
        public string PrcID
        {
            get { return prcid; }
            set { prcid = value; }
        }
        public string CycHdr
        {
            get { return cychdr; }
            set { cychdr = value; }
        }
        public string Exinf
        {
            get { return exinf; }
            set { exinf = value; }
        }

    }

    public class AlmInfo : ObjectBase
    {
        private string affinitymask;
        private string prcid;
        private string almhdr;
        private string exinf;

        public string AffinityMask
        {
            get { return affinitymask; }
            set { affinitymask = value; }
        }
        public string PrcID
        {
            get { return prcid; }
            set { prcid = value; }
        }
        public string AlmHdr
        {
            get { return almhdr; }
            set { almhdr = value; }
        }
        public string Exinf
        {
            get { return exinf; }
            set { exinf = value; }
        }

    }

    public class SpnInfo : ObjectBase
    {
    }

    public class InhInfo : ObjectBase
    {
        private int pri;
        private string inhhdr;

        public int Pri
        {
            get { return pri; }
            set { pri = value; }
        }
        public string InhHdr
        {
            get { return inhhdr; }
            set { inhhdr = value; }
        }
    }

    public class ExcInfo : ObjectBase
    {
        private string exchdr;

        public string ExcHdr
        {
            get { return exchdr; }
            set { exchdr = value; }
        }
    }

}
