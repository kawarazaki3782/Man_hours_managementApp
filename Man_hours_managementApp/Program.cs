
using System.Configuration;


namespace Man_hours_managementApp
{

    internal static class Program�@//�����v���W�F�N�g�Ȃ�A�N�Z�X��
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread] //Form�����s����Main�ł͕K��[STAThread]�Ŏw��
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();�@//�A�v���P�[�V�����̍\���v���p�e�B��������
            Application.Run(new LoginForm());
        }   
    }

    public class CommonUtil{
        public static string GetConnectionString() { �@//�ڑ��������Ԃ����\�b�h
            return ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;
        }
    }

}