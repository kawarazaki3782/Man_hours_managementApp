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

namespace Man_hours_managementApp
{
    public partial class ProjectMaster_Edit_Form : Form
    {
        public ProjectMaster_Edit_Form()
        {
            InitializeComponent();
            this.Load += ProjectMaster_Edit_Form_Load;
        }

        private void ProjectMaster_Edit_Form_Load(object sender, EventArgs e)
        {
            var connectionString = CommonUtil.GetConnectionString();   
            using(var connection = new SqlConnection(connectionString))
            {   
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Projects Where id = @id";
                command.Parameters.Add(new SqlParameter("@id", this.Project_id));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader["id"];
                        textBox4.Text = id.ToString();
                    }
                }
            }

        }
        public int Project_id { get; set; }

        private void button3_Click(object sender, EventArgs e)
        {
            ProjectsMaster_List projectsMaster_List = new ProjectsMaster_List();
            projectsMaster_List.Show();
            this.Close();
        }
    }
}
