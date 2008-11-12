using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// ファイルの状態を管理するクラス
    /// </summary>
    /// <typeparam name="T"><c>class, IFileContextData, new()</c></typeparam>
    public class FileContext<T> : IFileContext<T>
        where T : class, IFileContextData, new()
    {
        private bool _isSaved = true;
        private bool _isOpened = false;
        private string _path = string.Empty;
        private T _data = null;

        /// <summary>
        /// <c>IsSaved</c>プロパティが変更されるときに発生する
        /// </summary>
        public event EventHandler<GeneralEventArgs<bool>> IsSavedChanged = null;
        /// <summary>
        /// <c>IsOpened</c>プロパティが変更されるときに発生する
        /// </summary>
        public event EventHandler<GeneralEventArgs<bool>> IsOpenedChanged = null;
        /// <summary>
        /// <c>Path</c>プロパティが変更されるときに発生する
        /// </summary>
        public event EventHandler<GeneralEventArgs<string>> PathChanged = null;
        /// <summary>
        /// <c>Data</c>プロパティが変更されるときに発生する
        /// </summary>
        public event EventHandler<GeneralEventArgs<T>> DataChanged = null;
        /// <summary>
        /// ファイルが管理するシリアライズ可能なデータ
        /// where T : class, IFileContextData, new()
        /// </summary>
        public T Data
        {
            get { return _data; }
            set
            {
                _data = value;

                if (value != null)
                {
                    _data.IsDirtyChanged += (o, e) =>
                    {
                        IsSaved = !e.Arg;
                    };
                }

                IsOpened = (value != null);
                IsSaved = false;
                Path = string.Empty;

                if (DataChanged != null)
                    DataChanged(this, new GeneralEventArgs<T>(_data));
            }
        }
        /// <summary>
        /// ファイルが最新の状態かどうか
        /// </summary>
        public bool IsSaved
        {
            get { return _isSaved; }
            private set
            {
                _isSaved = value;

                if (_isSaved && _data != null)
                {
                    _data.IsDirty = false;
                }

                if (IsSavedChanged != null)
                    IsSavedChanged(this, new GeneralEventArgs<bool>(_isSaved));
            }
        }
        /// <summary>
        /// ファイルが開かれているかどうか
        /// </summary>
        public bool IsOpened
        {
            get { return _isOpened; }
            private set
            {
                _isOpened = value;
                if (IsOpenedChanged != null)
                    IsOpenedChanged(this, new GeneralEventArgs<bool>(_isOpened));
            }
        }
        /// <summary>
        /// ファイルのパス
        /// </summary>
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

        /// <summary>
        /// <c>FileContext</c>のインスタンスを生成する
        /// </summary>
        public FileContext()
        {
        }

        /// <summary>
        /// ファイルを閉じる
        /// </summary>
        public void Close()
        {
            Data = null;
        }

        /// <summary>
        /// パスを指定してファイルを開く
        /// </summary>
        /// <param name="path">開くファイルのパス</param>
        public void Open(string path)
        {
            if (_data == null)
            {
                _data = new T();
                _data.IsDirtyChanged += (o, e) => {IsSaved = !e.Arg;};
            }
            Data.Deserialize(path);
            Path = path;
            IsOpened = true;
            IsSaved = true;

            if (DataChanged != null)
                DataChanged(this, new GeneralEventArgs<T>(_data));
        }

        /// <summary>
        /// ファイルを保存する
        /// </summary>
        public void Save()
        {
            if (! IsOpened)
                throw new FilePathUndefinedException("保存するデータがありません。");

            if (Path == string.Empty)
                throw new FilePathUndefinedException("保存先のパスが未設定です。");

            IsSaved = true;
            Data.Serialize(Path);
        }

        /// <summary>
        /// 名前を付けてファイルを保存する
        /// </summary>
        /// <param name="path">保存する先のパス</param>
        public void SaveAs(string path)
        {
            Path = path;
            Save();
        }

    }
}
