using Mlmc.Operation.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Mlmc.Operation.MongoDb
{
    public class DeploymentPlatformService
    {
        private readonly IMongoCollection<DeploymentPlatform> _deploymentPlatformCollection;

        public DeploymentPlatformService(IOperationDatabaseSettings dbSettings, IUnitOfWork unitOfWork)
        {
            _deploymentPlatformCollection =
                unitOfWork.DatabaseInstance.GetCollection<DeploymentPlatform>(dbSettings.DeploymentPlatformsCollectionName);
        }

        internal IEnumerable<DeploymentPlatform> Get() => _deploymentPlatformCollection
            .Find(dp => true).ToEnumerable();

        internal DeploymentPlatform Get(long deploymentPlatformId) => _deploymentPlatformCollection
            .Find(dp => dp.DeploymentPlatformId == deploymentPlatformId).FirstOrDefault();
    }
}