using System.Collections.Generic;
using System.Linq;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Driver;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
    public class HijackingDomainRespository<T> : Repository<T>, IHijackingDomainRespository<T> where T : IPersistenceEntities
    {
        public HijackingDomainRespository(MongoDbContext context) : base(context)
        {
        }

        public List<HijackingDomainEntities> GetAllDomainWithCustomerId(string customerId)
        {
            var collection = Context.Database.GetCollection<HijackingDomainEntities>("HijackingDomain");

            var result = from data in collection.AsQueryable()
                         where data.CustomerId.Equals(customerId)
                         select data;

            return result.ToList();
        }
    }
}