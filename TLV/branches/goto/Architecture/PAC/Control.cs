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
        public ControlTable Children { get; protected set; }

        public IControl this[string name]
        {
            get { return this.Children[name]; }
        }

        protected Control(string name, Tp presentation, Ta abstraction)
        {
            this.Name = name;
            this.P = presentation;
            this.A = abstraction;
            this.Children = new ControlTable(this);
            this.Parent = null;
        }

        public IAbstraction getAProviding(Type type, string name, SearchAFlags flags)
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
            if ((flags & SearchAFlags.Parent) != SearchAFlags.None)
            {
                return Parent.getAProviding(type, name, SearchAFlags.Self);
            }
            if ((flags & SearchAFlags.Children) != SearchAFlags.None)
            {
                foreach(IControl ctrl in Children)
                {
                    IAbstraction a = ctrl.getAProviding(type, name, SearchAFlags.Children | SearchAFlags.Self);
                    if(a != null)
                    {
                        return a;
                    }
                }
            }

            return null;
        }

        public void BindPToA(string pPropertyName, Type aType, string aPropertyName)
        {
            BindPToA(pPropertyName, aType, aPropertyName, SearchAFlags.Self);
        }

        public void BindPToA(string pPropertyName, Type aType, string aPropertyName, SearchAFlags flags)
        {
            IAbstraction a = getAProviding(aType, aPropertyName, flags);
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

        public virtual void InitC()
        {

        }

    }

    public class ControlTable : ElementTable<IControl>
    {
        private IControl holder;

        public ControlTable(IControl holder)
        {
            this.holder = holder;
        }

        public override void Add(IControl control)
        {
            base.Add(control);
            control.Parent = holder;
        }
    }

    [Flags]
    public enum SearchAFlags
    {
        None        = 0x00,
        Self        = 0x01,
        Parent      = 0x02,
        Children    = 0x04,
        All         = Self | Parent | Children,
    }
}
