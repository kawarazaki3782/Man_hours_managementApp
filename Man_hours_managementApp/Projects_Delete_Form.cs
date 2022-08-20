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
    public partial class Projects_Delete_Form : Form
    {
        public Projects_Delete_Form()
        {
            InitializeComponent();
            this.Load += Projects_Delete_Form_load;
        }


        private void Projects_Delete_Form_load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Projects";
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }
            dt.Columns.Add("削除対象", typeof(bool));
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
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
                                //チェックが入っている場合
                                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[7].Value) == true)
                                {
                                    //行削除
                                    command.CommandText = @"DELETE FROM Projects WHERE id = @id" + i;
                                    command.Parameters.Add(new SqlParameter("@id" + i, dataGridView1.Rows[i].Cells[0].Value));
                                    command.ExecuteNonQuery();
                                    MessageBox.Show(command.CommandText);
                                }
                            }
                            transaction.Commit();
                            MessageBox.Show("プロジェクトを削除しました");
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
