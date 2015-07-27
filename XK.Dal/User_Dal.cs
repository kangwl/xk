using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XK.Common.help;
using XK.DBUtil; 
using XK.DBUtil.Helper;
using XK.DBUtil.Tool;
using XK.Model;

namespace XK.Dal {
    public class User_Dal:IDal.IUser<Model.User_Model> {

        public string TableName {
            get { return "[User]"; }
        }
         
        public bool Insert(Model.User_Model userModel) {
               
            InsertHelper insertHelper = new InsertHelper(TableName);
            insertHelper.AddParam("Name", userModel.Name);
            insertHelper.AddParam("AddDateTime", userModel.AddDateTime);
            insertHelper.AddParam("Age", userModel.Age);
            insertHelper.AddParam("Email", userModel.Email);
            insertHelper.AddParam("MobilePhone", userModel.MobilePhone);
            insertHelper.AddParam("Name", userModel.Name);
            insertHelper.AddParam("Sex", userModel.Sex);
            insertHelper.AddParam("UpdateDateTime", userModel.UpdateDateTime);
            insertHelper.AddParam("UserID", userModel.UserID);
            insertHelper.AddParam("UserPassword", userModel.UserPassword);
            insertHelper.AddParam("UserType", userModel.UserType);

            return DBExcute.Insert(insertHelper); 
        }

        public bool Update(List<Common.help.WhereItem> whereList, Dictionary<string, dynamic> dicKV) {

            WhereHelper whereHelper = new WhereHelper(whereList);

            UpdateHelper updateHelper = new UpdateHelper(TableName, whereHelper, dicKV);
            //updateHelper.AddUpdateItem(dicKV);

            return DBExcute.Update(updateHelper);
        }

        public bool Delete(List<Common.help.WhereItem> wheres) {
            WhereHelper whereHelper = new WhereHelper(wheres);
            DeleteHelper deleteHelper = new DeleteHelper(TableName, whereHelper);
            return DBExcute.Delete(deleteHelper);
        }

        public int GetRecordCount(List<Common.help.WhereItem> whereList) {
            WhereHelper whereHelper = new WhereHelper();
            foreach (WhereItem where in whereList) {
                whereHelper.AddWhere(where.Field, where.Sign, where.Value);
            }
            SelectHelper selectHelper = new SelectHelper(TableName, "count(1)", whereHelper);
            int recordCount = DBExcute.GetRecordCount(selectHelper);
            return recordCount;
        }

        public DataTable GetDataTable(List<Common.help.WhereItem> whereList,int top) {
            WhereHelper whereHelper = new WhereHelper(whereList);
            SelectHelper selectHelper = new SelectHelper(TableName, top, "*", whereHelper);
            return DBExcute.GetDataTable(selectHelper);
        }

        public DataTable GetDataTable(List<Common.help.WhereItem> whereList, int top,string orderBy) {
            WhereHelper whereHelper = new WhereHelper(whereList);
            SelectHelper selectHelper = new SelectHelper(TableName, top, "*", whereHelper,orderBy);
            return DBExcute.GetDataTable(selectHelper);
        }

        public DataTable GetDataTable(List<Common.help.WhereItem> whereList, int pageSize, int pageIndex, string orderBy) {
            WhereHelper whereHelper = new WhereHelper(whereList);

            SelectHelper selectHelper = new SelectHelper(TableName, "*", whereHelper, pageIndex, pageSize, orderBy);
            return DBExcute.GetDataTable(selectHelper);
        }

        public Model.User_Model GetModel(int ID) {
            WhereHelper whereHelper = new WhereHelper();
            whereHelper.AddWhere("ID", "=", ID);
            SelectHelper selectHelper = new SelectHelper(TableName, "*", whereHelper);
            Model.User_Model userModel = new User_Model();
            using (SqlDataReader reader = DBExcute.GetDataReader(selectHelper)) {
                if (reader.Read()) {
                    userModel = ReadModel(reader);
                }
            }
            return userModel;
        }

        public List<Model.User_Model> GetModels(List<Common.help.WhereItem> whereList, int pageSize, int pageIndex, string orderBy) {
            List<Model.User_Model> userModels = new List<User_Model>();
            WhereHelper whereHelper = new WhereHelper(whereList);
            SelectHelper selectHelper = new SelectHelper(TableName, "*", whereHelper, pageIndex, pageSize, orderBy);
            using (SqlDataReader reader = DBExcute.GetDataReader(selectHelper)) {
                while (reader.Read()) {
                    Model.User_Model userModel = ReadModel(reader);
                    userModels.Add(userModel);
                }
            }
            return userModels;
        }

        public List<Model.User_Model> GetModels(List<Common.help.WhereItem> whereList, int top, string orderBy) {
            List<Model.User_Model> userModels = new List<User_Model>();
            WhereHelper whereHelper = new WhereHelper(whereList);
            SelectHelper selectHelper = new SelectHelper(TableName, "*", whereHelper);
            using (SqlDataReader reader = DBExcute.GetDataReader(selectHelper)) {
                while (reader.Read()) {
                    Model.User_Model userModel = ReadModel(reader);
                    userModels.Add(userModel);
                }
            }
            return userModels;
        }

        public Model.User_Model ReadModel(SqlDataReader reader) {
            Model.User_Model userModel = new User_Model();
            userModel.AddDateTime = reader.GetDateTimeEXT("AddDateTime");
            userModel.Age = reader.GetIntEXT("Age");
            userModel.Email = reader.GetStringEXT("Email");
            userModel.ID = reader.GetIntEXT("ID");
            userModel.MobilePhone = reader.GetStringEXT("MobilePhone");
            userModel.Name = reader.GetStringEXT("Name");
            userModel.Sex = reader.GetIntEXT("Sex");
            userModel.UpdateDateTime = reader.GetDateTimeEXT("UpdateDateTime");
            userModel.UserID = reader.GetStringEXT("UserID");
            userModel.UserPassword = reader.GetStringEXT("UserPassword");
            userModel.UserType = reader.GetIntEXT("UserType");

            return userModel;
        }



    }
}
