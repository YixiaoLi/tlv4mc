using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TLV.Models;
using TLV.Controllers;

namespace TLV.Forms
{
    public partial class ResExplorer : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private IntPtr formHandle = IntPtr.Zero;

        private ResourceList m_resourceList;

        private ViewTypeList m_viewTypeList;

        public enum ViewType : int
        {
            PRC,
            CLASS,
            RES
        }

        public ResExplorer()
        {
            InitializeComponent();
        }

        public IntPtr SetHandle
        {

            set { this.formHandle = value; }

        }


        public void InitTreeView(string resFilePath)
        {
            FileManager fileManager = new FileManager();


            // リソースファイルのパス設定
            fileManager.SetFilePath(resFilePath);

            //ファイルからリソースデータ読み込み
            fileManager.ReadResFile(out m_resourceList, out m_viewTypeList);

            //リソースデータ削除
            prcView.Nodes.Clear();
            clsView.Nodes.Clear();
            resView.Nodes.Clear();

            //リソースデータ初期化
            initPrcView(m_resourceList, m_viewTypeList);
            initClsView(m_resourceList, m_viewTypeList);
            initResView(m_resourceList, m_viewTypeList);

            //ノードが展開する状態に
            prcView.ExpandAll();
            clsView.ExpandAll();
            resView.ExpandAll();

            //ソート
            prcView.Sort();
            clsView.Sort();
            resView.Sort();

        }


        private void initPrcView(ResourceList resList, ViewTypeList viewTypeList)
        {
            for (int i = 0; i < viewTypeList.Prc.Count; i++)
            {
                insertNode(i, ViewType.PRC, viewTypeList.Prc, resList.TskInfoList, ref prcView);
            }

        }

        private void initClsView(ResourceList resList, ViewTypeList viewTypeList)
        {

            List<ResourceList.ObjectBase> objList = getObjInfo(resList);
            
            for (int i = 0; i < viewTypeList.Cls.Count; i++)
            {
                insertNode(i, ViewType.CLASS, viewTypeList.Cls, objList, ref clsView);
            }

        }

        private void initResView(ResourceList resList, ViewTypeList viewTypeList)
        {

            List<ResourceList.ObjectBase> objList = getObjInfo(resList);

            for (int i = 0; i < viewTypeList.Res.Count; i++)
            {

                insertNode(i, ViewType.RES, viewTypeList.Res, objList, ref resView);
            }

        }


        private List<ResourceList.ObjectBase> getObjInfo(ResourceList resList)
        {
            List<ResourceList.ObjectBase> objList = new List<ResourceList.ObjectBase>();

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
            ViewType viewType,
            List<string> viewTypeList,
            List<ResourceList.TskInfo> refInfo,
            ref TreeView treeView)
        {
             string resInfoType =string.Empty;

             treeView.Nodes.Add(viewTypeList[viewCount]);

            for (int j = 0; j < refInfo.Count; j++)
            {
                switch (viewType)
                {
                    case ViewType.PRC:
                        resInfoType = refInfo[j].Prc;
                        break;
                    case ViewType.CLASS:
                        resInfoType = refInfo[j].Class;
                        break;
                    case ViewType.RES:
                        resInfoType = refInfo[j].Type;
                        break;
                }

                if (viewTypeList[viewCount].Equals(resInfoType))
                {
                    treeView.Nodes[viewCount].Nodes.Add(refInfo[j].Key, refInfo[j].Name);
                }
            }

        }

        private void insertNode(
           int viewCount,
           ViewType viewType,
           List<string> viewTypeList,
           List<ResourceList.ObjectBase> refInfo,
           ref TreeView treeView)
        {
            string resInfoType = string.Empty;

            treeView.Nodes.Add(viewTypeList[viewCount]);

            for (int j = 0; j < refInfo.Count; j++)
            {
                switch (viewType)
                {
                    case ViewType.CLASS:
                        resInfoType = refInfo[j].Class;
                        break;
                    case ViewType.RES:
                        resInfoType = refInfo[j].Type;
                        break;
                }

                if (viewTypeList[viewCount].Equals(resInfoType))
                {
                    treeView.Nodes[viewCount].Nodes.Add(refInfo[j].Key, refInfo[j].Name);
                }
            }

        }     

        private void prcView_AfterSelect(object sender, TreeViewEventArgs e)
        {

            string key = e.Node.Name;

            Control ctl = Control.FromHandle(this.formHandle);

            TLVSample tlvSample = (TLVSample)ctl;

            if (e.Node.Level == 1)
            {
                for (int i = 0; i < m_resourceList.TskInfoList.Count; i++)
                {
                    if (key.Equals(m_resourceList.TskInfoList[i].Key))
                    {
                        tlvSample.ChangePropty(m_resourceList.TskInfoList[i]);
                        return;
                    }
                }
            }
            else
            {
                Object obj = null;
                tlvSample.ChangePropty(obj);

            }


        }

        private void clsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string key = e.Node.Name;

            Control ctl = Control.FromHandle(this.formHandle);

            TLVSample tlvSample = (TLVSample)ctl;

            char[] split = { '_' };
            string[] resType = key.Split(split);

            
            if(e.Node.Level != 1)
            {
                tlvSample.ChangePropty((Object)null);
                return;
            }

            switch (resType[0])
            {
                case FileManager.ResType.TSK:
                    for (int i = 0; i < m_resourceList.TskInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.TskInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.TskInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.SEM:
                    for (int i = 0; i < m_resourceList.SemInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.SemInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.SemInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.FLG:
                    for (int i = 0; i < m_resourceList.FlgInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.FlgInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.FlgInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.DTQ:
                    for (int i = 0; i < m_resourceList.DtqInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.DtqInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.DtqInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.PDTQ:
                    for (int i = 0; i < m_resourceList.PdtqInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.PdtqInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.PdtqInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.MBX:
                    for (int i = 0; i < m_resourceList.MbxInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.MbxInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.MbxInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.MPF:
                    for (int i = 0; i < m_resourceList.MpfInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.MpfInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.MpfInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.CYC:
                    for (int i = 0; i < m_resourceList.CycInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.CycInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.CycInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.ALM:
                    for (int i = 0; i < m_resourceList.AlmInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.AlmInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.AlmInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.SPN:
                    for (int i = 0; i < m_resourceList.SpnInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.SpnInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.SpnInfoList[i]);
                            break;
                        }
                    }
                    break;
                case FileManager.ResType.INH:
                    for (int i = 0; i < m_resourceList.InhInfoList.Count; i++)
                    {
                        if (key.Equals(m_resourceList.InhInfoList[i].Key))
                        {
                            tlvSample.ChangePropty(m_resourceList.InhInfoList[i]);
                            break;
                        }
                    }
                    break;
            }

        }

        private void resView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            clsView_AfterSelect(sender, e);
        }

    }
}
