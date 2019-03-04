using System;
using _17ceBackendFunction.DataAccessLayer.Core.Repositories;
using _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface;
using _17ceBackendFunction.DataAccessLayer.Entities.GeneralData;

namespace _17ceBackendFunction.DataAccessLayer.Core.Context
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

        private IHijackingTestResultRespository<T> _hijackingTestResultRespository;
        public IHijackingTestResultRespository<T> HijackingTestResultRespository
        {
            get
            {
                if (this._hijackingTestResultRespository == null)
                {
                    this._hijackingTestResultRespository = new HijackingTestResultRepository<T>(_dbContext);
                }
                return _hijackingTestResultRespository;
            }
        }
        private IHijackingDomainRespository<T> _hijackingDomainRespository;
        public IHijackingDomainRespository<T> HijackingDomainRespository
        {
            get
            {
                if (this._hijackingDomainRespository == null)
                {
                    this._hijackingDomainRespository = new HijackingDomainRespository<T>(_dbContext);
                }
                return _hijackingDomainRespository;
            }
        }
        private IServerRespository<T> _serverRespository;
        public IServerRespository<T> ServerRespository
        {
            get
            {
                if (this._serverRespository == null)
                {
                    this._serverRespository = new ServerRespository<T>(_dbContext);
                }
                return _serverRespository;
            }
        }
	}
}