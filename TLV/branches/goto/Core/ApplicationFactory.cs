using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Third;
using NU.OJL.MPRTOS.TLV.Base;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public static class ApplicationFactory
    {
        private static readonly CommandManager _commandManager;
		private static readonly IZip _zip;
		private static readonly IJsonSerializer _json;
        public static IWindowManager WindowManager
        {
            get { return new WeifenLuoWindowManager(); }
        }

        /// <summary>
        /// <c>IZip</c>のインスタンス。これはシングルトンなインスタンスである。
        /// </summary>
        public static IZip Zip
        {
            get { return _zip; }
        }

		/// <summary>
		/// <c>IJson</c>のインスタンス。これはシングルトンなインスタンスである。
		/// </summary>
		public static IJsonSerializer JsonSerializer
		{
			get { return _json; }
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
			_zip = new SharpZipLibZip();
			_json = new NewtonsoftJson();

			JsonSerializer.AddConverter<ResourceHeader>(new ResourceHeaderConverter());
			JsonSerializer.AddConverter<Coordinate>(new CoordinateConverter());
			JsonSerializer.AddConverter<Color>(new ColorConverter());
			JsonSerializer.AddConverter<VisualizeRule>(new VisualizeRuleConverter());
        }
    }
}
