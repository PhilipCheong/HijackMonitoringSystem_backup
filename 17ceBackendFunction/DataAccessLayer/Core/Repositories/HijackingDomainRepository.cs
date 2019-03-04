using System.Collections.Generic;
using System.Linq;
using _17ceBackendFunction.DataAccessLayer.Core.Context;
using _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface;
using _17ceBackendFunction.DataAccessLayer.Entities;
using _17ceBackendFunction.DataAccessLayer.Entities.GeneralData;
using MongoDB.Driver;

namespace _17ceBackendFunction.DataAccessLayer.Core.Repositories
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