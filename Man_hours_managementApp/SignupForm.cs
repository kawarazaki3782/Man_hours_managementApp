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
        //画面を場合分けする区分
        public int _displayKbn;

        public SignupForm(int displayKbn)
        {
            InitializeComponent();
            this.Load += SignupForm_Load;
            _displayKbn = displayKbn;
        }
        
        //ErrorProviderのインスタンス生成
        ErrorProvider ep = new ErrorProvider();

        private void SignupForm_Load(object sender, EventArgs e)
        {
            //新規登録の場合
            if (_displayKbn == 1)
            {
                label14.Text = "新規登録";
            }

            //ユーザー編集画面の場合
            if (_displayKbn == 2)
            {
                label14.Text = "ユーザー編集";
            }

            //ユーザー編集ページの場合
            if (_displayKbn == 2)
            {
                textBox2.Text = UserSession.GetInstatnce().name;
                comboBox1.Text = UserSession.GetInstatnce().affiliation;
                textBox4.Text = UserSession.GetInstatnce().login_id;
                textBox5.Text = UserSession.GetInstatnce().password;
                ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            }
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

            //新規登録の場合
            if (_displayKbn == 1)
            {
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

            //ユーザー編集の場合
            if (_displayKbn == 2)
            {
                if (InputCheck.isError == true)
                {
                    MessageBox.Show("入力に不備があるため登録できません");
                }

                else
                {
                    UserSignupFormLogic userSignupFormLogic = new UserSignupFormLogic();
                    userSignupFormLogic.Register_1(textBox2.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString(), comboBox1.Text.ToString(), this);
                }
            }

        }
    }
}
