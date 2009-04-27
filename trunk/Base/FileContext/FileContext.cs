/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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
    /// �ե�����ξ��֤�������륯�饹
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
        /// <c>IsSaved</c>�ץ�ѥƥ����ѹ������Ȥ���ȯ������
        /// </summary>
		public event EventHandler<GeneralEventArgs<bool>> IsSavedChanged = null;
		/// <summary>
		/// Save���줿�Ȥ���ȯ������
		/// </summary>
		public event EventHandler Saved = null;
		/// <summary>
		/// Open���줿�Ȥ���ȯ������
		/// </summary>
		public event EventHandler Opened = null;
        /// <summary>
        /// <c>IsOpened</c>�ץ�ѥƥ����ѹ������Ȥ���ȯ������
        /// </summary>
        public event EventHandler<GeneralEventArgs<bool>> IsOpenedChanged = null;
        /// <summary>
        /// <c>Path</c>�ץ�ѥƥ����ѹ������Ȥ���ȯ������
        /// </summary>
        public event EventHandler<GeneralEventArgs<string>> PathChanged = null;
        /// <summary>
        /// <c>Data</c>�ץ�ѥƥ����ѹ������Ȥ���ȯ������
        /// </summary>
        public event EventHandler<GeneralEventArgs<T>> DataChanged = null;
        /// <summary>
        /// �ե����뤬�������륷�ꥢ�饤����ǽ�ʥǡ���
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
        /// �ե����뤬�ǿ��ξ��֤��ɤ���
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
        /// �ե����뤬������Ƥ��뤫�ɤ���
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
        /// �ե�����Υѥ�
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
        /// <c>FileContext</c>�Υ��󥹥��󥹤���������
        /// </summary>
        public FileContext()
		{
        }

        /// <summary>
        /// �ե�������Ĥ���
        /// </summary>
        public void Close()
        {
            Data = null;
        }

        /// <summary>
        /// �ѥ�����ꤷ�ƥե�����򳫤�
        /// </summary>
        /// <param name="path">�����ե�����Υѥ�</param>
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
        /// �ե��������¸����
        /// </summary>
        public void Save()
        {
            if (! IsOpened)
                throw new FilePathUndefinedException("��¸����ǡ���������ޤ���");

            if (Path == string.Empty)
                throw new FilePathUndefinedException("��¸��Υѥ���̤����Ǥ���");

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
			//        throw new Exception("�ե��������¸��˥��顼��ȯ�����ޤ�����\n" + e.Message);
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
				throw new Exception("�ե��������¸��˥��顼��ȯ�����ޤ�����\n" + e.Message);
			}

			IsSaved = true;

			if (Saved != null)
			{
				Saved(this, EventArgs.Empty);
			}
        }

        /// <summary>
        /// ̾�����դ��ƥե��������¸����
        /// </summary>
        /// <param name="path">��¸������Υѥ�</param>
        public void SaveAs(string path)
        {
            Path = path;
            Save();
        }

		public event EventHandler Saving = null;

	}
}
