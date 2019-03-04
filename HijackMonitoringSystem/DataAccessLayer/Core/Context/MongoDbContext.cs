using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Entities;
using MongoDB.Driver;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Context
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

        public DbSet<HijackDomainEntities> HijackDomainEntitieses { get; set; }
        public DbSet<HijackingTestResultEntities> HijackingTestResultEntitieses { get; set; }
        public DbSet<UserEntities> UserEntitieses { get; set; }
        public DbSet<SpeedTestResultEntities> SpeedTestResultEntitieses { get; set; }
        public DbSet<TFCDNserversEntities> TfcdNserversEntitieses { get; set; }
    }

}