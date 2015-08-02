using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;

namespace XK.MongoDBUtil {

    public class MongoDBExcute<TModel> {
         

        public MongoDBExcute(IMongoCollection<TModel> collection) {
            MongoCollection = collection;

        }

        public IMongoCollection<TModel> MongoCollection { get; set; }
         

        public async Task<bool> Delete(Expression<Func<TModel, bool>> filter) {
            DeleteResult result = await MongoCollection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        public async Task<long> DeleteMany(Expression<Func<TModel, bool>> filter) {
            DeleteResult result = await MongoCollection.DeleteManyAsync(filter);
            if (result.IsAcknowledged) {
                return result.DeletedCount;
            }
            return 0L;
        }

        public Task Add(TModel tModel) {
            return MongoCollection.InsertOneAsync(tModel);
        }

        public Task<TModel> Get(Expression<Func<TModel, bool>> filter) {
            return MongoCollection.Find(filter).SingleOrDefaultAsync();
        }

 

        public void GetListAction(Expression<Func<TModel, bool>> filter, Action<TModel> action) {
            Task<IAsyncCursor<TModel>> asyncCursorRask = MongoCollection.FindAsync(filter);
            asyncCursorRask.Result.ForEachAsync(action);
        }

        public void GetListFunc(Expression<Func<TModel, bool>> filter, Func<TModel, Task> func) {
            Task<IAsyncCursor<TModel>> asyncCursorTask = MongoCollection.FindAsync(filter);
            asyncCursorTask.Result.ForEachAsync(func);
        }

        public Task<List<TModel>> GetPaged(Expression<Func<TModel, bool>> filter, int pageIndex, int pageSize,
            Expression<Func<TModel, object>> sortExpression, bool asc) {

            IFindFluent<TModel, TModel> findFluent = MongoCollection.Find(filter);

            if (asc) {
                findFluent.SortBy(sortExpression);
            }
            else {
                findFluent.SortByDescending(sortExpression);
            }

            int skip = pageIndex*pageSize;

            findFluent.Skip(skip).Limit(pageSize);

            Task<List<TModel>> models = findFluent.ToListAsync();
            return models;
        }

        public bool Update(Expression<Func<TModel, bool>> filterExpression, string field, dynamic val) {

            var filter = Builders<TModel>.Filter.Where(filterExpression);
            var update = Builders<TModel>.Update.Set(field, val);

            Task<UpdateResult> resultTask =
                MongoCollection.UpdateOneAsync(filter, update);
            long updateCount = 0;
            if (resultTask.Result.IsAcknowledged) {
                updateCount = resultTask.Result.ModifiedCount;
            }
            return updateCount > 0;
        }


        public bool Update(Expression<Func<TModel, bool>> filterExpression,  IEnumerable<dynamic> vals) {

            var filter = Builders<TModel>.Filter.Where(filterExpression);
            var update = Builders<TModel>.Update.AddToSetEach("User", vals);

            Task<UpdateResult> resultTask =
                MongoCollection.UpdateOneAsync(filter, update);
            long updateCount = 0;
            if (resultTask.Result.IsAcknowledged) {
                updateCount = resultTask.Result.ModifiedCount;
            }
            return updateCount > 0;
        }







    }
}
