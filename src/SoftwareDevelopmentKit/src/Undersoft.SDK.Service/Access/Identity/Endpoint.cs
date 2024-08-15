using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Identity;

public class Endpoint : DataObject
{
    public string Host { get; set; }

    public string IP { get; set; }

    public int? Port { get; set; }

    public string Path { get; set; }

    public string BaseUrl { get; set; }

    public string OS { get; set; }

    public string Protocol { get; set; }

    public string Method { get; set; }

    public string[] Parameters { get; set; }

    public string ReturnUrl { get; set; }

    public string StateUrl { get; set; }
}
