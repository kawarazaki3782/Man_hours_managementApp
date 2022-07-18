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
using System.Configuration;
using System.Security.Cryptography;

namespace Man_hours_managementApp
{
    public partial class EditUserForm : Form
    {
        public EditUserForm()
        {
            InitializeComponent();
            this.Load += EditUserForm_Load;
        }

        //ErrorProviderのインスタンス生成
        ErrorProvider ep = new ErrorProvider();

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            textBox2.Text = UserSession.GetInstatnce().name;
            comboBox1.Text = UserSession.GetInstatnce().affiliation;
            textBox4.Text = UserSession.GetInstatnce().login_id;
            textBox5.Text = UserSession.GetInstatnce().password;
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MypageForm mypageForm = new MypageForm();
            mypageForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            comboBox1.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputCheck.errorClear(ep);
            InputCheck.isString(ep, "氏名", textBox2, true);
            InputCheck.isString(ep, "ログインID", textBox4, true);
            InputCheck.isString(ep, "パスワード", textBox5, true);
            InputCheck.IsOnlyAlphanumeri(ep, "ログインID", textBox4, true);
            InputCheck.IsOnlyAlphanumeri(ep, "パスワード", textBox5, true);

            if (InputCheck.isError == true)
            {
                MessageBox.Show("入力に不備があるため登録できません");
            }

            else
            {
                //ハッシュ値を計算
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] beforeByteArray = Encoding.UTF8.GetBytes(textBox5.Text);
                byte[] afterByteArray = sha256.ComputeHash(beforeByteArray);
                sha256.Clear();

                //バイト配列を16進数文字列に変換
                StringBuilder hash = new StringBuilder();
                foreach (byte b in afterByteArray)
                {
                    hash.Append(b.ToString("x2"));
                }

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

                                command.CommandText = @"UPDATE Users SET name = @name, affiliation = @affiliation, login_id = @login_id, password = @password WHERE login_id = @login_id";
                                command.Parameters.Add(new SqlParameter("@name", textBox2.Text));
                                command.Parameters.Add(new SqlParameter("@affiliation", comboBox1.Text));
                                command.Parameters.Add(new SqlParameter("@login_id", textBox4.Text));
                                command.Parameters.Add(new SqlParameter("@password", hash.ToString()));

                                command.ExecuteNonQuery();
                                transaction.Commit();
                                UserSession.GetInstatnce().name = textBox2.Text;
                                UserSession.GetInstatnce().affiliation = comboBox1.Text;
                                UserSession.GetInstatnce().login_id = textBox4.Text;
                                UserSession.GetInstatnce().password = textBox5.Text;
                                MessageBox.Show("ユーザー情報を更新しました");
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
        }
    }
}
