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
		private static readonly StatusManager _statusManager = new StatusManager();
		private static readonly CommandManager _commandManager = new CommandManager();
		private static readonly IZip _zip = new SharpZipLibZip();
		private static readonly IJsonSerializer _json = new NewtonsoftJson();
        public static WindowManager WindowManager
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

		public static StatusManager StatusManager
		{
			get { return _statusManager; }
		}

		static ApplicationFactory()
		{
			JsonSerializer.AddConverter(new TraceLogConverter());
			JsonSerializer.AddConverter(new ArcConverter());
			JsonSerializer.AddConverter(new AreaConverter());
			JsonSerializer.AddConverter(new ColorConverter());
			JsonSerializer.AddConverter(new CoordinateConverter());
			JsonSerializer.AddConverter(new ResourceHeaderConverter());
			JsonSerializer.AddConverter(new SizeConverter());
			JsonSerializer.AddConverter(new VisualizeRuleConverter());
			JsonSerializer.AddConverter(new JsonConverter());
			JsonSerializer.AddConverter(new FontFamilyConverter());
			JsonSerializer.AddConverter(new ArgumentTypeConverter());

			JsonSerializer.AddConverter(new ClassHavingNullablePropertyConverter());
			JsonSerializer.AddConverter(new INamedConverter());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter());
		}
    }
}
