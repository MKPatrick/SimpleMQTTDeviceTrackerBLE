using DeviceTracker.Config;
using System.Net;
using System.Text;
using uPLibrary.Networking.M2Mqtt;

namespace DeviceTracker.Businesss
{
    public class MqttHelper
    {
        private const string PAYLOADNOTHOME = "not_home";
        private const string PAYLOADHOME = "home";

        // create client instance 
        MqttClient client;
        private readonly MQTTConfig _config;

        public MqttHelper(MQTTConfig config)
        {
            this._config = config;
            client = new MqttClient(IPAddress.Parse(_config.BrokerHost));
        }



        /// <summary>
        /// Handles connection and Send Data to HomeAssistant
        /// </summary>
        public async Task SendUpdate(IEnumerable<TrackedDevice> devices)
        {
            await Initalize();
            foreach (TrackedDevice device in devices)
            {
                if (DateTime.Now - device.LastSeen > TimeSpan.FromMinutes(5))
                {
                    client.Publish(device.Topic, Encoding.UTF8.GetBytes(PAYLOADNOTHOME));
                }
                else
                {
                    client.Publish(device.Topic, Encoding.UTF8.GetBytes(PAYLOADHOME));
                }

            }
        }


        private async Task Initalize()
        {
            if (client.IsConnected == false)
                client.Connect(new Guid().ToString(), _config.BrokerUsername, _config.BrokerPassword);
        }


    }
}
