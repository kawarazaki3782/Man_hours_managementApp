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
    public partial class ProjectsMaster_List : Form
    {
        public ProjectsMaster_List()
        {
            InitializeComponent();
            this.Load += ProjectMaster_List_load;
        }

        private void ProjectMaster_List_load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString)) { 
                var command = connection.CreateCommand();
                command.CommandText = "SELECT id, name AS プロジェクト名, customer_name AS　顧客名, project_leader, total AS 総工数,  registration_date AS 登録日, end_date AS 終了日　FROM Projects";
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }
            dataGridView1.DataSource = dt;
        }
        //dataGridViewのセルをクリックし、project_idを編集画面に渡す
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var project_id = dataGridView1.CurrentRow.Cells[0].Value;
            ProjectMaster_Edit_Form projectmaster_Edit_Form = new ProjectMaster_Edit_Form();
            projectmaster_Edit_Form.Project_id = Convert.ToInt32(project_id);
            projectmaster_Edit_Form.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MypageForm mypage = new MypageForm();
            mypage.Show();
        }
    }
}
