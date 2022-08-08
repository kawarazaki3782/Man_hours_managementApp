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
    public sealed class UserService
    {
        public bool Authenticate(TextBox PasswordtextBox, TextBox LoginidtextBox)
        {
            var connectionString = CommonUtil.GetConnectionString();
            var connection = new SqlConnection(connectionString);
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] beforebytearray = Encoding.UTF8.GetBytes(PasswordtextBox.Text);
            byte[] afterbytearray = sha256.ComputeHash(beforebytearray);
            sha256.Clear();

            //バイト配列を16進数文字列に変換
            StringBuilder hash = new StringBuilder();
            foreach (byte b in afterbytearray)
            {
                hash.Append(b.ToString("x2"));
            }

            try
            {
                connection.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();

                SqlCommand command = connection.CreateCommand();

                command.CommandText = "SELECT id, password, name, affiliation, login_id, admin FROM Users WHERE login_id = @login_id";
                command.Parameters.Add("@login_id", System.Data.SqlDbType.NVarChar, 50);
                command.Parameters["@login_id"].Value = LoginidtextBox.Text;

                da.SelectCommand = command;
                da.Fill(dt);

                //検索結果が1件ではない場合
                if (dt.Rows.Count != 1)
                {
                    return false;
                }

                //パスワードが一致しない場合
                if (dt.Rows[0]["password"].ToString() != hash.ToString())
                {
                    return false;
                }
                int user_id = (int)dt.Rows[0]["id"];
                UserSession.GetInstatnce().id = user_id; 
                UserSession.GetInstatnce().name = dt.Rows[0]["name"].ToString();
                UserSession.GetInstatnce().affiliation = dt.Rows[0]["affiliation"].ToString();
                UserSession.GetInstatnce().password = PasswordtextBox.Text;
                UserSession.GetInstatnce().login_id = dt.Rows[0]["login_id"].ToString();
                var admin = Convert.ToBoolean(dt.Rows[0]["admin"]);
                UserSession.GetInstatnce().admin = admin;                 
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("接続エラー");
                return false;

            }
            finally
            {
                connection.Close();
            }
        }
    }
}
