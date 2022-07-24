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
    public partial class Man_Hours_Management_Form : Form
    {
        public Man_Hours_Management_Form()
        {
            InitializeComponent();
            this.Load += Man_Hours_Management_Form_Load;
        }

        private void Man_Hours_Management_Form_Load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString)) { 
                var command = connection.CreateCommand();
                command.CommandText = "SELECT registration_date AS 登録日, name AS 業務内容, cost AS 工数 FROM Costs";
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
               }
            dataGridView1.DataSource = dt;
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
                            int user_id = UserSession.GetInstatnce().id;
                            command.CommandText = @"INSERT INTO Costs (user_id, name, cost, registration_date) VALUES (@user_id, @name, @cost, @registration_date)";
                            //command.Parameters.Add(new SqlParameter("@project_id", hoge));
                            command.Parameters.Add(new SqlParameter("@user_id", user_id));
                            command.Parameters.Add(new SqlParameter("@name", textBox4.Text));
                            command.Parameters.Add(new SqlParameter("@cost", float.Parse(textBox5.Text)));
                            command.Parameters.Add(new SqlParameter("@registration_date", dateTimePicker1.Value));

                            command.ExecuteNonQuery();
                            transaction.Commit();
                            MessageBox.Show("工数を登録しました");
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

        private void mypage_button_Click(object sender, EventArgs e)
        {
            MypageForm mypage = new MypageForm();
            mypage.Show();
            this.Close();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            comboBox1.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
