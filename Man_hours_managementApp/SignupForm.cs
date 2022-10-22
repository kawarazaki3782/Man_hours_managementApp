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
using System.Security.Cryptography;

namespace Man_hours_managementApp
{
    public partial class SignupForm : Form
    {
        public SignupForm()
        {
            InitializeComponent();
        }

        private void return_button_Click(object sender, EventArgs e)
        {
            LoginForm loginform = new LoginForm();
            loginform.Show();
            this.Close();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            comboBox1.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            //ErrorProviderのインスタンス生成
            ErrorProvider ep = new ErrorProvider();
            //ErrorProviderのアイコンを点滅なしに設定する
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            InputCheck.errorClear(ep);
            InputCheck.isString(ep, "氏名",textBox2 , true);
            InputCheck.isString(ep, "ログインID", textBox4 , true);
            InputCheck.isString(ep, "パスワード", textBox5 , true);
            InputCheck.IsOnlyAlphanumeri(ep, "ログインID", textBox4, true);
            InputCheck.IsOnlyAlphanumeri(ep, "パスワード", textBox5, true);
            InputCheck.RequiredHalfSize(ep, "ログインID", textBox4,  true);
            InputCheck.RequiredHalfSize(ep, "パスワード", textBox5, true);

            if (InputCheck.isError == true)
            {
                MessageBox.Show("入力に不備があるため登録できません");
            }

            else
            {
                UserSignupFormLogic userSignupFormLogic = new UserSignupFormLogic();
                userSignupFormLogic.Register(textBox2.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString(), comboBox1.Text.ToString(), this);
            }
        }
    }
}
