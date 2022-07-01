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
            // �ڑ�������̎擾
            var connectionString = ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // �f�[�^�x�[�X�̐ڑ��J�n
                    connection.Open();

                    // SQL�̎��s
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
                    // �f�[�^�x�[�X�̐ڑ��I��
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