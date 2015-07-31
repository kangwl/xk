using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XK.MongoDBUtil.Test {
    public class Mongodb {
        public bool DeleteUser() {
            MongoDBHelper<UserModel> dbHelper = new MongoDBHelper<UserModel>("User");
            
            Task<bool> a = dbHelper.DbExcute.Delete(u => u.Age > 12);
            return a.Result;
        }
    }

    public class UserModel {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
