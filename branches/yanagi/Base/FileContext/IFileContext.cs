﻿using System;
namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface IFileContext<T>
     where T : class, NU.OJL.MPRTOS.TLV.Base.IFileContextData, new()
    {
        void Close();
        T Data { get; set; }
        event EventHandler<NU.OJL.MPRTOS.TLV.Base.GeneralEventArgs<T>> DataChanged;
        bool IsOpened { get; }
        event EventHandler<NU.OJL.MPRTOS.TLV.Base.GeneralEventArgs<bool>> IsOpenedChanged;
        bool IsSaved { get; }
        event EventHandler<NU.OJL.MPRTOS.TLV.Base.GeneralEventArgs<bool>> IsSavedChanged;
        void Open(string path);
        string Path { get; set; }
        event EventHandler<NU.OJL.MPRTOS.TLV.Base.GeneralEventArgs<string>> PathChanged;
        void Save();
        void SaveAs(string path);
    }
}
