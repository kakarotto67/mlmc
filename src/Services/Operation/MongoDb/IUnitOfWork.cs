using MongoDB.Driver;

namespace Mlmc.Operation.MongoDb
{
    public interface IUnitOfWork
    {
        IMongoDatabase DatabaseInstance { get; }
    }
}