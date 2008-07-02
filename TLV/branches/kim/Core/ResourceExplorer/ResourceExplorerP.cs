﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

using NU.OJL.MPRTOS.TLV.Core.Test_Main;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceExplorer
{
    public partial class ResourceExplorerP : WeifenLuo.WinFormsUI.Docking.DockContent, IPresentation
    {
        private IntPtr formHandle = IntPtr.Zero;

        private ResourceList resList;

        private ViewTypeList viewTypeList;

        // リソースチェックされてる情報持ち
        private List<string> nodeChekedList;

        public enum ViewType : int
        {
            PRC,
            CLASS,
            RES
        }

        public ResourceExplorerP(string name)
        {
            InitializeComponent();

            this.Name = name;
        }

        public void AddChild(Control control, object args)
        {

        }

        public IntPtr SetHandle
        {

            set { this.formHandle = value; }

        }


        public void InitTreeView(string resFilePath)
        {
            // リソースチェックリスト初期化
            this.nodeChekedList = new List<string>();
            
            
            FileManager fileManager = new FileManager();


            // リソースファイルのパス設定
            fileManager.SetFilePath(resFilePath);

            //ファイルからリソースデータ読み込み
            fileManager.ReadResFile(out this.resList, out this.viewTypeList);

            //リソースデータ削除
            this.prcView.Nodes.Clear();
            this.clsView.Nodes.Clear();
            this.resView.Nodes.Clear();

            //リソースデータ初期化
            initPrcView();
            initClsView();
            initResView();

            //ノードが展開する状態に
            this.prcView.ExpandAll();
            this.clsView.ExpandAll();
            this.resView.ExpandAll();

            //ソート
            this.prcView.Sort();
            this.clsView.Sort();
            this.resView.Sort();

        }


        private void initPrcView()
        {
            for (int i = 0; i < this.viewTypeList.Prc.Count; i++)
            {
                insertNode(i, this.viewTypeList.Prc, this.resList);
            }
                       
        }

        private void initClsView()
        {

            List<ObjectBase> objList = getObjInfo(this.resList);

            for (int i = 0; i < this.viewTypeList.Cls.Count; i++)
            {
                insertNode(i, ViewType.CLASS, this.viewTypeList.Cls, objList, ref this.clsView);
            }

        }

        private void initResView()
        {

            List<ObjectBase> objList = getObjInfo(this.resList);

            for (int i = 0; i < this.viewTypeList.Res.Count; i++)
            {

                insertNode(i, ViewType.RES, this.viewTypeList.Res, objList, ref this.resView);
            }

        }


        private List<ObjectBase> getObjInfo(ResourceList resList)
        {
            List<ObjectBase> objList = new List<ObjectBase>();

            for (int i = 0; i < resList.TskInfoList.Count; i++)
            {
                objList.Add(resList.TskInfoList[i]);
            }
            for (int i = 0; i < resList.SemInfoList.Count; i++)
            {
                objList.Add(resList.SemInfoList[i]);
            }
            for (int i = 0; i < resList.FlgInfoList.Count; i++)
            {
                objList.Add(resList.FlgInfoList[i]);
            }
            for (int i = 0; i < resList.DtqInfoList.Count; i++)
            {
                objList.Add(resList.DtqInfoList[i]);
            }
            for (int i = 0; i < resList.PdtqInfoList.Count; i++)
            {
                objList.Add(resList.PdtqInfoList[i]);
            }
            for (int i = 0; i < resList.MbxInfoList.Count; i++)
            {
                objList.Add(resList.MbxInfoList[i]);
            }
            for (int i = 0; i < resList.MpfInfoList.Count; i++)
            {
                objList.Add(resList.MpfInfoList[i]);
            }
            for (int i = 0; i < resList.CycInfoList.Count; i++)
            {
                objList.Add(resList.CycInfoList[i]);
            }
            for (int i = 0; i < resList.AlmInfoList.Count; i++)
            {
                objList.Add(resList.AlmInfoList[i]);
            }
            for (int i = 0; i < resList.SpnInfoList.Count; i++)
            {
                objList.Add(resList.SpnInfoList[i]);
            }
            for (int i = 0; i < resList.InhInfoList.Count; i++)
            {
                objList.Add(resList.InhInfoList[i]);
            }

            return objList;
        }

        private void insertNode(
            int viewCount, 
            List<string> viewTypeList,
            ResourceList resList)
        {
             string resInfoType =string.Empty;

             prcView.Nodes.Add(viewTypeList[viewCount]);

             foreach (TskInfo tskInfo in resList.TskInfoList)
             {
                 if (viewTypeList[viewCount].Equals(tskInfo.PrcID))
                 {
                     prcView.Nodes[viewCount].Nodes.Add(tskInfo.Key, tskInfo.Name);
                 }
             }
             foreach (CycInfo cycInfo in resList.CycInfoList)
             {
                 if (viewTypeList[viewCount].Equals(cycInfo.PrcID))
                 {
                     prcView.Nodes[viewCount].Nodes.Add(cycInfo.Key, cycInfo.Name);
                 }
             }
             foreach (AlmInfo almInfo in resList.AlmInfoList)
             {
                 if (viewTypeList[viewCount].Equals(almInfo.PrcID))
                 {
                     prcView.Nodes[viewCount].Nodes.Add(almInfo.Key, almInfo.Name);
                 }
             }

        }

        private void insertNode(
           int viewCount,
           ViewType viewType,
           List<string> viewTypeList,
           List<ObjectBase> refInfo,
           ref TreeView treeView)
        {
            string resInfoType = string.Empty;

            treeView.Nodes.Add(viewTypeList[viewCount]);


            foreach (ObjectBase objbase in refInfo)
            {
                switch (viewType)
                {
                    case ViewType.CLASS:
                        resInfoType = objbase.Class;
                        break;
                    case ViewType.RES:
                        resInfoType = objbase.Type;
                        break;
                }

                if (viewTypeList[viewCount].Equals(resInfoType))
                {
                    treeView.Nodes[viewCount].Nodes.Add(objbase.Key, objbase.Name);
                }
            }

        }     

        private void prcView_AfterSelect(object sender, TreeViewEventArgs e)
        {

            string key = e.Node.Name;

            Control ctl = Control.FromHandle(this.formHandle);

            MainP mainAgent = (MainP)ctl;

            char[] split = { '_' };
            string[] resType = key.Split(split);


            if (e.Node.Level != 1)
            {
                mainAgent.ChangePropty((Object)null);
                return;
            }
            switch (resType[0])
            {
                case FileManager.ResType.TSK:
                    for (int i = 0; i < this.resList.TskInfoList.Count; i++)
                    {
                        if (key.Equals(this.resList.TskInfoList[i].Key))
                        {
                            mainAgent.ChangePropty(this.resList.TskInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.CYC:
                    for (int i = 0; i < this.resList.CycInfoList.Count; i++)
                    {
                        if (key.Equals(this.resList.CycInfoList[i].Key))
                        {
                            mainAgent.ChangePropty(this.resList.CycInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.ALM:
                    for (int i = 0; i < this.resList.AlmInfoList.Count; i++)
                    {
                        if (key.Equals(this.resList.AlmInfoList[i].Key))
                        {
                            mainAgent.ChangePropty(this.resList.AlmInfoList[i]);
                            break;
                        }
                    }
                    break;
            }
        }

        private void clsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //string key = e.Node.Name;

            //Control ctl = Control.FromHandle(this.formHandle);

            //TLVSample tlvSample = (TLVSample)ctl;

            //char[] split = { '_' };
            //string[] resType = key.Split(split);

            
            //if(e.Node.Level != 1)
            //{
            //    tlvSample.ChangePropty((Object)null);
            //    return;
            //}

            //switch (resType[0])
            //{
            //    case FileManager.ResType.TSK:
            //        for (int i = 0; i < this.resList.TskInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.TskInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.TskInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.SEM:
            //        for (int i = 0; i < this.resList.SemInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.SemInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.SemInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.FLG:
            //        for (int i = 0; i < this.resList.FlgInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.FlgInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.FlgInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.DTQ:
            //        for (int i = 0; i < this.resList.DtqInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.DtqInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.DtqInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.PDTQ:
            //        for (int i = 0; i < this.resList.PdtqInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.PdtqInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.PdtqInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.MBX:
            //        for (int i = 0; i < this.resList.MbxInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.MbxInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.MbxInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.MPF:
            //        for (int i = 0; i < this.resList.MpfInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.MpfInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.MpfInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.CYC:
            //        for (int i = 0; i < this.resList.CycInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.CycInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.CycInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.ALM:
            //        for (int i = 0; i < this.resList.AlmInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.AlmInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.AlmInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.SPN:
            //        for (int i = 0; i < this.resList.SpnInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.SpnInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.SpnInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.INH:
            //        for (int i = 0; i < this.resList.InhInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.InhInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.InhInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //    case FileManager.ResType.EXC:
            //        for (int i = 0; i < this.resList.ExcInfoList.Count; i++)
            //        {
            //            if (key.Equals(this.resList.ExcInfoList[i].Key))
            //            {
            //                tlvSample.ChangePropty(this.resList.ExcInfoList[i]);
            //                break;
            //            }
            //        }
            //        break;
            //}

        }

        private void resView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            clsView_AfterSelect(sender, e);
        }

        private void prcView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0 && e.Action != TreeViewAction.Unknown)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
            else
            {
                bool hasNodeName = false;

                if (e.Node.Checked)
                {
                    foreach (string str in this.nodeChekedList)
                    {
                        if (str == e.Node.Name)
                        {
                            hasNodeName = true;
                            break;
                        }
                    }

                    if (!hasNodeName)
                    {
                        this.nodeChekedList.Add(e.Node.Name);
                    }
                }
                else
                {
                    foreach (string str in this.nodeChekedList)
                    {
                        if (str == e.Node.Name)
                        {
                            hasNodeName = true;
                            break;
                        }
                    }

                    if (hasNodeName)
                    {
                        this.nodeChekedList.Remove(e.Node.Name);
                    }
                }

            }
            
            if (e.Node.Level != 0)
            {
                if (e.Action != TreeViewAction.Unknown)
                {
                    bool isAllCheckedNodes = true;

                    foreach (TreeNode node in e.Node.Parent.Nodes)
                    {
                        if (!node.Checked)
                        {
                            isAllCheckedNodes = false;
                            break;
                        }
                    }

                    if (e.Node.Parent.Checked != isAllCheckedNodes)
                    {
                        e.Node.Parent.Checked = isAllCheckedNodes;
                        return;
                    }
                }
                else
                {
                    bool isAllCheckedNodes = true;

                    foreach (TreeNode node in e.Node.Parent.Nodes)
                    {
                        if (!node.Checked)
                        {
                            isAllCheckedNodes = false;
                            break;
                        }
                    }

                    if (isAllCheckedNodes == true)
                    {
                        e.Node.Parent.Checked = isAllCheckedNodes;
                        return;
                    }

                }
            }

        }

        private void clsView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            prcView_AfterCheck(sender, e);
        }

        private void resView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            prcView_AfterCheck(sender, e);
        }


        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
           switch(e.TabPageIndex)
           {
               case (int)ViewType.PRC:
                   CheckNode(ref this.prcView);
                   break;
               case (int)ViewType.CLASS:
                   CheckNode(ref this.clsView);
                   break;
               case (int)ViewType.RES:
                   CheckNode(ref this.resView);
                   break;
           }
        }

        private void CheckNode(ref TreeView treeView)
        {
            //リソースデータ無し場合
            if (this.nodeChekedList == null)
            {
                return;
            }

            List<string> nodeChekedList = new List<string>(this.nodeChekedList);

            this.nodeChekedList.Clear();

            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                treeView.Nodes[i].Checked = false;
                
                for (int j = 0; j < (treeView.Nodes[i].Nodes).Count; j++)
                {
                    treeView.Nodes[i].Nodes[j].Checked = false;
                }
            }

            foreach(string str in nodeChekedList)
            {
                for (int i = 0; i < treeView.Nodes.Count; i++)
                {
                    bool findNode = false;

                    for (int j = 0; j < (treeView.Nodes[i].Nodes).Count; j++)
                    {
                        if(str.Equals(treeView.Nodes[i].Nodes[j].Name))
                        {
                            treeView.Nodes[i].Nodes[j].Checked = true;
                            findNode = true;
                            break;
                        }
                    }

                    if (findNode == true)
                    {
                        break;
                    }

                }
            }
        }


    }
}