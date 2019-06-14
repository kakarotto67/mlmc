using System;
using System.Linq;
using Operation.Models;

namespace Operation.Database
{
    public class DoDDataWarehouseSeedData
    {
        public static void SeedDatabase(DoDDataWarehouseContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (!context.Missiles.Any())
            {
                context.Missiles.AddRange(
                    new Missile
                    {
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "AIM-7 Sparrow",
                        Type = MissileType.AirToAir,
                        InServiceDateStart = new DateTime(1958, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "AGM-158 JASSM",
                        Type = MissileType.AirToSurface,
                        InServiceDateStart = new DateTime(2009, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "BGM-109 Tomahawk",
                        Type = MissileType.SurfaceToSurface,
                        InServiceDateStart = new DateTime(1983, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "RIM-161 Standard Missile 3",
                        Type = MissileType.SurfaceToAir,
                        InServiceDateStart = new DateTime(2014, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "LGM-30 Minuteman",
                        Type = MissileType.ICBM,
                        InServiceDateStart = new DateTime(1970, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "UGM-133 Trident II",
                        Type = MissileType.SubmarineLaunched,
                        InServiceDateStart = new DateTime(1990, 1, 1),
                        Status = MissileStatus.InService
                    }
                );

                context.SaveChanges();
            }
        }
    }
}