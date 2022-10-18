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
            DataGridView dataGridView1 = new DataGridView();    
            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Users";
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }
            dt.Columns.Add("削除対象", typeof(bool));
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["削除対象"].DisplayIndex = 0;
            dataGridView1.Rows[0].Cells[7].Value = true;

            UserDeleteFormLogic userDeleteFormLogic = new UserDeleteFormLogic();
            
            bool ret = userDeleteFormLogic.Delete(dataGridView1, users_Delete_Form);
            Assert.IsTrue(ret); 
        }
    }
}
