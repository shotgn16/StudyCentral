using NLog.Extensions.Logging;
using StudyCentralV2.Components;
using StudyCentralV2.Components.AI.Logic;
using StudyCentralV2.Components.EmailSender;
using StudyCentralV2.Components.FileManager.Logic;
using StudyCentralV2.Components.HttpClient.Logic;
using StudyCentralV2.Components.Iterator.Logic;
using StudyCentralV2.Pages;
using StudyCentralV2.Pages.Admin;
using StudyCentralV2.Pages.App;

namespace StudyCentralV2.Components
{
    public class configureServices
    {
        internal static readonly object _lock = new object();
        public static IConfiguration configuration;
        public static IConfiguration _configuration
        {
            get
            {
                lock (_lock)
                {
                    if (configuration == null)
                    {
                        loadConfig(); 
                    }

                    return configuration;
                }
            }
        }
        private static async Task loadConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            configuration = builder.Build();
        }

        internal static readonly object _lock2 = new object();
        public static ILogger _logger;
        public static ILogger Logger
        {
            get
            {
                lock ( _lock2)
                {
                    if (_logger == null)
                    {
                        NewLogger();
                    }

                    return _logger;
                }
            }
        }

        private static async Task NewLogger()
        {
            ILoggerFactory factory = new LoggerFactory();
            factory.AddNLog();

            _logger = LoggerFactoryExtensions.CreateLogger(factory, typeof(ILogger));
        }
    }
}
