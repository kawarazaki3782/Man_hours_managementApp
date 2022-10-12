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
    public sealed class UserSignupFormLogic
    {
        public void Clear(TextBox textBox2, ComboBox comboBox1, TextBox textBox4, TextBox textBox5)
        {
            textBox2.Text = String.Empty;
            comboBox1.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
        }
    }
}
