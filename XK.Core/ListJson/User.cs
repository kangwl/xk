using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using XK.Common.help;
using XK.Core.act;

namespace XK.Core.ListJson {
    public class User : ListJsonBase {
        public User(HttpRequest request) : base(request) {
        }


        public override string GetDataJson() {
            List<Common.help.Where> wheres = new List<Where>();
            wheres.Add(new Where("1", "=", 1));

            Total = Bll.User_Bll.GetRecordCount(wheres);
            DataTable = Bll.User_Bll.GetDataTable(wheres, Limit, Offset / Limit + 1, string.Format("{0} {1}", Sort, Order));
            return base.GetDataJson(Total, DataTable);
        }
    }
}
