using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// <c>Attribute</c>のリスト
    /// </summary>
    [CollectionDataContract]
    public class AttributeList : IEnumerable<Attribute>
    {
        private Dictionary<string, Attribute> _list = new Dictionary<string, Attribute>();

        public Attribute this[string name] { get { return _list[name]; } }

        public IEnumerator<Attribute> GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
