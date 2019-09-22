using System;
using Mlmc.Shared.Models;

namespace Mlmc.Shared.Events
{
    public sealed class LaunchMissileEvent : BaseEvent
    {
        public long MissileId { get; set; }
        public Guid MissileServiceIdentityNumber { get; set; }
        public string MissileName { get; set; }
        public DateTime LaunchDate { get; set; }
        public Location DeploymentPlatformLocation { get; set; }
        public Location TargetLocation { get; set; }
    }
}