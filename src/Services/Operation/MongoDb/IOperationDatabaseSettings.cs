namespace Operation.MongoDb
{
    public interface IOperationDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string MissilesCollectionName { get; set; }
    }
}