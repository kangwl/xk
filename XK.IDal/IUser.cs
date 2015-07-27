using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XK.DBUtil.Helper;

namespace XK.IDal
{
    public interface IUser<in TModel> {
        string TableName { get;  } 
        bool Insert(TModel t);
        bool Update(List<Common.help.WhereItem> whereList, Dictionary<string, dynamic> dicKV);
        bool Delete(List<Common.help.WhereItem> wheres);
        int GetRecordCount(List<Common.help.WhereItem> whereList);
        DataTable GetDataTable(List<Common.help.WhereItem> whereList, int top);
        DataTable GetDataTable(List<Common.help.WhereItem> whereList, int top, string orderBy);
        DataTable GetDataTable(List<Common.help.WhereItem> whereList, int pageSize, int pageIndex, string order);
        Model.User_Model GetModel(int ID);

        List<Model.User_Model> GetModels(List<Common.help.WhereItem> whereList, int pageSize, int pageIndex,
            string orderBy);

        List<Model.User_Model> GetModels(List<Common.help.WhereItem> whereList, int top, string orderBy);
        Model.User_Model ReadModel(SqlDataReader reader);
    }
}
