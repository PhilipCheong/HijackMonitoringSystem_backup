using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Core.Context;
using HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringSystem.DataAccessLayer.Entities;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Repositories
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

            if (collection != null)
            {
                var LastDate = (collection.Find(new BsonDocument()).Sort("{CreateDate:-1}").Limit(1).FirstOrDefault()).CreateDate.Value.AddMinutes(-5);

                var GetLastTest = from data in collection.AsQueryable()
                                  where data.CreateDate.Value >= LastDate
                                  select data;

                TestedResult = GetLastTest.ToList();

                foreach (var dmn in TestedResult)
                {
                    if (domain.Any(s => s.Equals(dmn.Domain)))
                    {
                        GetLastTestResult.Add(dmn);
                    }
                }
            }
            return GetLastTestResult;
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



    }

}
