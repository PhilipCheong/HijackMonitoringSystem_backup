using Performance_Monitoring.BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using RestSharp;
using Performance_Monitoring.BusinessLayer.Services.IndependentServices;

namespace Performance_Monitoring.Repository
{
    public class MainProcess
	{
		readonly List<DomainExaminationDto> _urlList;
		PerformanceDataService performanceDataService = new PerformanceDataService();
		ServerInformationService serverInformationService = new ServerInformationService();
		DomainExaminationService domainExaminationService = new DomainExaminationService();

		public MainProcess(List<DomainExaminationDto> url)
		{
			this._urlList = url;
		}

		public void Run()
		{
			var serverList = serverInformationService.GetAll();

			var taskList = new List<Task>();

			foreach (var server in serverList)
			{
				var domainList = JsonConvert.SerializeObject(_urlList.Select(s => s.Protocol + s.Domain).ToList());
				var client = new RestClient($"http://{server.ServerIp}:9487/api/PerformanceTest");
                var request = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                request.AddOrUpdateParameter("passwd", "Toffstech_Performance-Monitoring@48969487");
				request.AddOrUpdateParameter("jsonDomain", domainList);
				request.Timeout = 5 * 60 * 1000;

				taskList.Add(Task.Run(() =>
				{
					var response = client.Execute(request);
					Console.WriteLine($"{DateTime.Now} - {server.ServerIp} API called.");
				}));
			}
			Task.WhenAll(taskList);
            var utcNow = DateTime.UtcNow;
            var now = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, utcNow.Minute / 5 * 5, 0, 0, DateTimeKind.Utc);
            foreach (var record in _urlList)
            {
                record.LastExecuted = utcNow;
                domainExaminationService.AddOrEdit(record);
            }
		}
	}
}
