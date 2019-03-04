using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
    public class DataForAnalysisRepository<T> : Repository<T>, IDataForAnalysisRepository<T> where T : IPersistenceEntities
    {
        public DataForAnalysisRepository(MongoDbContext context) : base(context)
        {
        }

        public List<DataForAnalysisEntities> GetOneDayData(DateTime startDate, DateTime endDate)
        {
            var collection = Context.Database.GetCollection<DataForAnalysisEntities>("DataForAnalysis");

            //var result = from data in collection.AsQueryable()
            //	where data.CreateDate.Value >= startDate && data.CreateDate.Value < startDate.AddDays(1)
            //	select data;

            var queryDate = new BsonDocument
            {
                {
                    "$match", new BsonDocument
                    {
                        {
                            "CreateDate", new BsonDocument
                            {
                                {"$gte", startDate},
                                {"$lt", endDate }
                            }
                        }
                    }
                }
            };

            var pipeline = new[] { queryDate };
            var coll = Context.Database.GetCollection<DataForAnalysisEntities>("DataForAnalysis");
            var result = coll.Aggregate<DataForAnalysisEntities>(pipeline);
            return result.ToList();
        }
    }
}