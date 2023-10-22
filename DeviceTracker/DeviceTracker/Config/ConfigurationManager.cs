using Microsoft.Extensions.Configuration;

namespace DeviceTracker.Config
{

    /// <summary>
    /// Small Configuration Manager like IOptions pattern
    /// </summary>
    public class ConfigurationManager
    {
        IConfigurationRoot _config;
        public ConfigurationManager()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json", true, true);
            _config = builder.Build();
        }
        public T ResolveConfig<T>(string section)
        {
            return _config.GetSection(section).Get<T>();

        }

    }
}
