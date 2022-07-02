using System;


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

        private void LoginidtextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}