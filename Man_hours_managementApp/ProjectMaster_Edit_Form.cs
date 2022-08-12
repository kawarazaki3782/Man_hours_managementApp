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

        private void ProjectMaster_Edit_Form_Load(object sender, EventArgs e)
        {
            //完了日イベント追加
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker2_KeyDown);
            this.dateTimePicker2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dateTimePicker2_MouseDown);

            DateTime? datetTime = null;
            setDateTimePicker(dateTime);
          
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            var connectionString = CommonUtil.GetConnectionString();
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


        public string Project_id { get; set; }

        private void button3_Click(object sender, EventArgs e)
        {
            ProjectsMaster_List projectsMaster_List = new ProjectsMaster_List();
            projectsMaster_List.Show();
            this.Close();
        }
    }
}
