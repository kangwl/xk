using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XK.DBUtil.Helper;

namespace XK.IDal
{
    public interface IUser<in TModel> {
        string TableName { get;  } 
        bool Insert(TModel t);
        /// <summary>
        /// 返回插入成功的个数
        /// </summary>
        /// <param name="userModels"></param>
        /// <returns></returns>
        int InsertTran(List<Model.User_Model> userModels);
        bool Update(Model.User_Model userModel);
        bool Update(List<Common.help.WhereItem> whereList, Dictionary<string, dynamic> dicKV);
        bool UpdatePwd(Model.User_Model userModel);
        bool Delete(List<Common.help.WhereItem> wheres);
        int GetRecordCount(List<Common.help.WhereItem> whereList);
        DataTable GetDataTable(List<Common.help.WhereItem> whereList, int top);
        DataTable GetDataTable(List<Common.help.WhereItem> whereList, int top, string orderBy);
        DataTable GetDataTable(List<Common.help.WhereItem> whereList, int pageSize, int pageIndex, string order);
        Model.User_Model GetModel(int ID);
        Model.User_Model GetModel(List<Common.help.WhereItem> whereItems);
        List<Model.User_Model> GetModels(List<Common.help.WhereItem> whereList, int pageSize, int pageIndex,
            string orderBy);
        List<Model.User_Model> GetModels(List<Common.help.WhereItem> whereList, int top, string orderBy);
        Model.User_Model ReadModel(SqlDataReader reader);
    }
}
