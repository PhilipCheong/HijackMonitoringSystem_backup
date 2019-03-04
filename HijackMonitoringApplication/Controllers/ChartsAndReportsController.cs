using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.ViewModel;
using MongoDB.Bson.IO;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace HijackMonitoringApplication.Controllers
{
	[Authorize]
	public class ChartsAndReportsController : BaseController
	{
		HijackingTestResultService hijackingTestResultService = new HijackingTestResultService();
		HijackingDomainService hijackingDomainService = new HijackingDomainService();
		ChartDataService chartDataService = new ChartDataService();

		// GET: ChartsAndReports
		public ActionResult Index()
		{
			return View();
		}

		private class DomainTag
		{
			public string value { get; set; }
			public string text { get; set; }
		}
		public string DomainPermission()
		{
			string currentId = CurrentAccount.CustomerId;
			var domainList = new List<HijackingDomainDto>();
			var allowedDomain = new List<DomainTag>();
			try
			{
				if (currentId.Equals("TOFFSTECH"))
				{
					domainList = hijackingDomainService.GetAll();
				}
				else
				{
					 domainList = hijackingDomainService.Find(p => p.CustomerId.Equals(currentId));
				}
				allowedDomain.AddRange(domainList.Select(s => new DomainTag { value = s.Protocol + s.Domain, text = s.Protocol + s.Domain }));
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(allowedDomain);
		}
		[HttpPost]
		public string HijackedCountsLine(string domainName, string start, string end)
		{
			var hijackedEvent = chartDataService.SingleDomainLineChart(domainName, Convert.ToDateTime(start), Convert.ToDateTime(end)).OrderBy(s => s.CreateDate);
			dynamic returnData = new ExpandoObject();

			if (hijackedEvent.Any())
			{
				try
				{
					var hijackedCountLineChart = this.HijackedCounts(hijackedEvent);
					var hijackedIspCount = this.IspHijackedCount(hijackedEvent);
					var hijackedDestinationCount = this.HijackedDestinationCounts(hijackedEvent);
					var hijackedDestinationEach = this.HijackedDestinationEach(hijackedEvent);
					var hijackeProvinceDns = this.HijackedProvinceCountDns(hijackedEvent);
					var hijackedProvinceHttp = this.HijackedProvinceCountHttp(hijackedEvent);
					var hijackedResolutionTime = this.ResolutionAverage(hijackedEvent);

					returnData.HijackedLine = hijackedCountLineChart;
					returnData.HijackedIspLinePie = hijackedIspCount;
					returnData.HijackedSmallPie = hijackedDestinationCount;
					returnData.HijackedBigPie = hijackedDestinationEach;
					returnData.HijackedProvinceDns = hijackeProvinceDns;
					returnData.HijackedProvinceHttp = hijackedProvinceHttp;
					returnData.HijackedResolutionTime = hijackedResolutionTime;

				}
				catch (Exception ex)
				{
					log.Error(ex);
				}
			}
			return JsonConvert.SerializeObject(returnData);
		}
		public List<List<string>> HijackedCounts(IOrderedEnumerable<ChartDataDto> summarizedData)
		{
			var deserializedDnsHijack = new List<string>() { "Dns Hijacked" };
			var deserializedHttpHijack = new List<string>() { "Http Hijacked" };
			var deserializedNormal = new List<string>() { "Normal" };
			var dataDate = new List<string>() { "Date" };

			try
			{
				//Structuring data for chart.
				dataDate.AddRange(summarizedData.Select(s => s.DataHourCategory.ToString()).Distinct());
				deserializedDnsHijack.AddRange(summarizedData.GroupBy(z => new { z.DataHourCategory }).Select(p => p.Sum(s => s.DnsHijackedCounts.DnsHijackedCount).ToString()));
				deserializedHttpHijack.AddRange(summarizedData.GroupBy(z => new { z.DataHourCategory }).Select(p => p.Sum(s => s.HttpHijackedCounts.HttpHijackedCounts).ToString()));
				deserializedNormal.AddRange(summarizedData.GroupBy(z => new { z.DataHourCategory }).Select(p => p.Sum(s => s.DestinationIpInfo.NormalCount).ToString()));
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return new List<List<string>> { dataDate, deserializedDnsHijack, deserializedHttpHijack, deserializedNormal };
		}
		public List<List<string>> IspHijackedCount(IOrderedEnumerable<ChartDataDto> summarizedData)
		{
			var deserializeIspOne = new List<string>() { "China Telecom" };
			var deserializeIspTwo = new List<string>() { "China Unicom" };
			var deserializeIspThree = new List<string>() { "China Mobile" };
			var dataDate = new List<string>() { "Date" };

			try
			{
				var groupingIsp = summarizedData.GroupBy(z => new { z.Isp, z.DataHourCategory }).ToList();
				deserializeIspOne.AddRange(groupingIsp.Where(z => z.Key.Isp.Equals("China Telecom")).Select(s => s.Sum(k => k.DnsHijackedCounts.DnsHijackedCount + k.HttpHijackedCounts.HttpHijackedCounts).ToString()));
				deserializeIspTwo.AddRange(groupingIsp.Where(z => z.Key.Isp.Equals("China Unicom")).Select(s => s.Sum(k => k.DnsHijackedCounts.DnsHijackedCount + k.HttpHijackedCounts.HttpHijackedCounts).ToString()));
				deserializeIspThree.AddRange(groupingIsp.Where(z => z.Key.Isp.Equals("China Mobile")).Select(s => s.Sum(k => k.DnsHijackedCounts.DnsHijackedCount + k.HttpHijackedCounts.HttpHijackedCounts).ToString()));


				dataDate.AddRange(summarizedData.Select(s => s.DataHourCategory.ToString()).Distinct());
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return new List<List<string>> { dataDate, deserializeIspOne, deserializeIspTwo, deserializeIspThree };
		}
		public List<List<string>> HijackedDestinationCounts(IOrderedEnumerable<ChartDataDto> summarizedData)
		{
			var hijackedType = new List<string> { "Type Of Hijacked", "Type" };
			var deserializeHijackedTypeDNS = new List<string>() { "Dns Hijacked" };
			var deserializeHijackedTypeHttp = new List<string>() { "Http Hijacked" };

			try
			{
				deserializeHijackedTypeDNS.Add(summarizedData.Sum(p => p.DnsHijackedCounts.DnsHijackedCount).ToString());
				deserializeHijackedTypeHttp.Add(summarizedData.Sum(p => p.HttpHijackedCounts.HttpHijackedCounts).ToString());
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return new List<List<string>> { hijackedType, deserializeHijackedTypeDNS, deserializeHijackedTypeHttp };
		}
		public List<List<string>> HijackedDestinationEach(IOrderedEnumerable<ChartDataDto> summarizedData)
		{

			var hijackedDestination = new List<string> { "Hijacked Destination", "Destination" };
			var deserializeHijackedDestination = new List<List<string>>();

			deserializeHijackedDestination.Add(hijackedDestination);

			try
			{
				if (summarizedData.FirstOrDefault().DnsHijackedCounts.DnsHijackedCount > 0)
				{
					var groupingDnsHijacked = summarizedData.Where(z => z.DnsHijackedCounts.HijackedToDestinationModel != null).SelectMany(p => p.DnsHijackedCounts.HijackedToDestinationModel.Select(k => new { k.Destination, k.DestinationCount })).ToList();
					var distinctKey = groupingDnsHijacked.Select(p => p.Destination).Distinct().ToList();

					foreach (var key in distinctKey)
					{
						var newList = new List<string>() { key };
						newList.Add(groupingDnsHijacked.Where(s => s.Destination.Equals(key)).Sum(p => p.DestinationCount).ToString());
						deserializeHijackedDestination.Add(newList.ToList());
					}
				}
				if (summarizedData.FirstOrDefault().HttpHijackedCounts.HttpHijackedCounts > 0)
				{
					var groupingHttpHijacked = summarizedData.Where(z => z.DnsHijackedCounts.HijackedToDestinationModel != null).SelectMany(p => p.HttpHijackedCounts.HttpHijackedDestinationModel).Select(s => new { s.RedirectedDestination, s.RedirectedCounts }).ToList();
					var distinctKey = groupingHttpHijacked.Select(p => p.RedirectedDestination).Distinct().ToList();

					foreach (var key in distinctKey)
					{
						var newList = new List<string>() { key };
						newList.Add(groupingHttpHijacked.Where(s => s.RedirectedDestination.Equals(key)).Sum(p => p.RedirectedCounts).ToString());
						deserializeHijackedDestination.Add(newList.ToList());
					}
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return deserializeHijackedDestination;
		}
		public List<List<string>> HijackedProvinceCountDns(IOrderedEnumerable<ChartDataDto> summarizedData)
		{
			var returnList = new List<List<string>>() { new List<string> { "Hijacked Province", "Dns Count" } };

			var provinceHijacked = summarizedData.Where(z => z.Province.ProvinceHijackedDestinationModel != null).SelectMany(s => s.Province.ProvinceHijackedDestinationModel.Select(p => new { p.Province, p.DnsCount })).ToList();

			try
			{
				var groupingProvince = provinceHijacked.Select(s => s.Province).Distinct().ToList();

				foreach (var province in groupingProvince)
				{
					var selectedProvince = provinceHijacked.Where(s => s.Province.Equals(province)).ToList();
					var hijackedProvince = new List<string>();
					hijackedProvince.Add(province);
					hijackedProvince.Add(selectedProvince.Sum(s => s.DnsCount).ToString());

					returnList.Add(hijackedProvince.ToList());
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return returnList;
		}
		public List<List<string>> HijackedProvinceCountHttp(IOrderedEnumerable<ChartDataDto> summarizedData)
		{
			var returnList = new List<List<string>>() { new List<string> { "Hijacked Province", "Http Count" } };
			try
			{
				var provinceHijacked = summarizedData.Where(z => z.Province.ProvinceHijackedDestinationModel != null).SelectMany(s => s.Province.ProvinceHijackedDestinationModel.Select(p => new { p.Province, p.HttpCount })).ToList();

				var groupingProvince = provinceHijacked.Select(s => s.Province).Distinct().ToList();


				foreach (var province in groupingProvince)
				{
					var hijackedProvince = new List<string>();
					var selectedProvince = provinceHijacked.Where(s => s.Province.Equals(province)).ToList();
					hijackedProvince.Add(province);
					hijackedProvince.Add(selectedProvince.Sum(s => s.HttpCount).ToString());

					returnList.Add(hijackedProvince.ToList());
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return returnList;
		}
		public List<List<string>> ResolutionAverage(IOrderedEnumerable<ChartDataDto> summarizedData)
		{
			var deserializedNsLookup = new List<string>() { "NsLookup" };
			var deserializedConnect = new List<string>() { "Connect" };
			var deserializedTTFB = new List<string>() { "Time To FIrst Byte" };
			var deserializedDownload = new List<string>() { "Download" };

			//Structuring data for chart.
			var dataDate = new List<string>() { "Date" };
			try
			{
				dataDate.AddRange(summarizedData.Select(s => s.DataHourCategory.ToString()).Distinct());

				var resolutionInfo = summarizedData.Where(z => z.ResolutionTimeModel.ResolutionTimeDestination != null).SelectMany(p => p.ResolutionTimeModel.ResolutionTimeDestination.Select(k => new { k.NsLookupTime, k.ConnectionTime, k.TimeToFirstByte, k.DownloadTime, k.ExecutionTime })).ToList();

				deserializedNsLookup.AddRange(resolutionInfo.GroupBy(z => new { z.ExecutionTime }).Select(p => Math.Round(p.Sum(k => k.NsLookupTime) / 3, 4).ToString()));
				deserializedConnect.AddRange(resolutionInfo.GroupBy(z => new { z.ExecutionTime }).Select(p => Math.Round(p.Sum(k => k.ConnectionTime) / 3, 4).ToString()));
				deserializedTTFB.AddRange(resolutionInfo.GroupBy(z => new { z.ExecutionTime }).Select(p => Math.Round(p.Sum(k => k.TimeToFirstByte) / 3, 4).ToString()));
				deserializedDownload.AddRange(resolutionInfo.GroupBy(z => new { z.ExecutionTime }).Select(p => Math.Round(p.Sum(k => k.DownloadTime) / 3, 4).ToString()));
			}
			catch(Exception ex)
			{
				log.Error(ex);
			}

			return new List<List<string>> { dataDate, deserializedNsLookup, deserializedConnect, deserializedTTFB, deserializedDownload };
		}
	}
}