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
            comboBox1.DataSource = dt2;
            comboBox1.SelectedIndex = -1;
            comboBox2.DisplayMember = "name";
            comboBox2.DataSource = dt2;
            comboBox2.SelectedIndex = -1;
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
            dataGridView1.Rows.Clear();
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
                            command.CommandText = @"INSERT INTO Projects (customer_name, id, name, registration_date, project_leader, total) VALUES (@customer_name, @id, @name, @registration_date, @project_leader, @total)";
                            command.Parameters.Add(new SqlParameter("@customer_name", textBox3.Text));
                            command.Parameters.Add(new SqlParameter("@id", textBox4.Text));
                            command.Parameters.Add(new SqlParameter("@name", textBox5.Text));
                            command.Parameters.Add(new SqlParameter("@registration_date", dateTimePicker1.Value));
                            command.Parameters.Add(new SqlParameter("@project_leader", comboBox1.Text));
                            command.Parameters.Add(new SqlParameter("@total", textBox7.Text));
                            command.ExecuteNonQuery();
                          
                            var rowCount = dataGridView1.RowCount;
                            if (rowCount > 0)
                            {
                                var command2 = new SqlCommand() { Connection = connection, Transaction = transaction };
                                //command.CommandText = @"INSERT INTO Members (user_id, project_id, estimated_time) VALUES ";
                                //var command2 = new SqlCommand(@"INSERT INTO Members (user_id, project_id, estimated_time) VALUES",connection,transaction);
                                StringBuilder sb = new StringBuilder(4096);
                                for (int i = 0; i < rowCount; i++)
                                {
                                    //command2.CommandText += "(@user_id"+i+", @project_id"+i+", @estimated_time"+i+"),";
                                    //command2.Parameters.Add(new SqlParameter("@user_id"+i, dataGridView1[1, i].Value));
                                    //command2.Parameters.Add(new SqlParameter("@project_id"+i, textBox4.Text));
                                    //command2.Parameters.Add(new SqlParameter("@estimated_time"+i, dataGridView1[2, i].Value));
                                    string user_id = "@user_id" + i.ToString();
                                    string project_id = "@project_id" + i.ToString();
                                    string estimated_time = "@estimated_time" + i.ToString();
                                    command2.Parameters.AddWithValue(user_id, dataGridView1[1, i].Value);
                                    command2.Parameters.AddWithValue(project_id, textBox4.Text);
                                    command2.Parameters.AddWithValue(estimated_time, dataGridView1[2, i].Value);

                                    sb.Append("INSERT INTO Members(user_id, project_id, estimated_time) VALUES(" + user_id + "," + project_id + "," + estimated_time + ");");
                                }
                                //最後の余計なカンマを削除   
                                //command2.CommandText = command2.CommandText.Substring(0, command2.CommandText.Length - 1);
                                command2.CommandText = sb.ToString();
                                command2.ExecuteNonQuery();
                            }
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
            dataGridView1.Rows.Add(comboBox2.Text, textBox6.Text, textBox2.Text);
        }
    }
}
