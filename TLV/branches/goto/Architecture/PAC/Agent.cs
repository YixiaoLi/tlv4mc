﻿using System;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IAgent : IElement
    {
        IAgent Parent { get; set; }
        AgentTable Children { get; }
        IControl Control { get; }
        void Show();
        bool IsMain { get; }
    }

    abstract public class Agent<Tp, Ta, Tc> : IAgent
        where Tp : IPresentation
        where Ta : IAbstraction
        where Tc : IControl
    {
        protected string name;
        protected Tc control;
        private IAgent parent;
        private AgentTable children;
        private bool isMain;

        public string Name { get { return name; } }
        public IAgent Parent { get { return parent; } set { parent = value; } }
        public AgentTable Children { get { return children; } }
        public IControl Control { get { return control; } }
        public bool IsMain { get { return isMain; } }
        public ApplicationContext ApplicationContext
        {
            get
            {
                if (this.IsMain)
                {
                    return new ApplicationContext((Form)(this.control.Presentation));
                }
                else
                {
                    return new ApplicationContext();
                }
            }
        }

        protected Agent(string name, Tc control)
            : this(name, control, false) { }

        protected Agent(string name, Tc control, bool isMain)
        {
            this.name = name;
            this.control = control;
            this.isMain = isMain;
            this.children = new AgentTable(this);

            if(this.IsMain)
            {
                ((Control)this.control.Presentation).Disposed += (object o, EventArgs e) => { ((Control)this.control.Presentation).Dispose(); Application.Exit(); };
            }
        }

        protected void AddChild(IAgent child)
        {
            children.Add(child);
            child.Parent = this;
        }

        public void Show()
        {
            control.ShowPresentation();
        }
    }

    public class AgentTable : ElementTable<IAgent>
    {
        private IAgent holder;

        public AgentTable(IAgent holder)
        {
            this.holder = holder;
        }

        public override void Add(IAgent agent)
        {
            Add(agent, null);
        }

        public void Add(IAgent agent, Object args)
        {
            base.Add(agent);
            holder.Control.AddSubPresentation(agent.Control.Presentation, args);
        }
    }
}
