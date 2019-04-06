using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Threading.Tasks;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices;
using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos;
using System.Net;
using WebAPI_PerformanceTest.ViewModels;
using System.Configuration;

namespace WebAPI_PerformanceTest.Repository
{
    public class MainProcess
    {
        public static readonly log4net.ILog log =
    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        readonly List<string> _urlList;
        readonly string _province;
        PerformanceDataService performanceDataService = new PerformanceDataService();
        ServerInformationService serverInformationService = new ServerInformationService();

        public MainProcess(List<string> url)
        {
            try
            {
                this._urlList = url;
                this._province = ConfigurationManager.AppSettings["Province_Data"];
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void Run()
        {
            var rawData = new List<string>();
            var page_Id = new List<string>();
            Int64 id = 0;
            List<Int64> idList = new List<Int64>();

            var utcNow = DateTime.UtcNow;
            var now = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, utcNow.Minute / 5 * 5, 0, 0, DateTimeKind.Utc);


            int dividedJobs = Convert.ToDecimal(_urlList.Count / 5) > Convert.ToInt32(_urlList.Count / 5) ? _urlList.Count / 5 + 1 : _urlList.Count / 5;

            var jobList = new List<List<string>>();

            if (dividedJobs > 0)
            {
                for (var i = 0; i < dividedJobs; i++)
                {
                    if (_urlList.Count > 4)
                    {
                        jobList.Add(_urlList.Take(5).ToList());
                        _urlList.RemoveRange(0, 5);
                    }
                    else
                    {
                        jobList.Add(_urlList.ToList());
                    }
                }
            }
            else
            {
                jobList.Add(_urlList.ToList());
            }

            try
            {
                using (WebSocket webSocket = new WebSocket(ConfigurationManager.AppSettings["DebugProtocol"]))
                {
                    webSocket.OnOpen += (sender, e) =>
                        Console.WriteLine($"Connected to Chrome Debugger WebSocket at {DateTime.Now.ToLocalTime()}");
                    webSocket.OnMessage += (sender, e) =>
                    page_Id.Add(JsonConvert.DeserializeObject(e.Data).ToString());
                    webSocket.OnMessage += (sender, e) =>
                        Console.WriteLine(e.Data);
                    webSocket.OnError += (sender, e) =>
                        Console.WriteLine($"{DateTime.Now.ToLocalTime()} - 17ce Request Error : {e.Message + e.Exception}");


                    webSocket.Connect();
                    foreach (var jobs in jobList)
                    {
                        try
                        {
                            var domain_ID = new Dictionary<string, string>();

                            foreach (var url in jobs)
                            {
                                var domainName = url;
                                var random = new Random();
                                id += random.Next(1, 5);
                                while(idList.Any(s => s == id))
                                {
                                    id += random.Next(1, 5);
                                }
                                idList.Add(id);
                                string createTabs = "{\"id\":"+ id +",\"method\":\"Target.createTarget\",\"params\":{\"url\":\""+ domainName +"\"}}";
                                webSocket.Send(createTabs);
                                Thread.Sleep(3 * 1000);

                                var jToken = JToken.Parse(page_Id.LastOrDefault());

                                domain_ID.Add(jToken["result"]["targetId"].ToString(), domainName);
                            }

                            foreach (var domain in domain_ID)
                            {
                                var random = new Random();
                                id += random.Next(1, 5);
                                while (idList.Any(s => s == id))
                                {
                                    id += random.Next(1, 100);
                                }
                                idList.Add(id);

                                Task.Run(() => SendCommand(domain.Key, domain.Value, now, id)).Wait();

                                Thread.Sleep(3 * 1000);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                        }
                    }

                    webSocket.Close();

                }
            }
            catch (Exception ex)
            {
                log.Error($"Outside Part Of WebSocket : {ex}");
            }
        }

        public void SendCommand(string page_Id, string url, DateTime now, Int64 id)
        {
            var rawData = new List<string>();

            var requestReturn = new List<RequestData>();
            var responseReturn = new List<ResponseModel>();
            var receivedReturn = new List<DataReceived>();
            var loadingReturn = new List<LoadingFinished>();
            var failedReturn = new List<LoadingFailed>();
            var pageList = new List<string>();
            string image = string.Empty;


            try
            {
                using (WebSocket webSocket2 = new WebSocket($"ws://localhost:9222/devtools/page/{page_Id}"))
                {

                    webSocket2.OnMessage += (sender, e) =>
                    rawData.Add(JsonConvert.DeserializeObject(e.Data).ToString());

                    dynamic jsonParam = new ExpandoObject();
                    jsonParam.One = "{\"id\":"+ id +",\"method\":\"Network.setCacheDisabled\", \"params\":{\"cacheDisabled\":true}}";
                    jsonParam.Two = "{\"id\":" + id + ",\"method\":\"Network.clearBrowserCache\"}";
                    jsonParam.Three = "{\"id\":" + id + ",\"method\":\"Network.enable\"}";


                    jsonParam.Nine = "{\"id\":4,\"method\":\"Page.enable\"}";
                    //jsonParam.Ten = "{\"id\":4,\"method\":\"Page.frameStartedLoading\", \"params\":{\"frame\":\"" + page_Id + "\"}}";
                    //jsonParam.Eleven = "{\"id\":4,\"method\":\"Page.frameStoppedLoading\", \"params\":{\"frame\":\"" + page_Id + "\"}}";
                    //jsonParam.Twelve = "{\"id\":4,\"method\":\"Page.frameStoppedLoading\", \"params\":{\"frame\":\"" + page_Id + "\"}}";


                    jsonParam.Four = "{\"id\":" + id + ",\"method\":\"Page.navigate\",\"params\":{\"url\":\"" + url + "\"}}";
                    jsonParam.Five = "{\"id\":" + id + ",\"method\":\"Target.closeTarget\",\"params\":{\"targetId\":\"" + page_Id + "\"}}";

                    //jsonParam.Six = "{\"id\":4,\"method\":\"Performance.setTimeDomain\", \"params\":{\"timeDomain\":\"threadTicks\"}}";
                    //jsonParam.Seven = "{\"id\":4,\"method\":\"Performance.enable\"}";
                    jsonParam.Eight = "{\"id\":" + id + ",\"method\":\"Page.captureScreenshot\"}";

                    webSocket2.Connect();
                    webSocket2.Send(jsonParam.One);
                    webSocket2.Send(jsonParam.Two);
                    webSocket2.Send(jsonParam.Three);
                    //webSocket2.Send(jsonParam.Six);
                    //webSocket2.Send(jsonParam.Seven);

                    webSocket2.Send(jsonParam.Nine);
                    //webSocket2.Send(jsonParam.Ten);
                    //webSocket2.Send(jsonParam.Eleven);
                    //webSocket2.Send(jsonParam.Twelve);


                    webSocket2.Send(jsonParam.Four);


                    var requestIdList = new List<string>();
                    var dataIdList = new List<string>();
                    do
                    {
                        dataIdList = rawData.ToList();
                        Thread.Sleep(3 * 1000);
                    } while (!dataIdList.Any(s => s.Contains("Page.frameStoppedLoading")));

                    webSocket2.Send(jsonParam.Eight);

                    Thread.Sleep(3 * 1000);

                    dataIdList = rawData.ToList();



                    foreach (var request in dataIdList)
                    {
                        var jObject = JObject.Parse(request);

                        if (jObject.ContainsKey("method"))
                        {
                            foreach (var method in jObject)
                            {
                                if (method.Value.ToString().Equals("Network.requestWillBeSent"))
                                {
                                    requestIdList.Add(JToken.Parse(request)["params"]["requestId"].ToString());
                                }
                                else if (method.Value.ToString().Equals("Page.domContentEventFired"))
                                {
                                    pageList.Add($"{method.Value},{JToken.Parse(request)["params"]["timestamp"].ToString()}");
                                }
                                else if (method.Value.ToString().Equals("Page.loadEventFired"))
                                {
                                    pageList.Add($"{method.Value},{JToken.Parse(request)["params"]["timestamp"].ToString()}");
                                }
                            }
                        }
                        else if (jObject.ContainsKey("result"))
                        {
                            if(jObject.SelectToken("result.data") != null)
                            {
                                image = jObject.SelectToken("result.data").ToString();
                            }
                        }
                    }

                    webSocket2.Send(jsonParam.Five);
                    webSocket2.Close();
                }
                decimal pageFinished = Convert.ToDecimal(pageList.First(s => s.Contains("Page.loadEventFired")).Split(',')[1]) + 2;
                foreach (var data in rawData)
                {
                    var returnList = new List<ResponseModel>();
                    string reqType = string.Empty;
                    var a = JObject.Parse(data);


                    if (a.ContainsKey("method"))
                    {

                        foreach (var obj in a)
                        {
                            if (obj.Value.ToString().Equals("Network.requestWillBeSent"))
                            {
                                reqType = "Request";
                                continue;
                            }
                            else if (obj.Value.ToString().Equals("Network.responseReceived"))
                            {
                                reqType = "Response";
                                continue;
                            }
                            else if (obj.Value.ToString().Equals("Network.dataReceived"))
                            {
                                reqType = "Received";
                                continue;
                            }
                            else if (obj.Value.ToString().Equals("Network.loadingFinished"))
                            {
                                reqType = "Loading";
                                continue;
                            }
                            else if (obj.Value.ToString().Equals("Network.loadingFailed"))
                            {
                                reqType = "Failed";
                                continue;
                            }
                            var y = obj.Value;

                            switch (reqType)
                            {
                                case "Request":
                                    var requestParams = JsonConvert.DeserializeObject<RequestData>(y.ToString());
                                    if (y["request"] != null)
                                    {
                                        requestParams.Url = y["request"]["url"].ToString();
                                    }
                                    if (Convert.ToDecimal(requestParams.TimeStamp) <= pageFinished)
                                    {
                                        requestReturn.Add(requestParams);
                                    }
                                    continue;
                                case "Response":
                                    var outerParams = new ResponseParams();
                                    var response = new ResponseDetail();
                                    var responseTimming = new ResponseRawData();
                                    outerParams = JsonConvert.DeserializeObject<ResponseParams>(y.ToString());
                                    if (y["response"] != null)
                                    {
                                        response = JsonConvert.DeserializeObject<ResponseDetail>(y["response"].ToString());
                                    }
                                    if (y["response"]["timing"] != null)
                                    {
                                        responseTimming = JsonConvert.DeserializeObject<ResponseRawData>(y["response"]["timing"].ToString());
                                    }
                                    var newResponse = new ResponseModel()
                                    {
                                        ResponseParams = outerParams,
                                        ResponseDetail = response,
                                        ResponseRawData = responseTimming
                                    };
                                    responseReturn.Add(newResponse);
                                    continue;
                                case "Received":
                                    var receivedData = JsonConvert.DeserializeObject<DataReceived>(y.ToString());
                                    receivedReturn.Add(receivedData);
                                    continue;
                                case "Loading":
                                    var loadingData = JsonConvert.DeserializeObject<LoadingFinished>(y.ToString());
                                    loadingReturn.Add(loadingData);
                                    continue;
                                case "Failed":
                                    var failedData = JsonConvert.DeserializeObject<LoadingFailed>(y.ToString());
                                    failedReturn.Add(failedData);
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"Send Command Error : {ex}");
            }


            ResponseCalculation(url, requestReturn, responseReturn, receivedReturn, loadingReturn, failedReturn, now, image);
        }

        public void ResponseCalculation(string url, List<RequestData> request, List<ResponseModel> response, List<DataReceived> received, List<LoadingFinished> loading, List<LoadingFailed> failed, DateTime now, string image)
        {
            var requestId = request.Select(s => s.RequestId).Distinct();
            var startedTime = request.OrderBy(s => s.TimeStamp).FirstOrDefault().TimeStamp;
            var returnList = new List<ResponseData>();

            foreach (var id in requestId)
            {
                try
                {
                    var startTime = requestId.FirstOrDefault();
                    var requestData = request.Find(s => s.RequestId.Equals(id));
                    var responseData = response.Where(s => s.ResponseParams.RequestId.Equals(id)).OrderBy(p => p.ResponseParams.TimeStamp).ToList();
                    var receivedData = received.Where(s => s.RequestId.Equals(id)).OrderBy(p => p.TimeStamp).ToList();
                    var loadingData = loading.Where(s => s.RequestId.Equals(id));
                    var failedData = failed.Where(s => s.RequestId.Equals(id));

                    if (!failedData.Any() && responseData.Any() && responseData.LastOrDefault().ResponseDetail.RemoteIPAddress != null)
                    {
                        var responseTime = new ResponseData()
                        {
                            Url = requestData.Url,
                            MimeType = requestData.Type,
                            Protocol = responseData.LastOrDefault().ResponseDetail.Protocol,
                            RemoteIPAddress = responseData.LastOrDefault().ResponseDetail.RemoteIPAddress,
                            Status = responseData.LastOrDefault().ResponseDetail.Status,
                            RequestTime = Math.Round((requestData.TimeStamp - startedTime) * 1000),
                            Dns = Math.Round(responseData.LastOrDefault().ResponseRawData.DnsEnd - responseData.LastOrDefault().ResponseRawData.DnsStart, 3),
                            Connect = Math.Round(responseData.LastOrDefault().ResponseRawData.ConnectEnd - responseData.LastOrDefault().ResponseRawData.ConnectStart, 3),
                            SSL = Math.Round(responseData.LastOrDefault().ResponseRawData.SendEnd - responseData.LastOrDefault().ResponseRawData.SslStart, 3),
                            Send = Math.Round(responseData.LastOrDefault().ResponseRawData.SendEnd - responseData.LastOrDefault().ResponseRawData.SendStart, 3),
                            TTFB = Math.Round(responseData.LastOrDefault().ResponseRawData.ReceiveHeadersEnd - responseData.LastOrDefault().ResponseRawData.SendEnd, 3),
                            Download = receivedData.Any() ? Math.Round((receivedData.LastOrDefault().TimeStamp - responseData.LastOrDefault().ResponseParams.TimeStamp) * 1000, 3) : 0,
                            ResponseTime = 0
                        };
                        responseTime.ResponseTime = Math.Round(responseTime.Dns + responseTime.Connect + responseTime.Send + responseTime.TTFB + responseTime.Download, 3);
                        returnList.Add(responseTime);
                    }
                    else if (failedData.Any())
                    {
                        var responseTime = new ResponseData()
                        {
                            Url = requestData.Url,
                            MimeType = requestData.Type,
                            Protocol = responseData.Any() ? responseData.LastOrDefault().ResponseDetail.Protocol : "Unknown",
                            RemoteIPAddress = responseData.Any() ? responseData.LastOrDefault().ResponseDetail.RemoteIPAddress : "",
                            Status = responseData.Any() ? responseData.LastOrDefault().ResponseDetail.Status : failedData.Any() ? failedData.LastOrDefault().ErrorText : "",
                            RequestTime = Math.Round((requestData.TimeStamp - startedTime) * 1000),
                            Dns = receivedData.Any() ? Math.Round((receivedData.LastOrDefault().TimeStamp - responseData.LastOrDefault().ResponseParams.TimeStamp) * 1000, 3) : 0,
                            Connect = responseData.Any() ? Math.Round(responseData.LastOrDefault().ResponseRawData.ConnectEnd - responseData.LastOrDefault().ResponseRawData.ConnectStart, 3) : 0,
                            SSL = responseData.Any() ? Math.Round(responseData.LastOrDefault().ResponseRawData.SendEnd - responseData.LastOrDefault().ResponseRawData.SslStart, 3) : 0,
                            Send = responseData.Any() ? Math.Round(responseData.LastOrDefault().ResponseRawData.SendEnd - responseData.LastOrDefault().ResponseRawData.SendStart, 3) : 0,
                            TTFB = responseData.Any() ? Math.Round(responseData.LastOrDefault().ResponseRawData.ReceiveHeadersEnd - responseData.LastOrDefault().ResponseRawData.SendEnd, 3) : 0,
                            Download = receivedData.Any() ? Math.Round((receivedData.LastOrDefault().TimeStamp - responseData.LastOrDefault().ResponseParams.TimeStamp) * 1000, 3) : 0,
                            ResponseTime = 0
                        };
                        responseTime.ResponseTime = Math.Round(responseTime.Dns + responseTime.Connect + responseTime.Send + responseTime.TTFB + responseTime.Download, 3);
                        returnList.Add(responseTime);
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"Calculatioin Error : {ex}");
                }
            }


            var newPerformanceData = new PerformanceDataDto()
            {
                TestTime = now,
                Url = url,
                Page_Total = 0,
                Province = _province,
                Image = image,
                Response = returnList.Select(s => s.RequestTime + s.ResponseTime).Max(),
                PerformancesData = returnList
            };
            performanceDataService.AddOrEdit(newPerformanceData);
        }
    }
}
