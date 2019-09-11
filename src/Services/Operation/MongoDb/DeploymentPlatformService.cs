using MongoDB.Driver;
using Operation.Models;
using System.Collections.Generic;
using System.Linq;

namespace Operation.MongoDb
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
    }
}