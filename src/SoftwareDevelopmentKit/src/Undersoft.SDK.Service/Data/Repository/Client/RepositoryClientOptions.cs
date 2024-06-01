namespace Undersoft.SDK.Service.Data.Repository.Client;

public class RepositoryClientOptions
{
    private Uri _uri = new UriBuilder().Uri;

    public string ConnectionString { get => _uri.ToString(); set => _uri = new UriBuilder(value).Uri; }

    public string Host { get => _uri.Host; set => _uri = new UriBuilder(_uri) { Host = value }.Uri; }

    public int Port { get => _uri.Port; set => _uri = new UriBuilder(_uri) { Port = value }.Uri; }

    public string Path { get => _uri.AbsolutePath; set => _uri = new UriBuilder(_uri) { Path = value }.Uri; }

    public bool Pooling { get; set; }

    public string ClientProvider { get; set; }

    public int PoolSize { get; set; }
}
