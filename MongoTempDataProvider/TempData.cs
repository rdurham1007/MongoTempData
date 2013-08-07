using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTempDataProvider
{
    public class TempData
    {
        [BsonId]
        public string SessionId { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
