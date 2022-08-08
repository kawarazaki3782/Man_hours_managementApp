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

        private void signup_button_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm();
            signupForm.Show();
            this.Hide();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            bool ret = false;

            //入力値チェック
            ret = this.Check();
            if (ret == false)
            {
                return;
            }

            //認証
            UserService user = new UserService(); 
            ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
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

        private void PasswordtextBox_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(PasswordtextBox.Text))
            {
                PasswordtextBox.SelectionStart = 0;
                PasswordtextBox.SelectionLength = PasswordtextBox.Text.Length;
            }
        }
    }
}