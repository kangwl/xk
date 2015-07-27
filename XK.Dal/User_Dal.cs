using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XK.Common.help;
using XK.DBUtil; 
using XK.DBUtil.Helper;
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
 

        public int GetRecordCount(List<Common.help.Where> whereList) {
            WhereHelper whereHelper = new WhereHelper();
            foreach (Where where in whereList) {
                whereHelper.AddWhere(where.Field, where.Sign, where.Value);
            }
            SelectHelper selectHelper = new SelectHelper(TableName, "count(1)", whereHelper);
            int recordCount = DBExcute.GetRecordCount(selectHelper);
            return recordCount;
        }

        public DataTable GetDataTable(List<Common.help.Where> whereList,int top) {
            WhereHelper whereHelper = new WhereHelper(whereList);
            SelectHelper selectHelper = new SelectHelper(TableName, top, "*", whereHelper);
            return DBExcute.GetDataTable(selectHelper);
        }

        public DataTable GetDataTable(List<Common.help.Where> whereList, int pageSize, int pageIndex, string order) {
            WhereHelper whereHelper = new WhereHelper();
            foreach (Where where in whereList) {
                whereHelper.AddWhere(where.Field, where.Sign, where.Value);
            }
            SelectHelper selectHelper = new SelectHelper(TableName, "*", whereHelper, pageIndex, pageSize, order);
            return DBExcute.GetDataTable(selectHelper);
        }

    }
}
