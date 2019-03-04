using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
	public class ChartDataRepository<T> : Repository<T>, IChartDataRepository<T> where T : IPersistenceEntities
	{
		public ChartDataRepository(MongoDbContext context) : base(context)
		{
		}

		public List<ChartDataEntities> GetResultByHour(DateTime lastQuery)
		{
			var filterByDate = new BsonDocument
			{
				{
					"$match", new BsonDocument
					{
						{
							"CreateDate", new BsonDocument
							{
								{"$gt", lastQuery}                          }
						}
					}
				}
			};

			var pipeline = new[] { filterByDate };
			var coll = Context.Database.GetCollection<ChartDataEntities>("ChartsData");
			var result = coll.Aggregate<ChartDataEntities>(pipeline);
			return result.ToList();
		}
		public List<ChartDataEntities> SingleDomainLineChart(string domainName, DateTime startDate, DateTime endDate)
		{
			var domain = new BsonDocument
			{
				{
					"$match", new BsonDocument
					{
						{
							"Domain", domainName
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
							"CreateDate", new BsonDocument
							{
								{"$gt", startDate}
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
							"CreateDate", new BsonDocument
							{
								{"$lte", endDate}

							}
						}
					}
				}
			};

			var pipeline = new[] { domain, filterByDateStart, filterByDateEnd };
			var coll = Context.Database.GetCollection<ChartDataEntities>("ChartsData");
			var result = coll.Aggregate<ChartDataEntities>(pipeline);
			return result.ToList();
		}

	}
}