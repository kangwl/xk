using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XK.Core.act {
    public abstract class ListJsonBase {
        protected ListJsonBase(System.Web.HttpRequest request) {
            Request = request;
        }

        protected System.Web.HttpRequest Request { get; set; }

        /// <summary>
        /// 数目总计
        /// </summary>
        protected virtual int Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        protected virtual DataTable DataTable { get; set; }

        public abstract string GetDataJson();

        protected string GetDataJson(int total, DataTable data) {
            DataJson jsonObj = new DataJson();
            jsonObj.total = total;
            jsonObj.rows = data;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
            return json;
        }

        protected int Limit {
            get {
                var limit = Request.QueryString["limit"].ToInt();
                if (limit < 1) {
                    limit = 10;
                }
                return limit;
            }
        }

        protected int Offset {
            get {
                var offset = Request.QueryString["offset"].ToInt();
                return offset;
            }
        }

        private string _sort = "ID";
        protected string Sort
        {
            get
            {
                string sort = Request.QueryString["sort"].ToStringEXT();
                if (!string.IsNullOrEmpty(sort)) {
                    _sort = sort;
                }
                return _sort;
            }
            set { _sort = value; }
        }

        private string _order = "ASC";
        protected string Order
        {
            get
            {
                string order= Request.QueryString["order"].ToStringEXT();
                if (!string.IsNullOrEmpty(order)) {
                    _order = order;
                }
                return _order;
            }
            set { _order = value; }
        }

        private class DataJson {
            public DataJson() {
                rows = new DataTable();
            }
            public int total { get; set; }
            public DataTable rows { get; set; }
        }
    }
}
