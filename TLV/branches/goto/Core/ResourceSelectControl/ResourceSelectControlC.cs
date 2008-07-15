using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceSelectControl
{
    public class ResourceSelectControlC : Control<ResourceSelectControlP, ResourceSelectControlA>
    {
        public ResourceSelectControlC(string name, ResourceSelectControlP presentation, ResourceSelectControlA abstraction)
            : base(name, presentation, abstraction)
        {
        }

        public override void InitParentFirst()
        {
            BindPToA("ViewableObjectList", typeof(Dictionary<Type, List<TaskInfo>>), "ViewableObjectList", SearchAFlags.Ancestors);
            BindPToA("ViewableObjectDataSource", typeof(SortableBindingList<TaskInfo>), "ViewableObjectDataSource", SearchAFlags.AncestorsWithSiblings);
            BindPToA("SelectedObject", typeof(object), "SelectedObject", SearchAFlags.AncestorsWithSiblings);
        }
    }
}
