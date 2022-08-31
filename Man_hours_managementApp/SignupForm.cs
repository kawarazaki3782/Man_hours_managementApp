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
    public partial class SignupForm : Form
    {
        public SignupForm()
        {
            InitializeComponent();
            this.Load += SignupForm_Load;
        }

        private void SignupForm_Load(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
            //ErrorProviderのアイコンを点滅なしに設定する
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        //ErrorProviderのインスタンス生成
        ErrorProvider ep = new ErrorProvider();

        private void return_button_Click(object sender, EventArgs e)
        {
            LoginForm loginform = new LoginForm();
            loginform.Show();
            this.Close();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            comboBox1.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
        }

        private void register_button_Click_1(object sender, EventArgs e)
        {

            InputCheck.errorClear(ep);
            InputCheck.isString(ep, "氏名", textBox2, true);
            InputCheck.isString(ep, "ログインID", textBox4, true);
            InputCheck.isString(ep, "パスワード", textBox5, true);
            InputCheck.IsOnlyAlphanumeri(ep, "ログインID", textBox4, true);
            InputCheck.IsOnlyAlphanumeri(ep, "パスワード", textBox5, true);
            InputCheck.RequiredHalfSize(ep, "ログインID", textBox4, true);
            InputCheck.RequiredHalfSize(ep, "パスワード", textBox5, true);

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

                                command.CommandText = @"INSERT INTO Users (name, affiliation, login_id, password) VALUES (@name, @affiliation, @login_id, @password)";
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

                                DataTable dt = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter();

                                SqlCommand command2 = connection.CreateCommand();
                                command2.CommandText = @"SELECT admin From Users Where login_id = @login_id";
                                command2.Parameters.Add(new SqlParameter("@login_id", textBox4.Text));
                                da.SelectCommand = command2;
                                da.Fill(dt);

                                var admin = Convert.ToBoolean(dt.Rows[0]["admin"]);
                                UserSession.GetInstatnce().admin = admin;
                                MessageBox.Show("ユーザー情報を登録しました");
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
