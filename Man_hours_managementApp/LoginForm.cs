using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace Man_hours_managementApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            Load += LoginForm_Load;
        }

        private void LoginForm_Load(object? sender, EventArgs e)
        {
            string filePath = @"C:\Users\kawar\source\repos\Man_hours_managementApp\login.text";

            if (File.Exists(filePath))
            {
                UserService user = new UserService();
                List<string>texts = new List<string>();
                StreamReader sr = new
                StreamReader(filePath);
                string st;
                string line = File.ReadLines(filePath).Skip(0).First();
                string line2 = File.ReadLines(filePath).Skip(1).First();
                LoginidtextBox.Text = line;
                PasswordtextBox.Text = line2;
                sr.Dispose();
                bool ret= user.Authenticate(PasswordtextBox, LoginidtextBox);
                if (ret)
                {
                    MypageForm mypageform = new MypageForm();
                    mypageform.ShowDialog(this);
                    mypageform.Dispose();
                }
                else
                {
                    MessageBox.Show("ログイン情報が誤っています、再度ログインしてください");
                }
            }
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
                if (checkBox1.Checked == true)
                {
                    string path = @"C:\Users\kawar\source\repos\Man_hours_managementApp\login.text";
                    FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);

                    string login_id = LoginidtextBox.Text;
                    string password = PasswordtextBox.Text;
                    Encoding enc = Encoding.GetEncoding("Shift_JIS");
                    using (StreamWriter writer = new StreamWriter(path, false, enc))
                    {
                        writer.WriteLine(login_id);
                        writer.WriteLine(password); 
                    }
                }
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