using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Third;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public static class ApplicationFactory
    {
        private static TransactionManager _transactionManager;

        public static IWindowManager WindowManager
        {
            get { return new WeifenLuoWindowManager(); }
        }

        public static TransactionManager TransactionManager
        {
            get { return _transactionManager; }
        }

        static ApplicationFactory()
        {
            _transactionManager = new TransactionManager();
        }
    }
}
