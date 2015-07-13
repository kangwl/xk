using System.Collections.Generic;
using System.Data;

namespace XK.IDal
{
    public interface IUser {
        string TableName { get;  }
        DataTable GetTable();
        bool Insert(Dictionary<string, dynamic> dicFileVals);
        List<int> InsertBatch(List<Dictionary<string, dynamic>> listDic);
        int GetRecordCount(string where);
        DataTable GetDataTable(string where);
        DataTable GetDataTable(string where, int pageSize, int pageIndex, string order);
        bool Delete(string where);
        DataTable GetOne(string where);
        bool Exist(string where);
        DataTable ExistModel(string where,out bool exist);
        bool CreateTable();
    }
}
