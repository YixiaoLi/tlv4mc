﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public partial class MainP : Form, IPresentation
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainP(string name)
        {
            InitializeComponent();

            this.Name = name;

            //this.toolStripContainer.ContentPanel.Hide();
        }

        public void Add(IPresentation presentation)
        {
            this.toolStripContainer.ContentPanel.Controls.Add((Control)presentation);
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }

}
