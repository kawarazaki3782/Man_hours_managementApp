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

        private void Users_Delete_Form_Load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString)) { 
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Users";
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }
            dt.Columns.Add("削除対象", typeof(bool));
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["削除対象"].DisplayIndex = 0; 
        }

        private void mypage_button_Click(object sender, EventArgs e)
        {
            MypageForm mypageForm = new MypageForm();
            mypageForm.Show();
            this.Close();
        }

        private void delete_button_Click(object sender, EventArgs e)
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
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[6].Value != DBNull.Value && Convert.ToBoolean(dataGridView1.Rows[i].Cells[6].Value) == true)
                                {
                                    command.CommandText = @"DELETE FROM Users WHERE id = @id" + i;
                                    command.Parameters.Add(new SqlParameter("@id" + i, dataGridView1.Rows[i].Cells[0].Value));
                                    command.ExecuteNonQuery();
                                    MessageBox.Show(command.CommandText);
                                }
                            }
                            transaction.Commit();
                            MessageBox.Show("ユーザーを削除しました");
                            UserListForm userListForm = new UserListForm();
                            userListForm.Show();
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
    }
}
