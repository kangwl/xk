using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XK.Model {
    public class User_Model {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public string UserPassword { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public DateTime AddDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int UserType { get; set; }
        public int UserType2 { get; set; }
    }
}
