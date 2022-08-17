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
    public partial class ProjectMaster_Edit_Form : Form
    {
        public ProjectMaster_Edit_Form()
        {
            InitializeComponent();
            this.Load += ProjectMaster_Edit_Form_Load;
        }

        DateTime? dateTime;
        //ErrorProviderのインスタンスを作成
        ErrorProvider ep = new ErrorProvider();

        private void ProjectMaster_Edit_Form_Load(object sender, EventArgs e)
        {
            textBox6.ReadOnly = true;
            textBox4.ReadOnly = true;

            //完了日の表示、非表示切り替え
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker2_KeyDown);
            this.dateTimePicker2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dateTimePicker2_MouseDown);

            DateTime? datetTime = null;
            setDateTimePicker(dateTime);

            //PLのコンポボックスにUsersテーブルをバインド
            var connectionString = CommonUtil.GetConnectionString();
            var dt3 = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            { 
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT name FROM Users";
                var pl = new SqlDataAdapter(command);
                pl.Fill(dt3);
            }
            comboBox1.DisplayMember = "name";
            comboBox1.DataSource = dt3;
            comboBox1.SelectedIndex = -1;

            //プロジェクトメンバーにUsersテーブルをバインド
            var dt4 = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT name FROM Users";
                var pm = new SqlDataAdapter(command);
                pm.Fill(dt4);
            }
            comboBox2.DisplayMember = "name";
            comboBox2.DataSource = dt4;
            comboBox2.SelectedIndex = -1;

            //ErrorProviderのアイコンを点滅なしに設定する
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            //プロジェクト一覧でクリックされたプロジェクトを表示
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM Projects Where id = @id";
                    command.Parameters.Add(new SqlParameter("@id", this.Project_id));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    textBox4.Text = dt.Rows[0][0].ToString();
                    textBox5.Text = dt.Rows[0][1].ToString();
                    comboBox1.Text = dt.Rows[0][2].ToString();
                    textBox7.Text = dt.Rows[0][3].ToString();
                    textBox3.Text = dt.Rows[0][4].ToString();
                    dateTimePicker1.Text = dt.Rows[0][6].ToString();
                    dateTimePicker2.Text = dt.Rows[0][5].ToString();

                    var command2 = connection.CreateCommand();
                    command2.CommandText = @"SELECT user_id AS ID, estimated_time AS 工数 FROM Members Where project_id = @project_id";
                    command2.Parameters.Add(new SqlParameter("@project_id", this.Project_id));
                    SqlDataAdapter adapter2 = new SqlDataAdapter(command2);
                    adapter2.Fill(dt2);
                    dt2.Columns.Add("プロジェクトメンバー", typeof(string));
                    var rowCount = dt2.Rows.Count;
                    if (rowCount > 0)
                    { 
                        var command3 = connection.CreateCommand();
                        for (int i = 0; i < rowCount; i++)
                        {
                            command3.CommandText = @"SELECT name FROM Users Where id = @id" + i +"";
                            command3.Parameters.Add(new SqlParameter("@id" + i, dt2.Rows[i][0].ToString()));
                            dt2.Rows[i][2] = command3.ExecuteScalar();
                        }
                    }
                    dataGridView1.DataSource = dt2;
                    dataGridView1.Columns["プロジェクトメンバー"].DisplayIndex = 0;
                    dataGridView1.Columns["ID"].DisplayIndex = 1;
                    dataGridView1.Columns["工数"].DisplayIndex = 2;

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //完了日の表示・非表示の設定
        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                dateTime = null;
                setDateTimePicker(dateTime);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTime = dateTimePicker2.Value;
            setDateTimePicker(dateTime);
        }

        private void setDateTimePicker(DateTime? datetime)
        {
            if (datetime == null)
            {
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = " ";
            }

            else
            {
                dateTimePicker2.Format = DateTimePickerFormat.Long;
                dateTimePicker2.Value = (DateTime)datetime;
            }
        }

        private void dateTimePicker2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > dateTimePicker2.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }
        
        //ProjectMaster_Edit_Formクラスのプロパティの設定
        public string Project_id { get; set; }

        //プロジェクト一覧ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            ProjectsMaster_List projectsMaster_List = new ProjectsMaster_List();
            projectsMaster_List.Show();
            this.Close();
        }

        //マイページボタン
        private void button2_Click(object sender, EventArgs e)
        {
            MypageForm mypageForm = new MypageForm();
            mypageForm.Show();
            this.Close();

        }
        //クリアボタン
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            comboBox1.Text = String.Empty;
            comboBox2.Text = String.Empty;
            textBox7.Text = String.Empty;
            var dt = new DataTable();
            dataGridView1.DataSource = dt;
        }

        //プロジェクトメンバーで選択されたメンバーのIDを自動取得
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

        //入力チェックメソッド
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

        //登録ボタン
        private void button6_Click(object sender, EventArgs e)
        {
            //バリデーション
            InputCheck.errorClear(ep);
            InputCheck.isString(ep, "プロジェクト名", textBox5, true);
            InputCheck.NumbersCheck(ep, "総工数(人/月)", textBox7, true);
            InputCheck.NumbersCheck(ep, "工数(人/月)", textBox2, true);

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
                                command.CommandText = @"UPDATE Projects SET customer_name = @customer_name, name = @name, registration_date = @registration_date, project_leader = @project_leader, total = @total, end_date = @end_date Where id = @id";
                                command.Parameters.Add(new SqlParameter("@customer_name", textBox3.Text));
                                command.Parameters.Add(new SqlParameter("@name", textBox5.Text));
                                command.Parameters.Add(new SqlParameter("@registration_date", dateTimePicker1.Value));
                                command.Parameters.Add(new SqlParameter("@project_leader", comboBox1.Text));
                                command.Parameters.Add(new SqlParameter("@total", textBox7.Text));
                                command.Parameters.Add(new SqlParameter("@end_date", dateTimePicker2.Value));
                                command.Parameters.Add(new SqlParameter("@id", this.Project_id));
                                command.ExecuteNonQuery();

                                var rowCount = dataGridView1.RowCount;
                                if (rowCount > 0)
                                {
                                    var command2 = new SqlCommand() { Connection = connection, Transaction = transaction};
                                    for (int i = 0; i < rowCount; i++)
                                    {
                                        var estimeted_time = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                        command2.CommandText = @"UPDATE Members SET user_id = @user_id" + i + ",project_id = @project_id" + i + ",estimated_time = @estimated_time" + i + " WHERE project_id = @project_id";
                                        command2.Parameters.Add(new SqlParameter("@user_id" + i, dataGridView1.Rows[i].Cells[1].Value));
                                        command2.Parameters.Add(new SqlParameter("@estimated_time" + i, Convert.ToSingle(estimeted_time)));
                                        command2.Parameters.Add(new SqlParameter("@project_id" + i, textBox4.Text));
                                        command2.Parameters.Add(new SqlParameter("@project_id", this.Project_id));
                                        MessageBox.Show(command2.CommandText);
                                        command2.ExecuteNonQuery();
                                    }
                                }
                                transaction.Commit();
                                MessageBox.Show("プロジェクトを編集しました");
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
    }
}
