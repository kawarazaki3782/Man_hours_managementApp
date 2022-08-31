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

        //ErrorProviderのインスタンスを生成
        ErrorProvider ep = new ErrorProvider();

        private void mypage_button_Click(object sender, EventArgs e)
        {
           MypageForm mypage = new MypageForm();
           mypage.Show();
           this.Close();
        }

        //文字数入力チェック
        private bool Check()
        {
            if (textBox4.Text.Length > 30)
            {
                MessageBox.Show("業務内容は30文字以内で入力してください");
                return false;
            }
            return true;
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

                //ErrorProviderのアイコンを点滅なしに設定する
                ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            }
        }

        private void management_button_Click(object sender, EventArgs e)
        {
            Man_Hours_Management_Form man_Hours_Management_Form = new Man_Hours_Management_Form();
            man_Hours_Management_Form.Show();
            this.Close();
        }

        private void clear_button_Click(object sender, EventArgs e)
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
                            command.CommandText = @"DELETE FROM Costs WHERE id = @id";
                            command.Parameters.Add(new SqlParameter("@id", this.Cost_id));
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            MessageBox.Show("工数を削除しました");
                            Man_Hours_Management_Form man_Hours_Management = new Man_Hours_Management_Form();
                            man_Hours_Management.Show();
                            this.Close();
                        }
                        catch
                        {
                            transaction.Rollback();
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
        private void register_button_Click(object sender, EventArgs e)
        {
            //バリデーション
            InputCheck.errorClear(ep);
            InputCheck.isString(ep, "業務内容", textBox4, true);
            InputCheck.NumbersCheck(ep, "工数", textBox5, true);
            InputCheck.RequiredHalfSize(ep, "工数", textBox5, true);

            var ret = this.Check();
            if (ret == false)
            {
                return;
            }

            if (InputCheck.isError == true)
            {
                MessageBox.Show("入力に不備があるため登録できません");
            }

            else
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
                                command.CommandText = @"UPDATE Costs SET registration_date = @registration_date, name = @name, cost = @cost WHERE id = @id";
                                command.Parameters.Add(new SqlParameter("@registration_date", dateTimePicker1.Value));
                                command.Parameters.Add(new SqlParameter("@name", textBox4.Text));
                                command.Parameters.Add(new SqlParameter("@cost", float.Parse(textBox5.Text)));
                                command.Parameters.Add(new SqlParameter("@id", this.Cost_id));

                                command.ExecuteNonQuery();
                                transaction.Commit();
                                MessageBox.Show("工数を編集しました");
                                Man_Hours_Management_Form man_Hours_Management = new Man_Hours_Management_Form();
                                man_Hours_Management.Show();
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
}
