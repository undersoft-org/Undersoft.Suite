using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Server.Controller.Open;

using Microsoft.AspNetCore.OData.Results;
using Operation.Command;
using Operation.Query;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

[OpenData]
public abstract class OpenDataController<TKey, TStore, TEntity, TDto, TService>
    : OpenServiceController<TStore, TService, TDto>,
        IOpenDataController<TKey, TEntity, TDto>
    where TDto : class, IOrigin, IInnerProxy, new()
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TService : class
{
    protected Func<TKey, Func<TDto, object>> _keysetter = k => e => e.SetId(k);
    protected Func<TKey, Expression<Func<TEntity, bool>>> _keymatcher = k => e => k.Equals(e.Id);
    protected Func<TDto, Expression<Func<TEntity, bool>>> _predicate;
    protected EventPublishMode _publishMode = EventPublishMode.PropagateCommand;

    public OpenDataController() { }

    public OpenDataController(IServicer servicer) : base(servicer) { }

    protected OpenDataController(
        IServicer servicer,
        EventPublishMode publishMode = EventPublishMode.PropagateCommand
    ) : this(servicer, null, k => e => e.SetId(k), k => e => k.Equals(e.Id), publishMode) { }

    protected OpenDataController(
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

    [EnableQuery]
    public virtual IQueryable<TDto> Get()
    {
        return _servicer.Send(new Get<TStore, TEntity, TDto>()).Result.Result;
    }

    [EnableQuery]
    public virtual SingleResult<TDto> Get([FromRoute] TKey key)
    {
        return new SingleResult<TDto>(
            _servicer
                .Send(
                    new Find<TStore, TEntity, TDto>(
                        new QueryParameters<TEntity>() { Filter = _keymatcher(key) }
                    )
                )
                .Result.Result
        );
    }

    public virtual async Task<IActionResult> Post(TDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _servicer.Send(new Create<TStore, TEntity, TDto>(_publishMode, dto));

        return !result.IsValid
            ? BadRequest(result.ErrorMessages)
            : Created(result.Contract);
    }

    public virtual async Task<IActionResult> Patch([FromRoute] TKey key, [FromBody] TDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _keysetter(key).Invoke(dto);

        var result = await _servicer.Send(
            new Change<TStore, TEntity, TDto>(_publishMode, dto, _predicate)
        );

        return !result.IsValid
            ? BadRequest(result.ErrorMessages)
            : Updated(result.Id as object);
    }

    public virtual async Task<IActionResult> Put([FromRoute] TKey key, [FromBody] TDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _keysetter(key).Invoke(dto);

        var result = await _servicer.Send(
            new Update<TStore, TEntity, TDto>(_publishMode, dto, _predicate)
        );

        return !result.IsValid
            ? BadRequest(result.ErrorMessages)
            : Updated(result.Id as object);
    }

    public virtual async Task<IActionResult> Delete([FromRoute] TKey key)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _servicer.Send(new Delete<TStore, TEntity, TDto>(_publishMode, key));

        return !result.IsValid
            ? BadRequest(result.ErrorMessages)
            : Ok(result.Id as object);
    }
}
