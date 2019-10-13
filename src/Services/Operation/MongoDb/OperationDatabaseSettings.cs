using System;

namespace Mlmc.Operation.MongoDb
{
    internal class OperationDatabaseSettings : IOperationDatabaseSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public string MissilesCollectionName { get; set; }
        public string DeploymentPlatformsCollectionName { get; set; }

        public string GetConnectionString()
        {
            if (String.IsNullOrEmpty(User) || String.IsNullOrEmpty(Password))
            {
                return $@"mongodb://{Host}:{Port}";
            }

            return $@"mongodb://{User}:{Password}@{Host}:{Port}";
        }
    }
}