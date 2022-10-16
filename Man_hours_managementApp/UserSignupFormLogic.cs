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
        /// <summary>
        /// 画面に入力されたユーザー情報を消す
        /// </summary>
        /// <param name="textBox2"></param>
        /// <param name="comboBox1"></param>
        /// <param name="textBox4"></param>
        /// <param name="textBox5"></param>
        public void Clear(TextBox textBox2, ComboBox comboBox1, TextBox textBox4, TextBox textBox5)
        {
            textBox2.Text = String.Empty;
            comboBox1.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
        }

        /// <summary>
        /// 画面に入力されたユーザー情報をDBに登録する
        /// </summary>
        /// <param name="textBox2"></param>
        /// <param name="textBox4"></param>
        /// <param name="textBox5"></param>
        /// <param name="comboBox1"></param>
        /// <param name="ep"></param>
        /// <param name="signupForm"></param>
        public void Register(TextBox textBox2, TextBox textBox4, TextBox textBox5, ComboBox comboBox1, ErrorProvider ep, SignupForm signupForm)
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
                                signupForm.Close();
            
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
