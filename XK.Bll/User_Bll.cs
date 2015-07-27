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
            
            List<Common.help.Where> wheres = new List<Where>();
            wheres.Add(new Where("UserID", "=", userID));
            wheres.Add(new Where("UserPassword", "=", password));
            
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


        public static int GetRecordCount(List<Common.help.Where> whereList) {
            return userDal.GetRecordCount(whereList);
        }


        public static DataTable GetDataTable(List<Common.help.Where> whereList, int pageSize, int pageIndex, string order) {

            return userDal.GetDataTable(whereList, pageSize, pageIndex, order);
        }

 

        //public static DataTable ExistModel(string @where,out bool exist) {
        //    return userDal.ExistModel(where, out exist);
        //}
    }
}
