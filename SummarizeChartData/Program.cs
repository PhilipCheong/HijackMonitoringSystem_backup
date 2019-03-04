using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.ViewModel;
using Newtonsoft.Json;

namespace SummarizeChartData
{
	class Program
	{
		ChartDataService chartDataService = new ChartDataService();

		static void Main(string[] args)
		{
			DataStructuring dataStructuring = new DataStructuring();
			var hijackingTestResultService = new HijackingTestResultService();
			var chartDataService = new ChartDataService();

			while (true)
			{
				try
				{
					Console.WriteLine($"{DateTime.Now} - Application Started...");
					var newData = hijackingTestResultService.GetDataForSummarize();
					dataStructuring.SummarizeDataNew(newData);
					Task.Run(() => dataStructuring.UpdateSummarizedData(newData));
					Console.WriteLine($"{DateTime.Now} - Application Ended...");
				}
				catch (Exception ex)
				{
					DataStructuring.log.Error(ex);
				}
				Thread.Sleep(15 * 60 * 1000);
			}
		}
	}

}




