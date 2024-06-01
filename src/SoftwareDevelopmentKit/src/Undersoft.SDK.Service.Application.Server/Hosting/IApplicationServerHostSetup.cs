using Microsoft.AspNetCore.Builder;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.SDK.Service.Application.Server.Hosting
{
    public interface IApplicationServerHostSetup : IServerHostSetup
    {
        IApplicationServerHostSetup UseServiceApplication();
    }
}