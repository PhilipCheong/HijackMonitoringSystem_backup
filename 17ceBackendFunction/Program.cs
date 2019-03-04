using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using WebSocketSharp;
using System.Configuration;
using _17ceBackendFunction.BusinessLayer.Services.IndependentServices;
using _17ceBackendFunction.Repository;
using _17ceBackendFunction.BusinessLayer.Dtos;
using log4net;

namespace _17ceBackendFunction
{
	public class Program
	{
		static void Main(string[] args)
		{
			HijackingDomainService hijackingDomainService = new HijackingDomainService();
			DataOrganizing dataOrganizing = new DataOrganizing();
			BackendFunction backendFunction = new BackendFunction();

			while (true)
			{
				Console.WriteLine($"{DateTime.Now.ToLocalTime()}   Application Started...");
				var domainList = dataOrganizing.DomainList();
				if (domainList.Any())
				{
					backendFunction.WS_17ce(domainList);
				}
				Console.WriteLine($"{DateTime.Now.ToLocalTime()}    Application Ended...");
				Thread.Sleep(120 * 1000);
			}
		}
	}

	public class BackendFunction
	{
		public static readonly log4net.ILog log =
	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		public const string userName = "noc-admin@toffstech.com";
		public const string Pwd = "80bbdfbb8806e105b65";
		public const string wsUrl = "wss://wsapi.17ce.com:8001/socket";

		DataOrganizing dataOrganizing = new DataOrganizing();

		public void WS_17ce(List<HijackingDomainDto> domainDto)
		{
			var unixTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString();
			var executionTime = DateTime.Now.ToLocalTime();

			var subPwd = Pwd + userName + unixTime;
			var bas64Str = Convert.ToBase64String(Encoding.UTF8.GetBytes(subPwd));
			var token = string.Join("",
				MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(bas64Str)).Select(x => x.ToString("X2").ToLower()));
			var taskResults = new List<ResultsReturned>();
			var provinceValue = ConfigurationManager.AppSettings["17ceProvince"].Split(',');
			var cityValue = ConfigurationManager.AppSettings["17ceCity"].Split(',');
			WebsocketData websocketData = new WebsocketData
			{
				txnid = 1,
				TimeOut = 30,
				nodetype = new[] { 1, 2 },
				type = 1,
				num = "1",
				Url = "",
				TestType = "HTTP",
				//UserAgent = /*"curl/7.47.0"*/,
				FollowLocation = 3,
				GetMD5 = true,
				MaxDown = 1024 * 1024,
				AutoDecompress = true,
				Request = "GET",
				isps = new[] { 1, 2, 7 },
				pro_ids = provinceValue,
				city_ids = cityValue,
				areas = new[] { 1 },
				NoCache = true
			};

			var rawData = new List<string>();
			var domainId = new Dictionary<int, string>();
			var rndValue = new Random().Next(1, 1529475);

			using (WebSocket webSocket = new WebSocket(wsUrl + "/?ut=" + unixTime + "&code=" + token + "&user=" + userName))
			{
				webSocket.OnOpen += (sender, e) =>
					Console.WriteLine($"Connected to 17ce WebSocket at {DateTime.Now.ToLocalTime()}");
				webSocket.OnMessage += (sender, e) =>
					rawData.Add(JsonConvert.DeserializeObject(e.Data).ToString());
				//webSocket.OnMessage += (sender, e) =>
				//	Console.WriteLine(e.Data);
				//webSocket.OnError += (sender, e) =>
				//	Console.WriteLine($"{DateTime.Now.ToLocalTime()} - 17ce Request Error : {e.Message + e.Exception}");
				webSocket.OnError += (sender, e) =>
					log.Error(e.Message + e.Exception);
				try
				{

					webSocket.Connect();
					foreach (var dto in domainDto)
					{
						try
						{
							rndValue += new Random().Next(1, 30);
							websocketData.Url = dto.Protocol + dto.Domain;
							websocketData.isps = Array.ConvertAll(dto.Isp.Split(','), s => int.Parse(s));
							websocketData.city_ids = dto.Province.Split(',').ToArray();
							websocketData.txnid = rndValue;
							domainId.Add(rndValue, dto.Protocol + dto.Domain);

							var postData = JsonConvert.SerializeObject(websocketData);
							webSocket.Send(postData);
						}
						catch (Exception ex)
						{
							log.Error(ex);
						}
					}
					Thread.Sleep(30 * 1000);

					int listCount = 0;
					int currentCount;
					int waitingCount = 0;

					do
					{
						if (listCount < 3)
						{
							waitingCount++;
						}
						listCount = rawData.Count();
						Thread.Sleep(5 * 1000);
						currentCount = rawData.Count();
					} while (listCount < 3 && waitingCount < 13 || listCount != currentCount);

					webSocket.Close();
				}
				catch (Exception ex)
				{
					log.Error(ex);
				}
			}

			dataOrganizing.OrganizeData(rawData, domainId, executionTime);
		}
	}
}
