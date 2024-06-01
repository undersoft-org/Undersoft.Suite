using Microsoft.AspNetCore.Mvc;

namespace Undersoft.SDK.Service.Server.Controller.Api;

using Undersoft.SDK;


public interface IApiServiceRemoteController<TStore, TService, TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TService : class
    where TStore : IDataServiceStore
{
    Task<IActionResult> Action(string method, [FromBody] Arguments arguments);

    Task<IActionResult> Access(string method, [FromBody] Arguments arguments);

    Task<IActionResult> Setup(string method, [FromBody] Arguments arguments);
}