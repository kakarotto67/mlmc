using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mlmc.EnterpriseServiceBus.RabbitMq.MessageBus;
using Mlmc.MGCC.ChipSimulation;
using Mlmc.Shared.Events;
using RabbitMQ.Client;
using System;
using System.IO;

namespace MGCC
{
    public class Program
    {
        private static readonly ServiceCollection services = new ServiceCollection();
        private static IConfiguration configuration;

        public static void Main()
        {
            ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
            var messageBus = serviceProvider.GetService<MessageBus>();

            var queue = configuration.GetValue<String>("MessageBusConfiguration:Queues:LaunchCenterQueue");
            var mgccSimulator = new MgccSimulator();

            // Handle incoming messages
            messageBus.ConsumeMessage<LaunchMissileEvent>(queue, (eventMessage) =>
            {
                mgccSimulator.RunNewSimulation(eventMessage);
            });
        }

        private static void ConfigureServices()
        {
            SetupConfiguration();

            // Setup RabbitMQ client
            var connectionFactory = new ConnectionFactory
            {
                HostName = configuration.GetValue<String>("MessageBusConfiguration:HostName")
            };
            services.AddSingleton<IConnectionFactory>(sp => connectionFactory);

            // Setup Message Bus
            services.AddScoped<MessageBus>();
        }

        private static void SetupConfiguration()
        {
            if(configuration != null)
            {
                return;
            }

            // TODO: Fix might be needed for production
            // Other options to consider:
            // - Environment.CurrentDirectory
            // - AppDomain.CurrentDomain.BaseDirectory
            var baseDir = AppContext.BaseDirectory;
            var contentRootPath = Path.GetDirectoryName(baseDir.Substring(0, baseDir.IndexOf("bin\\")));

            var esbSettingsLocation = Path.Combine(contentRootPath, "..",
                "EnterpriseServiceBus", "esb-settings.json");

            var builder = new ConfigurationBuilder()
                .AddJsonFile(esbSettingsLocation, optional: false, reloadOnChange: false);
            configuration = builder.Build();
        }
    }
}