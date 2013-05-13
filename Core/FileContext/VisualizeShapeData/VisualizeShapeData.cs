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
 */﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class VisualizeShapeData : IJsonable<VisualizeShapeData>
    {
        public Dictionary<string, EventShapes> RuleShapes;
        public Dictionary<string, EventShapes> RuleResourceShapes;

       public VisualizeShapeData() {
           RuleShapes = new Dictionary<string, EventShapes>();
           RuleResourceShapes = new Dictionary<string, EventShapes>();
       }

       public void Add(VisualizeRule rule, EventShapes shapes)
       {
           RuleShapes.Add(rule.Name, shapes);
       }

       public EventShapes Get(VisualizeRule rule) {
           return RuleShapes[rule.Name];
       }

       public EventShapes Get(VisualizeRule rule, Event e)
       {
           EventShapes es = new EventShapes();

           foreach (List<EventShape> shapes in Get(rule).List.Values)
           {
               foreach (EventShape shape in shapes)
               {
                   if (shape.Event.Name == e.Name)
                   {
                       es.Add(shape);
                   }
               }
           }
           return es;
       }
        
       public EventShapes Get(Resource res) {
           EventShapes es = new EventShapes();

           foreach (KeyValuePair<string, EventShapes> kvp in RuleResourceShapes) {
               string r = kvp.Key.Split(':')[1];
               if (res.Name == r) {
                   foreach (List<EventShape> shapes in kvp.Value.List.Values) {
                       foreach (EventShape shape in shapes) {
                           es.Add(shape);
                       }
                   }
               }
           }

           return es;
       }

       public void Add(VisualizeRule rule, Resource res, EventShapes shapes) {
           RuleResourceShapes.Add(rule.Name + ":" + res.Name, shapes);
       }
       public EventShapes Get(VisualizeRule rule, Resource res) {
           return RuleResourceShapes[rule.Name + ":" + res.Name];
       }

       public EventShapes Get(VisualizeRule rule, Event e, Resource res) {
           EventShapes es = new EventShapes();
           foreach (List<EventShape> shapes in Get(rule, res).List.Values)
           {
               foreach (EventShape shape in shapes) {
                   if (shape.Event.Name == e.Name) {
                       es.Add(shape);
                   }
               }
           }
           return es;
       }
        

        #region IJsonable<VisualizeShapeData> メンバ
        public string ToJson()
        {
            var dict =
               new Dictionary<string,Dictionary<string,EventShapes>>();
            dict.Add("RuleShapes", RuleShapes);
            dict.Add("RuleResourceShapes", RuleResourceShapes);

            return ApplicationFactory.JsonSerializer.Serialize(dict); 
        }

        public VisualizeShapeData Parse(string data)
        {
            Dictionary<string, Dictionary<string, EventShapes>> dict =
                ApplicationFactory.JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, EventShapes>>>(data);
            VisualizeShapeData vizShape= new VisualizeShapeData();
            vizShape.RuleShapes = dict["RuleShapes"];
            vizShape.RuleResourceShapes = dict["RuleResourceShapes"];

            return vizShape; 
        }

        #endregion
    }
}
