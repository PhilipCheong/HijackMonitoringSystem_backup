using System;
using System.Configuration;
using System.Data.Entity;
using _17ceBackendFunction.DataAccessLayer.Entities;
using MongoDB.Driver;

namespace _17ceBackendFunction.DataAccessLayer.Core.Context
{
    public class MongoDbContext
    {
        private readonly MongoClient _mongodbClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbServer"]);

        public MongoDbContext()
        {
			string connectionstring = ConfigurationManager.AppSettings["MongoDbDatabase"];

			Database = _mongodbClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbDatabase"]);
        }
        public IMongoDatabase Database { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public DbSet<HijackingDomainEntities> HijackingDomainEntitieses { get; set; }
        public DbSet<HijackingTestResultEntities> HijackingTestResultEntitieses { get; set; }
        public DbSet<TFCDNserversEntities> TfcdNserversEntitieses { get; set; }

	}

}