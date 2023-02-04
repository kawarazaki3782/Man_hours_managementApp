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
    /// <summary>
    /// UserSignupFormLogicのテスト
    /// </summary>
    /// 
    [TestClass]
    public class UserSignupUnitTest
    {
        [TestMethod]
        //新規登録ができることのテスト
        public void UserSignupTest001_SignupMethod()
        {
            int displayKbn = 1;
            SignupForm signupForm = new SignupForm(displayKbn);

            TextBox textBox2 = new TextBox();
            textBox2.Text = "テストユーザー";

            TextBox textBox4 = new TextBox();
            textBox4.Text = "testUser";

            TextBox textBox5 = new TextBox();
            textBox5.Text = "password3782";

            ComboBox comboBox1 = new ComboBox();
           
            UserSignupFormLogic userSignupFormLogic = new UserSignupFormLogic();
            bool ret = userSignupFormLogic.Register(textBox2.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString(), comboBox1.Text.ToString(), signupForm);
            Assert.IsTrue(ret);
            Assert.AreEqual(UserSession.GetInstatnce().name, "テストユーザー");
            Assert.AreEqual(UserSession.GetInstatnce().password, "password3782");
            Assert.AreEqual(UserSession.GetInstatnce().affiliation, "");
            Assert.AreEqual(UserSession.GetInstatnce().login_id, "testUser");
            Assert.AreEqual(UserSession.GetInstatnce().admin, false);
        }

        [TestMethod]
        //新規登録に失敗すること
        public void UserSignupTest002_SignupFailureMethod()
        {
            int displayKbn = 1;
            SignupForm signupForm = new SignupForm(displayKbn);

            TextBox textBox2 = new TextBox();
            textBox2.Text = "新規登録失敗";

            TextBox textBox4 = new TextBox();
            textBox4.Text = "新規登録失敗";

            TextBox textBox5 = new TextBox();
            textBox5.Text = "新規登録失敗";

            ComboBox comboBox1 = new ComboBox();

            UserSignupFormLogic userSignupFormLogic = new UserSignupFormLogic();
            bool ret = userSignupFormLogic.Register(textBox2.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString(), comboBox1.Text.ToString(), signupForm);
            Assert.IsFalse(ret);

        }

        [TestMethod]
        //ユーザー情報を編集後に登録ができることのテスト
        public void UserSignupTest003_SignupMethod()
        {
            int displayKbn = 2;
            SignupForm signupForm = new SignupForm(displayKbn);

            TextBox textBox2 = new TextBox();
            textBox2.Text = "テストユーザー(更新)";

            TextBox textBox4 = new TextBox();
            textBox4.Text = "testUser";

            TextBox textBox5 = new TextBox();
            textBox5.Text = "password3782";

            ComboBox comboBox1 = new ComboBox();


            UserSignupFormLogic userSignupFormLogic = new UserSignupFormLogic();
            bool ret = userSignupFormLogic.Register_1(textBox2.Text, textBox4.Text, textBox5.Text, comboBox1.Text, signupForm);
            Assert.IsTrue(ret);
            Assert.AreEqual(UserSession.GetInstatnce().name, "テストユーザー(更新)");
            Assert.AreEqual(UserSession.GetInstatnce().password, "password3782");
            Assert.AreEqual(UserSession.GetInstatnce().affiliation, "");
            Assert.AreEqual(UserSession.GetInstatnce().login_id, "testUser");
            Assert.AreEqual(UserSession.GetInstatnce().admin, false);
        }
    }
}
