using Man_hours_managementApp;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace TestProject
{
    [TestClass]
    public class UserDeleteUnitTest
    {
        [TestMethod]
        //ユーザーが削除できること
        public void UserDeleteTest001_DeleteMethod()
        {   
            Users_Delete_Form users_Delete_Form = new Users_Delete_Form();

            var connectionString = CommonUtil.GetConnectionString();
            DataTable dt = new DataTable();
            DataGridView dataGridView = new DataGridView();
            users_Delete_Form.Controls.Add(dataGridView);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Users";
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }

            dt.Columns.Add("削除対象", typeof(bool));
            dataGridView.DataSource = dt;

            //11行目のユーザーの削除
            dataGridView.Rows[11].Cells[7].Value = true; 

            dataGridView.DataSource = dt;
            UserDeleteFormLogic userDeleteFormLogic = new UserDeleteFormLogic();
            bool ret = userDeleteFormLogic.Delete(dataGridView, users_Delete_Form);
            Assert.IsTrue(ret); 
        }
    }
}
