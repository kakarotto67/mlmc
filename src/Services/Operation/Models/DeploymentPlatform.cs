using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Operation.Models
{
    public class DeploymentPlatform
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long DeploymentPlatformId { get; set; }
        public Guid ServiceIdentityNumber { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        // TODO: Minor - add capacity
    }
}