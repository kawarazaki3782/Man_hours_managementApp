using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Man_hours_managementApp
{
    public class UserSession
    {
        private static UserSession _myinstance = new UserSession();
       
        public int id { get; set; }
        public string name { get; set; }
        public string affiliation { get; set; }
        public string login_id { get; set; }
        public string password { get; set; }
        public bool admin { get; set; }

        public static UserSession GetInstatnce(){ 
            return _myinstance;
        }
    }
}
