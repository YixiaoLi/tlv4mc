/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
		private static readonly ApplicationBlackBoard _blackBoard = new ApplicationBlackBoard();
		private static readonly StatusManager _statusManager = new StatusManager();
		private static readonly CommandManager _commandManager = new CommandManager();
		private static readonly IZip _zip = new SharpZipLibZip();
		private static readonly IJsonSerializer _json = new NewtonsoftJson();
		private static readonly RotateColorFactory _colorFactory = new RotateColorFactory();
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

		/// <summary>
		/// <c>ApplicationBlackBoard</c>のインスタンス。これはシングルトンなインスタンスである。
		/// </summary>
		public static ApplicationBlackBoard BlackBoard
		{
            get { return _blackBoard; }
		}

		public static StatusManager StatusManager
		{
			get { return _statusManager; }
		}

		public static RotateColorFactory ColorFactory
		{
			get { return _colorFactory; }
		}

		static ApplicationFactory()
		{
			JsonSerializer.AddConverter(new TraceLogConverter());
			JsonSerializer.AddConverter(new ArcConverter());
			JsonSerializer.AddConverter(new AreaConverter());
			JsonSerializer.AddConverter(new ColorConverter());
			JsonSerializer.AddConverter(new PointConverter());
			JsonSerializer.AddConverter(new ResourceHeaderConverter());
			JsonSerializer.AddConverter(new SizeConverter());
			JsonSerializer.AddConverter(new JsonConverter());
			JsonSerializer.AddConverter(new FontFamilyConverter());
			JsonSerializer.AddConverter(new ArgumentTypeConverter());
			JsonSerializer.AddConverter(new ShapesConverter());
			JsonSerializer.AddConverter(new FiguresConverter());
            JsonSerializer.AddConverter(new ShapeConverter());
            JsonSerializer.AddConverter(new EventShapesConverter());
            JsonSerializer.AddConverter(new EventShapeConverter());
			JsonSerializer.AddConverter(new TimeConverter());
			JsonSerializer.AddConverter(new TimeLineConverter());

			JsonSerializer.AddConverter(new ClassHavingNullablePropertyConverter());
			JsonSerializer.AddConverter(new INamedConverter());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter());
			//JsonSerializer.AddConverter(new ObservableNamedCollectionConverter());
		}
    }
}
