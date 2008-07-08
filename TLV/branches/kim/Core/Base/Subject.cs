using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class Subject
    {
        // メンバ変数との不一致リスク低減のためメンバ変数をなるべくなくす
        // そのかわりプロパティにprotected setを使ってアクセス制限

        public ResourceType Type { get; protected set; }
        public int Id { get; protected set; }

        public Subject(ResourceType type, int id)
        {
            this.Type = type;
            this.Id = id;
        }
    }
}
