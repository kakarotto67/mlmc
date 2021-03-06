using System;
using Mlmc.Shared.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mlmc.Operation.Models
{
    public class Missile
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long MissileId { get; set; }
        [BsonRepresentation(BsonType.Int64)]
        public long DeploymentPlatformId { get; set; }
        public Guid ServiceIdentityNumber { get; set; }
        public string Name { get; set; }
        public MissileType Type { get; set; }
        public DateTime InServiceDateStart { get; set; }
        public DateTime? InServiceDateEnd { get; set; }
        public MissileStatus Status { get; set; }
        public Location TargetLocation { get; set; }
    }
}