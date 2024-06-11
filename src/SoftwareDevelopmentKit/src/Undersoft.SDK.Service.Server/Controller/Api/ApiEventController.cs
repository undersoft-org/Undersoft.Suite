using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;

namespace Undersoft.SDK.Service.Server.Controller.Api;
using MediatR;
using Operation.Command;
using Operation.Query;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Store;

[ApiData]
[ApiController]
[Route($"{StoreRoutes.ApiEventRoute}/[controller]")]
public abstract class ApiEventController<TKey, TStore, TEntity, TDto> : ControllerBase
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected Func<TKey, Func<TDto, object>> _keysetter = k => e => e.SetId(k);
    protected Func<TKey, Expression<Func<TEntity, bool>>> _keymatcher;
    protected Func<TDto, Expression<Func<TEntity, bool>>> _predicate;
    protected readonly IServicer _servicer;
    protected readonly EventPublishMode _publishMode;

    protected ApiEventController(IServicer servicer)
    {
        _servicer = servicer;
    }

    protected ApiEventController(
        IServicer servicer,
        Func<TKey, Expression<Func<TEntity, bool>>> keymatcher
    )
    {
        _servicer = servicer;
        _keymatcher = keymatcher;
    }

    [HttpGet]
    public virtual async Task<IActionResult> Get([FromHeader] int page, [FromHeader] int limit)
    {
        return Ok(
            (await _servicer
                .Send(new Get<TStore, TEntity, TDto>((page - 1) * limit, limit))).Result.Commit()
        );
    }

    [HttpGet("{key}")]
    public virtual async Task<IActionResult> Get([FromRoute] TKey key)
    {
        return Ok(
         (_keymatcher == null
               ? await _servicer
                   .Send(new Find<TStore, TEntity, TDto>(key))
               : await _servicer
                   .Send(
                       new Find<TStore, TEntity, TDto>(
                               new QueryParameters<TEntity>() { Filter = _keymatcher(key) }
                       )
                   )).Result.FirstOrDefault()
       );
    }

    [HttpGet("count")]
    public virtual async Task<IActionResult> Count()
    {
        return Ok(await Task.Run(() => _servicer.StoreSet<TStore, TEntity>().Count()));
    }

    [HttpPost("query")]
    public virtual async Task<IActionResult> Post([FromBody] QueryParameters query)
    {
        var entity = typeof(TEntity).ToProxy();

        query.FilterItems.ForEach(
            (fi) =>
                fi.Value = JsonSerializer.Deserialize(
                    ((JsonElement)fi.Value).GetRawText(),
                    entity.Rubrics[fi.Member].RubricType)
        );

        return Ok(
            (await _servicer
                .Send(new Filter<TStore, TEntity, TDto>(0, 0, new QueryParameters<TEntity>(query)))).Result.Commit()
        );
    }

    [HttpPost]
    public virtual async Task<IActionResult> Post([FromBody] TDto[] dtos)
    {
        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await ExecuteSet(new CreateSet<TStore, TEntity, TDto>(_publishMode, dtos));
    }

    [HttpPost("{key}")]
    public virtual async Task<IActionResult> Post([FromRoute] TKey key, [FromBody] TDto dto)
    {
        _keysetter(key).Invoke(dto);

        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await Execute(new Create<TStore, TEntity, TDto>(_publishMode, dto));
    }

    [HttpPatch]
    public virtual async Task<IActionResult> Patch([FromBody] TDto[] dtos)
    {
        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await ExecuteSet(
                new ChangeSet<TStore, TEntity, TDto>(_publishMode, dtos, _predicate)
            );
    }

    [HttpPatch("{key}")]
    public virtual async Task<IActionResult> Patch([FromRoute] TKey key, [FromBody] TDto dto)
    {
        _keysetter(key).Invoke(dto);

        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await Execute(new Change<TStore, TEntity, TDto>(_publishMode, dto, _predicate));
    }

    [HttpPut]
    public virtual async Task<IActionResult> Put([FromBody] TDto[] dtos)
    {
        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await ExecuteSet(
                new UpdateSet<TStore, TEntity, TDto>(_publishMode, dtos, _predicate)
            );
    }

    [HttpPut("{key}")]
    public virtual async Task<IActionResult> Put([FromRoute] TKey key, [FromBody] TDto dto)
    {
        _keysetter(key).Invoke(dto);

        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await Execute(new Update<TStore, TEntity, TDto>(_publishMode, dto, _predicate));
    }

    [HttpDelete]
    public virtual async Task<IActionResult> Delete([FromBody] TDto[] dtos)
    {
        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await ExecuteSet(new DeleteSet<TStore, TEntity, TDto>(_publishMode, dtos));
    }

    [HttpDelete("{key}")]
    public virtual async Task<IActionResult> Delete([FromRoute] TKey key, [FromBody] TDto dto)
    {
        _keysetter(key).Invoke(dto);

        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await Execute(new Delete<TStore, TEntity, TDto>(_publishMode, dto));
    }


    protected virtual async Task<IActionResult> ExecuteSet<TResult>(IRequest<TResult> request)
        where TResult : CommandSet<TDto>
    {
        var result = await _servicer.Send(request).ConfigureAwait(false);

        object[] response = result
            .ForEach(c => c.IsValid ? c.Id as object : c.ErrorMessages)
            .ToArray();

        return result.Any(c => !c.IsValid) ? UnprocessableEntity(response) : Ok(response);
    }

    protected virtual async Task<IActionResult> Execute<TResult>(IRequest<TResult> request)
        where TResult : Command<TDto>
    {
        var result = await _servicer.Send(request).ConfigureAwait(false);

        return !result.IsValid ? UnprocessableEntity(result.ErrorMessages) : Ok(result.Id);
    }
}
