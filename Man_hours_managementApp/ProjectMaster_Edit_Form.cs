using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            MessageBox.Show(ProjectMaster_Edit_Form.Project_id);
        }
        public int Project_id { get; set; }
    }
}
