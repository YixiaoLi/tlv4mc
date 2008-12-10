﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceType : IJsonable<ResourceType>, INamed
	{
		private string _name = string.Empty;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				if (DisplayName == null)
					DisplayName = value;
			}
		}
		/// <summary>
		/// 表示名
		/// </summary>
		public string DisplayName { get; set; }
        /// <summary>
        /// 属性リスト
        /// </summary>
        public AttributeTypeList Attributes { get; set; }
		/// <summary>
		/// 振る舞いリスト
		/// </summary>
		public BehaviorList Behaviors { get; set; }

        /// <summary>
        /// <c>Resource</c>のインスタンスを生成する
        /// </summary>
		public ResourceType()
        {
			DisplayName = string.Empty;
        }

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize(this);
		}

		public ResourceType Parse(string data)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<ResourceType>(data);
		}
	}
}
