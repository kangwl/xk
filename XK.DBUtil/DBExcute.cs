using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XK.DBUtil.Helper;

namespace XK.DBUtil {
    /// <summary>
    /// 执行DB操作
    /// DBDefault 为web.config的连接字符串的配置节点名
    /// </summary>
    public class DBExcute {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected static string connStr =
            System.Configuration.ConfigurationManager.ConnectionStrings["DBDefault"].ConnectionString;

        public static bool Insert(InsertHelper insertHelper, string connectionStr="") {
            using (SqlCommand sqlCmd = CreateSqlCmd(insertHelper.Sql, insertHelper.SqlParameters, connectionStr)) {
                int ret = sqlCmd.ExecuteNonQuery();
                return ret > 0;
            } 
        }


        /// <summary>
        /// 事务批量插入 
        /// 返回：已成功插入数据的当前索引
        /// 如果参数insertHelpers的数目和返回值相等，说明全部添加成功
        /// </summary>
        /// <param name="insertHelpers"></param>
        /// <param name="connectionStr">如果不写默认webconfig配置的节点为DBDefault的连接字符串</param>
        /// <returns>插入成功的个数</returns>
        public static int InsertTran(List<InsertHelper> insertHelpers, string connectionStr="") {
            //记录已成功插入数据的当前索引
            int successCount = 0;
            using (SqlCommand command = CreateSqlCmd(connectionStr)) {
                try {
                    //deal
                    command.Transaction = command.Connection.BeginTransaction();
                    foreach (InsertHelper insertHelper in insertHelpers) {
                        command.CommandText = insertHelper.Sql;
                        command.Parameters.Clear();
                        command.Parameters.AddRange(insertHelper.SqlParameters);
                        bool success = command.ExecuteNonQuery() > 0;
                        if (!success) {
                            throw new Exception("数据插入失败");
                        }
                        successCount++;//记录当前成功的索引
                    }
                    command.Transaction.Commit();
                }
                catch (Exception) {
                    command.Transaction.Rollback();
                }
                return successCount;
            }
        }

        public static bool Update(UpdateHelper updateHelper, string connectionStr = "") {
            using (SqlCommand sqlCmd = CreateSqlCmd(updateHelper.Sql, updateHelper.SqlParameters,connectionStr)) {
                int ret = sqlCmd.ExecuteNonQuery();
                return ret > 0;
            }
        }

        /// <summary>
        /// 批量事务更新
        /// 返回：已成功处理的个数
        /// 如果参数 updateHelpers 的数目和返回值相等，说明全部更新成功
        /// </summary>
        /// <param name="updateHelpers">List UpdateHelper</param>
        /// <param name="showErrorIfNotExist">
        /// 默认false，即如果 ExecuteNonQuery 返回值是0（有可能待更新的目标数据不存在）不计入失败
        /// 如果true ，即如果 ExecuteNonQuery 返回值是0则计入失败
        /// </param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public static int UpdateTran(List<UpdateHelper> updateHelpers, bool showErrorIfNotExist = false, string connectionStr = "") {
            //记录已成功更新数据的当前索引
            int successCount = 0;
            using (SqlCommand command = CreateSqlCmd(connectionStr)) {
                try {
                    //deal
                    command.Transaction = command.Connection.BeginTransaction();
                    foreach (UpdateHelper updateHelper in updateHelpers) {
                        command.CommandText = updateHelper.Sql;
                        command.Parameters.Clear();
                        command.Parameters.AddRange(updateHelper.SqlParameters);
                        bool success = command.ExecuteNonQuery() > 0;
                        if (showErrorIfNotExist) {
                            if (!success) {
                                throw new Exception("待更新的数据不存在");
                            }
                        }
                        successCount++; //记录当前成功的索引
                    }
                    command.Transaction.Commit();
                }
                catch (Exception) {
                    command.Transaction.Rollback();
                }
                return successCount;
            }
        }


        public static bool Delete(DeleteHelper deleteHelper, string connectionStr = "") {
            using (SqlCommand sqlCmd = CreateSqlCmd(deleteHelper.Sql, deleteHelper.SqlParameters,connectionStr)) {
                int ret = sqlCmd.ExecuteNonQuery();
                return ret > 0;
            }
        }

        /// <summary>
        /// 批量事务删除
        /// 返回：已成功处理的个数
        /// 如果参数 deleteHelpers 和返回值个数相等，说明全部删除成功
        /// </summary>
        /// <param name="deleteHelpers"></param>
        /// <param name="showErrorIfNotExist">
        /// 默认false，即如果ExecuteNonQuery返回值是0（有可能待删除的目标数据不存在）不计入失败
        /// 如果true ，即如果ExecuteNonQuery返回值是0则计入失败
        /// </param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public static int DeleteTran(List<DeleteHelper> deleteHelpers, bool showErrorIfNotExist = false, string connectionStr = "") {
            //记录已成功删除数据的当前索引
            int successCount = 0;
            using (SqlCommand command = CreateSqlCmd(connectionStr)) {
                try {
                    //deal
                    command.Transaction = command.Connection.BeginTransaction();
                    foreach (DeleteHelper deleteHelper in deleteHelpers) {
                        command.CommandText = deleteHelper.Sql;
                        command.Parameters.Clear();
                        command.Parameters.AddRange(deleteHelper.SqlParameters);
                        bool success = command.ExecuteNonQuery() > 0;
                        if (showErrorIfNotExist) {
                            if (!success) {
                                throw new Exception("待删除的数据不存在");
                            }
                        }
                        successCount++; //记录当前成功的索引
                    }
                    command.Transaction.Commit();
                }
                catch (Exception) {
                    //回滚操作
                    command.Transaction.Rollback();
                }
                return successCount;
            }
        }

        public static DataTable GetDataTable(SelectHelper selectHelper, string connectionStr = "") {
            DataTable dt = new DataTable();
            using (SqlCommand sqlCmd = CreateSqlCmd(selectHelper.Sql, selectHelper.SqlParameters,connectionStr)) {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd)) {
                    dataAdapter.Fill(dt);
                }
                return dt;
            }
        }

        public static SqlDataReader GetDataReader(SelectHelper selectHelper, string connectionStr = "") {
            SqlDataReader dataReader;
            using (SqlCommand sqlCmd = CreateSqlCmd(selectHelper.Sql, selectHelper.SqlParameters,connectionStr)) {
                dataReader = sqlCmd.ExecuteReader();
            }
            return dataReader;
        }

        public static int GetRecordCount(SelectHelper selectHelper, string connectionStr = "") {
            using (SqlCommand sqlCmd = CreateSqlCmd(selectHelper.Sql, selectHelper.SqlParameters,connectionStr)) {
                object obj = sqlCmd.ExecuteScalar();
                return obj.ToInt();
            }
        }

        #region 基础方法

        private static SqlCommand CreateSqlCmd(string sqlText, SqlParameter[] sqlParameters, string connectionStr = "") {
            SqlCommand sqlCmd = CreateSqlCmd(connectionStr);
            sqlCmd.CommandText = sqlText;
            sqlCmd.Parameters.AddRange(sqlParameters);
            return sqlCmd;
        }

        private static SqlCommand CreateSqlCmd(string connectionStr = "") {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = new SqlConnection(string.IsNullOrWhiteSpace(connectionStr) ? connStr : connectionStr);
            sqlCmd.Connection.Open();

            return sqlCmd;
        }

        #endregion


    }
}
