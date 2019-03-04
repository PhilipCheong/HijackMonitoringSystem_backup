using System.Collections.Generic;
using System.Linq;
using _17ceBackendFunction.DataAccessLayer.Core.Context;
using _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface;
using _17ceBackendFunction.DataAccessLayer.Entities;
using _17ceBackendFunction.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _17ceBackendFunction.DataAccessLayer.Core.Repositories
{
    public class ServerRespository<T> : Repository<T>, IServerRespository<T> where T : IPersistenceEntities
    {
        public ServerRespository(MongoDbContext context) : base(context)
        {
        }
	    //public List<TFCDNserversEntities> GetAllForAdminOnly()
	    //{
		   // var collection = Context.Database.GetCollection<TFCDNserversEntities>("Server");

		   // var result = new List<TFCDNserversEntities>();

		   // if (collection == null) return result;
		   // var dateTime = (collection.Find(new BsonDocument()).Sort("{CreateDate:-1}").Limit(1).FirstOrDefault()).CreateDate;
		   // if (dateTime == null) return result;
		   // var lastTestTime = dateTime.Value.AddMinutes(-5);

		   // var lastTest = from data in collection.AsQueryable()
			  //  where data.CreateDate.Value >= lastTestTime
			  //  select data;

		   // return lastTest.ToList();
	    //}

	}
}