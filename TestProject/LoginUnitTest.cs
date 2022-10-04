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
    //Authenticate���\�b�h�̃e�X�g
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
            user.Authenticate(PasswordtextBox, LoginidtextBox);
        }

        [TestMethod]
        //�p�X���[�h���Ԉ���Ă����ꍇ�Ƀ��O�C���ł��Ȃ�����
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
        //���O�C��ID���Ԉ���Ă����ꍇ�Ƀ��O�C���ł��Ȃ�����
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
        //���O�C��ID�������͂̏ꍇ�Ƀ��O�C���ł��Ȃ�����
        public void TestLoginIDnotEnteredMethod()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "honkon";

            UserService user = new UserService();
            bool ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsFalse(ret);
        }

        [TestMethod]
        //�p�X���[�h�������͂̏ꍇ�Ƀ��O�C���ł��Ȃ�����
        public void TestPasswordnotEnteredMethod()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "honkon";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "";

            UserService user = new UserService();
            bool ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsFalse(ret);
        }

        [TestMethod]
        //���O�C��ID�������͂̍ۂ�false���Ԃ邱��
        public void TestChecknotEnteredMethod()
        {
            LoginForm loginForm = new LoginForm();
            bool ret = false;
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "";
            ret = loginForm.Check();
            Assert.IsFalse(ret);
        }
    }
}