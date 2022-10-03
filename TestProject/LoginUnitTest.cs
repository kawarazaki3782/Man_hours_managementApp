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
        //ログインできることのテスト
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
        //ログインを省略にチェックを入れた状態でログインした際にローカルにテキストファイルが作成されること
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
        //ログインIDが間違っていた場合にログインできないこと
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
        //パスワードが間違っていた場合にログインできないこと
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
        //ログインIDが未入力の場合にメッセージが表示されること
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