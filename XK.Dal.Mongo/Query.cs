using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using XK.Model;

namespace XK.Dal.Mongo {
    public class Query {
        //mongodb://<dbuser>:<dbpassword>@ds061651.mongolab.com:61651/appharbor_wbxfktsj
        //mongodb://user:pass@host:port/db

        private readonly string mongoConnUri = string.Format(
            "mongodb://{0}:{1}@ds061651.mongolab.com:61651/{2}", 
            "kangwl",
            "abc123", 
            "appharbor_wbxfktsj"
            );

        public async Task<bool> InsertUser() {

            IMongoCollection<BsonDocument> collection = GetDBCollection<BsonDocument>("User");

            BsonDocument bsonDocument = new BsonDocument() {
                {"name", "kwl"},
                {"age", 12},
                {"phone", "15888883333"}
            };
            await collection.InsertOneAsync(bsonDocument);
            return true;
        }

        public IMongoDatabase GetMongoDB() {
            var client = new MongoDB.Driver.MongoClient(mongoConnUri);
            var mongoDB = client.GetDatabase("appharbor_wbxfktsj");
            return mongoDB;
        }

        public IMongoCollection<T> GetDBCollection<T>(string collectionName) where T : class {
            IMongoDatabase mongoDatabase = GetMongoDB();
            
            return mongoDatabase.GetCollection<T>(collectionName);
        }


        public bool InsertUserModel() {
            Model.User_Model userModel = new User_Model() {
                AddDateTime = DateTime.Now,
                Age = 12,
                Email = "kangwl2009@163.com",
                MobilePhone = "1223334",
                Name = "k5",
                Sex = 1,
                UpdateDateTime = DateTime.Now,
                UserID = "kwl2011",
                UserPassword = "abc123",
                UserType = 2
            };

            IMongoCollection<Model.User_Model> userCollection = GetDBCollection<Model.User_Model>("User");
            userCollection.InsertOneAsync(userModel).Wait();
            return true;
        }

        public Model.User_Model GetUer() {
            
            IMongoCollection<Model.User_Model> userCollection = GetDBCollection<Model.User_Model>("User");
            Task<Model.User_Model> userTask = userCollection.Find(u => u.Name == "k5").SingleOrDefaultAsync();
            userTask.Wait();
            return userTask.Result;
        }

        public User_Model  TestMongoDB() {
            
            IMongoCollection<Model.User_Model> userCollection = GetDBCollection<Model.User_Model>("User");
            User_Model user = userCollection.FindOneAndDeleteAsync(u => u.Name == "k5").Result;
            return user;
        }



    }
}
