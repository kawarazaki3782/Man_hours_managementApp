using System.Configuration;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Data.SqlClient;

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

    public class CommonUtil
    {
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
                msg = $"{itemName}�͕K�{���ڂł�";
                errorSet(ep, c, msg);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //�R���|�{�b�N�X�̓��̓`�F�b�N
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
                msg = $"{itemName}�͕K�{���ڂł�";
                errorSet(ep, c, msg);
                return true;
            }
            else
            {
                return false;
            }
        }

       

        //���p�p�����𐳋K�\���Ń`�F�b�N
        public static bool IsOnlyAlphanumeri(ErrorProvider ep,
                            string itemName,
                            TextBox c,
                            bool required,
                            StrKind strKind = StrKind.half)
        {
            backColorClear(c);
            string msg = "";
            var�@result = Regex.IsMatch(c.Text, @"^[0-9a-zA-Z]+$");

            if (result == true)
            {
                return true;
            }

            else
            {
                msg = $"{itemName}�͔��p�p�����œ��͂��Ă�������";
                errorSet(ep, c, msg);
                return false;
            }
        }

        //���p�̕K�{�`�F�b�N
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
                msg = $"{itemName}�͕K�{���ڂł�";
                errorSet(ep, c, msg);
                return true;
            }
            else
            {
                return false;
            }
        }

        //�����𐳋K�\���Ń`�F�b�N
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
                msg = $"{itemName}��0.00�`99.99�͈̔͂œ��͂��Ă�������";
                errorSet(ep, c, msg);
                return false;
            }
        }
        //�v���W�F�N�gID�̏d���`�F�b�N
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
                    msg = $"{itemName}���d�����Ă��܂�";
                    errorSet(ep, c, msg);
                    return false;
                }
                else
                {
                    return true;
                }
            }
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