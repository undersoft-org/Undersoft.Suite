using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;

namespace Undersoft.SDK.Service.Server.Controller.Api;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Operation.Remote.Command;
using Operation.Remote.Query;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

[ApiDataRemote]
[ApiController]
[Route($"{StoreRoutes.ApiDataRoute}/[controller]")]
public abstract class ApiDataRemoteController<TKey, TStore, TDto, TModel, TService>
    : ApiServiceRemoteController<TStore, TService, TModel>,
        IApiDataRemoteController<TKey, TDto, TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
    where TService : class
{
    protected Func<TKey, Func<TModel, object>> _keysetter = k => e => e.SetId(k);
    protected Func<TKey, Expression<Func<TDto, bool>>> _keymatcher;
    protected Func<TModel, Expression<Func<TDto, bool>>> _predicate;
    protected readonly EventPublishMode _publishMode;

    protected ApiDataRemoteController() { }

    protected ApiDataRemoteController(
        IServicer servicer,
        EventPublishMode publishMode = EventPublishMode.PropagateCommand
    ) : this(servicer, null, k => e => e.SetId(k), null, publishMode) { }

    protected ApiDataRemoteController(
        IServicer servicer,
        Func<TModel, Expression<Func<TDto, bool>>> predicate
    ) : this(servicer, predicate, k => e => e.SetId(k), null, EventPublishMode.PropagateCommand) { }

    protected ApiDataRemoteController(
        IServicer servicer,
        Func<TModel, Expression<Func<TDto, bool>>> predicate,
        Func<TKey, Func<TModel, object>> keysetter,
        Func<TKey, Expression<Func<TDto, bool>>> keymatcher,
        EventPublishMode publishMode = EventPublishMode.PropagateCommand
    ) : base(servicer)
    {
        _keymatcher = keymatcher;
        _keysetter = keysetter;
        _publishMode = publishMode;
    }

    [HttpGet]
    public virtual async Task<IActionResult> Get([FromHeader] int page, [FromHeader] int limit)
    {
        return Ok(
            (
                await _servicer.Report(
                    new RemoteGet<TStore, TDto, TModel>((page - 1) * limit, limit)
                )
            ).Result.Commit()
        );
    }

    [HttpGet("count")]
    public virtual async Task<IActionResult> Count()
    {
        return Ok(await Task.Run(() => _servicer.RemoteSet<TStore, TDto>().Query.Count()));
    }

    [HttpGet("{key}")]
    public virtual async Task<IActionResult> Get(TKey key)
    {
        return Ok(
            (
                _keymatcher == null
                    ? await _servicer
                        .Report(new RemoteFind<TStore, TDto, TModel>(key))
                    : await _servicer
                        .Report(
                            new RemoteFind<TStore, TDto, TModel>(
                                new QueryParameters<TDto>() { Filter = _keymatcher(key) }
                            )
                        )
            ).Result.FirstOrDefault()
        );
    }

    [HttpPost("query")]
    public virtual async Task<IActionResult> Post([FromBody] QueryParameters query)
    {
        var entity = typeof(TDto).ToProxy();

        query.FilterItems.ForEach(
            (fi) =>
                fi.Value = JsonSerializer.Deserialize(
                    ((JsonElement)fi.Value).GetRawText(),
                    entity.Rubrics[fi.Member].RubricType
                )
        );

        var param = new QueryParameters<TDto>()
        {
            Filter = query.GetFilter<TDto>(),
            Sort = query.GetSort<TDto>()
        };

        return Ok(
            (
                await _servicer
                    .Report(new RemoteFilter<TStore, TDto, TModel>(0, 0, param))
            ).Result.Commit()
        );
    }

    [HttpPost]
    public virtual async Task<IActionResult> Post([FromBody] TModel[] dtos)
    {
        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await ExecuteSet(new RemoteCreateSet<TStore, TDto, TModel>(_publishMode, dtos));
    }

    [HttpPost("{key}")]
    public virtual async Task<IActionResult> Post([FromRoute] TKey key, [FromBody] TModel dto)
    {
        _keysetter(key).Invoke(dto);

        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await Execute(new RemoteCreate<TStore, TDto, TModel>(_publishMode, dto));
    }

    [HttpPatch]
    public virtual async Task<IActionResult> Patch([FromBody] TModel[] dtos)
    {
        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await ExecuteSet(
                new RemoteChangeSet<TStore, TDto, TModel>(_publishMode, dtos, _predicate)
            );
    }

    [HttpPatch("{key}")]
    public virtual async Task<IActionResult> Patch([FromRoute] TKey key, [FromBody] TModel dto)
    {
        _keysetter(key).Invoke(dto);

        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await Execute(new RemoteChange<TStore, TDto, TModel>(_publishMode, dto, _predicate));
    }

    [HttpPut]
    public virtual async Task<IActionResult> Put([FromBody] TModel[] dtos)
    {
        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await ExecuteSet(
                new RemoteUpdateSet<TStore, TDto, TModel>(_publishMode, dtos, _predicate)
            );
    }

    [HttpPut("{key}")]
    public virtual async Task<IActionResult> Put([FromRoute] TKey key, [FromBody] TModel dto)
    {
        _keysetter(key).Invoke(dto);

        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await Execute(new RemoteUpdate<TStore, TDto, TModel>(_publishMode, dto, _predicate));
    }

    [HttpDelete]
    public virtual async Task<IActionResult> Delete([FromBody] TModel[] dtos)
    {
        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await ExecuteSet(new RemoteDeleteSet<TStore, TDto, TModel>(_publishMode, dtos));
    }

    [HttpDelete("{key}")]
    public virtual async Task<IActionResult> Delete([FromRoute] TKey key, [FromBody] TModel dto)
    {
        _keysetter(key).Invoke(dto);

        return (!ModelState.IsValid)
            ? BadRequest(ModelState)
            : await Execute(new RemoteDelete<TStore, TDto, TModel>(_publishMode, dto));
    }

    protected virtual async Task<IActionResult> ExecuteSet<TResult>(IRequest<TResult> request)
        where TResult : RemoteCommandSet<TModel>
    {
        var result = await _servicer.Send(request).ConfigureAwait(false);

        object[] response = result
            .ForEach(c => c.IsValid ? c.Id as object : c.ErrorMessages)
            .ToArray();

        return result.Any(c => !c.IsValid) ? UnprocessableEntity(response) : Ok(response);
    }

    protected virtual async Task<IActionResult> Execute<TResult>(IRequest<TResult> request)
        where TResult : RemoteCommand<TModel>
    {
        var result = await _servicer.Send(request).ConfigureAwait(false);

        return !result.IsValid ? UnprocessableEntity(result.ErrorMessages) : Ok(result.Id);
    }
}
