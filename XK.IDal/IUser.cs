using System.Collections.Generic;
using System.Data;
using XK.DBUtil.Helper;

namespace XK.IDal
{
    public interface IUser<in TModel> {
        string TableName { get;  } 
        bool Insert(TModel t); 
        int GetRecordCount(List<Common.help.Where> whereList);
        DataTable GetDataTable(List<Common.help.Where> whereList, int top);
        DataTable GetDataTable(List<Common.help.Where> whereList, int pageSize, int pageIndex, string order);
    }
}
