using MongoDB.Driver;
using System;

namespace Mlmc.Operation.MongoDb
{
    internal class OperationDatabaseContext : IUnitOfWork
    {
        public OperationDatabaseContext(IOperationDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.GetConnectionString());
            DatabaseInstance = client.GetDatabase(dbSettings.DatabaseName);
        }

        public IMongoDatabase DatabaseInstance { get; }
    }
}