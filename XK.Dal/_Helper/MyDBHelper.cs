using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XK.Dal._Helper.UtilBase;

namespace XK.Dal._Helper {
    public class MyDBHelper {
        public class SelectDB {

            public struct SelectDBParams {
                public string TableName;
                public string Fields;
                public Dictionary<string, dynamic> DicWhere;
                public string OrderBy;
                public int PageIndex;
                public int PageSize;
                public bool Page;
            }

            private SelectDBParams DbParams { get; set; }

            public SelectDB(string fields,string tableName,Dictionary<string,dynamic> dicWhere ,string orderby) {
                _fields = fields;
                TableName = tableName;
            }

            public SelectDB(SelectDBParams selectDbParams) {
                DbParams = selectDbParams;
            }

            public DataTable GetTable() {
                Dictionary<string, dynamic> dicWhere = DbParams.DicWhere;
                string sqlWhereParam = InitWhereSqlParam(dicWhere);

                string sql = string.Format("select {0} from [{1}] where {2}", DbParams.Fields, DbParams.TableName,
                    sqlWhereParam);
                SqlParameter[] parameters = InitSqlParamVal(dicWhere);
                DataTable dt = DbHelperSQL.QueryDataTable(sql, parameters);
                return dt;
            }

            public DataTable GetTablePage() {
                Dictionary<string, dynamic> dicWhere = DbParams.DicWhere;
                string sqlWhereParam = InitWhereSqlParam(dicWhere);

                string sql = string.Format("select {0} from [{1}] where {2}", DbParams.Fields, DbParams.TableName,
                    DbParams.DicWhere);
                int startPageIndex = (DbParams.PageIndex - 1)*DbParams.PageSize + 1;
                int endPageIndex = DbParams.PageIndex*DbParams.PageSize;
                string sqlPage = string.Format(@"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}", sql, DbParams.OrderBy, startPageIndex, endPageIndex);
                SqlParameter[] parameters = InitSqlParamVal(dicWhere);
                DataTable dt = DbHelperSQL.QueryDataTable(sqlPage, parameters);
                return dt;
            }


            private string InitWhereSqlParam(Dictionary<string,dynamic> dic) {
                string sqlParamStr = "";
                List<string> sqlParamList = new List<string>();
                foreach (KeyValuePair<string, dynamic> pair in dic) {
                    sqlParamList.Add(string.Format("{0}=@{0}", pair.Key));
                }
                string sqlWhereParam = string.Join(" and ", sqlParamList);
                return sqlWhereParam;
            }

            private SqlParameter[] InitSqlParamVal(Dictionary<string, dynamic> dic) {
                int count = dic.Count;
                SqlParameter[] parameters = new SqlParameter[count];
                int i = 0;
                foreach (KeyValuePair<string, dynamic> pair in dic) {
                    parameters[i] = new SqlParameter("@" + pair.Key, pair.Value);
                    i += 1;
                }
                return parameters;
            }

            private string _fields = "*";
            private string _where = "";
            private string _orderby = "";



            public string Fileds {
                get { return _fields; }
                set { _fields = value; }
            }

            public string TableName { get; set; }

            public string Where {
                get { return _where; }
                set { _where = value; }
            }

            public string OrderBy {
                get { return _orderby; }
                set { _orderby = value; }
            }

        }
    }
}
