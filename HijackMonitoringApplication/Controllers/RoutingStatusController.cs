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
            var allResult = _serverPerformanceService.GetAll().Where(s => !string.IsNullOrEmpty(s.Province)).OrderByDescending(p => p.ModifyDate);

            var servers = allResult.Select(s => new { s.Province, s.Isp }).Distinct().ToList();

            var returnList = new List<ServerStatusViewModel>();

            foreach (var status in servers)
            {
                var lastData = allResult.FirstOrDefault(s => s.Province.Equals(status.Province) && s.Isp.Equals(status.Isp));
                var serverStatus = new ServerStatusViewModel()
                {
                    Province = status.Province,
                    Isp = status.Isp,
                    LastExecution = lastData.ModifyDate.Value.ToLocalTime(),
                    Status = lastData.ModifyDate.Value > DateTime.UtcNow.AddMinutes(-5) ? "Success" : "Failed"
                };
                returnList.Add(serverStatus);
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
            now = now.AddHours(-100).AddMinutes(-now.Minute).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);

            var monitoringDomain = _domainExaminationService.Find(s => s.Status == 1 && s.ToStartTime <= now && s.ToEndTime >= now && s.TestType.Contains("2"));

            var alert = _alertInFiveService.Find(s => s.CreateDate.Value >= now);

            var returnData = new List<AlertModel>();

            if (monitoringDomain.Any())
            {
                var domainData = _performanceDataService.Find(s => s.TestTime >= now);

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
