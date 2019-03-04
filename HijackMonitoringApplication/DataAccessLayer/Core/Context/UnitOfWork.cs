using System;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Context
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

        private ISpeedTestRespository<T> _speedTestRespository;
        public ISpeedTestRespository<T> SpeedTestRespository
        {
            get
            {
                if (this._speedTestRespository == null)
                {
                    this._speedTestRespository = new SpeedTestRepository<T>(_dbContext);
                }
                return _speedTestRespository;
            }
        }
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
        private IUserRespository<T> _userRespository;
        public IUserRespository<T> UserRespository
        {
            get
            {
                if (this._userRespository == null)
                {
                    this._userRespository = new UserRespository<T>(_dbContext);
                }
                return _userRespository;
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
	    private IChartDataRepository<T> _chartDataRespository;
	    public IChartDataRepository<T> ChartDataRepository
	    {
		    get
		    {
			    if (this._chartDataRespository == null)
			    {
				    this._chartDataRespository = new ChartDataRepository<T>(_dbContext);
			    }
			    return _chartDataRespository;
		    }
	    }
		private IServerPerformanceRepository<T> _serverPerformanceRepository;
		public IServerPerformanceRepository<T> ServerPerformanceRepository
		{
			get
			{
				if (this._serverPerformanceRepository == null)
				{
					this._serverPerformanceRepository = new ServerPerformanceRepository<T>(_dbContext);
				}
				return _serverPerformanceRepository;
			}
		}
		private ISummarizedDataRepository<T> _summarizedDataRepository;
		public ISummarizedDataRepository<T> SummarizedDataRepository
		{
			get
			{
				if (this._summarizedDataRepository == null)
				{
					this._summarizedDataRepository = new SummarizedDataRepository<T>(_dbContext);
				}
				return _summarizedDataRepository;
			}
		}
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
        private IDataForAnalysisRepository<T> _dataForAnalysisRepository;
        public IDataForAnalysisRepository<T> DataForAnalysisRepository
        {
            get
            {
                if (this._dataForAnalysisRepository == null)
                {
                    this._dataForAnalysisRepository = new DataForAnalysisRepository<T>(_dbContext);
                }
                return _dataForAnalysisRepository;
            }
        }
        private IAlertInFiveRepository<T> _alertInFiveRepository;
        public IAlertInFiveRepository<T> AlertInFiveRepository
        {
            get
            {
                if (this._alertInFiveRepository == null)
                {
                    this._alertInFiveRepository = new AlertInFiveRepository<T>(_dbContext);
                }
                return _alertInFiveRepository;
            }
        }
    }
}