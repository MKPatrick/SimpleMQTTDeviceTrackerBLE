
using DeviceTracker.Config;

var cfg = new ConfigurationManager();


var resolvedCFG = cfg.ResolveConfig<MQTTConfig>(MQTTConfig.CONFIGKEY);

Console.WriteLine(resolvedCFG.BrokerHost);


Console.ReadLine();