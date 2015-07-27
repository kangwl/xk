using System.Collections.Generic;
using System.Data;
using XK.Common.help;
using XK.Dal;

namespace XK.Bll
{
    public class User_Bll {


        private static readonly IDal.IUser<Model.User_Model> userDal = new User_Dal();
 
        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="success">认证成功</param>
        /// <returns></returns>
        public static DataTable CheckLogin(string userID, string password,out bool success) {
            
            List<Common.help.WhereItem> wheres = new List<WhereItem>();
            wheres.Add(new WhereItem("UserID", "=", userID));
            wheres.Add(new WhereItem("UserPassword", "=", password));
            
            success = false;
            DataTable dt = userDal.GetDataTable(wheres, 1);
            if (dt.Rows.Count > 0) {
                success = true;
            } 
            return dt;
        }
 

        public static bool Insert(Model.User_Model userModel) {
            return userDal.Insert(userModel);
        }

        public static bool Update(List<Common.help.WhereItem> whereList,Dictionary<string,dynamic> dicKV) {
            return userDal.Update(whereList, dicKV);
        }

        public static bool Delete(List<Common.help.WhereItem> whereItems) {
            return userDal.Delete(whereItems);
        }

        public static int GetRecordCount(List<Common.help.WhereItem> whereList) {
            return userDal.GetRecordCount(whereList);
        }


        public static DataTable GetDataTable(List<Common.help.WhereItem> whereList, int pageSize, int pageIndex, string order) {

            return userDal.GetDataTable(whereList, pageSize, pageIndex, order);
        }

        public static DataTable GetDataTable(List<Common.help.WhereItem> whereList, int top) {
            return userDal.GetDataTable(whereList, top);
        }

        public static Model.User_Model GetModel(int ID) {
            return userDal.GetModel(ID);
        }

        public static List<Model.User_Model> GetModels(List<Common.help.WhereItem> whereList, int top,string orderBy) {
            return userDal.GetModels(whereList, top, orderBy);
        }

        public static List<Model.User_Model> GetModels(List<Common.help.WhereItem> whereList,int pageIndex,int pageSize,string orderBy) {
            return userDal.GetModels(whereList, pageSize, pageIndex, orderBy);
        } 


        //public static DataTable ExistModel(string @where,out bool exist) {
        //    return userDal.ExistModel(where, out exist);
        //}
    }
}
