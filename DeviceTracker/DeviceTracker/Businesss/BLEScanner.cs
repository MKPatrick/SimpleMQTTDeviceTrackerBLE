using DeviceTracker.Config;
using System.Diagnostics;
using System.Text;

namespace DeviceTracker.Businesss
{
    public class BLEScanner
    {
        private readonly DeviceConfig _deviceConfig;

        public BLEScanner(DeviceConfig deviceConfig)
        {
            _deviceConfig = deviceConfig;
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


                await Task.Delay(10000);
                StopScanningProcess();
                await Task.Delay(500);
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
                Console.WriteLine("DONE with scanning");
                return devices;


            }

        }



        //this is necessary cause we have to send a SIGINT kill signal (like CNTRL + C)
        private void StopScanningProcess()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "bash";
                process.StartInfo.Arguments = $"-c \"pkill --signal SIGINT hcitool\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
            }

        }
    }
}
