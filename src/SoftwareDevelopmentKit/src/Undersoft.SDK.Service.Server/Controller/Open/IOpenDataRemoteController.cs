using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace Undersoft.SDK.Service.Server.Controller.Open;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

public interface IOpenDataRemoteController<TKey, TDto, TModel> where TModel : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
{
    Task<IActionResult> Delete([FromODataUri] TKey key);
    IQueryable<TModel> Get(ODataQueryOptions<TModel> options);
    SingleResult<TModel> Get([FromODataUri] TKey key, ODataQueryOptions<TModel> options);
    Task<IActionResult> Patch([FromODataUri] TKey key, [FromODataBody] TModel model);
    Task<IActionResult> Post([FromODataBody] TModel model);
    Task<IActionResult> Put([FromODataUri] TKey key, [FromODataBody] TModel model);
}