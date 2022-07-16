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

            //入力値チェック
            ret = this.Check();
            if (ret == false)
            {
                return;
            }

            //認証
            ret = this.Authenticate();
            if (ret)
            {
                MessageBox.Show("ログインに成功しました");
                MypageForm mypageform = new MypageForm();
                mypageform.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("ログインIDまたはパスワードが間違っています");
            }
        }

        private bool Check()
        {
            //必須チェック
            if (LoginidtextBox.Text == "")
            {
                MessageBox.Show("ログインIDを入力してください");
                return false;
            }

            if (PasswordtextBox.Text == "")
            {
                MessageBox.Show("パスワードを入力してください");
                return false;
            }

            if (LoginidtextBox.Text.Length > 20)
            {
                MessageBox.Show("ログインIDの入力値に誤りがあります");
                return false;
            }

            if (PasswordtextBox.Text.Length > 20)
            {
                MessageBox.Show("パスワードの入力値に誤りがあります");
                return false;
            }
            //禁則文字チェック

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

            //バイト配列を16進数文字列に変換
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

                //検索結果が1件ではない場合
                if (dt.Rows.Count != 1)
                {
                    return false;
                }

                //パスワードが一致しない場合
                if (dt.Rows[0]["password"].ToString() != hash.ToString())
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("接続エラー");
                return false;

            }
            finally
            { 
                connection.Close();
            }
        }
           
    }
}