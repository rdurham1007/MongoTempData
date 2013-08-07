using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoTempDataProvider
{
    internal class MongoDbContext : IDisposable
    {
        private MongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            _database = client.GetServer().GetDatabase(url.DatabaseName);
        }

        public MongoCollection<TempData> TempData { get { return GetCollection<TempData>("MvcTempData"); } }

        public MongoCollection<T> GetCollection<T>(string collectionName) where T : class
        {
            return GetCollection<T>(collectionName, null); 
        }

        public MongoCollection<T> GetCollection<T>(string collectionName, IMongoCollectionOptions options) where T : class
        {
            if (!_database.GetCollectionNames().Contains(collectionName))
                _database.CreateCollection(collectionName, options);

            return _database.GetCollection<T>(collectionName);
        }

        public void Dispose()
        {
            _database = null;
        }
    }
}
