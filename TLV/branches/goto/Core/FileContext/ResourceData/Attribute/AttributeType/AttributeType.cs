using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースの属性を表すクラス
    /// </summary>
    public class AttributeType : INamed
	{
		private JsonValueType _variableType;

		public string Name { get; set; }
        /// <summary>
        /// 属性名を表示する際に使用されるテキスト
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 属性の変数型
        /// </summary>
		public JsonValueType VariableType
		{
			get { return _variableType; }
			set
			{
				_variableType = value;
				if (Default == null)
				{
					switch(value)
					{
						case JsonValueType.String:
							Default = "";
							break;
						case JsonValueType.Decimal:
							Default = "0";
							break;
						case JsonValueType.Boolean:
							Default = "Flase";
							break;
						default:
							Default = "null";
							break;
					}
				}
			}
		}
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
		/// デフォルト可視化ルール
		/// </summary>
		public string VisualizeRule { get; set; }

        /// <summary>
        /// <c>Attribute</c>のインスタンスを生成する
        /// </summary>
        public AttributeType()
        {
            AllocationType = AllocationType.Static;
            CanGrouping = false;
        }

	}

}
