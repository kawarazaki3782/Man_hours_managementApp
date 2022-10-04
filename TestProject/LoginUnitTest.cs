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
    //Authenticateメソッドのテスト
    public class LoginUnitTest
    {

        [TestMethod]
        //ログインできることのテスト
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
        //パスワードが間違っていた場合にログインできないこと
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
        //ログインIDが間違っていた場合にログインできないこと
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
        //ログインIDが未入力の場合にログインできないこと
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
        //パスワードが未入力の場合にログインできないこと
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
        //ログインIDが未入力の際にfalseが返ること
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