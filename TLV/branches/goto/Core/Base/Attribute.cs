﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースの属性を表すクラス
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// 属性名を表示する際に使用されるテキスト
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 属性の変数型
        /// </summary>
        public string VariableType { get; set; }
        /// <summary>
        /// 属性の配置型
        /// </summary>
        public AllocationType AllocationType { get; set; }
        /// <summary>
        /// この属性の値を用いてグループ化するかどうか
        /// </summary>
		public bool CanGrouping { get; set; }
		/// <summary>
		/// デフォルト値
		/// </summary>
		public string Default { get; set; }

        /// <summary>
        /// <c>Attribute</c>のインスタンスを生成する
        /// </summary>
        public Attribute()
        {
			DisplayName = string.Empty;
            VariableType = string.Empty;
            AllocationType = AllocationType.Static;
            CanGrouping = false;
			Default = string.Empty;
        }

    }

    public enum AllocationType
    {
        Static,
        Dynamic
    }

}