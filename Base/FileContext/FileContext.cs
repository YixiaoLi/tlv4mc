/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
		/// Saveされたときに発生する
		/// </summary>
		public event EventHandler Saved = null;
		/// <summary>
		/// Openされたときに発生する
		/// </summary>
		public event EventHandler Opened = null;
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

        public bool IsFileSaved()
        {
            return this.Path == String.Empty;
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

			if (Opened != null)
				Opened(this, EventArgs.Empty);
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

			//Thread thread = new Thread(new ThreadStart(() =>
			//{
			//    if (Saving != null)
			//        Saving(this, EventArgs.Empty);
				
			//    try
			//    {
			//        Data.Serialize(Path);
			//    }
			//    catch (Exception e)
			//    {
			//        throw new Exception("ファイルの保存中にエラーが発生しました。\n" + e.Message);
			//    }
				
			//    IsSaved = true;

			//    if (Saved != null)
			//    {
			//        Saved(this, EventArgs.Empty);
			//    }
			//}));
			//thread.IsBackground = true;

			//thread.Start();

			if (Saving != null)
				Saving(this, EventArgs.Empty);

			try
			{
				Data.Serialize(Path);
			}
			catch (Exception e)
			{
				throw new Exception("ファイルの保存中にエラーが発生しました。\n" + e.Message);
			}

			IsSaved = true;

			if (Saved != null)
			{
				Saved(this, EventArgs.Empty);
			}
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

		public event EventHandler Saving = null;

	}
}
