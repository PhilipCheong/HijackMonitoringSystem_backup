using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
	public class SummarizedDataRepository<T> : Repository<T>, ISummarizedDataRepository<T> where T : IPersistenceEntities
	{
		public SummarizedDataRepository(MongoDbContext context) : base(context)
		{
		}

		public List<SummarizedDataEntities> SingleDomainLineChart(string serverName, DateTime startDate, DateTime endDate)
		{
			var domain = new BsonDocument
			{
				{
					"$match", new BsonDocument
					{
						{
							"ServerCName", serverName
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
							"HourCategory", new BsonDocument
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
							"HourCategory", new BsonDocument
							{
								{"$lte", endDate}

							}
						}
					}
				}
			};

			var pipeline = new[] { domain, filterByDateStart, filterByDateEnd };
			var coll = Context.Database.GetCollection<SummarizedDataEntities>("SummarizedData");
			var result = coll.Aggregate<SummarizedDataEntities>(pipeline);
			return result.ToList();
		}

	}
}