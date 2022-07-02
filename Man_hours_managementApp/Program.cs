
using System.Configuration;
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

    public static class Utilites
    {
        public static string GetConnectionString()　//接続文字列を返すメソッド
        {
            return ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;
        }
    }

}