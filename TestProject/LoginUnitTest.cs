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
        public void TestLoginMethod()
        {
            TextBox PasswordtextBox = new TextBox();
            PasswordtextBox.Text = "honkon";
            TextBox LoginidtextBox = new TextBox();
            LoginidtextBox.Text = "honkon";

            bool ret = false;
            UserService user = new UserService();
            ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
            Assert.IsTrue(ret);
        }
    }
}