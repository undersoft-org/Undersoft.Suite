using Microsoft.AspNetCore.Mvc;

namespace Undersoft.SDK.Service.Server.Controller.Api
{
    public interface IApiServiceController<TStore, TService, TModel> where TModel : class, IOrigin, IInnerProxy
    where TService : class
    where TStore : IDataServerStore
    {
        Task<IActionResult> Access([FromRoute] string method, [FromBody] Arguments arguments);
        Task<IActionResult> Action([FromRoute] string method, [FromBody] Arguments arguments);
        Task<IActionResult> Setup([FromRoute] string method, [FromBody] Arguments arguments);
    }
}