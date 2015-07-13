using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace XK.Dal._Helper.UtilBase //可以修改成实际项目的命名空间名称 
{

    public class DbHelperSQL
    {
        //数据库连接字符串
        protected static string connStr = ConnectionHelper.ConnectString;


        /// <summary> 
        /// 执行SQL语句，返回影响的记录数 
        /// </summary> 
        /// <param name="SQLString">SQL语句</param> 
        /// <returns>影响的记录数</returns> 
        public static int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection)) {

                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
        }

        /**/
        /// <summary> 
        /// 执行多条SQL语句，实现数据库事务。 
        /// </summary> 
        /// <param name="SQLStringList">多条SQL语句</param>         
        public static bool ExecuteSqlTran(List<string> SQLStringList)
        {
            int tmp = 0;
            using (SqlConnection conn = new SqlConnection(connStr)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand()) {
                    cmd.Connection = conn;
                    using (SqlTransaction tx = conn.BeginTransaction()) {
                        cmd.Transaction = tx;
                        try {
                            foreach (string sql in SQLStringList) {
                                string strsql = sql;
                                if (strsql.Trim().Length > 1) {
                                    cmd.CommandText = strsql;
                                    tmp += cmd.ExecuteNonQuery();
                                }
                            }
                            tx.Commit();
                            if (tmp > 0) {
                                return true;
                            }

                            return false;
                        }
                        catch (SqlException E) {
                            tx.Rollback();
                            throw new Exception(E.Message);
                        }
                    }
                }
            }
        }

        /// <summary> 
        /// 执行带一个存储过程参数的的SQL语句。 
        /// </summary> 
        /// <param name="SQLString">SQL语句</param> 
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param> 
        /// <returns>影响的记录数</returns> 
        public static int ExecuteSql(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection)) {
                    SqlParameter myParameter = new SqlParameter("@content", SqlDbType.NText);
                    myParameter.Value = content;
                    cmd.Parameters.Add(myParameter);

                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;

                }
            }
        }


        /// <summary> 
        /// 执行一条计算查询结果语句，返回查询结果（object）。 
        /// </summary> 
        /// <param name="SQLString">计算查询结果语句</param> 
        /// <returns>查询结果（object）</returns> 
        public static object GetSingle(string SQLString) {
            using (SqlConnection connection = new SqlConnection(connStr)) {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection)) {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Equals(obj, null)) || (Equals(obj, DBNull.Value))) {
                        return null;
                    }

                    return obj;
                }
            }
        }

        /**/
        /// <summary> 
        /// 执行查询语句，返回SqlDataReader 
        /// </summary> 
        /// <param name="strSQL">查询语句</param> 
        /// <returns>SqlDataReader</returns> 
        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(connStr);

            using (SqlCommand cmd = new SqlCommand(strSQL, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return myReader;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
            }
        }


        /**/
        /// <summary> 
        /// 执行查询语句，返回DataSet 
        /// </summary> 
        /// <param name="SQLString">查询语句</param> 
        /// <returns>DataSet</returns> 
        public static DataSet Query(string SQLString)
        {

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                DataSet ds = new DataSet();
                connection.Open();
                using (SqlDataAdapter command = new SqlDataAdapter(SQLString, connection))
                {
                    command.Fill(ds, "ds");
                    return ds;
                }
            }

        }

        public static void ExecuteSqls(Dictionary<string, SqlParameter[]> dicSqlPara) {
            using (SqlConnection conn = new SqlConnection(connStr)) {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction()) {
                    using (SqlCommand cmd = new SqlCommand()) {
                        try {
                            //循环 
                            foreach (KeyValuePair<string, SqlParameter[]> myDE in dicSqlPara) {
                                string cmdText = myDE.Key.ToString(CultureInfo.InvariantCulture);
                                SqlParameter[] cmdParms =  myDE.Value;
                                PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                                int val = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                trans.Commit();
                            }
                        }
                        catch {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }


        /// <summary> 
        /// 执行SQL语句，返回影响的记录数 
        /// </summary> 
        /// <param name="SQLString">SQL语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>影响的记录数</returns> 
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand()) {

                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
        }


        /**/
        /// <summary> 
        /// 执行多条SQL语句，实现数据库事务。 
        /// </summary> 
        /// <param name="SQLStringList">SQL语句的键值（key为sql语句，value是该语句的SqlParameter[]）</param> 
        public static void ExecuteSqlTran(Dictionary<string,dynamic> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand()) {
                        try {

                            //循环 
                            foreach (KeyValuePair<string, dynamic> myDE in SQLStringList) {
                                string cmdText = myDE.Key.ToString();
                                SqlParameter[] cmdParms = (SqlParameter[]) myDE.Value;
                                PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                                int val = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                            trans.Commit();
                        }
                        catch (Exception) {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }


        /// <summary> 
        /// 执行一条计算查询结果语句，返回查询结果（object）。 
        /// </summary> 
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>查询结果（object）</returns> 
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms) {
            using (SqlConnection connection = new SqlConnection(connStr)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Equals(obj, null)) || (Equals(obj, DBNull.Value))) {
                        return null;
                    }

                    return obj;
                }
            }
        }

        /// <summary> 
        /// 执行查询语句，返回SqlDataReader 
        /// </summary> 
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>SqlDataReader</returns> 
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connStr);

            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return myReader;
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }

            }
        }

 
        /// <summary> 
        /// 执行查询语句，返回DataSet 
        /// </summary> 
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>DataSet</returns> 
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand()) {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();

                        return ds;
                    }
                }
            }
        }

        public static DataTable QueryDataTable(string SQLString, params SqlParameter[] cmdParms) {
            using (SqlConnection connection = new SqlConnection(connStr)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmd.Parameters.Clear();
                        return dt;
                    }
                }
            }
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, IEnumerable<SqlParameter> cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType; 
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


        /// <summary> 
        /// 执行存储过程 
        /// </summary> 
        /// <param name="storedProcName">存储过程名</param> 
        /// <param name="parameters">存储过程参数</param> 
        /// <returns>SqlDataReader</returns> 
        public static SqlDataReader RunProcedure(string storedProcName, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connStr);
            connection.Open();
            using (SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters))
            {
                command.CommandType = CommandType.StoredProcedure;
                try {
                    SqlDataReader returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    return returnReader;
                }
                catch (Exception) {
                    connection.Close();
                    throw;
                }
            }
        }


        /// <summary> 
        /// 执行存储过程 
        /// </summary> 
        /// <param name="storedProcName">存储过程名</param> 
        /// <param name="parameters">存储过程参数</param> 
        /// <param name="tableName">DataSet结果中的表名</param> 
        /// <returns>DataSet</returns> 
        public static DataSet RunProcedure(string storedProcName, SqlParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connStr)) {
                DataSet dataSet = new DataSet();
                connection.Open();
                using (SqlDataAdapter sqlDA = new SqlDataAdapter()) {
                    sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    sqlDA.Fill(dataSet, tableName);

                    return dataSet;
                }
            }
        }


        /// <summary> 
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值) 
        /// </summary> 
        /// <param name="connection">数据库连接</param> 
        /// <param name="storedProcName">存储过程名</param> 
        /// <param name="parameters">存储过程参数</param> 
        /// <returns>SqlCommand</returns> 
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IEnumerable<SqlParameter> parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (IDataParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

    }
}
