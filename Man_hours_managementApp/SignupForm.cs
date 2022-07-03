using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Man_hours_managementApp
{
    public partial class SignupForm : Form
    {
        public SignupForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm loginform = new LoginForm();
            loginform.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
        }


        private void button3_Click(object sender, EventArgs e)
        { 
                var connectionString = ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        // データベースの接続開始
                        connection.Open();

                        // SQLの準備
                        command.CommandText = @"INSERT INTO USERS (NAME) VALUES (@NAME)";
                        command.Parameters.Add(new SqlParameter("@NAME", textBox2));

                        // SQLの実行
                        command.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                        throw;
                    }
                    finally
                    {
                        // データベースの接続終了
                        connection.Close();
                    }
                }        
        }
    }
}
