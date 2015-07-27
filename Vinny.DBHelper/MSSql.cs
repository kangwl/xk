using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Vinny.DBHelper.Core;

namespace Vinny.DBHelper {
    public  class MSSql:Interface.ISql {
        public bool Add(Dictionary<string, dynamic> dicKV) {
            throw new NotImplementedException();
        }

        public bool Delete(WhereHelper whereHelper) {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable(WhereHelper whereHelper) {
            throw new NotImplementedException();
        }

        public DataRow GetOne(WhereHelper whereHelper) {
            throw new NotImplementedException();
        }

        public bool Update(Dictionary<string, dynamic> dicKV, WhereHelper whereHelper) {
            throw new NotImplementedException();
        }
    }
}
