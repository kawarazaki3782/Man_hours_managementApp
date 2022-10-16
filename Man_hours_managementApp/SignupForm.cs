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
            this.Load += SignupForm_Load;
        }

        private void SignupForm_Load(object? sender, EventArgs e)
        {
            //ErrorProviderのアイコンを点滅なしに設定する
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        //ErrorProviderのインスタンス生成
        ErrorProvider ep = new ErrorProvider();

        private void return_button_Click(object sender, EventArgs e)
        {
            LoginForm loginform = new LoginForm();
            loginform.Show();
            this.Close();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            UserSignupFormLogic userSignupFormLogic = new UserSignupFormLogic();
            userSignupFormLogic.Clear(textBox2, comboBox1, textBox4, textBox5);
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            UserSignupFormLogic userSignupFormLogic = new UserSignupFormLogic();
            userSignupFormLogic.Register(textBox2, textBox4, textBox5, comboBox1, ep, this);
        }
    }
}
