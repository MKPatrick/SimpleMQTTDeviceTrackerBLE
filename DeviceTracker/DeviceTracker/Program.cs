
using DeviceTracker;
using DeviceTracker.Config;

const int SCANTIME = 5;
var cfg = new ConfigurationManager();
BLEScanner bleScanner = new(cfg.ResolveConfig<DeviceConfig>(DeviceConfig.CONFIGKEY));
Console.WriteLine("Starting..");
var resolvedCFG = cfg.ResolveConfig<MQTTConfig>(MQTTConfig.CONFIGKEY);


  var result=await   bleScanner.ScanForDevices(SCANTIME);

