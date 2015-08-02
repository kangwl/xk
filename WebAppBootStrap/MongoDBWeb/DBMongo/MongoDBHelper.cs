using System;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;

namespace WebAppBS.MongoDBWeb.DBMongo {

    public class MongoDBHelper<T> {
 
        //mongo db 连接的 uri
        //mongodb://<dbuser>:<dbpassword>@ds061651.mongolab.com:61651/appharbor_wbxfktsj
        //mongodb://user:pass@host:port/db
        //mongodb://host:port/db
        private readonly string mongoConnectUrl = string.Format(
            "mongodb://{0}:{1}@ds061651.mongolab.com:61651/{2}",
            "kangwl",
            "abc123",
            "appharbor_wbxfktsj"
            );

        public string MongoConnectUrl {
            get { return mongoConnectUrl; }
        }


        private IMongoCollection<T> MongoCollection { get; set; }

        /// <summary>
        /// 类似sql数据库中的表名
        /// </summary>
        public string CollectionName { get; set; }

        public MongoDBHelper() {
           
        }

        /// <summary>
        /// mongodb://dbuser:dbpassword@ds061651.mongolab.com:61651/appharbor_wbxfktsj
        /// </summary>
        public MongoDBHelper(string collectionName) : this() {
            CollectionName = collectionName;
            MongoCollection = GetCollection(MongoConnectUrl);
        }

        /// <summary>
        /// mongoUrl:mongodb://user:pass@host:port/db
        /// mongodb://host:port/db
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="mongoUrl"></param>
        public MongoDBHelper(string collectionName, string mongoUrl) : this(collectionName) {
            mongoConnectUrl = mongoUrl;
        }

        public MongoDBHelper(string host, string port, string dbName) : this() {
            string mongoUrl = string.Format("http://{0}:{1}/{2}", host, port, dbName);
            mongoConnectUrl = mongoUrl;
        }

        public MongoDBHelper(string host, string port, string dbName, string dbuser, string dbpassword) : this() {
            string mongoUrl = string.Format("http://{3}:{4}@{0}:{1}/{2}", host, port, dbName, dbuser, dbpassword);
            mongoConnectUrl = mongoUrl;
        }

        /// <summary>
        /// 获取集合（类似于数据库中的表）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_mongoConnectUrl"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection(string _mongoConnectUrl = "") {

            Ensure.IsNotNullOrEmpty(CollectionName, "Collection");

            IMongoDatabase database = GetDatabase();
            return database.GetCollection<T>(CollectionName);
        }

        private IMongoDatabase GetDatabase() {
            IMongoClient client = GetMongoClient();

            string dbName = GetDataBaseName();
            IMongoDatabase database = client.GetDatabase(dbName);

            return database;
        }

        private IMongoClient GetMongoClient() {
            IMongoClient client = new MongoClient(MongoConnectUrl);
            return client;
        }


        public MongoUrl GetMongoUrl() {
            MongoUrl mongoUrl = new MongoUrl(MongoConnectUrl);
            return mongoUrl;
        }


        public string GetDataBaseName() {
            MongoUrl mongoUrl = GetMongoUrl();
            return mongoUrl.DatabaseName;
        }

 

    }
}
