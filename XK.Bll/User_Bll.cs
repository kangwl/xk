using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XK.Dal;
using XK.IDal;

namespace XK.Bll
{
    public class User_Bll {


        private static readonly IDal.IUser userDal = new User_Dal();

        public static bool CreateTable() {
            return userDal.CreateTable();
        }

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="success">认证成功</param>
        /// <returns></returns>
        public static DataTable CheckLogin(string userID, string password,out bool success) {

            string where = string.Format("[UserID]='{0}' and [UserPassword]='{1}'", userID, password);
            DataTable dt = ExistModel(where, out success);
            return dt;
        }

        public static DataTable GeTable() {
            return userDal.GetTable();
        }

        public static bool Insert(Dictionary<string, dynamic> dicFileVals) {
            return userDal.Insert(dicFileVals);
        }

        public static List<int> InsertBatch(List<Dictionary<string,dynamic>> listDic) {
            return userDal.InsertBatch(listDic);
        }

        public static int GetRecordCount() {
           return userDal.GetRecordCount("1=1");
        }

        public static int GetRecordCount(string where) {
            return userDal.GetRecordCount(where);
        }

        public static DataTable GetDataTable(string @where) {
            throw new NotImplementedException();
        }

        public static DataTable GetDataTable(string @where, int pageSize, int pageIndex, string order) {
            return userDal.GetDataTable(where, pageSize, pageIndex, order);
        }

        public static bool Delete(string @where) {
            throw new NotImplementedException();
        }

        public static DataTable GetOne(string @where) {
            throw new NotImplementedException();
        }

        public static bool Exist(string @where) {
            throw new NotImplementedException();
        }

        public static DataTable ExistModel(string @where,out bool exist) {
            return userDal.ExistModel(where, out exist);
        }
    }
}
