using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MongoDB.Bson;
using MongoDB.Driver;

namespace WebAppBS.MongoDBWeb.Test {
    public partial class _default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) { 
            GetUser();
            Response.Write("ok");
        }

        private async void GetUser() {
            DBMongo.MongoDBHelper<BsonDocument> mongoHelper = new DBMongo.MongoDBHelper<BsonDocument>("User");
            IMongoCollection<BsonDocument> collection = mongoHelper.GetCollection();
            // var list = collection.Find<XK.Model.User_Model>(u=>u.Name== "k1199").ToListAsync();
            FilterDefinitionBuilder<BsonDocument> builder = new FilterDefinitionBuilder<BsonDocument>();
           var define= builder.Lt("Age", 122);

            var list = collection.Find<BsonDocument>(define).ToListAsync();
            string usernames = "";
            list.Result.ForEach(d => usernames += d["Name"]+"</br>");
            Response.Write(usernames);
        }
    }
}