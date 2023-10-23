
using DeviceTracker;
using DeviceTracker.Businesss;
using DeviceTracker.Config;

const int SCANTIME = 7;
var cfg = new ConfigurationManager();
List<TrackedDevice> trackedDevices = new List<TrackedDevice>();
BLEScanner bleScanner = new(cfg.ResolveConfig<DeviceConfig>(DeviceConfig.CONFIGKEY));
MqttHelper mqttHelper = new MqttHelper(cfg.ResolveConfig<MQTTConfig>(MQTTConfig.CONFIGKEY));

Console.WriteLine("Start DeviceTracker BLE");


do
{
    IEnumerable<Device> devicesFound = await bleScanner.ScanForDevices(SCANTIME);

    foreach (var device in devicesFound)
    {
        var alreadyTrackedDevice = trackedDevices.FirstOrDefault(x => x.MAC == device.MAC);
        if (alreadyTrackedDevice != null)
            alreadyTrackedDevice.LastSeen = DateTime.Now;
        else
        {
            trackedDevices.Add(new TrackedDevice(device) { LastSeen = DateTime.Now });
        }
    }

    await mqttHelper.SendUpdate(trackedDevices);

} while (true);

