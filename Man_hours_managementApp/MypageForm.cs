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
            var admin = UserSession.GetInstatnce().admin;
            if (admin == false)
            {
                button5.Visible = false;
                button6.Visible = false;
            }

            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            { 
                var command = connection.CreateCommand();
                command.CommandText = "SELECT id AS プロジェクトID, name AS プロジェクト名, total AS 総工数, end_date AS 終了日  FROM Projects WHERE id IN(SELECT project_id FROM Members WHERE user_id = @user_id)";
                command.Parameters.Add(new SqlParameter("@user_id", UserSession.GetInstatnce().id));
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }
            dataGridView1.DataSource = dt;            
        }


        private void project_register_button_Click(object sender, EventArgs e)
        {
            ProjectsMaster projectsMaster = new ProjectsMaster();
            projectsMaster.Show();
            this.Close();
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();  
            loginForm.Show();
            this.Close();
        }

        private void man_hour_input_button_Click(object sender, EventArgs e)
        {
            Man_Hours_Management_Form man_Hours_Management_Form = new Man_Hours_Management_Form();
            man_Hours_Management_Form.Show();
            this.Close();
        }

        private void edituser_button_Click(object sender, EventArgs e)
        {
            EditUserForm editUserForm = new EditUserForm(); 
            editUserForm.Show();
            this.Close();
        }

        private void userlist_button_Click(object sender, EventArgs e)
        {
            UserListForm userListForm = new UserListForm();
            userListForm.Show();
            this.Close();
        }

        private void users_delete_button_Click(object sender, EventArgs e)
        {
            Users_Delete_Form users_Delete_From = new Users_Delete_Form();
            users_Delete_From.Show();
            this.Close();
        }
    }
}
