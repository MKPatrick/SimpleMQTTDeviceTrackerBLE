namespace DeviceTracker.Config
{
    public class MQTTConfig
    {
        public const string CONFIGKEY = "MQTTConfig";
        public string BrokerHost { get; set; }
        public string BrokerUsername { get; set; }
        public string BrokerPassword { get; set; }
    }
}
