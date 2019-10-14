using Microsoft.Extensions.Configuration;
using Mlmc.EnterpriseServiceBus.RabbitMq.MessageBus;
using Mlmc.Shared.Events;
using Mlmc.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mlmc.MGCC.Api.ChipSimulation
{
    internal class MgccSimulator
    {
        private int stepInKm;
        private readonly MessageBus messageBus;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;

        public MgccSimulator(MessageBus messageBus, IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            this.messageBus = messageBus;
            this.configuration = configuration;
            this.clientFactory = clientFactory;
        }

        public void SetStepInKm(int stepInKm)
        {
            var defaultStepInKm = 100;
            this.stepInKm = stepInKm > 0 ? stepInKm : defaultStepInKm;
        }

        public void RunNewSimulation(LaunchMissileEvent eventMessage)
        {
            if (eventMessage == null)
            {
                return;
            }

            Task.Factory.StartNew(() => SimulationEngine(eventMessage));
        }

        private void SimulationEngine(LaunchMissileEvent eventMessage)
        {
            var deploymentPlatformLocation = eventMessage.DeploymentPlatformLocation;
            var targetLocation = eventMessage.TargetLocation;

            // Find bearing between deployment platform and target locations
            var bearing = CoordinatesHelper.FindInitialBearing(deploymentPlatformLocation, targetLocation);

            var launchedMissileCurrentStatusEvent = new LaunchedMissileCurrentStatusEvent
            {
                MissileId = eventMessage.MissileId,
                MissileServiceIdentityNumber = eventMessage.MissileServiceIdentityNumber,
                MissileName = eventMessage.MissileName,
                MissileStatus = MissileStatus.Launched,
                MissileGpsLocation = deploymentPlatformLocation,
                InformationPostedDate = DateTime.UtcNow,
                Bearing = bearing
            };

            // Post initial infromation about launched missile
            PostCurrentStatusEvent(launchedMissileCurrentStatusEvent);

            var distance = CoordinatesHelper.GetDistance(deploymentPlatformLocation, targetLocation);
            if (distance <= stepInKm)
            {
                // Post final information about launched missile
                launchedMissileCurrentStatusEvent.SetFinalInfo(targetLocation);
                PostCurrentStatusEvent(launchedMissileCurrentStatusEvent);

                return;
            }

            var currentDistance = 0;
            var maxNotificationsBeforeFinish = (Int32)Math.Floor(distance / stepInKm);
            for (var i = 0; i < maxNotificationsBeforeFinish; i++)
            {
                currentDistance += stepInKm;

                // Double check if current distance is less than total distance
                if (currentDistance >= distance)
                {
                    break;
                }

                // Get current missile GPS coordinates
                var currentMissileGpsLocation =
                   CoordinatesHelper.GetIntermediateLocation(
                       deploymentPlatformLocation, bearing, currentDistance);

                // Post current status information about launched missile
                launchedMissileCurrentStatusEvent.SetIntermediaryInfo(currentMissileGpsLocation);
                PostCurrentStatusEvent(launchedMissileCurrentStatusEvent);
            }

            // Post final information about launched missile
            launchedMissileCurrentStatusEvent.SetFinalInfo(targetLocation);
            PostCurrentStatusEvent(launchedMissileCurrentStatusEvent);
        }

        private void PostCurrentStatusEvent(LaunchedMissileCurrentStatusEvent eventMessage)
        {
            if (eventMessage == null)
            {
                return;
            }

            // TODO: Extract delay simulation from this method
            // TODO: Add random delay
            // 1 second delay
            Thread.Sleep(1000);

            // Post SignalR message with current status and GPS coordinates
            // of launched missile so it can be handled on UI map.
            PostSignalRMessage(eventMessage);

            // If this is final event - also post message to Missile Finished Queue of Message Bus
            if (eventMessage.IsFinished)
            {
                PostMissileFinishedEvent(eventMessage);
            }
        }

        private void PostSignalRMessage(LaunchedMissileCurrentStatusEvent eventMessage)
        {
            if (eventMessage == null)
            {
                return;
            }

            using (var client = clientFactory.CreateClient())
            {
                var uri = configuration.GetValue<String>("ApiPath:Mgcc");

                var eventJson = JsonConvert.SerializeObject(eventMessage);

                var contentToPost = new StringContent(eventJson, Encoding.UTF8, "application/json");

                var postResult = client.PostAsync(uri, contentToPost).Result;
            }
        }

        /// <summary>
        /// Message produced by this method will be used by Reporting service.
        /// </summary>
        private void PostMissileFinishedEvent(LaunchedMissileCurrentStatusEvent eventMessage)
        {
            if (eventMessage == null || !eventMessage.IsFinished)
            {
                return;
            }

            // Publish missile finished message
            var finishedMissilesQueue = configuration
                .GetValue<String>("MessageBusSettings:Queues:FinishedMissilesQueue");
            // TODO: Add more info
            var missileFinishedEvent = new MissileFinishedEvent
            {
                MissileId = eventMessage.MissileId,
                MissileServiceIdentityNumber = eventMessage.MissileServiceIdentityNumber,
                MissileName = eventMessage.MissileName,
                MissileStatus = eventMessage.MissileStatus,
                MissileFinishedAtLocation = eventMessage.MissileGpsLocation,
                MissileFinishedAtDate = eventMessage.InformationPostedDate
            };
            messageBus.PublishMessage(finishedMissilesQueue, missileFinishedEvent);
        }
    }
}