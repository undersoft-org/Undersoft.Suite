namespace Undersoft.SDK
{
    [Flags]
    public enum OperationType
    {
        Any = 65555,
        Create = 1,
        Change = 2,
        Update = 4,
        Delete = 8,
        Upsert = 16,
        Get = 32,
        Find = 64,
        Filter = 128,
        Action = 256,
        Setup = 512,
        Access = 1024,
        Query = 2048,
        Remote = 4096,
        Compute = 8192,
        Upload = 16384,
        Download = 32768,
        Aggregate = 65536
    }
}
