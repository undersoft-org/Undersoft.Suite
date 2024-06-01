using Microsoft.AspNetCore.Mvc;

namespace Undersoft.SDK.Service.Server.Controller.Api;
public interface IApiEventRemoteController<TKey, TDto, TModel> where TModel : class, IOrigin, IInnerProxy
{
    Task<IActionResult> Count();
    Task<IActionResult> Delete([FromBody] TModel[] models);
    Task<IActionResult> Delete([FromRoute] TKey key, [FromBody] TModel model);
    Task<IActionResult> Get([FromHeader] int page, [FromHeader] int limit);
    Task<IActionResult> Get(TKey key);
    Task<IActionResult> Patch([FromBody] TModel[] models);
    Task<IActionResult> Patch([FromRoute] TKey key, [FromBody] TModel model);
    Task<IActionResult> Post([FromBody] QueryParameters query);
    Task<IActionResult> Post([FromBody] TModel[] models);
    Task<IActionResult> Post([FromRoute] TKey key, [FromBody] TModel model);
    Task<IActionResult> Put([FromBody] TModel[] models);
    Task<IActionResult> Put([FromRoute] TKey key, [FromBody] TModel model);
}