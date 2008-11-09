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
			
			JsonSerializer.AddConverter(new INamedConverter<Attribute>());
			JsonSerializer.AddConverter(new INamedConverter<AttributeType>());
			JsonSerializer.AddConverter(new INamedConverter<Behavior>());
			JsonSerializer.AddConverter(new INamedConverter<ResourceType>());
			JsonSerializer.AddConverter(new INamedConverter<Shapes>());
			JsonSerializer.AddConverter(new INamedConverter<VisualizeRule>());

			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<Attribute>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<AttributeType>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<Behavior>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<ResourceType>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<Shapes>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<VisualizeRule>());

			JsonSerializer.AddConverter(new TraceLogConverter());
			JsonSerializer.AddConverter(new ArcConverter());
			JsonSerializer.AddConverter(new AreaConverter());
			JsonSerializer.AddConverter(new ColorConverter());
			JsonSerializer.AddConverter(new CoordinateConverter());
			JsonSerializer.AddConverter(new ResourceConverter());
			JsonSerializer.AddConverter(new ResourceHeaderConverter());
			JsonSerializer.AddConverter(new ResourceListConverter());
			JsonSerializer.AddConverter(new SizeConverter());
			JsonSerializer.AddConverter(new VisualizeRuleConverter());
        }
    }
}
