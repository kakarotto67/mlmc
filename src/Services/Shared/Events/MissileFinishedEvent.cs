using Mlmc.Shared.Models;
using System;

namespace Mlmc.Shared.Events
{
    public sealed class MissileFinishedEvent : BaseEvent
    {
        public long MissileId { get; set; }
        public Guid MissileServiceIdentityNumber { get; set; }
        public string MissileName { get; set; }
        public MissileStatus MissileStatus { get; set; }
        public Location MissileFinishedAtLocation { get; set; }
        public DateTime MissileFinishedAtDate { get; set; }
    }
}