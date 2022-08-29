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
            var admin = UserSession.GetInstatnce().admin;
            if (admin == false)
            {
                button1.Visible = false;
            }

            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT id AS プロジェクトID, name AS プロジェクト名, total AS 総工数, end_date AS 終了日 FROM Projects WHERE id IN(SELECT project_id FROM Members WHERE user_id = @user_id)";
                command.Parameters.Add(new SqlParameter("@user_id", UserSession.GetInstatnce().id));
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }
            dataGridView1.DataSource = dt;
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
