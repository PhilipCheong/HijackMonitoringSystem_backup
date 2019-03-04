using _17ceBackendFunction.BusinessLayer.Dtos.GeneralData;

namespace _17ceBackendFunction.BusinessLayer.Dtos
{
    public class TFCDNserverDto : PersistenceDtos, IPersistenceDtos
    {
        public string GroupName { get; set; }
        public string ServerCname { get; set; }
        public string BandwidthLimitation { get; set; }
        public string ServerIp { get; set; }

    }
}