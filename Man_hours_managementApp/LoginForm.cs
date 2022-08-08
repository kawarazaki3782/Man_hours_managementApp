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

            //���͒l�`�F�b�N
            ret = this.Check();
            if (ret == false)
            {
                return;
            }

            //�F��
            UserService user = new UserService(); 
            ret = user.Authenticate(PasswordtextBox, LoginidtextBox);
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