using Microsoft.Extensions.Hosting;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Server.Hosting;

public class ServerHostOptions
{
    public string ServerName { get; set; }

    public IHost Host { get; set; }

    public string HostName { get; set; }

    public int Port { get; set; }

    public string Route { get; set; }

    public long TenantId { get; set; }

    public string TenantName { get; set; }
}
