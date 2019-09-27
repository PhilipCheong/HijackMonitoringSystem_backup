using HijackMonitoringApplication.BusinessLayer;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HijackMonitoringApplication.Controllers
{
    [Authorize(Roles = "Toffstech_Admin")]
    public class RoutingStatusController : BaseController
    {
        ServerPerformanceService _serverPerformanceService = new ServerPerformanceService();
        DataForAnalysisService _dataForAnalysisService = new DataForAnalysisService();
        AlertInFiveService _alertInFiveService = new AlertInFiveService();
        PerformanceDataService _performanceDataService = new PerformanceDataService();
        DomainExaminationService _domainExaminationService = new DomainExaminationService();

        // GET: RoutingSatus
        public ActionResult Index()
        {
            var nullRecord = _serverPerformanceService.Find(s => s.Province == null || s.Isp == null);

            var allData = _serverPerformanceService.GetAll();

            if (nullRecord.Any())
            {
                foreach (var delete in nullRecord)
                {
                    _serverPerformanceService.Remove(delete.Id);
                }
                DataForAnalysisService _dataForAnalysisService = new DataForAnalysisService();
                var data = _dataForAnalysisService.Find(s => s.Province == null || s.Isp == null);
                if (data.Any())
                {
                    foreach (var dat in data)
                    {
                        _dataForAnalysisService.Remove(dat.Id);
                    }
                }
                SummarizedDataService _summarizedDataService = new SummarizedDataService();
                var summarized = _summarizedDataService.Find(s => s.Province == null || s.Isp == null);
                if (summarized.Any())
                {
                    foreach (var sum in summarized)
                    {
                        _summarizedDataService.Remove(sum.Id);
                    }
                }
            }
            var allResult = allData.Where(s => !string.IsNullOrEmpty(s.Province)).OrderByDescending(p => p.ModifyDate);

            string[] province = new string[4] { "Beijing", "Chongqing", "Guangdong", "Huzhou" };
            string[] isp = new string[3] { "Telecom", "Mobile", "Unicom" };

            var returnList = new List<ServerStatusViewModel>();

            for (var i = 0; i < province.Count(); i++)
            {
                for (var p = 0; p < isp.Count(); p++)
                {

                    var lastData = allResult.FirstOrDefault(s => s.Province.Equals(province[i]) && s.Isp.Equals(isp[p]));
                    if (lastData != null)
                    {
                        var serverStatus = new ServerStatusViewModel()
                        {
                            Province = province[i],
                            Isp = isp[p],
                            LastExecution = lastData.ModifyDate.Value.ToLocalTime(),
                            Status = lastData.ModifyDate.Value > DateTime.UtcNow.AddMinutes(-5) ? "Success" : "Failed"
                        };
                        returnList.Add(serverStatus);
                    }
                    else
                    {

                        var serverStatus = new ServerStatusViewModel()
                        {
                            Province = province[i],
                            Isp = isp[p],
                            LastExecution = new DateTime(1970, 1, 1, 0, 0, 0),
                            Status = "Failed"
                        };
                        returnList.Add(serverStatus);

                    }
                }
            }

            return View(returnList.OrderBy(s => s.Province).ThenBy(p => p.Status).ToList());
        }

        public ActionResult RealtimeResults()
        {
            var now = DateTime.UtcNow;

            now = now.AddMinutes(-now.Minute).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);

            var lifeCount = DateTime.UtcNow;

            var allResults = _dataForAnalysisService.Find(s => s.CreateDate.Value >= now).GroupBy(s => s.ServerCName).ToList();

            var returnList = new List<RealtimeServerSatus>();

            foreach (var result in allResults)
            {
                if (result.Any(s => s.CreateDate.Value >= lifeCount.AddMinutes(-3)))
                {
                    returnList.Add(new RealtimeServerSatus()
                    {
                        ServerCname = result.Key,
                        LastExecution = result.OrderBy(s => s.CreateDate).Last().CreateDate.Value.ToLocalTime(),
                        CountOnOne = result.Count(s => s.DownloadTime >= 1 && s.DownloadTime < 2),
                        CountOnTwo = result.Count(s => s.DownloadTime >= 2 && s.DownloadTime < 3),
                        CountOnThree = result.Count(s => s.DownloadTime >= 3),
                        DownloadTime = Math.Round(result.OrderBy(s => s.CreateDate).Last().DownloadTime * 1000, 0),
                        Status = "Success"
                    });
                }
                else
                {
                    returnList.Add(new RealtimeServerSatus()
                    {
                        ServerCname = result.Key,
                        LastExecution = result.OrderBy(s => s.CreateDate).Last().CreateDate.Value.ToLocalTime(),
                        CountOnOne = result.Count(s => s.DownloadTime >= 1 && s.DownloadTime < 2),
                        CountOnTwo = result.Count(s => s.DownloadTime >= 2 && s.DownloadTime < 3),
                        CountOnThree = result.Count(s => s.DownloadTime >= 3),
                        DownloadTime = Math.Round(result.OrderBy(s => s.CreateDate).Last().DownloadTime * 1000, 0),
                        Status = "Failed"
                    });
                }
            }

            returnList = returnList.OrderByDescending(z => z.Status).ThenByDescending(s => s.CountOnOne + s.CountOnTwo + s.CountOnThree).ToList();

            return View(returnList);

        }

        public ActionResult RealtimeAlert()
        {
            var now = DateTime.UtcNow;
            dynamic returnList = new ExpandoObject();
            now = now.AddHours(-2).AddMinutes(-now.Minute).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);

            var monitoringDomain = _domainExaminationService.Find(s => s.Status == 1 && s.ToStartTime <= now && s.ToEndTime >= now && s.TestType.Contains("2"));

            var alert = _alertInFiveService.Find(s => s.CreateDate.Value >= now);

            var returnData = new List<AlertModel>();

            if (monitoringDomain.Any())
            {
                var domainData = _performanceDataService.FindWithoutImage(now);

                if (domainData.Any())
                {
                    var distinctDomain = domainData.Select(s => s.Url).Distinct().ToList();

                    foreach (var domain in monitoringDomain)
                    {
                        var groupDomain = domainData.Where(s => s.Url.Contains(domain.Domain)).OrderByDescending(s => s.TestTime).GroupBy(s => s.Province).ToList();

                        foreach (var province in groupDomain)
                        {
                            if (province.Count() > 1)
                            {
                                var average = province.Skip(1).Average(s => s.Response);

                                var lastDownload = province.First().Response;

                                var percentage = Math.Round((lastDownload - average) / average * 100, 0);

                                if (percentage > 50)
                                {
                                    returnData.Add(new AlertModel()
                                    {
                                        Id = province.First().Id,
                                        ServerCname = domain.Domain,
                                        ProvinceIsp = province.Key,
                                        AlertDetail = new List<AlertInformation>()
                                {
                                    new AlertInformation()
                                    {
                                        AlertType = 1,
                                        DownloadSpeed = province.First().Response.ToString() + "ms",
                                        OccuredDate = province.First().TestTime.ToLocalTime().ToString(),
                                        Average = average.ToString() + "ms",
                                        Percentage = percentage.ToString() +"%"
                                    }
                                }
                                    });

                                }
                            }
                        }
                    }
                    returnList.Domain = returnData.ToList();
                }
            }
            else
            {
                returnData.Add(new AlertModel()
                {
                    ServerCname = "Non-Alert",
                    ProvinceIsp = "Non_Alert",
                    AlertDetail = new List<AlertInformation>()
                });
                returnList.Domain = returnData.ToList();
            }
            returnData.Clear();
            if (alert.Any())
            {
                var groupingAlert = alert.GroupBy(s => new { s.ServerCName, s.Province, s.Isp }).ToList();

                foreach (var group in groupingAlert)
                {
                    var newData = new AlertModel()
                    {
                        ServerCname = group.Key.ServerCName,
                        ProvinceIsp = $"{group.Key.Province} {group.Key.Isp}"
                    };

                    var alertList = new List<AlertInformation>();

                    foreach (var info in group.Select(s => s.AlertInfo))
                    {
                        var deserializedData = JsonConvert.DeserializeObject<List<AlertInformation>>(info);

                        foreach (var data in deserializedData)
                        {
                            data.OccuredDate = Convert.ToDateTime(data.OccuredDate).ToLocalTime().ToString();
                            alertList.Add(data);
                        }
                    }

                    newData.AlertDetail = alertList;
                    returnData.Add(newData);
                }
            }
            else
            {
                returnData.Add(new AlertModel()
                {
                    ServerCname = "Non-Alert",
                    ProvinceIsp = "Non_Alert",
                    AlertDetail = new List<AlertInformation>()
                });

            }
            returnList.Server = returnData.ToList();
            return View(returnList);
        }
        public string ExceededDetail(string serverCname)
        {
            var now = DateTime.UtcNow;
            now = now.AddMinutes(-now.Minute).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
            var allData = _dataForAnalysisService.Find(s => s.ServerCName.Equals(serverCname) && s.CreateDate.Value >= now);

            var cData = allData.GroupBy(p => new { p.Province, p.Isp });

            var returnList = new List<RealtimeInformation>();

            foreach (var data in cData)
            {
                returnList.Add(new RealtimeInformation()
                {
                    ProvinceIsp = $"{data.Key.Province} {data.Key.Isp}",
                    CountOnOne = data.Count(s => s.DownloadTime >= 1 && s.DownloadTime < 2),
                    CountOnTwo = data.Count(s => s.DownloadTime >= 2 && s.DownloadTime < 3),
                    CountOnThree = data.Count(s => s.DownloadTime >= 3),
                });
            }

            dynamic returnData = new ExpandoObject();
            returnData = returnList;

            return JsonConvert.SerializeObject(returnData);
        }
    }
}
