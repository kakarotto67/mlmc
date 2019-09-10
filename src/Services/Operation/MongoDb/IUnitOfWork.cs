using MongoDB.Driver;

namespace Operation.MongoDb
{
    public interface IUnitOfWork
    {
        IMongoDatabase DatabaseInstance { get; }
    }
}