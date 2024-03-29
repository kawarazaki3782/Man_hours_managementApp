﻿using System;
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

        //ErrorProviderのインスタンスを生成
        ErrorProvider ep = new ErrorProvider();

        private void ProjectsMaster_Load(object sender, EventArgs e)
        {
            textBox6.ReadOnly = true;
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
            comboBox2.DisplayMember = "name";
            comboBox2.DataSource = dt2;
            comboBox2.SelectedIndex = -1;

            var dt3 = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name FROM Users";
                var pl = new SqlDataAdapter(command);
                pl.Fill(dt3);
            }
            comboBox1.DisplayMember = "name";
            comboBox1.DataSource = dt3;
            comboBox1.SelectedIndex = -1;

            //ErrorProviderのアイコンを点滅なしに設定する
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
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
            textBox6.Text = String.Empty;       
            comboBox1.Text = String.Empty;
            comboBox2.Text = String.Empty;
            textBox7.Text = String.Empty;
            dataGridView1.Rows.Clear();
        }

        private bool Check()
        {
            if (textBox3.Text.Length > 30)
            {
                MessageBox.Show("顧客名は30文字以内で入力してください");
                return false;
            }

            if (textBox4.Text.Length > 30)
            {
                MessageBox.Show("プロジェクトIDは30文字以内で入力してください");
                return false;
            }

            if (textBox5.Text.Length > 30)
            {
                MessageBox.Show("プロジェクト名は30文字以内で入力してください");
                return false;
            }
            return true;

        }
       
        private void register_button_Click(object sender, EventArgs e)
        {
            //バリデーション
            InputCheck.errorClear(ep);
            InputCheck.IsOnlyAlphanumeri(ep, "プロジェクトID", textBox4, true);
            InputCheck.RequiredHalfSize(ep, "プロジェクトID", textBox4, true);
            InputCheck.isString(ep, "プロジェクト名", textBox5, true);
            InputCheck.NumbersCheck(ep, "総工数(人/月)", textBox7, true);
            InputCheck.NumbersCheck(ep, "工数(人/月)", textBox2, true);
            InputCheck.RequiredHalfSize(ep, "総工数(人/月)", textBox7, true);
            InputCheck.RequiredHalfSize(ep, "工数(人/月)", textBox2, true);
            InputCheck.ProjectsidCheck(ep, "プロジェクトID", textBox4, true);

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
                                    var command2 = new SqlCommand(@"INSERT INTO Members (user_id, project_id, estimated_time) VALUES", connection, transaction);
                                    for (int i = 0; i < rowCount; i++)
                                    {
                                        command2.CommandText += "(@user_id" + i + ", @project_id" + i + ", @estimated_time" + i + "),";
                                        command2.Parameters.Add(new SqlParameter("@user_id" + i, dataGridView1.Rows[i].Cells[1].Value));
                                        command2.Parameters.Add(new SqlParameter("@estimated_time" + i, dataGridView1.Rows[i].Cells[2].Value));
                                        command2.Parameters.Add(new SqlParameter("@project_id" + i, textBox4.Text));
                                    }
                                    command2.CommandText = command2.CommandText.Substring(0, command2.CommandText.Length - 1);
                                    MessageBox.Show(command2.CommandText);
                                    // 最後の余計なカンマを削除
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
        }

        //dataGridViewに行を追加
        private void add_button_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(comboBox2.Text, textBox6.Text, textBox2.Text);
        }

        //プロジェクトメンバーを選択し、user_idを自動取得
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            string sql = @"SELECT id FROM Users Where name = @name";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@name", comboBox2.Text));
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader["id"];
                        textBox6.Text = id.ToString();
                    }
                }           
            }
        }

        //dataGridViewの行を削除
        private void row_delete_Click(object sender, EventArgs e)
        {
            int idx = this.dataGridView1.Rows.Count - 1;
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(idx);
            }
        }

        private void project_list_Click(object sender, EventArgs e)
        {
            ProjectsMaster_List projectsMaster_List = new ProjectsMaster_List();
            projectsMaster_List.Show();
            this.Close();
        }
    }
}
