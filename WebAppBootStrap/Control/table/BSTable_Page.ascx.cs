using System;
using System.Collections.Generic;
using WebAppBS.Control.table.bsTableCore;

namespace WebAppBS.Control.table {
    public partial class BSTable_Page1 : System.Web.UI.UserControl, bsTableCore.IBaseTable_Page {

        public BSTable_Page1() { }
        public BSTable_Page1(string bsUrl, string bsTableFields) {
            BSUrl = bsUrl;
            BSTableFields = bsTableFields;
        }

        private string _bsTableId = Guid.NewGuid().ToString("n");
        private string _bsTableFields;
        private string _bsUrl;
        private string _bsTableHeight="auto";
        private string _bsTableClass = "table table-hover table-no-bordered";
        private string _pageList = "5,10,20,50";
        private int _pageNumber=1;
        private int _pageSize=10;
        private bool _hasCheck = false;
        private string _sortFields;

        protected void Page_Load(object sender, EventArgs e) {
            lit_bs_table.Text = Create();
        }

        public string BSTableID {
            get { return _bsTableId; }
            set { _bsTableId = value; }
        }

        public string BSTableFields {
            get { return _bsTableFields; }
            set { _bsTableFields = value; }
        }

        public string BSUrl {
            get { return _bsUrl; }
            set { _bsUrl = value; }
        }

        public string BSTableHeight {
            get { return _bsTableHeight; }
            set { _bsTableHeight = value; }
        }

        public string BSTableClass {
            get { return _bsTableClass; }
            set { _bsTableClass = value; }
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
            if (string.IsNullOrEmpty(BSUrl)) {
                throw new Exception("请为BSUrl属性赋值");
            }
            if (string.IsNullOrEmpty(BSTableFields)) {
                throw new Exception("请为BSTableFields属性赋值");
            }

            return table.bsTableCore.BSTable.Create(BSTableID, BSTableFields, HasCheck, SortFields);
        }


        public int PageNumber {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }

        public int PageSize {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public string PageList {
            get { return _pageList; }
            set { _pageList = value; }
        }
    }
}