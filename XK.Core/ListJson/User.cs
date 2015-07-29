using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using XK.Common.help;
using XK.Core.act;

namespace XK.Core.ListJson {
    public class User : ListJsonBase<List<XK.Model.User_Model>> {
        public User(HttpRequest request) : base(request) {

        }


        public override string GetDataJson() {
            List<Common.help.WhereItem> wheres = new List<WhereItem>();
            wheres.Add(new WhereItem("1", "=", 1));

            Total = Bll.User_Bll.GetRecordCount(wheres);

            //Data = Bll.User_Bll.GetDataTable(wheres, Limit, Offset / Limit + 1, string.Format("{0} {1}", Sort, Order));
            base.Data = Bll.User_Bll.GetModels(wheres, Offset/Limit + 1, Limit, string.Format("{0} {1}", Sort, Order));
            return base.GetDataJson(Total, Data);
        }
    }
}
