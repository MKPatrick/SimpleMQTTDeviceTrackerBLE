namespace DeviceTracker
{
    public class TrackedDevice : Device
    {
        public DateTime LastSeen { get; set; }
        public TrackedDevice(Device device)
        {
            this.MAC = device.MAC;
            this.Topic = device.Topic;
            this.Name = device.Name;
        }
    }
}
