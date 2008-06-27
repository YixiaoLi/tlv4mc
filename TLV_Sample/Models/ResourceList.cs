using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TLV.Models
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
        }

        public class ObjectBase
        {
            protected int id;
            protected string key;
            protected string type;
            protected string name;
            protected string cls;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            public string Key
            {
                get { return key; }
                set { key = value; }
            }
            public string Type
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
            private int pri;
            private int stkSize;
            private string prc;

            public int Pri
            {
                get { return pri; }
                set { pri = value; }
            }
            public int StkSize
            {
                get { return stkSize; }
                set { stkSize = value; }
            }
            public string Prc
            {
                get { return prc; }
                set { prc = value; }
            }
        }

        public class SemInfo : ObjectBase
        {

        }

        public class FlgInfo : ObjectBase
        {

        }

        public class DtqInfo : ObjectBase
        {

        }

        public class PdtqInfo : ObjectBase
        {

        }

        public class MbxInfo : ObjectBase
        {

        }

        public class MpfInfo : ObjectBase
        {

        }

        public class CycInfo : ObjectBase
        {

        }

        public class AlmInfo : ObjectBase
        {

        }

        public class SpnInfo : ObjectBase
        {

        }

        public class InhInfo : ObjectBase
        {
            private int pri;
            private string prc;

            public int Pri
            {
                get { return pri; }
                set { pri = value; }
            }
            public string Prc
            {
                get { return prc; }
                set { prc = value; }
            }
        }

    }

}
