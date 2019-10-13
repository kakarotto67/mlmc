namespace Mlmc.Operation.MongoDb
{
    public interface IOperationDatabaseSettings
    {
        string Host { get; set; }
        string Port { get; set; }
        string User { get; set; }
        string Password { get; set; }
        string DatabaseName { get; set; }
        string MissilesCollectionName { get; set; }
        string DeploymentPlatformsCollectionName { get; set; }
        string GetConnectionString();
    }
}