using System.Collections.Generic;
using XK.Dal._Helper.UtilBase;

namespace XK.Dal._Helper {
    public class DeleteOP {
        public static string PrimaryKey = "ID";
        /// <summary>
        /// 通过主键删除一条记录
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="ID">主键ID</param>
        /// <returns>成功 true/失败 false</returns>
        public static bool DeleteByID(string table,int ID) {
            string delSql = string.Format("delete from [{0}] where {2}={1}", table, ID, PrimaryKey);
            return DbHelperSQL.ExecuteSql(delSql) > 0;
        }
        /// <summary>
        /// 通过主键ID列表批量删除
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="IDList">主键ID列表</param>
        /// <returns>成功 true/失败 false</returns>
        public static bool DeleteByIDs(string table,List<int> IDList) {
            string ids = string.Join(",", IDList);
            string delSql = string.Format("delete from [{0}] where {2} in ({1})", table, ids, PrimaryKey);
            return DbHelperSQL.ExecuteSql(delSql) > 0;
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="where">删除条件</param>
        /// <returns>成功 true/失败 false</returns>
        public static bool DeleteWhere(string table,string where) {
            string delSql = string.Format("delete from [{0}] where {1}", table, where);
            return DbHelperSQL.ExecuteSql(delSql) > 0;
        }




    }
}
