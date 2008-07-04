using System;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    abstract public class Agent<Tp, Ta, Tc> : IAgent
        where Tp : Control, IPresentation
        where Ta : Abstraction
        where Tc : Control<Tp, Ta>
    {
        private IAgent parent;

        public string Name { get; protected set; }
        public IAgent Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                if (value != null)
                {
                    C.Parent = value.Control;
                }
            }
        }
        public ChildrenTable<IAgent> Children { get; protected set; }
        public Tc C { get; protected set; }
        public Ta A { get { return C.A; } }
        public Tp P { get { return C.P; } }
        public bool IsMain { get; protected set; }
        public ApplicationContext ApplicationContext
        {
            get
            {
                if (this.IsMain)
                {
                    return new ApplicationContext((Form)((IAgent)this).Presentation);
                }
                else
                {
                    return new ApplicationContext();
                }
            }
        }
        Abstraction IAgent.Abstraction
        {
            get { return (Abstraction)this.C.A; }
        }
        IPresentation IAgent.Presentation
        {
            get { return (IPresentation)this.C.P; }
        }
        IControl IAgent.Control
        {
            get { return (IControl)this.C; }
        }

        protected Agent(string name, Tc control)
            : this(name, control, false) { }

        protected Agent(string name, Tc control, bool isMain)
        {
            this.Name = name;
            this.C = control;
            this.IsMain = isMain;
            this.Children = new ChildrenTable<IAgent>(this);
            this.Parent = null;
            this.Children.Added += (object o, ChildrenTableAddedEventArgs<IAgent> e) =>
                {
                    Children.Holder.Presentation.Add(e.AddedChild.Presentation);
                };

            if(this.IsMain)
            {
                ((Control)this.C.P).Disposed += (object o, EventArgs e) => { ((Control)this.C.P).Dispose(); Application.Exit(); };
            }
        }

        public IAgent this[string name]
        {
            get { return this.Children[name]; }
        }

        public void Add(IAgent agent)
        {
            this.Children.Add(agent);
            this.C.Children.Add(agent.Control);
        }

        public void Show()
        {
            if(this.IsMain)
            {
                this.InitPAC();
            }
            this.C.P.Show();
            foreach(IAgent agent in Children)
            {
                agent.Show();
            }
        }

        public void InitPAC()
        {
            foreach (IAgent agent in Children)
            {
                agent.InitPAC();
            }
            this.C.Init();
        }

    }

}
