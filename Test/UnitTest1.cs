using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    /// <summary>
    /// UnitTest1 �γ��פ�����
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: ���󥹥ȥ饯�� ���å��򤳤����ɲä��ޤ�
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///���ߤΥƥ��Ȥμ¹ԤˤĤ��Ƥξ��󤪤�ӵ�ǽ��
        ///�󶡤���ƥ��� ����ƥ����Ȥ�����ޤ������ꤷ�ޤ���
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region �ɲäΥƥ���°��
        //
        // �ƥ��Ȥ��������ݤˤϡ������ɲ�°������ѤǤ��ޤ�:
        //
        // ���饹��Ǻǽ�Υƥ��Ȥ�¹Ԥ������ˡ�ClassInitialize ����Ѥ��ƥ����ɤ�¹Ԥ��Ƥ�������
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ���饹��Υƥ��Ȥ򤹤٤Ƽ¹Ԥ����顢ClassCleanup ����Ѥ��ƥ����ɤ�¹Ԥ��Ƥ�������
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // �ƥƥ��Ȥ�¹Ԥ������ˡ�TestInitialize ����Ѥ��ƥ����ɤ�¹Ԥ��Ƥ�������
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // �ƥƥ��Ȥ�¹Ԥ�����ˡ�TestCleanup ����Ѥ��ƥ����ɤ�¹Ԥ��Ƥ�������
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: �ƥ��� ���å��򤳤����ɲä��Ƥ�������
            //
        }
    }
}
