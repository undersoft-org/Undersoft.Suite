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
public abstract class OpenCqrsController<TKey, TEntry, TReport, TEntity, TDto, TService>
    : OpenDataController<TKey, TEntry, TEntity, TDto, TService>
    where TDto : class, IOrigin, IInnerProxy, new()
    where TEntity : class, IOrigin, IInnerProxy
    where TEntry : IDataServerStore
    where TReport : IDataServerStore
    where TService : class
{
    protected OpenCqrsController() { }

    public OpenCqrsController(IServicer servicer) : base(servicer) { }

    protected OpenCqrsController(
        IServicer servicer,
        EventPublishMode publishMode = EventPublishMode.PropagateCommand
    ) : this(servicer, null, k => e => e.SetId(k), k => e => k.Equals(e.Id), publishMode) { }

    protected OpenCqrsController(
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
    public override IQueryable<TDto> Get()
    {
        return _servicer.Send(new Get<TReport, TEntity, TDto>()).Result.Result;
    }

    [EnableQuery]
    public override SingleResult<TDto> Get([FromRoute] TKey key)
    {
        return new SingleResult<TDto>(
           _servicer
               .Send(
                   new Find<TReport, TEntity, TDto>(
                       new QueryParameters<TEntity>() { Filter = _keymatcher(key) }
                   )
               )
               .Result.Result
       );
    }

    public override async Task<IActionResult> Post([FromBody] TDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _servicer.Send(new Create<TEntry, TEntity, TDto>(_publishMode, dto));

        return !result.IsValid
            ? BadRequest(result.ErrorMessages)
            : Created(result.Contract);
    }

    public override async Task<IActionResult> Patch([FromRoute] TKey key, [FromBody] TDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _keysetter(key).Invoke(dto);

        var result = await _servicer.Send(
            new Change<TEntry, TEntity, TDto>(_publishMode, dto, _predicate)
        );

        return !result.IsValid
            ? BadRequest(result.ErrorMessages)
            : Updated(result.Id as object);
    }

    public override async Task<IActionResult> Put([FromRoute] TKey key, [FromBody] TDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _keysetter(key).Invoke(dto);

        var result = await _servicer.Send(
            new Update<TEntry, TEntity, TDto>(_publishMode, dto, _predicate)
        );

        return !result.IsValid
            ? BadRequest(result.ErrorMessages)
            : Updated(result.Id as object);
    }

    public override async Task<IActionResult> Delete([FromRoute] TKey key)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _servicer.Send(new Delete<TEntry, TEntity, TDto>(_publishMode, key));

        return !result.IsValid
            ? BadRequest(result.ErrorMessages)
            : Ok(result.Id as object);
    }
}
