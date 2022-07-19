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

namespace Man_hours_managementApp
{
    public partial class UserListForm : Form
    {
        public UserListForm()
        {
            InitializeComponent();
            this.Load += UserListForm_Load;
        }

        private void UserListForm_Load(object sender, EventArgs e)
        {

        }
 
         private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MypageForm mypageForm = new MypageForm();   
            mypageForm.Show();
            this.Close();
        }
    }
}
