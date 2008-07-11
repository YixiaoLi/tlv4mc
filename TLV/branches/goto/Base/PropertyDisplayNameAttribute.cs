using System;
using System.Collections;
using System.ComponentModel;


namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// プロパティ表示名を外部から設定するための属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyDisplayNameAttribute : Attribute
    {
        public int Order { get; protected set; }
        public string PropertyDisplayName { get; protected set; }

        public PropertyDisplayNameAttribute(string name, int order)
        {
            PropertyDisplayName = name;
            Order = order;
        }
    }

    /// <summary>
    /// プロパティ表示名でPropertyDisplayPropertyDescriptorクラスを使用するために
    /// TypeConverter属性に指定するためのTypeConverter派生クラス。
    /// </summary>
    public class PropertyDisplayConverter : TypeConverter
    {
        public PropertyDisplayConverter()
        {
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object instance, Attribute[] filters)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);

            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(instance, filters, true);
            foreach (PropertyDescriptor pd in pdc)
            {
                collection.Add(new PropertyDisplayPropertyDescriptor(pd));
            }

            return collection.Sort(new PropertyDescriptorCollectionComparer());
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    public class PropertyDescriptorCollectionComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((PropertyDisplayNameAttribute)((PropertyDescriptor)x).Attributes[typeof(PropertyDisplayNameAttribute)]).Order - ((PropertyDisplayNameAttribute)((PropertyDescriptor)y).Attributes[typeof(PropertyDisplayNameAttribute)]).Order;
        }
    }

    /// <summary>
    /// プロパティの説明（＝情報）を提供するクラス。DisplayNameをカスタマイズする。
    /// </summary>
    public class PropertyDisplayPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor oneProperty;

        public PropertyDisplayPropertyDescriptor(PropertyDescriptor desc)
            : base(desc)
        {
            oneProperty = desc;
        }

        public override bool CanResetValue(object component)
        {
            return oneProperty.CanResetValue(component);
        }

        public override Type ComponentType
        {
            get
            {
                return oneProperty.ComponentType;
            }
        }

        public override object GetValue(object component)
        {
            return oneProperty.GetValue(component);
        }

        public override string Description
        {
            get
            {
                return oneProperty.Description;
            }
        }

        public override string Category
        {
            get
            {
                return oneProperty.Category;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return oneProperty.IsReadOnly;
            }
        }

        public override void ResetValue(object component)
        {
            oneProperty.ResetValue(component);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return oneProperty.ShouldSerializeValue(component);
        }

        public override void SetValue(object component, object value)
        {
            if (oneProperty.Converter.CanConvertFrom(value.GetType()))
            {
                TypeConverter converter = oneProperty.Converter;
                oneProperty.SetValue(component, converter.ConvertFrom(value));
            }
            else
            {
                oneProperty.SetValue(component, value);
            }
        }

        public override Type PropertyType
        {
            get
            {
                return oneProperty.PropertyType;
            }
        }

        public override string DisplayName
        {
            get
            {
                PropertyDisplayNameAttribute attrib = (PropertyDisplayNameAttribute)oneProperty.Attributes[typeof(PropertyDisplayNameAttribute)];
                if (attrib != null)
                {
                    return attrib.PropertyDisplayName;
                }

                return oneProperty.DisplayName;
            }
        } 

    }

    public static class PropertyDescriptorCollectionUtils
    {
        public static PropertyDescriptorCollection ConvertToPropertyDisplayPropertyDescriptor(PropertyDescriptorCollection pdc)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);

            foreach (PropertyDescriptor pd in pdc)
            {
                collection.Add(new PropertyDisplayPropertyDescriptor(pd));
            }

            return collection.Sort(new PropertyDescriptorCollectionComparer());
        }
    }

}
