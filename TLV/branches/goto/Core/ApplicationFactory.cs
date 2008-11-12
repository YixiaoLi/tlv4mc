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
		private static readonly CommandManager _commandManager = new CommandManager();
		private static readonly IZip _zip = new SharpZipLibZip();
		private static readonly IJsonSerializer _json = new NewtonsoftJson();
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

        public static void Setup()
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

			JsonSerializer.AddConverter(new INamedConverter<AttributeType>());
			JsonSerializer.AddConverter(new INamedConverter<ApplyRule>());
			JsonSerializer.AddConverter(new INamedConverter<Behavior>());
			JsonSerializer.AddConverter(new INamedConverter<ResourceType>());
			JsonSerializer.AddConverter(new INamedConverter<Resource>());

			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<AttributeType, AttributeTypeList>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<ApplyRule, ApplyRuleList>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<Behavior, BehaviorList>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<ResourceType, ResourceTypeList>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<Resource, ResourceList>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<Shapes, ShapesList>());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter<VisualizeRule, VisualizeRuleList>());
        }
    }
}
