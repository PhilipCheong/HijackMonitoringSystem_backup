using Performance_Monitoring.BusinessLayer.Dtos;
using Performance_Monitoring.BusinessLayer.Services.IndependentServices;
using Performance_Monitoring.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Performance_Monitoring
{
	class Program
	{
		static void Main(string[] args)
		{
			DomainExaminationService domainExaminationService = new DomainExaminationService();

			while (true)
			{
                Console.WriteLine($"{DateTime.Now.ToLocalTime()} - Application Running");

				var domainList = new List<DomainExaminationDto>();
				try
				{
					var filterDomain = domainExaminationService.GetAll().Where(s => s.ToEndTime > DateTime.Now.ToLocalTime() && DateTime.Now.ToLocalTime() > s.ToStartTime && s.Status == 1);

					domainList = filterDomain.Where(s => DateTime.UtcNow > s.LastExecuted.AddMinutes(s.Interval)).ToList();

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

				if (domainList.Any())
				{
					MainProcess mainProcess = new MainProcess(domainList);

					mainProcess.Run();
				}
				Thread.Sleep(30 * 1000);
			}
		}
	}
}
