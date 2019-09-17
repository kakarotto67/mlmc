using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Mlmc.EnterpriseServiceBus.RabbitMq.MessageBus;
using Mlmc.MGCC.Api.ChipSimulation;
using Mlmc.Shared.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mlmc.MGCC.Api.MessageBusConsumer
{
    internal class ConsumeMessageBusHostedService : BackgroundService
    {
        private readonly MessageBus messageBus;
        private readonly MgccSimulator mgccSimulator;
        private readonly IConfiguration configuration;

        public ConsumeMessageBusHostedService(MessageBus messageBus,
            MgccSimulator mgccSimulator, IConfiguration configuration)
        {
            this.messageBus = messageBus;
            this.mgccSimulator = mgccSimulator;
            this.configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            // Handle messages from Message Bus
            HandleMessageBusMessages();

            return Task.CompletedTask;
        }

        private void HandleMessageBusMessages()
        {
            mgccSimulator.SetStepInKm(100);

            var queue = configuration.GetValue<String>("MessageBusConfiguration:Queues:LaunchCenterQueue");

            // Handle incoming messages of type LaunchMissileEvent
            messageBus.ConsumeMessage<LaunchMissileEvent>(queue, (eventMessage) =>
            {
                mgccSimulator.RunNewSimulation(eventMessage);
            });
        }
    }
}