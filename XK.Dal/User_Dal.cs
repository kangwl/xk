using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XK.Dal._Helper;
using XK.Dal._Helper;
using XK.Dal._Helper.UtilBase;

namespace XK.Dal {
    public class User_Dal : IDal.IUser {

        public string TableName { get { return "User"; } }

        public bool CreateTable() {
            string userTable = @"CREATE TABLE [User](
	                            ID INT PRIMARY KEY IDENTITY,
	                            Name NVARCHAR(10),
	                            UserID VARCHAR(50),
	                            UserPassword VARCHAR(30),
	                            Age INT,
	                            Sex INT,
	                            MobilePhone VARCHAR(20),
	                            Email varchar(60),
	                            AddDateTime DATETIME,
	                            UpdateDateTime DATETIME,
	                            UserType INT
                            )";

            return DbHelperSQL.ExecuteSql(userTable) > 0;
        }

        public void CheckLogin(string userID, string password) {
          
        }

        public DataTable GetTable() {
            MyDBHelper.SelectDB.SelectDBParams pParams = new MyDBHelper.SelectDB.SelectDBParams();
            pParams.TableName = TableName;
            pParams.Fields = "*";
            pParams.OrderBy = "id asc";
            pParams.DicWhere = new Dictionary<string, dynamic>() {{"id", "12"}};
            MyDBHelper.SelectDB selectDb = new MyDBHelper.SelectDB(pParams);
            DataTable dt = selectDb.GetTable();
            return dt;
        }

        public bool Insert(Dictionary<string, dynamic> dicFileVals) {
            return InsertOP.Insert(TableName, dicFileVals);
        }

        public List<int> InsertBatch(List<Dictionary<string,dynamic>> listDic) {
            return InsertOP.InsertBatch(TableName, listDic);
        }

        public int GetRecordCount(string where) {
            return SelectOP.GetTotalCount(TableName, where);
        }

        public DataTable GetDataTable(string @where) {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable(string @where, int pageSize, int pageIndex, string order) {
            return SelectOP.QueryTablePager(TableName, where, pageIndex, pageSize, order);
        }

        public bool Delete(string @where) {
            throw new NotImplementedException();
        }

        public DataTable GetOne(string @where) {
            throw new NotImplementedException();
        }

        public bool Exist(string @where) {
            throw new NotImplementedException();
        }

        public DataTable ExistModel(string @where,out bool exist) {
            DataTable dt = SelectOP.QueryTable(TableName, 1, "*", where);

            exist = (dt.Rows.Count > 0);

            return dt;
        }
    }
}
