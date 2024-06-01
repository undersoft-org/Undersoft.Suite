using Microsoft.AspNetCore.Mvc;

namespace Undersoft.SDK.Service.Server.Controller.Api
{
    public interface IApiDataSetController<TKey, TEntity, TDto> where TDto : class, IOrigin, IInnerProxy
    {
        Task<IActionResult> Count();
        Task<IActionResult> Delete([FromBody] TDto[] dtos);
        Task<IActionResult> Get([FromHeader] int page, [FromHeader] int limit);
        Task<IActionResult> Patch([FromBody] TDto[] dtos);
        Task<IActionResult> Post([FromBody] QueryParameters query);
        Task<IActionResult> Post([FromBody] TDto[] dtos);
        Task<IActionResult> Put([FromBody] TDto[] dtos);
    }
}