using Microsoft.AspNetCore.Mvc;

namespace Undersoft.SDK.Service.Server.Controller.Api;

using Microsoft.AspNetCore.Http;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Invocation;

[ApiService]
public abstract class ApiServiceController<TStore, TService, TModel>
    : ControllerBase,
        IApiServiceController<TStore, TService, TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TService : class
    where TStore : IDataServerStore
{
    protected readonly IServicer _servicer;

    protected ApiServiceController() { }

    protected ApiServiceController(IServicer servicer)
    {
        var accessor = servicer.GetService<IHttpContextAccessor>();
        _servicer =
            (accessor != null)
                ? servicer.GetTenantServicer(accessor.HttpContext.User)
                : servicer;
    }

    [HttpPost("Action/{method}")]
    public virtual async Task<IActionResult> Action(
        [FromRoute] string method,
        [FromBody] Arguments arguments
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _servicer.Perform(
            new Access<TStore, TService, TModel>(method, arguments)
        );

        return !result.IsValid ? BadRequest(result.ErrorMessages) : Ok(result.Response);
    }

    [HttpPost("Access/{method}")]
    public virtual async Task<IActionResult> Access(
        [FromRoute] string method,
        [FromBody] Arguments arguments
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _servicer.Perform(
            new Action<TStore, TService, TModel>(method, arguments)
        );

        return !result.IsValid ? BadRequest(result.ErrorMessages) : Ok(result.Response);
    }

    [HttpPost("Setup/{method}")]
    public virtual async Task<IActionResult> Setup(
        [FromRoute] string method,
        [FromBody] Arguments arguments
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _servicer.Perform(
            new Setup<TStore, TService, TModel>(method, arguments)
        );

        return !result.IsValid ? BadRequest(result.ErrorMessages) : Ok(result.Response);
    }
}
