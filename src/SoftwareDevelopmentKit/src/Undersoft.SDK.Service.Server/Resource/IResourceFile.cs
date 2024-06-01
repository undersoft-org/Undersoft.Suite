using Microsoft.AspNetCore.Http;

namespace Undersoft.SDK.Service.Server.Resource
{
    public interface IResourceFile 
    {
        IHeaderDictionary Headers { get; }
    }
}