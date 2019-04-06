using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HijackMonitoringApplication.Controllers
{
    public class PerformancesResultsController : BaseController
    {
        readonly PerformanceDataService performanceDataService = new PerformanceDataService();
        
        public ActionResult Index(string domainName, string startDate, string endDate)
        {

            return View();
        }

        public string GetResults(string domainName, string startDate, string endDate)
        {
            var query = performanceDataService.GetAll().Where(s => s.Url.Equals(domainName) && s.CreateDate.Value >= Convert.ToDateTime(startDate) && s.CreateDate.Value < Convert.ToDateTime(endDate)).ToList();

            return JsonConvert.SerializeObject(query);
        }

        public string DetailModelData(string province, string dateTime, string domainName)
        {
            var query = performanceDataService.FindWithoutImage(domainName, Convert.ToDateTime(dateTime)).ToList();

            //var currentQuery = query.Where(s => s.TestTime.ToLocalTime() == Convert.ToDateTime(dateTime)).ToList();

            return JsonConvert.SerializeObject(query.Where(s => s.Province.Contains(province)));
        }

        public ActionResult GetWaterFall(string id)
        {

            ViewBag.id = id;
            return View();
        }

        public string WaterFallChart(string id)
        {

            var queryResults = performanceDataService.GetById(id);

            dynamic returnData = new ExpandoObject();

            var orderData = queryResults;

            var urlList = new List<string>() { };
            var mimeType = new List<string>() { };
            var statusCode = new List<string>() { };
            var ipAddress = new List<string>() { };
            var startedTime = new List<string>() { };
            var dnsList = new List<string>() { };
            var connectList = new List<string>() { };
            var sendList = new List<string>() { };
            var TTFBlist = new List<string>() { };
            var downloadList = new List<string>() { };
            var requestList = new List<string>() { };


            requestList.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.RequestTime.ToString()));
            urlList.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.Url.ToString()));
            mimeType.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.MimeType.ToString()));
            statusCode.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.Status.ToString()));
            ipAddress.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.RemoteIPAddress.ToString()));
            startedTime.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.RequestTime.ToString()));
            dnsList.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.Dns.ToString()));
            connectList.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.Connect.ToString()));
            sendList.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.Send.ToString()));
            TTFBlist.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.TTFB.ToString()));
            downloadList.AddRange(orderData.PerformancesData.OrderBy(p => p.RequestTime).Select(s => s.Download.ToString()));

            //for(var i =1; i < orderData.FirstOrDefault().PerformancesData.Count(); i++)
            //{

            //	var dataInput = $"{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Url[i]}:{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().RequestTime.ToString()[i]}";
            //	startedTime.Add(dataInput);
            //	dataInput = $"{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Url[i]}:{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Dns.ToString()[i]}";
            //	dnsList.Add(dataInput);
            //	dataInput = $"{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Url[i]}:{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Connect.ToString()[i]}";
            //	connectList.Add(dataInput);
            //	dataInput = $"{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Url[i]}:{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Send.ToString()[i]}";
            //	sendList.Add(dataInput);
            //	dataInput = $"{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Url[i]}:{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().TTFB.ToString()[i]}";
            //	TTFBlist.Add(dataInput);
            //	dataInput = $"{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Url[i]}:{orderData.FirstOrDefault().PerformancesData.FirstOrDefault().Download.ToString()[i]}";
            //	downloadList.Add(dataInput);
            //}

            returnData.Url = urlList;
            returnData.StartedTime = startedTime;
            returnData.Dns = dnsList;
            returnData.Connect = connectList;
            returnData.Send = sendList;
            returnData.TTFB = TTFBlist;
            returnData.Download = downloadList;
            returnData.Response = orderData.Response;
            returnData.MimeType = mimeType;
            returnData.Status = statusCode;
            returnData.RemoteIPAddress = ipAddress;
            returnData.RequestTime = requestList;


            return JsonConvert.SerializeObject(returnData);
        }

        public string PlotWaterFallChart(string id)
        {
            var queryResults = performanceDataService.GetById(id);

            string image = string.Format("data:image/png;base64,{0}", queryResults.Image);

            dynamic returnData = new ExpandoObject();

            returnData.Chart = ChromeWaterFall(queryResults);
            returnData.Title = ChromeWaterFallTitle(queryResults);
            //returnData.Image = image;
            returnData.Image = image;
            return JsonConvert.SerializeObject(returnData);

        }

        public dynamic ChromeWaterFall(PerformanceDataDto queryResults)
        {
            var duplicateCheck = queryResults.PerformancesData.Distinct().ToList();

            foreach (var data in duplicateCheck)
            {
                var query = queryResults.PerformancesData.Where(s => s.Url.Equals(data.Url)).ToList();

                if (query.Count > 1)
                {
                    for (var i = 1; i < query.Count; i++)
                    {
                        for (var z = 0; z < i; z++)
                        {
                            query[i].Url = query[i].Url.Insert(query[i].Url.Count(), " ");
                        }
                    }
                }
            }

            string[] dataLabel = new string[5] { "DNS", "Connect", "Send", "TTFB", "Download" };

            dynamic returnData = new ExpandoObject();

            dynamic chartData = new ExpandoObject();
            chartData = queryResults;

            dynamic objectForChart = new ExpandoObject();

            objectForChart.cols = new List<Object>();
            objectForChart.cols.Add(new { type = "string", id = "url" });
            objectForChart.cols.Add(new { type = "string", id = "name" });
            objectForChart.cols.Add(new { type = "string", role = "style" });
            objectForChart.cols.Add(new { type = "number", id = "start" });
            objectForChart.cols.Add(new { type = "number", id = "end" });


            objectForChart.rows = new List<Object>();

            foreach (var data in queryResults.PerformancesData)
            {
                decimal[] dataValue = new decimal[5] { data.Dns, data.Connect, data.Send, data.TTFB, data.Download };
                string status = data.Status.StartsWith("2") || data.Status.StartsWith("3") || data.Status.StartsWith("1") ? "null" : "error";

                for (var i = 0; i < 5; i++)
                {

                    if (i < 1)
                    {
                        string label = dataLabel[i];
                        long response = Convert.ToInt64(data.RequestTime + dataValue[i]);


                        var cItem = new List<Object>
                        {
                            new { v = data.Url },
                            new { v = label },
                            new { v = status },
                            new { v = data.RequestTime},
                            new { v = response }
                        };
                        objectForChart.rows.Add(new { c = cItem });

                    }
                    else
                    {
                        var requestTime = RequestTimeRestructure(dataValue, i, data.RequestTime);
                        string label = dataLabel[i];
                        long response = Convert.ToInt64(requestTime + dataValue[i]);

                        var cItem = new List<Object>
                        {
                            new { v = data.Url },
                            new { v = label },
                            new { v = status },
                            new { v = requestTime},
                            new { v = response }
                        };
                        objectForChart.rows.Add(new { c = cItem });
                    }

                }

            }

            returnData.ObjectForChart = objectForChart;
            returnData.ChartData = chartData;

            return returnData;
        }

        public WaterfallTitle ChromeWaterFallTitle(PerformanceDataDto queryResults)
        {
            var waterTitle = new WaterfallTitle()
            {
                Url = queryResults.Url,
                Response = queryResults.Response.ToString(),
                FailedCount = queryResults.PerformancesData.Count(s => !s.Status.StartsWith("1") && !s.Status.StartsWith("2") && !s.Status.StartsWith("3")).ToString()
            };


            return waterTitle;
        }

        public long RequestTimeRestructure(decimal[] dataValue, int count, decimal requestTime)
        {
            long result = Convert.ToInt64(requestTime);
            for (var i = 0; i < count; i++)
            {
                result += Convert.ToInt32(dataValue[i]);
            }

            return Convert.ToInt64(result);
        }

        public string PlotCharts(string domainName, string startDate, string endDate)
        {
            var domainList = new List<string>();
            var returnList = new List<ExpandoObject>();


            if (domainName.Contains(","))
            {
                domainList = domainName.Split(',').ToList();
            }
            else
            {
                domainList.Add(domainName);
            }

            foreach (var domain in domainList)
            {
                dynamic returnObject = new ExpandoObject();
                returnObject.ResponseData = ResponseLineChart(domain, startDate, endDate);
                returnList.Add(returnObject);
            }
            return JsonConvert.SerializeObject(returnList);
        }

        public List<List<List<string>>> ResponseLineChart(string domainName, string startDate, string endDate)
        {
            var query = performanceDataService.FindWithoutImage(domainName, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate)).ToList();


            var dateTime = new List<string>() { "Date Time" };
            dateTime.AddRange(query.Select(s => s.TestTime.ToLocalTime().ToString()).Distinct());

            //var results = new List<string>() { query.First().Url };
            //results.AddRange(query.OrderBy(s => s.CreateDate).Select(s => s.Response.ToString().Contains(".") ? s.Response.ToString().Remove(s.Response.ToString().IndexOf(".")) : s.Response.ToString()));

            var beijing = new List<string>() { "Beijing" };
            var chongqing = new List<string>() { "Chongqing" };
            var guangdong = new List<string>() { "Guangdong" };
            var huzhou = new List<string>() { "Huzhou" };

            foreach (var time in dateTime.Skip(1))
            {
                var beijingData = query.Where(s => s.Province.Contains("Beijing") && s.TestTime.ToLocalTime().ToString().Equals(time)).ToList();
                var chongqingData = query.Where(s => s.Province.Contains("Chongqing") && s.TestTime.ToLocalTime().ToString().Equals(time)).ToList();
                var guangdongData = query.Where(s => s.Province.Contains("Guangdong") && s.TestTime.ToLocalTime().ToString().Equals(time)).ToList();
                var huzhouData = query.Where(s => s.Province.Contains("Huzhou") && s.TestTime.ToLocalTime().ToString().Equals(time)).ToList();

                beijing.Add(beijingData.Average(s => s.Response).ToString());
                chongqing.Add(chongqingData.Average(s => s.Response).ToString());
                guangdong.Add(guangdongData.Average(s => s.Response).ToString());
                huzhou.Add(huzhouData.Average(s => s.Response).ToString());

            }

            //beijing.AddRange(query.Where(z => z.Province.Contains("Beijing")).OrderBy(s => s.TestTime).Select(s => s.Response.ToString().Contains(".") ? s.Response.ToString().Remove(s.Response.ToString().IndexOf(".")) : s.Response.ToString()));
            //chongqing.AddRange(query.Where(z => z.Province.Contains("Chongqing")).OrderBy(s => s.TestTime).Select(s => s.Response.ToString().Contains(".") ? s.Response.ToString().Remove(s.Response.ToString().IndexOf(".")) : s.Response.ToString()));
            //guangdong.AddRange(query.Where(z => z.Province.Contains("Guangdong")).OrderBy(s => s.TestTime).Select(s => s.Response.ToString().Contains(".") ? s.Response.ToString().Remove(s.Response.ToString().IndexOf(".")) : s.Response.ToString()));
            //huzhou.AddRange(query.Where(z => z.Province.Contains("Huzhou")).OrderBy(s => s.TestTime).Select(s => s.Response.ToString().Contains(".") ? s.Response.ToString().Remove(s.Response.ToString().IndexOf(".")) : s.Response.ToString()));

            var returnData = new List<List<List<string>>>();

            returnData.Add(new List<List<string>>() { dateTime, beijing });
            returnData.Add(new List<List<string>>() { dateTime, chongqing });
            returnData.Add(new List<List<string>>() { dateTime, guangdong });
            returnData.Add(new List<List<string>>() { dateTime, huzhou });

            return returnData;
        }

        public List<List<string>> AvailablityCharts(string domainName, string startDate, string endDate)
        {
            var query = performanceDataService.GetAll().Where(s => s.Url.Equals(domainName) && s.CreateDate.Value >= Convert.ToDateTime(startDate) && s.CreateDate.Value < Convert.ToDateTime(endDate)).ToList();


            var dateTime = new List<string>() { "Date Time" };
            dateTime.AddRange(query.OrderBy(s => s.CreateDate).Select(s => s.CreateDate.ToString()).Distinct());

            var results = new List<string>() { query.First().Url };
            results.AddRange(query.OrderBy(s => s.CreateDate).SelectMany(s => s.PerformancesData.Select(p => p.ResponseTime.ToString())));

            return new List<List<string>> { dateTime, results };
        }
    }
}