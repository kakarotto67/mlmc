namespace Operation.MongoDb
{
    internal class OperationDatabaseSettings : IOperationDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string MissilesCollectionName { get; set; }
        public string DeploymentPlatformsCollectionName { get; set; }
    }
}