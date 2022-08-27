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
    public partial class Man_Hours_Management_Edit_Form : Form
    {
        public Man_Hours_Management_Edit_Form()
        {
            InitializeComponent();
            this.Load += Man_Hours_Management_Edit_From_Load;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           MypageForm mypage = new MypageForm();
           mypage.Show();
           this.Close();
        }

        public string Cost_id { get; set; }

        private void Man_Hours_Management_Edit_From_Load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            string sql = @"SELECT name FROM Projects WHERE id IN(SELECT project_id FROM Costs WHERE id = @id)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            { 
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@id", this.Cost_id));
                var reader = cmd.ExecuteReader();
                reader.Read();
                var project_name = reader["name"];
                label7.Text = project_name.ToString();
                conn.Close();

                DataTable dt = new DataTable();
                var command = conn.CreateCommand();
                command.CommandText = @"SELECT registration_date AS 日付, name AS 業務内容, cost AS 工数 FROM Costs WHERE id = @id";
                command.Parameters.Add(new SqlParameter("@id", this.Cost_id));
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                dateTimePicker1.Text = dt.Rows[0][0].ToString();
                textBox4.Text = dt.Rows[0][1].ToString();
                textBox5.Text = dt.Rows[0][2].ToString();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Man_Hours_Management_Form man_Hours_Management_Form = new Man_Hours_Management_Form();
            man_Hours_Management_Form.Show();
            this.Close();
        }
    }
}
