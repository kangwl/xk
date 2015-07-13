using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using WebAppBootStrap.Control.table.bsTableCore;

namespace WebAppBootStrap.Control.table {
    public partial class BSTable_Simple : System.Web.UI.UserControl,table.bsTableCore.IBaseTable {
        public BSTable_Simple() {
            
        }

        public BSTable_Simple(string bsUrl, string bsTableFields) {
            BSUrl = bsUrl;
            BSTableFields = bsTableFields;
        }

        protected void Page_Load(object sender, EventArgs e) {
        
            string table = Create();
            lit_bs_table.Text = table;
        }

        /// <summary>
        /// 生成table的ID
        /// </summary>
        public string BSTableID { get; set; }

        /// <summary>
        /// 格式 id:#,name:名称,price:价格
        /// 说明 字段和页面显示的文字组成，中间用逗号分开
        /// </summary>
        public string BSTableFields { get; set; }

        /// <summary>
        /// 数据请求地址 bootstrapTable url
        /// 例如：url:/Content/bootstrap/table/data/data.aspx?act=testsimple
        /// </summary>
        public string BSUrl { get; set; }


        private string _bsTableHeight = "500";
        /// <summary>
        /// table高度 可以为auto 或者 500 ...
        /// </summary>
        public string BSTableHeight {
            get { return _bsTableHeight; }
            set { _bsTableHeight = value; }
        }

        private string _bsTableClaSS = "table table-hover table-no-bordered";
        private bool _hasCheck = false;
        private string _sortFields;

        /// <summary>
        /// table 样式
        /// table table-hover table-no-bordered
        /// </summary>
        public string BSTableClass {
            get { return _bsTableClaSS; }
            set { _bsTableClaSS = value; }
        }

        public bool HasCheck {
            get { return _hasCheck; }
            set { _hasCheck = value; }
        }

        public string SortFields {
            get { return _sortFields; }
            set { _sortFields = value; }
        }

        public string Create() {
 
                return table.bsTableCore.BSTable.Create(BSTableID, BSTableFields,HasCheck,SortFields);
    
        }

 
    }
}