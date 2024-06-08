using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace Undersoft.SDK.Service.Server.Controller.Open;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Undersoft.SDK.Proxies;

public interface IOpenDataController<TKey, TEntity, TDto>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
{
    Task<IActionResult> Delete([FromODataUri] TKey key);

    [EnableQuery]
    IQueryable<TDto> Get();

    [EnableQuery]
    SingleResult<TDto> Get([FromODataUri] TKey key);
    Task<IActionResult> Patch([FromODataUri] TKey key, TDto dto);
    Task<IActionResult> Post(TDto dto);
    Task<IActionResult> Put([FromODataUri] TKey key, TDto dto);
}
