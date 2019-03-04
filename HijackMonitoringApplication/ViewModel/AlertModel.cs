using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.ViewModel
{
    public class AlertModel
    {
        public string Id { get; set; }
        public string ServerCname { get; set; }
        public string ProvinceIsp { get; set; }
        public List<AlertInformation> AlertDetail { get; set; }
    }
    public class AlertInformation
    {
        public string OccuredDate { get; set; }
        public string DownloadSpeed { get; set; }
        public int AlertType { get; set; }
        public string Percentage { get; set; }
        public string Average { get; set; }
    }
}