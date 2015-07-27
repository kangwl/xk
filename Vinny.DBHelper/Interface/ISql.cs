using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Vinny.DBHelper.Interface {
    public interface ISql {
        bool Add(Dictionary<string, dynamic> dicKV);
        bool Update(Dictionary<string, dynamic> dicKV, Core.WhereHelper whereHelper);
        bool Delete(Core.WhereHelper whereHelper);
        DataRow GetOne(Core.WhereHelper whereHelper);
        DataTable GetDataTable(Core.WhereHelper whereHelper);

    }
}
