using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.ViewModel
{
	public class ServerViewModel
	{
		public string Id { get; set; }
		public string GroupName { get; set; }
		public string ServerCname { get; set; }
		public string BandwidthLimitation { get; set; }
		public string ServerIp { get; set; }
	}

	public class ServerViewModelList
	{
		public ServerViewModelList()
		{
			ServerViewModel = new List<ServerViewModel>();
		}

		public List<ServerViewModel> ServerViewModel { get; set; }
	}
}