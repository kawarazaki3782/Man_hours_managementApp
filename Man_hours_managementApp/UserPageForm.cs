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

namespace Man_hours_managementApp
{
    public partial class UserPageForm : Form
    {
        public UserPageForm()
        {
            InitializeComponent();
            this.Load += UserPageForm_Load;
        }

        private void UserPageForm_Load(object sender, EventArgs e)
        {
            label2.Text = this.User_name;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public string User_id { get; set; }
        public string User_name { get; set; }
        public string User_affiliation { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
          Users_Delete_Form users_Delete_Form = new Users_Delete_Form();
          users_Delete_Form.Show();
          this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserListForm userListForm = new UserListForm();
            userListForm.Show();
            this.Close();
        }
    }
}
