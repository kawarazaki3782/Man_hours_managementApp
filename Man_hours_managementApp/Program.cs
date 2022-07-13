using System.Configuration;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Man_hours_managementApp
{

    internal static class Program　//同じプロジェクトならアクセス可
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread] //Formを実行するMainでは必ず[STAThread]で指定
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();　//アプリケーションの構成プロパティを初期化
            Application.Run(new LoginForm());
        }   
    }

    public class CommonUtil{
        public static string GetConnectionString() { 　//接続文字列を返すメソッド
            return ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;
        }
    }

    public class PasswordService {
        //ハッシュ化...平文パスワードを渡すとハッシュ化パスワード、使用されたソルトが返る
        public (string hashedPassword, byte[] salt) HashPassword(string rawPassword)
        {
            byte[] salt = GetSalt();
            string hashed = HashPassword(rawPassword, salt);
            return (hashed, salt);
        }

        public bool VerifyPassword(string hashedPassword, string rawPasswrod, byte[] salt) =>
            hashedPassword == HashPassword(rawPasswrod, salt);
        private string HashPassword(string rawPassword, byte[] salt) =>
            Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: rawPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
        private byte[] GetSalt() {
            using (var gen = RandomNumberGenerator.Create())
            {
                var salt = new byte[128 / 8];
                gen.GetBytes(salt);
                return salt;
            }
        }
    }
    
    //入力チェッククラス
    static class InputCheck
    {
        //エラーフラグ
        static public bool isError { get; set; } = false;
        //エラー項目フォーカスセット
        static public bool onSetFocus { get; set; } = true;
        //エラー項目カラーセット
        static public bool onSetColor { get; set; } = true;
        //エラー項目にセットする背景色
        static private Color ErrorbackColor = Color.MistyRose;

        //文字区分
        public enum StrKind
        { 
            mix, //全角半角混在
            full, //全角のみ
            half //半角のみ
        };

        //文字列の入力チェック
        public static bool isString(ErrorProvider ep,
                            string itemName,
                            TextBox c,
                            bool required,
                            StrKind strKind = StrKind.mix)
        {
            backColorClear(c);
            string msg = "";

            if (String.IsNullOrEmpty(c.Text))
            {
                msg = $"{itemName}は必須項目です";
                errorSet(ep, c, msg);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //半角英数字チェック
        public static bool IsOnlyAlphanumeri(ErrorProvider ep,
                            string itemName,
                            TextBox c,
                            bool required,
                            StrKind strKind = StrKind.half)
        {
            backColorClear(c);
            string msg = "";
            var　result = Regex.IsMatch(c.Text, @"^[0-9a-zA-Z]+$");

            if (result == true && String.IsNullOrEmpty(c.Text) == false)
            {
                return true;
            }

            if (result == false && String.IsNullOrEmpty(c.Text) == true)
            {
                msg = $"{itemName}は必須項目です";
                errorSet(ep, c, msg);

                return false;
            }
            else
            {
                msg = $"{itemName}は半角英数字で入力してください";
                errorSet(ep, c, msg);

                return false;
            }
        }

        //エラークリア処理
        public static void errorClear(ErrorProvider ep)
        {
            isError = false;
            ep.Clear();
        }
        //エラーメッセージの設定
        public static void errorSet(ErrorProvider ep, Control c, string msg)
        { 
            ep.SetError(c, msg);

            if (onSetFocus && isError == false)
            {
                c.Focus();    
            }

            if (onSetColor)
            {
                c.BackColor = ErrorbackColor;
            }

            isError = true;

        }
        //エラークリア処理
        public static void backColorClear(Control c)
        {
            if (onSetColor)
            {
                c.BackColor = Color.Empty;
            }
        }
    }

}