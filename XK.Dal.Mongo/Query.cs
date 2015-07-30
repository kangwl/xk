using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace XK.Dal.Mongo {
    public class Query {
        //mongodb://<dbuser>:<dbpassword>@ds061651.mongolab.com:61651/appharbor_wbxfktsj
        //mongodb://user:pass@host:port/db
        //mongodb://appharbor_wbxfktsj:k19900606@ds061651.mongolab.com:61651/appharbor_wbxfktsj
        public static async Task<bool> InitMongoDB() {
            var uri = string.Format("mongodb://<{0}>:<{1}>@ds061651.mongolab.com:61651/{2}", "appharbor_wbxfktsj",
                "k19900606", "appharbor_wbxfktsj");
            var client = new MongoDB.Driver.MongoClient(uri);
            var mongoDB = client.GetDatabase("appharbor_wbxfktsj");
            IMongoCollection<BsonDocument> collection = mongoDB.GetCollection<BsonDocument>("User");

            BsonDocument bsonDocument = new BsonDocument() {
                {"name", "kwl"},
                {"age", 12},
                {"phone", "15888883333"}
            };
            await collection.InsertOneAsync(bsonDocument);
            return true;
        }


    }
}
