
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

    //���̓`�F�b�N�N���X
    static class InputCheck
    {
        //�G���[�t���O
        static public bool isError { get; set; } = false;
        //�G���[���ڃt�H�[�J�X�Z�b�g
        static public bool onSetFocus { get; set; } = true;
        //�G���[���ڃJ���[�Z�b�g
        static public bool onSetColor { get; set; } = true;
        //�G���[���ڂɃZ�b�g����w�i�F
        static private Color ErrorbackColor = Color.MistyRose;

        //�����敪
        public enum StrKind
        { 
            mix, //�S�p���p����
            full, //�S�p�̂�
            half //���p�̂�
        };

        //������̓��̓`�F�b�N
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
                if(required)
                {
                    msg = $"[itemName]�͕K�{���ڂł��B";
                    errorSet(ep, c, msg);

                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        //�G���[�N���A����
        public static void errorClear(ErrorProvider ep)
        {
            isError = false;
            ep.Clear();
        }
        //�G���[���b�Z�[�W�̐ݒ�
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
        //�G���[�N���A����
        public static void backColorClear(Control c)
        {
            if (onSetColor)
            {
                c.BackColor = Color.Empty;
            }
        }
    }

}