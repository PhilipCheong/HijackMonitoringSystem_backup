using System;
using System.Collections.Generic;
using System.Linq;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
    public class HijackingTestResultRepository<T> : Repository<T>, IHijackingTestResultRespository<T> where T : IPersistenceEntities
    {
        public HijackingTestResultRepository(MongoDbContext context) : base(context)
        {

        }
        public List<HijackingTestResultEntities> GetByDomain(string domain)
        {
            var filterByDate = new BsonDocument
            {
                {
                    "$match", new BsonDocument
                    {
                        {
                            "Domain", domain
                        }
                    }
                }
            };

            var LastValue = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        {
                            "CreateDate", -1
                        }
                    }
                }
            };

            var queryResult = new BsonDocument
            {
                {
                    "$limit", 100
                }
            };

            var pipeline = new[] { filterByDate, LastValue, queryResult };
            var coll = Context.Database.GetCollection<HijackingTestResultEntities>("HijackingTestResult");
            var result = coll.Aggregate<HijackingTestResultEntities>(pipeline);
            return result.ToList();
        }

        public List<HijackingTestResultEntities> GetLastTestResult(List<string> domain)
        {
            var collection = Context.Database.GetCollection<HijackingTestResultEntities>("HijackingTestResult");

            var TestedResult = new List<HijackingTestResultEntities>();
            List<HijackingTestResultEntities> GetLastTestResult = new List<HijackingTestResultEntities>();

            if (collection == null) return GetLastTestResult;
            var dateTime = (collection.Find(new BsonDocument()).Sort("{CreateDate:-1}").Limit(1).FirstOrDefault()).CreateDate;
            if (dateTime != null)
            {
                var LastDate = dateTime.Value.AddMinutes(-5);

                var GetLastTest = from data in collection.AsQueryable()
                                  where data.CreateDate.Value >= LastDate
                                  select data;

                TestedResult = GetLastTest.ToList();
            }

            foreach (var dmn in TestedResult)
            {
                if (domain.Any(s => s.Equals(dmn.Domain)))
                {
                    GetLastTestResult.Add(dmn);
                }
            }
            return GetLastTestResult;
        }
        public List<HijackingTestResultEntities> GetAllForAdminOnly()
        {
            var collection = Context.Database.GetCollection<HijackingTestResultEntities>("HijackingTestResult");

            var result = new List<HijackingTestResultEntities>();

            if (collection == null) return result;
            var dateTime = (collection.Find(new BsonDocument()).Sort("{CreateDate:-1}").Limit(1).FirstOrDefault()).CreateDate;
            if (dateTime == null) return result;
            var lastTestTime = dateTime.Value.AddMinutes(-5);

            var lastTest = from data in collection.AsQueryable()
                           where data.CreateDate.Value >= lastTestTime
                           select data;

            return lastTest.ToList();
        }

        public List<HijackingTestResultEntities> GetAllForUser(List<string> domainList)
        {
            var collection = Context.Database.GetCollection<HijackingTestResultEntities>("HijackingTestResult");

            var result = new List<HijackingTestResultEntities>();

            if (collection == null) return result;
            var dateTime = (collection.Find(new BsonDocument()).Sort("{CreateDate:-1}").Limit(1).FirstOrDefault())
                .CreateDate;
            if (dateTime == null) return result;
            var lastTestTime = dateTime.Value.AddMinutes(-5);

            var lastTest = from data in collection.AsQueryable()
                where data.CreateDate.Value >= lastTestTime
                select data;

            foreach (var data in lastTest)
            {
                if (domainList.Any(s => s.Equals(data.Domain)))
                {
                    result.Add(data);
                }
            }

            return result.ToList();
        }

        public List<HijackingTestResultEntities> GetResultByTime(string domain, DateTime Start, DateTime End)
        {
            var collection = Context.Database.GetCollection<HijackingTestResultEntities>("HijackingTestResult");

            var GetResult = from data in collection.AsQueryable()
                            where data.CreateDate.Value >= Start && data.CreateDate.Value <= End && data.Domain.Equals(domain)
                            select data;

            var TestedResult = GetResult.ToList();

            return TestedResult;
        }

        public List<HijackingTestResultEntities> PerDayEvents(List<string> domainList, DateTime choosenDate)
        {
            var collection = Context.Database.GetCollection<HijackingTestResultEntities>("HijackingTestResult");
            var EndRange = choosenDate.AddDays(1);

            var GetResult = from data in collection.AsQueryable()
                            where data.CreateDate.Value >= choosenDate && data.CreateDate.Value <= EndRange
                            select data;

            return GetResult.ToList();
        }

	    public List<HijackingTestResultEntities> GetDataForSummarize()
	    {
		    var collection = Context.Database.GetCollection<HijackingTestResultEntities>("HijackingTestResult");

		    var returnData = from data in collection.AsQueryable()
			    where data.Summarized == false
			    select data;

		    return returnData.ToList();
	    }

	}

}
