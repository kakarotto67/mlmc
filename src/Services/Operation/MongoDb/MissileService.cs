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

        internal bool Insert(Missile missile)
        {
            if (missile == null)
            {
                return false;
            }

            missile.MissileId = RandomHelper.GetTrueRandom();
            missile.ServiceIdentityNumber = Guid.NewGuid();
            missile.Status = MissileStatus.InService;
            missile.InServiceDateStart = DateTime.UtcNow;
            missile.InServiceDateEnd = null;

            // Insert new missile into database
            _missilesCollection.InsertOne(missile);

            return true;
        }
    }
}