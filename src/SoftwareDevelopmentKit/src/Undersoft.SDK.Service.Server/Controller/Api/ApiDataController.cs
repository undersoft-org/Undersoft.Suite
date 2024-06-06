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
using Undersoft.SDK.Service.Data.Repository;
using Undersoft.SDK.Service.Data.Store;

[ApiController]
[ApiData]
[Route($"{StoreRoutes.ApiDataRoute}/[controller]")]
public abstract class ApiDataController<TKey, TStore, TEntity, TDto, TService>
    : ApiServiceController<TStore, TService, TDto>,
        IApiDataController<TKey, TEntity, TDto>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TService : class
{
    protected Func<TKey, Func<TDto, object>> _keysetter = k => e => e.SetId(k);
    protected Func<TKey, Expression<Func<TEntity, bool>>> _keymatcher;
    protected Func<TDto, Expression<Func<TEntity, bool>>> _predicate;
    protected EventPublishMode _publishMode;

    protected ApiDataController() { }

    protected ApiDataController(IServicer servicer) : base(servicer) { }

    protected ApiDataController(
        IServicer servicer,
        EventPublishMode publishMode = EventPublishMode.PropagateCommand
    ) : this(servicer, null, k => e => e.SetId(k), null, publishMode) { }

    protected ApiDataController(
        IServicer servicer,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate,
        Func<TKey, Func<TDto, object>> keysetter,
        Func<TKey, Expression<Func<TEntity, bool>>> keymatcher,
        EventPublishMode publishMode = EventPublishMode.PropagateCommand
    ) : base(servicer)
    {
        _keymatcher = keymatcher;
        _keysetter = keysetter;
        _publishMode = publishMode;
    }

    public virtual Func<IRepository<TEntity>, IQueryable<TEntity>> Transformations { get; set; }

    [HttpGet]
    public virtual async Task<IActionResult> Get([FromHeader] int page, [FromHeader] int limit)
    {
        return Ok((await _servicer
                .Report(new Get<TStore, TEntity, TDto>((page - 1) * limit, limit))).Result.Commit()
        );
    }

    [HttpGet("count")]
    public virtual async Task<IActionResult> Count()
    {
        return Ok(await Task.Run(() => _servicer.StoreSet<TStore, TEntity>().Query.Count()));
    }

    [HttpGet("{key}")]
    public virtual async Task<IActionResult> Get([FromRoute] TKey key)
    {
        return Ok((
            _keymatcher == null
                ? await _servicer
                    .Report(new Find<TStore, TEntity, TDto>(key))
                : await _servicer
                    .Report(
                        new Find<TStore, TEntity, TDto>(
                                new QueryParameters<TEntity>() { Filter = _keymatcher(key) }
                        ))).Result.FirstOrDefault()
        );
    }

    [HttpPost("query")]
    public virtual async Task<IActionResult> Post([FromBody] QueryParameters query)
    {
        var entity = typeof(TEntity).ToProxy();

        query.FilterItems.ForEach(
            (fi) =>
                fi.Value = JsonSerializer.Deserialize(
                    ((JsonElement)fi.Value).GetRawText(),
                    entity.Rubrics[fi.Member].RubricType
                )
        );

        var param = new QueryParameters<TEntity>(query.FilterItems, query.SortItems);

        return Ok(
            (await _servicer
                .Report(new Filter<TStore, TEntity, TDto>(0, 0, param))
                ).Result.Commit()
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
            .ForEach(c => c.IsValid ? c.Id.ToString() : c.ErrorMessages)
            .ToArray();

        return result.Any(c => !c.IsValid) ? UnprocessableEntity(response) : Ok(response);
    }

    protected virtual async Task<IActionResult> Execute<TResult>(IRequest<TResult> request)
        where TResult : Command<TDto>
    {
        var result = await _servicer.Send(request).ConfigureAwait(false);

        return !result.IsValid ? UnprocessableEntity(result.ErrorMessages) : Ok(result.Id.ToString());
    }
}
