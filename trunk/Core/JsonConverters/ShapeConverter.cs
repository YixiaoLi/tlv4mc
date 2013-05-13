/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
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
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new ArgumentException();
            }
           Shape shape = new Shape();

           reader.Read();
           while (reader.TokenType == JsonTokenType.PropertyName)
           {
               string name = (string)reader.Value;
               if (name == "Alpha")
               {
                   shape.Alpha = ApplicationFactory.JsonSerializer.Deserialize<int>(reader);
               }
               else if (name == "Area")
               {
                   shape.Area = ApplicationFactory.JsonSerializer.Deserialize<Area>(reader);  
               }
               else if (name == "Arc")
               {
                   shape.Arc = ApplicationFactory.JsonSerializer.Deserialize<Arc>(reader); 
               }
               else if (name == "Fill")
               {
                   shape.Fill = ApplicationFactory.JsonSerializer.Deserialize<System.Drawing.Color>(reader);  
               }
               else if (name == "Font")
               {
                   shape.Font = ApplicationFactory.JsonSerializer.Deserialize<Font>(reader);
               }
               else if (name == "Location")
               {
                   shape.Location = ApplicationFactory.JsonSerializer.Deserialize<Point>(reader);  
               }
               else if (name == "MetaData")
               {
                   shape.MetaData = ApplicationFactory.JsonSerializer.Deserialize<Json>(reader);
               }
               else if (name == "Offset")
               {
                   shape.Offset = ApplicationFactory.JsonSerializer.Deserialize<Size>(reader);
               }
               else if (name == "Pen")
               {
                   shape.Pen = ApplicationFactory.JsonSerializer.Deserialize<Pen>(reader);
               }
               else if (name == "Points")
               {
                   shape.Points = ApplicationFactory.JsonSerializer.Deserialize<PointList>(reader);  
               }
               else if (name == "Size")
               {
                   shape.Size = ApplicationFactory.JsonSerializer.Deserialize<Size>(reader);  
               }
               else if (name == "Text")
               {
                   reader.Read();  
                   shape.Text = (string)reader.Value;  
               }
               else if (name == "Type")
               {
                   shape.Type = ApplicationFactory.JsonSerializer.Deserialize<ShapeType>(reader);
               }
               else {
                   throw new ArgumentException();
               }
               reader.Read(); 
           }

           if (reader.TokenType != JsonTokenType.EndObject)
           {
               throw new ArgumentException();
           }
           shape.SetDefaultValue();
           shape.ChackValidate();
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
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Type.Value);
                }

            });
//			ApplicationFactory.JsonSerializer.Serialize(writer, obj);
		}
	}
}
