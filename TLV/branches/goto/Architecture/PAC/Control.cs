using System;
using System.Reflection;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public abstract class Control<Tp, Ta> : IControl
        where Tp : Control, IPresentation
        where Ta : Abstraction
    {
        public string Name { get; protected set; }
        public Tp P { get; protected set; }
        public Ta A { get; protected set; }
        Abstraction IControl.Abstraction
        {
            get { return (Abstraction)A; }
        }
        IPresentation IControl.Presentation
        {
            get { return (IPresentation)P; }
        }
        public IControl Parent { get; set; }
        public ChildrenTable<IControl> Children { get; protected set; }

        public IControl this[string name]
        {
            get { return this.Children[name]; }
        }

        protected Control(string name, Tp presentation, Ta abstraction)
        {
            this.Name = name;
            this.P = presentation;
            this.A = abstraction;
            this.Children = new ChildrenTable<IControl>(this);
            this.Parent = null;
        }

        public IAbstraction getAProviding(Type type, string name, SearchAFlags flags, IControl self)
        {
            if ((flags & SearchAFlags.Self) != SearchAFlags.None)
            {
                Type aType = A.GetType();
                PropertyInfo pi = aType.GetProperty(type.Name.ToString(), type);
                if(pi != null)
                {
                    return A;
                }
            }
            if ((flags & (SearchAFlags.Children | SearchAFlags.Descendants)) != SearchAFlags.None)
            {
                if (Children.Count != 0)
                {
                    SearchAFlags f = SearchAFlags.Self;
                    if ((flags & (SearchAFlags.Descendants)) != SearchAFlags.None)
                    {
                        f |= SearchAFlags.Descendants;
                    }
                    foreach (IControl ctrl in Children)
                    {
                        if(ctrl.Equals(self))
                        {
                            continue;
                        }
                        IAbstraction a = ctrl.getAProviding(type, name, f);
                        if (a != null)
                        {
                            return a;
                        }
                    }
                }
            }
            if ((flags & (SearchAFlags.Parent | SearchAFlags.Ancestors | SearchAFlags.AncestorsWithSiblings)) != SearchAFlags.None)
            {
                if (Parent != null)
                {
                    SearchAFlags f = SearchAFlags.Self;
                    if ((flags & (SearchAFlags.AncestorsWithSiblings | SearchAFlags.Ancestors)) != SearchAFlags.None)
                    {
                        f |= flags;
                    }
                    if ((flags & (SearchAFlags.AncestorsWithSiblings)) != SearchAFlags.None)
                    {
                        f |= SearchAFlags.Descendants;
                    }
                    return Parent.getAProviding(type, name, f, this);
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public void BindPToA(string pPropertyName, Type aType, string aPropertyName, SearchAFlags flags)
        {
            IAbstraction a = this.getAProviding(aType, aPropertyName, flags);
            if (a != null)
            {
                P.DataBindings.Add(pPropertyName, a, aPropertyName, false, DataSourceUpdateMode.OnPropertyChanged);
            }
            else
            {
                throw new Exception("型 : " + aType.ToString() + ", プロパティ名 : " + aPropertyName + " をもつAbstractionは見つかりませんでした");
            }

        }

        public void Add(IControl control)
        {
            this.Children.Add(control);
        }

        public virtual void Init()
        {

        }

    }

}
