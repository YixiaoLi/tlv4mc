using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Collections.Specialized;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class StatisticsExplorerSetting : ISetting
    {

        #region ISetting メンバー

        public event SettingChangeEventHandler BecameDirty = null;

        #endregion

        public ObservableMultiKeyDictionary<bool> StatisticsVisibility { get; set; }

        public StatisticsExplorerSetting()
        {
            StatisticsVisibility = new ObservableMultiKeyDictionary<bool>();
            StatisticsVisibility.CollectionChanged += CollectionChangedFactory("ResourceVisibility");
        }

        NotifyCollectionChangedEventHandler CollectionChangedFactory(string propertyName)
        {
            return (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                if (BecameDirty != null)
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            BecameDirty(e.NewItems, propertyName);
                            break;
                    }
                }
            };
        }
    }
}
