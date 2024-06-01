using Microsoft.AspNetCore.Mvc;

namespace Undersoft.SDK.Service.Server.Controller.Api;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;

public interface IApiDataController<TKey, TEntity, TDto> where TDto : class, IOrigin, IInnerProxy
{
    [HttpGet]
    Task<IActionResult> Count();
    [HttpDelete]
    Task<IActionResult> Delete([FromBody] TDto[] dtos);
    [HttpDelete("{key}")]
    Task<IActionResult> Delete([FromRoute] TKey key, [FromBody] TDto dto);
    [HttpGet]
    Task<IActionResult> Get([FromHeader] int page, [FromHeader] int limit);
    [HttpGet("{key}")]
    Task<IActionResult> Get([FromRoute] TKey key);
    [HttpPatch]
    Task<IActionResult> Patch([FromBody] TDto[] dtos);
    [HttpPatch("{key}")]
    Task<IActionResult> Patch([FromRoute] TKey key, [FromBody] TDto dto);
    [HttpPost]
    Task<IActionResult> Post([FromBody] QueryParameters query);
    [HttpPost]
    Task<IActionResult> Post([FromBody] TDto[] dtos);
    [HttpPost("{key}")]
    Task<IActionResult> Post([FromRoute] TKey key, [FromBody] TDto dto);
    [HttpPut]
    Task<IActionResult> Put([FromBody] TDto[] dtos);
    [HttpPut("{key}")]
    Task<IActionResult> Put([FromRoute] TKey key, [FromBody] TDto dto);
}