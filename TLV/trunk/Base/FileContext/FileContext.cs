using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class FileContext<T>
        where T : class, IFileContextData, new()
    {
        private bool _isSaved = true;
        private bool _isOpened = false;
        private string _path = string.Empty;
        private T _data = null;

        public event EventHandler<GeneralEventArgs<bool>> IsSavedChanged = null;
        public event EventHandler<GeneralEventArgs<bool>> IsOpenedChanged = null;
        public event EventHandler<GeneralEventArgs<string>> PathChanged = null;
        public event EventHandler<GeneralEventArgs<T>> DataChanged = null;
        public T Data
        {
            get { return _data; }
            set
            {
                _data = value;

                if (value != null)
                {
                    _data.BecameDirty += (o, e) =>
                    {
                        IsSaved = !_data.IsDirty;
                    };
                }

                IsOpened = (value != null);
                IsSaved = false;
                Path = string.Empty;

                if (DataChanged != null)
                    DataChanged(this, new GeneralEventArgs<T>(_data));
            }
        }
        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                if (_isSaved != value)
                {
                    _isSaved = value;
                    if (IsSavedChanged != null)
                        IsSavedChanged(this, new GeneralEventArgs<bool>(_isSaved));
                }
            }
        }
        public bool IsOpened
        {
            get { return _isOpened; }
            set
            {
                if (_isOpened != value)
                {
                    _isOpened = value;
                    if (IsOpenedChanged != null)
                        IsOpenedChanged(this, new GeneralEventArgs<bool>(_isOpened));
                }
            }
        }
        public string Path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                {
                    _path = value;
                    if (PathChanged != null)
                        PathChanged(this, new GeneralEventArgs<string>(_path));
                }
            }
        }

        public FileContext()
        {
        }

        public void Close()
        {
            IsOpened = false;
            IsSaved = true;
            Data = null;
            Path = string.Empty;
        }

        public void Open(string path)
        {
            if (_data == null)
                _data = new T();
            Data.Deserialize(path);
            Path = path;
            IsOpened = true;
            IsSaved = true;

            if (DataChanged != null)
                DataChanged(this, new GeneralEventArgs<T>(_data));
        }

        public void Save()
        {
            if (! IsOpened)
                throw new FilePathUndefinedException("保存するデータがありません。");

            if (Path == string.Empty)
                throw new FilePathUndefinedException("保存先のパスが未設定です。");

            IsSaved = true;
            Data.Serialize(Path);
        }

        public void SaveAs(string path)
        {
            Path = path;
            Save();
        }

    }
}
