using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Man_hours_managementApp
{
    public partial class MypageForm : Form
    {
        public MypageForm()
        {
            InitializeComponent();
            this.Load += MypageForm_Load;
        }

        private void MypageForm_Load(object sender, EventArgs e)
        {
            label12.Text = UserSession.GetInstatnce().name;
        }

        //ListViewコントロールを初期化
        private void InitializeListView()
        {
            //ListViewコントロールのプロパティを設定
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Sorting = SortOrder.Ascending;
            listView1.View = View.Details;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProjectsMaster projectsMaster = new ProjectsMaster();
            projectsMaster.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();  
            loginForm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Man_Hours_Management_Form man_Hours_Management_Form = new Man_Hours_Management_Form();
            man_Hours_Management_Form.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditUserForm editUserForm = new EditUserForm(); 
            editUserForm.Show();
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
