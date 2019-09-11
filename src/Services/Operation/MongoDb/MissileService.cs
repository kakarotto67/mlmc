using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Operation.Helpers;
using Operation.Models;

namespace Operation.MongoDb
{
    public class MissileService
    {
        private readonly IMongoCollection<Missile> _missilesCollection;

        public MissileService(IOperationDatabaseSettings dbSettings, IUnitOfWork unitOfWork)
        {
            _missilesCollection = unitOfWork.DatabaseInstance.GetCollection<Missile>(dbSettings.MissilesCollectionName);
        }

        internal IEnumerable<Missile> Get() => _missilesCollection
            .Find(missile => true).ToEnumerable();

        internal IEnumerable<Missile> Get(MissileStatus status) => _missilesCollection
            .Find(missile => missile.Status == status).ToEnumerable();

        internal void Insert(int deploymentPlatformId, string name, int type)
        {
            if (deploymentPlatformId < 0
                || String.IsNullOrEmpty(name)
                || type < 0)
            {
                return;
            }

            var missile = new Missile
            {
                MissileId = RandomHelper.GetTrueRandom(),
                DeploymentPlatformId = deploymentPlatformId,
                ServiceIdentityNumber = new Guid(),
                Name = name,
                Type = (MissileType)type,
                Status = MissileStatus.InService,
                InServiceDateStart = DateTime.UtcNow,
                InServiceDateEnd = null
            };

            // Insert new missile into database
            _missilesCollection.InsertOne(missile);
        }
    }
}