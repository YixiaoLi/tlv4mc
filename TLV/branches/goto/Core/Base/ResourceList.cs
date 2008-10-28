using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースリスト
    /// </summary>
    [CollectionDataContract]
    public class ResourceList : IEnumerable<Resource>
    {
        private Dictionary<string, Resource> _list = new Dictionary<string, Resource>();
        public Resource this[string name] { get { return _list[name]; } }
        public int Count { get { return _list.Count; } }

        public IEnumerator<Resource> GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public static ResourceList Serialize(string resourceData)
        {
            return new JsonQueryStringConverter().ConvertStringToValue(resourceData, typeof(ResourceList)) as ResourceList;
            
        }

        public static string Desirialize(ResourceList resourceList)
        {
            return new JsonQueryStringConverter().ConvertValueToString(resourceList, typeof(ResourceList));
        }

    }
}
