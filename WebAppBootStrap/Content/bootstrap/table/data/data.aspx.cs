using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppBS.Content.bootstrap.table.data {
    public partial class data : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Request.QueryString["act"] == "products") {
                InitData();
            }
            if (Request.QueryString["act"] == "testsimple") {
                TestSimple();
            }
        }

        public int Limit {
            get {
                var limit = Request.QueryString["limit"].ToInt();
                if (limit < 1) {
                    limit = 10;
                }
                return limit;
            }
        }

        public int Offset {
            get {
                var offset = Request.QueryString["offset"].ToInt();
                return offset;
            }
        }

        public string Sort{get { return Request.QueryString["sort"].ToStringEXT(); }}
        public string Order{get { return Request.QueryString["order"].ToStringEXT(); }}

        public string SearchWord{get { return Request.QueryString["search"].ToStringEXT(); }}


        private static int Total{get { return 125; }}

        private void InitData() {
            DataJson dataJson = new DataJson();
            int total = 0;
            dataJson.rows = GetProducts(out total);
            dataJson.total = total;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dataJson);
            Response.Write(json);
            Response.End();
        }

        private void TestSimple() {
            product p = new product();
            List<product> products = new List<product>();
            for (int i = 1; i <= 33; i++) {
                products.Add(new product() {id = i,name = "p" + i, price = i});
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(products);
            Response.Write(json);
            Response.End();
        }


        private List<product> GetProducts(out int total) {

            List<product> products = new List<product>();

            for (int i = 1; i <= Total; i++) {
                product p = new product() {id = i, name = "name" + i, price = i + 1};
                products.Add(p);
            }

            var productEnum = products.AsEnumerable();
            if (!string.IsNullOrEmpty(Sort)) {
                if (Sort == "id") {
                    if (Order == "asc") {
                        productEnum = productEnum.OrderBy(p => p.id);
                    }
                    else {
                        productEnum = productEnum.OrderByDescending(p => p.id);
                    }
                }
                else if (Sort == "price") {
                    if (Order == "asc") {
                        productEnum = productEnum.OrderBy(p => p.price);
                    }
                    else {
                        productEnum = productEnum.OrderByDescending(p => p.price);
                    }
                }
            }

            if (!string.IsNullOrEmpty(SearchWord)) {
                //search
                productEnum = productEnum.Where(p => p.name.Contains(SearchWord.Trim()));
            }
            IEnumerable<product> enumerable = productEnum as IList<product> ?? productEnum.ToList();
            total = enumerable.Count();

            productEnum = enumerable.Skip(Offset).Take(Limit);
            
            return productEnum.ToList();
        }

        private class DataJson {
            public DataJson() {
                rows = new List<product>();
            }
            public int total { get; set; }
            public List<product> rows { get; set; } 
        }

        private class product {
            public int id { get; set; }
            public string name { get; set; }
            public int price { get; set; }
        }
        
    }
}