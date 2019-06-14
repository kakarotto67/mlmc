using System;

namespace Operation.Models
{
    public class Missile
    {
        public long MissileId { get; set; }
        public Guid ServiceIdentityNumber { get; set; }
        public string Name { get; set; }
        public MissileType Type {get;set;}
        public DateTime InServiceDateStart { get; set; }
        public DateTime? InServiceDateEnd { get; set; }
        public MissileStatus Status {get;set;}
    }
}