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
        private static CommandManager _commandManager;

        public static IWindowManager WindowManager
        {
            get { return new WeifenLuoWindowManager(); }
        }

        /// <summary>
        /// <c>CommandManager</c>のインスタンス。これはシングルトンなインスタンスである。
        /// </summary>
        public static CommandManager CommandManager
        {
            get { return _commandManager; }
        }

        static ApplicationFactory()
        {
            _commandManager = new CommandManager();
        }
    }
}
