using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TLV.Models
{
    public class ViewTypeList
    {
        private List<string> prc;
        private List<string> cls;
        private List<string> res;

        public List<string> Prc
        {
            get { return prc; }
        }
        public List<string> Cls
        {
            get { return cls; }
        }
        public List<string> Res
        {
            get { return res; }
        }

        public ViewTypeList()
        {
            prc = new List<string>();
            cls = new List<string>();
            res = new List<string>();
        }


    }
}
