using Man_hours_managementApp;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.IO;



namespace TestProject

{
    [TestClass]
    public class LoginUnitTest
    {
        [TestMethod]
        //���O�C���ł��邱�Ƃ̃e�X�g
        public void TestLoginMethod()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "honkon";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "honkon";

            UserService user = new UserService();
            bool ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsTrue(ret);
        }

        [TestMethod]
        //���O�C�����ȗ��Ƀ`�F�b�N����ꂽ��ԂŃ��O�C�������ۂɃ��[�J���Ƀe�L�X�g�t�@�C�����쐬����邱��
        public void TestLoginOmitMethod()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "honkon";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "honkon";
            CheckBox checkBox1 = new CheckBox();
            checkBox1.Checked = true;

            UserService user = new UserService();
            bool ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsTrue(ret);
        }

        [TestMethod]
        //���O�C��ID���Ԉ���Ă����ꍇ�Ƀ��O�C���ł��Ȃ�����
        public void TestLoginFailedMethod()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "honkon";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "test";

            UserService user = new UserService();
            bool ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsFalse(ret);
        }

        [TestMethod]
        //�p�X���[�h���Ԉ���Ă����ꍇ�Ƀ��O�C���ł��Ȃ�����
        public void TestLoginFailed2Method()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "test";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "honkon";

            UserService user = new UserService();
            bool ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsFalse(ret);
        }

        [TestMethod]
        //���O�C��ID�������͂̏ꍇ�Ƀ��b�Z�[�W���\������邱��
        public void TestLoginIDnotEnteredMethod()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "honkon";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "";

            UserService user = new UserService();
            bool ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsFalse(ret);
        }
    }
}