using DeviceTracker.Config;
using System.Diagnostics;
using System.Text;

namespace DeviceTracker
{
    public class BLEScanner
    {
        private readonly DeviceConfig _deviceConfig;

        public BLEScanner(DeviceConfig deviceConfig)
        {
            this._deviceConfig = deviceConfig;
        }

        public async Task<IEnumerable<Device>> ScanForDevices(int scanTimeInS)
        {

            List<Device> devices = new List<Device>();
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "bash";
                process.StartInfo.Arguments = $"ScannScript.sh";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();


                //Stop scannrpocess after scanTimeInS seconds
                if (!await Task.Run(() => process.WaitForExit(scanTimeInS * 1000)))
                {
                    var lines = await File.ReadAllLinesAsync("scanneddevices.txt");
                    Console.WriteLine(lines.Count());
                    foreach (var item in lines)
                    {
                        Console.WriteLine(item);
                        var deviceMatch = _deviceConfig.Devices.FirstOrDefault(x => item.Contains(x.MAC));
                        if (!devices.Contains(deviceMatch))
                        {
                            Console.WriteLine("MATCH!!");
                            devices.Add(deviceMatch);
                        }
                    }
                    process.Kill();
                }
              

                Console.WriteLine("DONE with scanning");
                return devices;


            }

        }


    }
}
