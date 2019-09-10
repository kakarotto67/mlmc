using MongoDB.Driver;

namespace Operation.MongoDb
{
    internal class OperationDatabaseContext : IUnitOfWork
    {
        public OperationDatabaseContext(IOperationDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            DatabaseInstance = client.GetDatabase(dbSettings.DatabaseName);
        }

        public IMongoDatabase DatabaseInstance { get; }
    }
}