using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Mlmc.Operation.Helpers;
using Mlmc.Operation.Models;

namespace Mlmc.Operation.MongoDb
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

        internal Missile Get(Guid serviceIdentityNumber) => _missilesCollection
            .Find(missile => missile.ServiceIdentityNumber == serviceIdentityNumber).FirstOrDefault();

        internal IEnumerable<Missile> Get(MissileStatus status) => _missilesCollection
            .Find(missile => missile.Status == status).ToEnumerable();

        internal bool Update(Guid serviceIdentityNumber, MissileStatus newStatus)
        {
            var filter = Builders<Missile>.Filter.Eq(s => s.ServiceIdentityNumber, serviceIdentityNumber);
            var update = Builders<Missile>.Update.Set(s => s.Status, newStatus);

            var updateResult = _missilesCollection.UpdateOne(filter, update);

            return updateResult.MatchedCount < 1 || updateResult.ModifiedCount == 1;
        }

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

        // TODO
        // Currently missile is deleted on Decommission,
        // but it is enough to just update its status to 'Decommissioned'.
        internal bool Delete(Guid serviceIdentityNumber)
        {
            var deleteResult = _missilesCollection
                .DeleteOne(x => x.ServiceIdentityNumber == serviceIdentityNumber);

            return deleteResult.DeletedCount <= 1;
        }
    }
}