using System.Configuration;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Data.SqlClient;

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

    public class CommonUtil
    {
        public static string GetConnectionString() { 　//接続文字列を返すメソッド
            return ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;
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

        //コンポボックスの入力チェック
        public static bool isCombobox(ErrorProvider ep,
                            string itemName,
                            ComboBox c,
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

        //半角英数字を正規表現でチェック
        public static bool IsOnlyAlphanumeri(ErrorProvider ep,
                            string itemName,
                            TextBox c,
                            bool required,
                            StrKind strKind = StrKind.half)
        {
            backColorClear(c);
            string msg = "";
            var　result = Regex.IsMatch(c.Text, @"^[0-9a-zA-Z]+$");

            if (result == true)
            {
                return true;
            }

            else
            {
                msg = $"{itemName}は半角英数字で入力してください";
                errorSet(ep, c, msg);
                return false;
            }
        }

        //半角の必須チェック
        public static bool RequiredHalfSize(ErrorProvider ep,
                           string itemName,
                           TextBox c,
                           bool required,
                           StrKind strKind = StrKind.half)
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

        //数字を正規表現でチェック
        public static bool NumbersCheck(ErrorProvider ep,
                           string itemName,
                           TextBox c,
                           bool required,
                           StrKind strKind = StrKind.half)
        {
            backColorClear(c);
            string msg = "";
            var result = Regex.IsMatch(c.Text, @"^([1-9][0-9]{0,1}|0)(\.[0-9]{1,2})?$");

            if (result == true)
            {
                return true;
            }
            else
            {
                msg = $"{itemName}は0.00〜99.99の範囲で入力してください";
                errorSet(ep, c, msg);
                return false;
            }
        }
        //プロジェクトIDの重複チェック
        public static bool ProjectsidCheck(ErrorProvider ep,
                           string itemName,
                           TextBox c,
                           bool required,
                           StrKind strKind = StrKind.half)

        {
            backColorClear(c);
            string msg = "";
            var connectionString1 = CommonUtil.GetConnectionString();
            string sql = @"SELECT id FROM Projects Where id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString1))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("id",c.Text));
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    msg = $"{itemName}が重複しています";
                    errorSet(ep, c, msg);
                    return false;
                }
                else
                {
                    return true;
                }
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