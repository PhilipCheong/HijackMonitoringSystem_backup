using System;
using System.Configuration;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Context
{
	public class MongoDbContext
    {
        private readonly MongoClient _mongodbClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbServer"]);

        public MongoDbContext()
        {
            Database = _mongodbClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbDatabase"]);
        }
        public IMongoDatabase Database { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

		public DbSet<PerformanceDataEntities> PerformanceDataEntities { get; set; }
		public DbSet<DomainExaminationEntities> DomainExaminationEntities { get; set; }
		public DbSet<ServerInformationEntities> ServerInformationEntities { get; set; }
	}

}