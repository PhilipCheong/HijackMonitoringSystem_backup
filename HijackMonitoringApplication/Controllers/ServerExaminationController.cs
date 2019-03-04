using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using HijackMonitoringApplication.Resources;
using static System.Net.Mime.MediaTypeNames;
using HijackMonitoringApplication.ViewModel;

namespace HijackMonitoringApplication.Controllers
{
	public class ServerExaminationController : BaseController
	{
		SummarizedDataService summarizedDataService = new SummarizedDataService();

		// GET: ServerExamination
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public string GetResult(string serverName, string start, string end)
		{
			var multipleServer = new List<string>();
			var queryResults = new Dictionary<string, IOrderedEnumerable<SummarizedDataDto>>();
			var returnList = new List<ExpandoObject>();

			if (serverName.Contains(","))
			{
				multipleServer.AddRange(serverName.Split(','));
			}
			else
			{
				multipleServer.Add(serverName);
			}

			foreach (var server in multipleServer)
			{
				var serverResults = summarizedDataService.GetQueryResult(server, Convert.ToDateTime(start), Convert.ToDateTime(end)).OrderBy(s => s.HourCategory);
				queryResults.Add(server, serverResults);
			}

			if (queryResults.Any())
			{
				try
				{
					foreach (var server in queryResults)
					{
						dynamic returnData = new ExpandoObject();
						{
							try
							{
								var provinceData = this.ProvinceIspResult(server.Value);
								var ispData = this.IspResult(server.Value);
								var highestData = this.HighestResponseResults(server.Value);
								var over3Data = this.CountOver3Results(server.Value);
								var over5Data = this.CountOver5Results(server.Value);
								var over10Data = this.CountOver10Results(server.Value);
								var totalTest = this.TotalTest(server.Value);
								var summaryTable = this.SummaryTable(server.Value);

								returnData.ProvinceData = provinceData;
								returnData.IspData = ispData;
								returnData.HighestData = highestData;
								returnData.Over3Data = over3Data;
								returnData.Over5Data = over5Data;
								returnData.Over10Data = over10Data;
								returnData.TotalTest = totalTest;
								returnData.SummaryTable = summaryTable;
							}
							catch (Exception ex)
							{
								log.Error(ex);
							}
						}
						returnList.Add(returnData);
					}
				}
				catch (Exception ex)
				{
					log.Error(ex);
				}

			}
			return JsonConvert.SerializeObject(returnList);
		}
		public List<List<string>> ProvinceIspResult(IOrderedEnumerable<SummarizedDataDto> summarizedData)
		{
			var deserializedBeijingTelecom = new List<string>() { "Beijing Telecom" };
			var deserializedBeijingUnicom = new List<string>() { "Beijing Unicom" };
			var deserializedBeijingMobile = new List<string>() { "Beijing Mobile" };
			var deserializedChongqingTelecom = new List<string>() { "Chongqing Telecom" };
			var deserializedChongqingUnicom = new List<string>() { "Chongqing Unicom" };
			var deserializedChongqingMobile = new List<string>() { "Chongqing Mobile" };
			var deserializedShanghaiTelecom = new List<string>() { "Huzhou Telecom" };
			var deserializedShanghaiUnicom = new List<string>() { "Huzhou Unicom" };
			var deserializedShanghaiMobile = new List<string>() { "Huzhou Mobile" };
			var deserializedGuangdongTelecom = new List<string>() { "Guangdong Telecom" };
			var deserializedGuangdongUnicom = new List<string>() { "Guangdong Unicom" };
			var deserializedGuangdongMobile = new List<string>() { "Guangdong Mobile" };


            var testing = summarizedData.Where(k => k.Province.Equals("huzhou") && (k.Isp.Equals("unicom") || k.Isp.Equals("mobile"))).ToList();


            var dataDate = new List<string>() { "Date" };

			try
			{
				//Structuring data for chart.
				dataDate.AddRange(summarizedData.Select(s => s.HourCategory.ToLocalTime().ToString()).Distinct());
                foreach (var date in dataDate.Skip(1))
                {
                    deserializedBeijingTelecom.Add(summarizedData.Any(k => k.Province.Equals("beijing") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("beijing") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedBeijingUnicom.Add(summarizedData.Any(k => k.Province.Equals("beijing") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("beijing") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedBeijingMobile.Add(summarizedData.Any(k => k.Province.Equals("beijing") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("beijing") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedShanghaiTelecom.Add(summarizedData.Any(k => k.Province.Equals("huzhou") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("huzhou") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedShanghaiUnicom.Add(summarizedData.Any(k => k.Province.Equals("huzhou") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("huzhou") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedShanghaiMobile.Add(summarizedData.Any(k => k.Province.Equals("huzhou") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("huzhou") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedChongqingTelecom.Add(summarizedData.Any(k => k.Province.Equals("chongqing") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("chongqing") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedChongqingUnicom.Add(summarizedData.Any(k => k.Province.Equals("chongqing") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("chongqing") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedChongqingMobile.Add(summarizedData.Any(k => k.Province.Equals("chongqing") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("chongqing") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedGuangdongTelecom.Add(summarizedData.Any(k => k.Province.Equals("guangdong") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("guangdong") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedGuangdongUnicom.Add(summarizedData.Any(k => k.Province.Equals("guangdong") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("guangdong") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                    deserializedGuangdongMobile.Add(summarizedData.Any(k => k.Province.Equals("guangdong") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("guangdong") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).DownloadTime.ToString() : "-0.1");
                }
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return new List<List<string>> { dataDate, deserializedBeijingTelecom, deserializedBeijingUnicom, deserializedBeijingMobile, deserializedShanghaiTelecom, deserializedShanghaiUnicom, deserializedShanghaiMobile, deserializedChongqingTelecom,
				deserializedChongqingUnicom, deserializedChongqingMobile, deserializedGuangdongTelecom, deserializedGuangdongUnicom, deserializedGuangdongMobile };
		}
		public List<List<string>> IspResult(IOrderedEnumerable<SummarizedDataDto> summarizedData)
		{
			var deserializeIspOne = new List<string>() { "China Telecom" };
			var deserializeIspTwo = new List<string>() { "China Unicom" };
			var deserializeIspThree = new List<string>() { "China Mobile" };
			var dataDate = new List<string>() { "Date" };

			try
			{
				//Structuring data for chart.
				dataDate.AddRange(summarizedData.Select(s => s.HourCategory.ToLocalTime().ToString()).Distinct());
                foreach (var date in dataDate)
                {
                    deserializeIspOne.AddRange(summarizedData.Where(k => k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).GroupBy(s => s.HourCategory).Select(p => p.Average(z => z.DownloadTime).ToString()));
                    deserializeIspTwo.AddRange(summarizedData.Where(k => k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).GroupBy(s => s.HourCategory).Select(p => p.Average(z => z.DownloadTime).ToString()));
                    deserializeIspThree.AddRange(summarizedData.Where(k => k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).GroupBy(s => s.HourCategory).Select(p => p.Average(z => z.DownloadTime).ToString()));
                }
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return new List<List<string>> { dataDate, deserializeIspOne, deserializeIspTwo, deserializeIspThree };
		}
		public List<List<string>> HighestResponseResults(IOrderedEnumerable<SummarizedDataDto> summarizedData)
		{
			var deserializedBeijingTelecom = new List<string>() { "Beijing Telecom" };
			var deserializedBeijingUnicom = new List<string>() { "Beijing Unicom" };
			var deserializedBeijingMobile = new List<string>() { "Beijing Mobile" };
			var deserializedChongqingTelecom = new List<string>() { "Chongqing Telecom" };
			var deserializedChongqingUnicom = new List<string>() { "Chongqing Unicom" };
			var deserializedChongqingMobile = new List<string>() { "Chongqing Mobile" };
			var deserializedShanghaiTelecom = new List<string>() { "Huzhou Telecom" };
			var deserializedShanghaiUnicom = new List<string>() { "Huzhou Unicom" };
			var deserializedShanghaiMobile = new List<string>() { "Huzhou Mobile" };
			var deserializedGuangdongTelecom = new List<string>() { "Guangdong Telecom" };
			var deserializedGuangdongUnicom = new List<string>() { "Guangdong Unicom" };
			var deserializedGuangdongMobile = new List<string>() { "Guangdong Mobile" };


			var dataDate = new List<string>() { "Date" };

			try
			{
				//Structuring data for chart.
				dataDate.AddRange(summarizedData.Select(s => s.HourCategory.ToLocalTime().ToString()).Distinct());
                foreach (var date in dataDate.Skip(1))
                {
                    deserializedBeijingTelecom.Add(summarizedData.Any(k => k.Province.Equals("beijing") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("beijing") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedBeijingUnicom.Add(summarizedData.Any(k => k.Province.Equals("beijing") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("beijing") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedBeijingMobile.Add(summarizedData.Any(k => k.Province.Equals("beijing") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("beijing") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedShanghaiTelecom.Add(summarizedData.Any(k => k.Province.Equals("huzhou") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("huzhou") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedShanghaiUnicom.Add(summarizedData.Any(k => k.Province.Equals("huzhou") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("huzhou") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedShanghaiMobile.Add(summarizedData.Any(k => k.Province.Equals("huzhou") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("huzhou") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedChongqingTelecom.Add(summarizedData.Any(k => k.Province.Equals("chongqing") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("chongqing") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedChongqingUnicom.Add(summarizedData.Any(k => k.Province.Equals("chongqing") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("chongqing") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedChongqingMobile.Add(summarizedData.Any(k => k.Province.Equals("chongqing") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("chongqing") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedGuangdongTelecom.Add(summarizedData.Any(k => k.Province.Equals("guangdong") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("guangdong") && k.Isp.Equals("telecom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedGuangdongUnicom.Add(summarizedData.Any(k => k.Province.Equals("guangdong") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("guangdong") && k.Isp.Equals("unicom") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                    deserializedGuangdongMobile.Add(summarizedData.Any(k => k.Province.Equals("guangdong") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date))?
                        summarizedData.First(k => k.Province.Equals("guangdong") && k.Isp.Equals("mobile") && k.HourCategory.ToLocalTime().ToString().Equals(date)).HighestResponse.ToString() : "-1");
                }
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return new List<List<string>> { dataDate, deserializedBeijingTelecom, deserializedBeijingUnicom, deserializedBeijingMobile, deserializedShanghaiTelecom, deserializedShanghaiUnicom, deserializedShanghaiMobile, deserializedChongqingTelecom,
				deserializedChongqingUnicom, deserializedChongqingMobile, deserializedGuangdongTelecom, deserializedGuangdongUnicom, deserializedGuangdongMobile };
		}
		public List<List<string>> CountOver3Results(IOrderedEnumerable<SummarizedDataDto> summarizedData)
		{
			var returnList = new List<List<string>>() { new List<string> { "Province Isp", "Count Over 3 seconds" } };

			var groupedProvince = summarizedData.Where(z => z.Province != null).GroupBy(p => new { p.Province, p.Isp }).ToList();

			try
			{
				foreach (var data in groupedProvince)
				{
					var over3 = new List<string>();
					over3.Add($"{data.Key.Province} {data.Key.Isp}");
					over3.Add(data.Sum(s => s.CountOver3).ToString());

					returnList.Add(over3.ToList());
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return returnList;
		}
		public List<List<string>> CountOver5Results(IOrderedEnumerable<SummarizedDataDto> summarizedData)
		{
			var returnList = new List<List<string>>() { new List<string> { "Province Isp", "Count Over 5 seconds" } };

			var groupedProvince = summarizedData.Where(z => z.Province != null).GroupBy(p => new { p.Province, p.Isp }).ToList();

			try
			{
				foreach (var data in groupedProvince)
				{
					var over5 = new List<string>();
					over5.Add($"{data.Key.Province} {data.Key.Isp}");
					over5.Add(data.Sum(s => s.CountOver5).ToString());

					returnList.Add(over5.ToList());
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return returnList;
		}
		public List<List<string>> CountOver10Results(IOrderedEnumerable<SummarizedDataDto> summarizedData)
		{
			var returnList = new List<List<string>>() { new List<string> { "Province Isp", "Count Over 10 seconds" } };

			var groupedProvince = summarizedData.Where(z => z.Province != null).GroupBy(p => new { p.Province, p.Isp }).ToList();

			try
			{
				foreach (var data in groupedProvince)
				{
					var over10 = new List<string>();
					over10.Add($"{data.Key.Province} {data.Key.Isp}");
					over10.Add(data.Sum(s => s.CountOver10).ToString());

					returnList.Add(over10.ToList());
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return returnList;
		}
		public List<List<string>> TotalTest(IOrderedEnumerable<SummarizedDataDto> summarizedData)
		{
			var returnList = new List<List<string>>() { new List<string> { "Province Isp", "TotalTest" } };

			var groupedProvince = summarizedData.Where(z => z.Province != null).GroupBy(p => new { p.Province, p.Isp }).ToList();

			try
			{
				foreach (var data in groupedProvince)
				{
					var over10 = new List<string>();
					over10.Add($"{data.Key.Province} {data.Key.Isp}");
					over10.Add(data.Sum(s => s.TotalTest).ToString());

					returnList.Add(over10.ToList());
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return returnList;
		}
		public ExpandoObject SummaryTable(IOrderedEnumerable<SummarizedDataDto> summarizedData)
		{
			var returnList = new List<List<string>>();

			var groupedProvince = summarizedData.Where(z => z.Province != null).GroupBy(p => new { p.Province, p.Isp }).ToList();

			var distinctTime = summarizedData.Select(s => s.HourCategory).Distinct().ToList();

			dynamic returnData = new ExpandoObject();

			try
			{
				

				foreach (var data in groupedProvince)
				{
					var listValue = new List<string>();
					listValue.Add($"{data.Key.Province} {data.Key.Isp}");
					foreach(var time in distinctTime)
					{
						var currentData = data.Where(s => s.HourCategory.Equals(time));
						if (currentData.Any())
						{
							listValue.Add($"{Math.Round(currentData.FirstOrDefault().DownloadTime, 2).ToString()} | {(currentData.Sum(s => s.CountOver3) + currentData.Sum(s => s.CountOver5) + currentData.Sum(s => s.CountOver10)).ToString()}");
						}
						else
						{
							listValue.Add($"- | 0");
						}
					}

					returnList.Add(listValue.ToList());
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}

			returnData.DistinctTime = distinctTime.Select(s=>s.ToLocalTime().TimeOfDay);
			returnData.SummaryValue = returnList;

			return returnData;
		}


		[HttpPost]
		public string ExportToExcel(string serverCName, string startDate, string endDate)
		{
			var multipleServer = new List<string>();
			var queryResults = new Dictionary<string, IOrderedEnumerable<SummarizedDataDto>>();
			var returnList = new List<ExpandoObject>();

			if (serverCName.Contains(","))
			{
				multipleServer.AddRange(serverCName.Split(','));
			}
			else
			{
				multipleServer.Add(serverCName);
			}

			foreach (var server in multipleServer)
			{
				var serverResults = summarizedDataService.GetQueryResult(server, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate)).OrderBy(s => s.HourCategory);
				queryResults.Add(server, serverResults);
			}

			var rows = new Dictionary<string, List<string>>();

			var ExcelFile = string.Empty;

			var exportProvinceData = new List<List<List<string>>>();
			var exportIspData = new List<List<List<string>>>();
			var exportHighestData = new List<List<List<string>>>();
			var exportOver3Data = new List<List<List<string>>>();
			var exportOver5Data = new List<List<List<string>>>();
			var exportOver10Data = new List<List<List<string>>>();
			var exportTestData = new List<List<List<string>>>();


			foreach (var x in queryResults)
			{
				var provinceData = this.ProvinceIspResult(x.Value);
				var ispData = this.IspResult(x.Value);
				var highestData = this.HighestResponseResults(x.Value);
				var over3Data = this.CountOver3Results(x.Value);
				var over5Data = this.CountOver5Results(x.Value);
				var over10Data = this.CountOver10Results(x.Value);
				var totalTest = this.TotalTest(x.Value);

				exportProvinceData.Add(provinceData);
				exportIspData.Add(ispData);
				exportHighestData.Add(highestData);
				exportOver3Data.Add(over3Data);
				exportOver5Data.Add(over5Data);
				exportOver10Data.Add(over10Data);
				exportTestData.Add(totalTest);

			}
			var exportData = new List<List<List<List<string>>>>
			{
				exportProvinceData,
				exportIspData,
				exportHighestData,
				exportOver3Data,
				exportOver5Data,
				exportOver10Data,
				exportTestData
			};
			ExportFunction _documentExport = new ExportFunction();
			ExcelFile = _documentExport.ExcelExportFunction(serverCName, exportData);

			return JsonConvert.SerializeObject(new { filedownload = ExcelFile });
			//return ExcelFile;
		}
		public FileResult DownloadReport(string downloadfile)
		{
			var servePath = Server.MapPath("~");

			byte[] filebytes = System.IO.File.ReadAllBytes(servePath + "\\" + downloadfile);
			System.IO.File.Delete(servePath + "\\" + downloadfile);
			return File(filebytes, Application.Octet, downloadfile.Substring(downloadfile.IndexOf("-") + 1));
			//return ExcelFile;
		}

	}
}