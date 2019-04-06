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
	public class PerformanceDataRepository<T> : Repository<T>, IPerformanceDataRepository<T> where T : IPersistenceEntities
	{
		public PerformanceDataRepository(MongoDbContext context) : base(context)
		{

		}
        public List<PerformanceDataEntities> FindWithoutImageAsync(string domainName, DateTime startDate, DateTime endDate)
        {
            var domain = new BsonDocument
            {
                {
                    "$match", new BsonDocument
                    {
                        {
                            "Url", domainName
                        }
                    }
                }
            };
            var filterByDateStart = new BsonDocument
            {
                {
                    "$match", new BsonDocument
                    {
                        {
                            "TestTime", new BsonDocument
                            {
                                {"$gte", startDate}
                            }
                        }
                    }
                }
            };
            var filterByDateEnd = new BsonDocument
            {
                {
                    "$match", new BsonDocument
                    {
                        {
                            "TestTime", new BsonDocument
                            {
                                {"$lte", endDate}
                            }
                        }
                    }
                }
            };
            var excludeImage = new BsonDocument
            {
                {
                    "$project", new BsonDocument
                    {
                        {
                            "Image", 0
                        }
                    }
                }
            };

            var pipeline = new[] { domain, filterByDateStart, filterByDateEnd, excludeImage };
            var coll = Context.Database.GetCollection<PerformanceDataEntities>("PerformanceData");
            var result = coll.Aggregate<PerformanceDataEntities>(pipeline);
            return result.ToList();
        }

        public List<PerformanceDataEntities> FindWithoutImageAsync(DateTime startDate)
        {

            var filterByDateStart = new BsonDocument
            {
                {
                    "$match", new BsonDocument
                    {
                        {
                            "TestTime", new BsonDocument
                            {
                                {"$gte", startDate}
                            }
                        }
                    }
                }
            };
            var excludeImage = new BsonDocument
            {
                {
                    "$project", new BsonDocument
                    {
                        {
                            "Image", 0
                        }
                    }
                }
            };

            var pipeline = new[] {filterByDateStart, excludeImage };
            var coll = Context.Database.GetCollection<PerformanceDataEntities>("PerformanceData");
            var result = coll.Aggregate<PerformanceDataEntities>(pipeline);
            return result.ToList();
        }

        public List<PerformanceDataEntities> FindWithoutImageAsync(string domainName, DateTime dateTime)
        {
            var domain = new BsonDocument
            {
                {
                    "$match", new BsonDocument
                    {
                        {
                            "Url", domainName
                        }
                    }
                }
            };
            var filterByDateStart = new BsonDocument
            {
                {
                    "$match", new BsonDocument
                    {
                        {
                            "TestTime", dateTime
                        }
                    }
                }
            };
            var excludeImage = new BsonDocument
            {
                {
                    "$project", new BsonDocument
                    {
                        {
                            "Image", 0
                        }
                    }
                }
            };

            var pipeline = new[] { domain, filterByDateStart, excludeImage };
            var coll = Context.Database.GetCollection<PerformanceDataEntities>("PerformanceData");
            var result = coll.Aggregate<PerformanceDataEntities>(pipeline);
            return result.ToList();
        }

    }
}