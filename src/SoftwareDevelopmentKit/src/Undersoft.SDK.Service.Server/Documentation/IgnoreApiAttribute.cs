using Microsoft.AspNetCore.Mvc.Filters;

namespace Undersoft.SDK.Service.Server.Documentation
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class IgnoreApiAttribute : ActionFilterAttribute { }
}

