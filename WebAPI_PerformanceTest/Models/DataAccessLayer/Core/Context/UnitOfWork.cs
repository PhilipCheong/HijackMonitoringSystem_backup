using System;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Context
{
    public class UnitOfWork<T> : IDisposable, IUnitOfWork<T> where T : IPersistenceEntities
    {
        private static MongoDbContext _dbContext;
        public UnitOfWork()
        {
            _dbContext = new MongoDbContext();
        }
        public void Commit()
        {
            // Save changes with the default options
            //_dbContext.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /**List of Repository*/
        //{task:}

		private IPerformanceDataRepository<T> _performanceDataRepository;
		public IPerformanceDataRepository<T> PerformanceDataRepository
		{
			get
			{
				if (this._performanceDataRepository == null)
				{
					this._performanceDataRepository = new PerformanceDataRepository<T>(_dbContext);
				}
				return _performanceDataRepository;
			}
		}
		private IDomainExaminationRepository<T> _domainExaminationRepository;
		public IDomainExaminationRepository<T> DomainExaminationRepository
		{
			get
			{
				if (this._domainExaminationRepository == null)
				{
					this._domainExaminationRepository = new DomainExaminationRepository<T>(_dbContext);
				}
				return _domainExaminationRepository;
			}
		}
		private IServerInformationRepository<T> _serverInformationRepository;
		public IServerInformationRepository<T> ServerInformationRepository
		{
			get
			{
				if (this._serverInformationRepository == null)
				{
					this._serverInformationRepository = new ServerInformationRepository<T>(_dbContext);
				}
				return _serverInformationRepository;
			}
		}
	}
}