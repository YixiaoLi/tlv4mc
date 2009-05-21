/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
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
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ShapeConverter : GeneralConverter<Shape>
	{
		public override object ReadJson(NU.OJL.MPRTOS.TLV.Base.IJsonReader reader)
		{
           Shape shape = new Shape();

           return shape.MetaData = ApplicationFactory.JsonSerializer.Deserialize<Json>(reader);
			return shape;
		}

		protected override void WriteJson(NU.OJL.MPRTOS.TLV.Base.IJsonWriter writer, Shape obj)
		{
            writer.WriteObject(w =>
            {
                if (obj.Alpha.HasValue)
                {
                    w.WriteProperty("Alpha");
                    w.WriteValue(obj.Alpha.Value);
                }
                if (obj.Area != null)
                {
                    w.WriteProperty("Area");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Area);  
                }
                if (obj.Arc != null)
                {
                    w.WriteProperty("Arc");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Arc);  
                }

                if (obj.Fill.HasValue)
                {
                    w.WriteProperty("Fill");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Fill.Value);  
                }

                if (obj.Font != null)
                {
                    w.WriteProperty("Font");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Font);  
                }

                if (obj.Location != null)
                {
                    w.WriteProperty("Location");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Location);  
                }

                if (obj.MetaData != null)
                {
                    w.WriteProperty("MetaData");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.MetaData);  
                }

                if (obj.Offset != null)
                {
                    w.WriteProperty("Offset");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Offset);  
                }

                if (obj.Pen != null)
                {
                    w.WriteProperty("Pen");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Pen);  
                }

                if (obj.Points != null)
                {
                    w.WriteProperty("Points");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Points);  
                }

                if (obj.Size != null)
                {
                    w.WriteProperty("Size");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Size);
                }

                if (obj.Text != null)
                {
                    w.WriteProperty("Text");
                    w.WriteValue(obj.Text);
                }

                if (obj.Type.HasValue)
                {
                    w.WriteProperty("Type");
                    w.WriteValue(obj.Type.Value.ToString());
                }

            });
//			ApplicationFactory.JsonSerializer.Serialize(writer, obj);
		}
	}
}
