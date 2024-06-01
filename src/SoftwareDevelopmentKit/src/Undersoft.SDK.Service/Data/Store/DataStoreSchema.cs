namespace Undersoft.SDK.Service.Data.Store
{
    public static class DataStoreSchema
    {
        public static string DomainSchema { get; } = "domain";
        public static string RemoteSchema { get; } = "remote";
        public static string IdentifierSchema { get; } = "identifiers";
        public static string RelationSchema { get; } = "relations";
        public static string PropertySchema { get; } = "properties";
    }
}