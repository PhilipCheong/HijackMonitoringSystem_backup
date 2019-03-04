using System.Collections.Generic;
using System.Linq;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
    public class UserRespository<T> : Repository<T>, IUserRespository<T> where T : IPersistenceEntities
    {
        public UserRespository(MongoDbContext context) : base(context)
        {
        }

        public List<UserEntities> LoginProcess(string userName, string passWord)
        {
            var collection = Context.Database.GetCollection<UserEntities>("User");

            var GetResult = from data in collection.AsQueryable()
                where data.Username.Equals(userName) && data.Password.Equals(passWord)
                select data;

            var authResult = GetResult.ToList();

            return authResult;
        }
        public List<string> GetAllCustomerId()
        {
            var collection = Context.Database.GetCollection<UserEntities>("User");

            var result = from data in collection.AsQueryable()
                select data.CustomerId;

            return result.ToList();
        }

    }
}