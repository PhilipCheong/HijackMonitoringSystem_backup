using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.ViewModel
{
	public class ServerStatusViewModel
	{
		public string Province { get; set; }
		public string Isp { get; set; }
		public DateTime? LastExecution { get; set; }
		public string Status { get; set; }
	}
    public class RealtimeServerSatus
    {
        public string ServerCname { get; set; }
        public string Province { get; set; }
        public string Isp { get; set; }
        public DateTime? LastExecution { get; set; }
        public double DownloadTime { get; set; }
        public string Status { get; set; }
        public int CountOnOne { get; set; }
        public int CountOnTwo { get; set; }
        public int CountOnThree { get; set; }
    }
    public class RealtimeInformation
    {
        public string ProvinceIsp { get; set; }
        public int CountOnOne { get; set; }
        public int CountOnTwo { get; set; }
        public int CountOnThree { get; set; }
    }
}