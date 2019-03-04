using System;
using System.Configuration;
using System.Data.Entity;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using MongoDB.Driver;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Context
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

        public DbSet<HijackingDomainEntities> HijackingDomainEntitieses { get; set; }
        public DbSet<HijackingTestResultEntities> HijackingTestResultEntitieses { get; set; }
        public DbSet<UserEntities> UserEntitieses { get; set; }
        public DbSet<SpeedTestResultEntities> SpeedTestResultEntitieses { get; set; }
        public DbSet<TFCDNserversEntities> TfcdNserversEntitieses { get; set; }
	    public DbSet<ChartDataEntities> ChartDataEntitieses { get; set; }
		public DbSet<ServerPerformanceEntities> ServerPerformanceEntities { get; set; }
		public DbSet<SummarizedDataEntities> SummarizedDataEntities { get; set; }
		public DbSet<PerformanceDataEntities> PerformanceDataEntities { get; set; }
		public DbSet<DomainExaminationEntities> DomainExaminationEntities { get; set; }
        public DbSet<DataForAnalysisEntities> DataForAnalysisEntities { get; set; }
        public DbSet<AlertInFiveEntities> AlertInFiveEntities { get; set; }
    }

}