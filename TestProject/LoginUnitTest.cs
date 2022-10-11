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
    //UserServiceのテスト
    public class UserServiceTest
    {

        [TestMethod]
        //ログインできることのテスト
        public void UserServiceTest001_LoginMethod()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "honkon";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "honkon";

            UserService user = new UserService();
            bool ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsTrue(ret);

            Assert.AreEqual(UserSession.GetInstatnce().id, 23);
            Assert.AreEqual(UserSession.GetInstatnce().name, "ごぶごぶ");
            Assert.AreEqual(UserSession.GetInstatnce().affiliation, "第一システム部");
            Assert.AreEqual(UserSession.GetInstatnce().password, "honkon");
            Assert.AreEqual(UserSession.GetInstatnce().login_id, "honkon");
            Assert.AreEqual(UserSession.GetInstatnce().admin, true);
        }

        [TestMethod]
        //パスワードが間違っていた場合にログインできないこと
        public void UserServiceTest002_LoginFailedMethod()
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
        //ログインIDが間違っていた場合にログインできないこと
        public void UserServiceTest003_LoginFailed2Method()
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
        //ログインIDが未入力の場合にログインできないこと
        public void UserServiceTest004_LoginIDnotEnteredMethod()
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
        //パスワードが未入力の場合にログインできないこと
        public void UserServiceTest005_PasswordnotEnteredMethod()
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
        //ログインIDが未入力の際にfalseが返ること
        public void TUserServiceTest006_ChecknotEnteredMethod()
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