using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Man_hours_managementApp
{
    public class ResultInfo
    {
        //可否を格納するフィールド変数
        private bool _result = true;

        //エラーメッセージを格納するフィールド変数
        private string _errorMessage = string.Empty;

        //可否を格納するプロパティ、初期値はtrue
        public bool Result
        {
            get
            { 
                return _result;
            }

            set
            { 
                _result = value;
            }
        }

        //エラーメッセージを格納するプロパティ、初期値は空文字
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
            }
        }
    }
}
