using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace Undersoft.SDK.Service.Server.Controller.Open;

using Microsoft.AspNetCore.OData.Results;
using Undersoft.SDK.Proxies;

public interface IOpenEventController<TKey, TEntity, TDto> where TDto : class, IOrigin, IInnerProxy
{
    Task<IActionResult> Delete([FromODataUri] TKey key);
    IQueryable<TDto> Get();
    SingleResult<TDto> Get([FromODataUri] TKey key);
    Task<IActionResult> Patch([FromODataUri] TKey key, [FromODataBody] TDto dto);
    Task<IActionResult> Post([FromODataBody] TDto dto);
    Task<IActionResult> Put([FromODataUri] TKey key, [FromODataBody] TDto dto);
}