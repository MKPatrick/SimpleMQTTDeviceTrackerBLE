namespace DeviceTracker
{
    public class BLEScanner
    {

       public async Task<IEnumerable<Device>> ScanForDevices(int scanTimeInS)
        {
            var scanCMD= $"sudo timeout {scanTimeInS} hcitool lescann";
            return null;
        }

    }
}
