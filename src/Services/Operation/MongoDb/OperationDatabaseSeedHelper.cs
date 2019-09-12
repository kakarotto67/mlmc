using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Operation.Models;

namespace Operation.MongoDb
{
    internal static class OperationDatabaseSeedHelper
    {
        internal static void SeedDatabase(IOperationDatabaseSettings dbSettings, IUnitOfWork unitOfWork)
        {
            var deploymentPlatformsCollection = unitOfWork.DatabaseInstance.GetCollection<DeploymentPlatform>(dbSettings.DeploymentPlatformsCollectionName);
            var deploymentPlatformsDefaultList = new List<DeploymentPlatform>
            {
                new DeploymentPlatform
                {
                    DeploymentPlatformId = 1,
                    ServiceIdentityNumber = Guid.NewGuid(),
                    Name = "DP-Default",
                    Location = new Location
                    {
                        Longitude = 23.943529,
                        Latitude = 49.813046
                    }
                }
            };

            if (!deploymentPlatformsCollection.Find(missile => true).Any())
            {
                deploymentPlatformsCollection.InsertMany(deploymentPlatformsDefaultList);
            }

            var missilesCollection = unitOfWork.DatabaseInstance.GetCollection<Missile>(dbSettings.MissilesCollectionName);
            var missilesDefaultList = new List<Missile>
            {
                new Missile
                    {
                        MissileId = 1,
                        DeploymentPlatformId = 1,
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "AIM-7 Sparrow",
                        Type = MissileType.AirToAir,
                        InServiceDateStart = new DateTime(1958, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        MissileId = 2,
                        DeploymentPlatformId = 1,
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "AGM-158 JASSM",
                        Type = MissileType.AirToSurface,
                        InServiceDateStart = new DateTime(2009, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        MissileId = 3,
                        DeploymentPlatformId = 1,
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "BGM-109 Tomahawk",
                        Type = MissileType.SurfaceToSurface,
                        InServiceDateStart = new DateTime(1983, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        MissileId = 4,
                        DeploymentPlatformId = 1,
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "RIM-161 Standard Missile 3",
                        Type = MissileType.SurfaceToAir,
                        InServiceDateStart = new DateTime(2014, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        MissileId = 5,
                        DeploymentPlatformId = 1,
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "LGM-30 Minuteman",
                        Type = MissileType.ICBM,
                        InServiceDateStart = new DateTime(1970, 1, 1),
                        Status = MissileStatus.InService
                    },
                    new Missile
                    {
                        MissileId = 6,
                        DeploymentPlatformId = 1,
                        ServiceIdentityNumber = Guid.NewGuid(),
                        Name = "UGM-133 Trident II",
                        Type = MissileType.SubmarineLaunched,
                        InServiceDateStart = new DateTime(1990, 1, 1),
                        Status = MissileStatus.InService
                    }
            };

            if (!missilesCollection.Find(missile => true).Any())
            {
                missilesCollection.InsertMany(missilesDefaultList);
            }
        }
    }
}