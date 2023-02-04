using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Man_hours_managementApp
{
    public sealed class UserDeleteFormLogic
    {
        /// <summary>
        /// 削除対象にチェックが入っているユーザーをDBから削除する
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="users_Delete_Form"></param>
        /// <returns></returns>
        public bool Delete(DataGridView dataGridView1, Users_Delete_Form users_Delete_Form)
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
                                if (dataGridView1.Rows[i].Cells[7].Value != DBNull.Value && Convert.ToBoolean(dataGridView1.Rows[i].Cells[7].Value) == true)
                                {
                                    command.CommandText = @"DELETE FROM Users WHERE id = @id" + i;
                                    command.Parameters.Add(new SqlParameter("@id" + i, dataGridView1.Rows[i].Cells[0].Value));
                                    command.ExecuteNonQuery();
                                    MessageBox.Show(command.CommandText);
                                }
                            }
                            transaction.Commit();
                            MessageBox.Show("ユーザーを削除しました");
                            return true;
                            UserListForm userListForm = new UserListForm();
                            userListForm.Show();
                            users_Delete_Form.Close();
                        }
                        catch
                        {
                            return false;
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                catch
                {
                    return false;
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
