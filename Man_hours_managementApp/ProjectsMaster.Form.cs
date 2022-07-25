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
    public partial class ProjectsMaster : Form
    {
        public ProjectsMaster()
        {
            InitializeComponent();
            this.Load += ProjectsMaster_Load;
        }

        private void ProjectsMaster_Load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name AS メンバー FROM Users";
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }
     
            var dt2 = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name FROM Users";
                var pl = new SqlDataAdapter(command);
                pl.Fill(dt2);
            }
            comboBox1.DisplayMember = "name";
            comboBox1.SelectedIndex = -1;
            comboBox1.DataSource = dt2;
            comboBox2.DisplayMember = "name";
            comboBox2.SelectedIndex = -1;
            comboBox2.DataSource = dt2;
        }

       

        private void mypage_button_Click(object sender, EventArgs e)
        {
            MypageForm mypageForm = new MypageForm();
            mypageForm.Show();
            this.Close();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            comboBox1.Text = String.Empty;
            textBox7.Text = String.Empty;
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    using (var command = new SqlCommand() { Connection = connection, Transaction = transaction })
                    {
                        try
                        {
                            command.CommandText = @"INSERT INTO Projects (customer_name, project_id, name, registration_date, project_leader, total) VALUES (@customer_name, @project_id, @name, @registration_date, @project_leader, @total)";
                            command.Parameters.Add(new SqlParameter("@customer_name", textBox3.Text));
                            command.Parameters.Add(new SqlParameter("@project_id", textBox4.Text));
                            command.Parameters.Add(new SqlParameter("@name", textBox5.Text));
                            command.Parameters.Add(new SqlParameter("@registration_date", dateTimePicker1.Value));
                            command.Parameters.Add(new SqlParameter("@project_leader", comboBox1.Text));
                            command.Parameters.Add(new SqlParameter("@total", textBox7.Text));

                            command.ExecuteNonQuery();

                            command.CommandText = @"INSERT INTO Members (customer_name, project_id, name, registration_date, project_leader, total) VALUES (@customer_name, @project_id, @name, @registration_date, @project_leader, @total)";





                            transaction.Commit();
                            MessageBox.Show("プロジェクトを登録しました");
                            MypageForm mypageForm = new MypageForm();
                            mypageForm.Show();
                            this.Close();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void add_button_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(comboBox2.Text, textBox2.Text);
        }
    }
}
