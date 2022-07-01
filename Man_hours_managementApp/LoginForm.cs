using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Man_hours_managementApp
{
    public partial class LoginForm : Form
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;
        }



        public LoginForm()
        {
            InitializeComponent();
        }

        public void Connect()
        {
            // 接続文字列の取得
            var connectionString = ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの実行
                    command.CommandText = @"SELECT count(*) FROM T_USER";
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


        private void button2_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm();   
            signupForm.Show();
            this.Hide();
        }

        private void LoginidtextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}