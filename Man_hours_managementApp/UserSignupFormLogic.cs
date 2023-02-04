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
        public bool Select(string loginId)
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
                            command.CommandText = @"SELECT login_id FROM Users WHERE login_id = @login_id";
                            command.Parameters.Add(new SqlParameter("@login_id", loginId));

                            command.ExecuteNonQuery();
                            transaction.Commit();
    
                            return true;
                        }
                        catch
                        {
                            return false;
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                catch (Exception exception)
                {
                    return false;
                    Console.WriteLine(exception.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        /// <summary>
        /// ユーザー情報の新規登録
        /// 画面に入力されたユーザー情報をDBに登録する
        /// </summary>
        /// <param name="textBox2"></param>
        /// <param name="textBox4"></param>
        /// <param name="textBox5"></param>
        /// <param name="comboBox1"></param>
        /// <param name="ep"></param>
        /// <param name="signupForm"></param>
        public bool Register(string name, string loginId, string password, string afiliation, SignupForm signupForm)
        {
            bool result = Select(loginId);
            if (result == true)
            {

                //ハッシュ値を計算
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] beforeByteArray = Encoding.UTF8.GetBytes(password);
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
                                command.Parameters.Add(new SqlParameter("@name", name));
                                command.Parameters.Add(new SqlParameter("@affiliation", afiliation));
                                command.Parameters.Add(new SqlParameter("@login_id", loginId));
                                command.Parameters.Add(new SqlParameter("@password", hash.ToString()));

                                command.ExecuteNonQuery();
                                transaction.Commit();
                                UserSession.GetInstatnce().name = name;
                                UserSession.GetInstatnce().affiliation = afiliation;
                                UserSession.GetInstatnce().login_id = loginId;
                                UserSession.GetInstatnce().password = password;

                                DataTable dt = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter();

                                SqlCommand command2 = connection.CreateCommand();
                                command2.CommandText = @"SELECT admin From Users Where login_id = @login_id";
                                command2.Parameters.Add(new SqlParameter("@login_id", loginId));
                                da.SelectCommand = command2;
                                da.Fill(dt);

                                var admin = Convert.ToBoolean(dt.Rows[0]["admin"]);
                                UserSession.GetInstatnce().admin = admin;
                                MessageBox.Show("ユーザー情報を登録しました");
                                return true;

                                MypageForm mypageForm = new MypageForm();
                                mypageForm.Show();
                                signupForm.Close();

                            }
                            catch
                            {
                                return false;
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        return false;
                        Console.WriteLine(exception.Message);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            if (result == false)
            {
                MessageBox.Show("データの登録に失敗しました。");
                result = false;
            }
            return result;    
        }

        /// <summary>
        /// ユーザー情報を編集後に登録
        /// 画面に入力されたユーザー情報をDBに登録する
        /// </summary>
        /// <param name="textBox2"></param>
        /// <param name="textBox4"></param>
        /// <param name="textBox5"></param>
        /// <param name="comboBox1"></param>
        /// <param name="ep"></param>
        /// <param name="signupForm"></param>
        public bool Register_1(string name, string loginId, string password, string afiliation, SignupForm signupForm)
        {
            //ハッシュ値を計算
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] beforeByteArray = Encoding.UTF8.GetBytes(password);
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
                            command.Parameters.Add(new SqlParameter("@name", name));
                            command.Parameters.Add(new SqlParameter("@affiliation", afiliation));
                            command.Parameters.Add(new SqlParameter("@login_id", loginId));
                            command.Parameters.Add(new SqlParameter("@password", hash.ToString()));

                            command.ExecuteNonQuery();
                            transaction.Commit();
                            UserSession.GetInstatnce().name = name;
                            UserSession.GetInstatnce().affiliation = afiliation;
                            UserSession.GetInstatnce().login_id = loginId;
                            UserSession.GetInstatnce().password = password;
                            MessageBox.Show("ユーザー情報を更新しました");
                            return true;

                            MypageForm mypageForm = new MypageForm();
                            mypageForm.Show();
                            signupForm.Close();
                        }
                        catch
                        {
                            return false;
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                catch (Exception exception)
                {
                    return false;
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
