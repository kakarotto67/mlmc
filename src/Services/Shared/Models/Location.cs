﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mlmc.Shared.Models
{
    public class Location
    {
        [BsonRepresentation(BsonType.Double)]
        public double Longitude { get; set; }
        [BsonRepresentation(BsonType.Double)]
        public double Latitude { get; set; }
    }
}