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
    public partial class Users_Delete_Form : Form
    {
        public Users_Delete_Form()
        {
            InitializeComponent();
            this.Load += Users_Delete_Form_Load;
        }

        public void Users_Delete_Form_Load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString)) { 
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Users";
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
                dt.Columns.Add("削除対象", typeof(bool));
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["削除対象"].DisplayIndex = 0;
            }
        }

        private void mypage_button_Click(object sender, EventArgs e)
        {
            MypageForm mypageForm = new MypageForm();
            mypageForm.Show();
            this.Close();
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            UserDeleteFormLogic userDeleteFormLogic = new();
            userDeleteFormLogic.Delete(dataGridView1, this);
        }
    }
}
