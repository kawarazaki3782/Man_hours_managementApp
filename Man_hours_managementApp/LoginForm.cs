using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;


namespace Man_hours_managementApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm();
            signupForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ret = false;

            //���͒l�`�F�b�N
            ret = this.Check();
            if (ret == false)
            {
                return;
            }

            //�F��
            ret = this.Authenticate();
            if (ret)
            {
                MessageBox.Show("���O�C���ɐ������܂���");
                MypageForm mypageform = new MypageForm();
                mypageform.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("���O�C��ID�܂��̓p�X���[�h���Ԉ���Ă��܂�");
            }
        }

        private bool Check()
        {
            //�K�{�`�F�b�N
            if (LoginidtextBox.Text == "")
            {
                MessageBox.Show("���O�C��ID����͂��Ă�������");
                return false;
            }

            if (PasswordtextBox.Text == "")
            {
                MessageBox.Show("�p�X���[�h����͂��Ă�������");
                return false;
            }

            if (LoginidtextBox.Text.Length > 20)
            {
                MessageBox.Show("���O�C��ID�̓��͒l�Ɍ�肪����܂�");
                return false;
            }

            if (PasswordtextBox.Text.Length > 20)
            {
                MessageBox.Show("�p�X���[�h�̓��͒l�Ɍ�肪����܂�");
                return false;
            }
            //�֑������`�F�b�N

            return true;

        }

        private bool Authenticate()
        {
            var connectionString = CommonUtil.GetConnectionString();
            var connection = new SqlConnection(connectionString);
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] beforebytearray = Encoding.UTF8.GetBytes(PasswordtextBox.Text);
            byte[] afterbytearray = sha256.ComputeHash(beforebytearray);
            sha256.Clear();

            //�o�C�g�z���16�i��������ɕϊ�
            StringBuilder hash = new StringBuilder();
            foreach (byte b in afterbytearray)
            {
                hash.Append(b.ToString("x2"));
            }

            try
            {
                connection.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();

                SqlCommand command = connection.CreateCommand();

                command.CommandText = "SELECT password FROM Users WHERE login_id = @login_id";
                command.Parameters.Add("@login_id", System.Data.SqlDbType.NVarChar, 50);
                command.Parameters["@login_id"].Value = LoginidtextBox.Text;

                da.SelectCommand = command;
                da.Fill(dt);

                //�������ʂ�1���ł͂Ȃ��ꍇ
                if (dt.Rows.Count != 1)
                {
                    return false;
                }

                //�p�X���[�h����v���Ȃ��ꍇ
                if (dt.Rows[0]["password"].ToString() != hash.ToString())
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("�ڑ��G���[");
                return false;

            }
            finally
            { 
                connection.Close();
            }
        }
           
    }
}