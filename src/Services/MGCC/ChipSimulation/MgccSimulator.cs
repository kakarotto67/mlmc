using Mlmc.Shared.Events;
using Mlmc.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Mlmc.MGCC.ChipSimulation
{
    internal class MgccSimulator
    {
        private const int StepInKm = 100;

        public MgccSimulator()
        {
        }

        public void RunNewSimulation(LaunchMissileEvent eventMessage)
        {
            if(eventMessage == null)
            {
                return;
            }

            Task.Factory.StartNew(() => SimulationEngine(eventMessage));
        }

        private void SimulationEngine(LaunchMissileEvent eventMessage)
        {
            var deploymentPlatformLocation = new Location();
            var targetLocation = eventMessage.TargetLocation;

            var launchedMissileCurrentStatusEvent = new LaunchedMissileCurrentStatusEvent
            {
                MissileServiceIdentityNumber = eventMessage.MissileServiceIdentityNumber,
                MissileName = eventMessage.MissileName,
                MissileStatus = MissileStatus.Launched,
                MissileGpsLocation = deploymentPlatformLocation,
                InformationPostedDate = DateTime.UtcNow
            };

            // Post initial infromation about launched missile
            PostCurrentStatusEvent(launchedMissileCurrentStatusEvent);

            var distance = CoordinatesHelper.GetDistance(deploymentPlatformLocation, targetLocation);
            if(distance <= StepInKm)
            {
                // Post final information about launched missile
                launchedMissileCurrentStatusEvent.SetFinalInfo(targetLocation);
                PostCurrentStatusEvent(launchedMissileCurrentStatusEvent);

                return;
            }

            var currentDistance = 0;
            var maxNotificationsBeforeFinish = (Int32)Math.Floor(distance / StepInKm);
            for (var i = 0; i < maxNotificationsBeforeFinish; i++)
            {
                currentDistance += StepInKm;

                // Dobule check if current distance is less than total distance
                if(currentDistance >= distance)
                {
                    break;
                }

                // Get current missile GPS coordinates
                var currentMissileGpsLocation =
                    CoordinatesHelper.GetIntermediateLocation(
                        deploymentPlatformLocation, targetLocation, currentDistance);

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
            if(eventMessage == null)
            {
                return;
            }

            // TODO: Add random delay
            // 1 second delay
            Task.Delay(1000);

            // TODO: Implement
            // Post into queue or post into database?
        }
    }
}