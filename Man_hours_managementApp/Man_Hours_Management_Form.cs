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

        //ErrorProviderのインスタンスを生成
        ErrorProvider ep = new ErrorProvider();
       

        private void Man_Hours_Management_Form_Load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            var dt2 = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command2 = connection.CreateCommand();
                command2.CommandText = @"SELECT name FROM Projects WHERE id IN(SELECT project_id FROM Members WHERE user_id = @user_id)";
                command2.Parameters.Add(new SqlParameter("@user_id", UserSession.GetInstatnce().id));
                var my_projects = new SqlDataAdapter(command2);
                my_projects.Fill(dt2);
            }
            comboBox1.DisplayMember = "name";
            comboBox1.DataSource = dt2;
         
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;

            //ErrorProviderのアイコンを点滅なしに設定する
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            { 
                connection.Open();  
                var command3 = connection.CreateCommand();
                command3.CommandText = @"SELECT id, total FROM Projects WHERE name = @name";
                command3.Parameters.Add(new SqlParameter("@name", comboBox1.Text));
               
                var reader = command3.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader["id"];
                        textBox2.Text = id.ToString();
                        var total = reader["total"];
                        textBox3.Text = total.ToString();   
                    }
                }
            }
            var dt = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT id AS ID, registration_date AS 登録日, name AS 業務内容, cost AS 工数 FROM Costs WHERE project_id = @project_id";
                command.Parameters.Add(new SqlParameter("@project_id", textBox2.Text));
                var sda = new SqlDataAdapter(command);
                sda.Fill(dt);
            }
            dataGridView1.DataSource = dt;
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            //バリデーション
            InputCheck.errorClear(ep);
            InputCheck.isCombobox(ep, "プロジェクト名", comboBox1, true);
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
                                int user_id = UserSession.GetInstatnce().id;
                                command.CommandText = @"INSERT INTO Costs (project_id, user_id, name, cost, registration_date) VALUES (@project_id, @user_id, @name, @cost, @registration_date)";
                                command.Parameters.Add(new SqlParameter("@project_id", textBox2.Text));
                                command.Parameters.Add(new SqlParameter("@user_id", user_id));
                                command.Parameters.Add(new SqlParameter("@name", textBox4.Text));
                                command.Parameters.Add(new SqlParameter("@cost", float.Parse(textBox5.Text)));
                                command.Parameters.Add(new SqlParameter("@registration_date", dateTimePicker1.Value));

                                command.ExecuteNonQuery();
                                transaction.Commit();
                                MessageBox.Show("工数を登録しました");
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
                var dt = new DataTable();
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT id AS ID, registration_date AS 登録日, name AS 業務内容, cost AS 工数 FROM Costs WHERE project_id = @project_id";
                    command.Parameters.Add(new SqlParameter("@project_id", textBox2.Text));
                    var sda = new SqlDataAdapter(command);
                    sda.Fill(dt);
                }
                dataGridView1.DataSource = dt;
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

        //クリックしたセルのcost_idを編集画面のプロパティに設定
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            string cost_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Man_Hours_Management_Edit_Form man_Hours_Management_Edit = new Man_Hours_Management_Edit_Form();
            man_Hours_Management_Edit.Cost_id = cost_id;
            man_Hours_Management_Edit.Show();
            this.Close();
        }
    }
}
