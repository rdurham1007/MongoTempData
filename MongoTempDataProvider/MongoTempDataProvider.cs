using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoTempDataProvider
{
    public class MongoTempDataProvider: ITempDataProvider
    {
        private readonly string _connectionString;

        public MongoTempDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            using (var db = new MongoDbContext(_connectionString))
            {
                var sessionId = controllerContext.HttpContext.Request.UserHostAddress;
                var tempRecord = db.TempData.AsQueryable().FirstOrDefault(d => d.SessionId == sessionId);

                if (tempRecord != null)
                {
                    db.TempData.Remove(Query.EQ("_id", tempRecord.SessionId));
                    return tempRecord.Data;
                }

                return null;
            }
            
        }

        public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            using (var db = new MongoDbContext(_connectionString))
            {
                if (values != null)
                {
                    var sessionId = controllerContext.HttpContext.Request.UserHostAddress;

                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        db.TempData.Insert(new TempData()
                        {
                            Data = values.ToDictionary(k => k.Key, v => v.Value),
                            SessionId = sessionId
                        });
                    }

                }
            }
        }
    }
}
