using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using XK.Core.act;

namespace XK.Core.ListJson {
    public class User : ListJsonBase {
        public User(HttpRequest request) : base(request) {
        }


        public override string GetDataJson() {
            Total = Bll.User_Bll.GetRecordCount();
            DataTable = Bll.User_Bll.GetDataTable("1=1", Limit, Offset / Limit + 1, string.Format("{0} {1}", Sort, Order));
            return base.GetDataJson(Total, DataTable);
        }
    }
}
