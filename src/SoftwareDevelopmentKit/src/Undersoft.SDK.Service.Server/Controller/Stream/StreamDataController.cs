using System.Linq.Expressions;
using System.Text.Json;

namespace Undersoft.SDK.Service.Server.Controller.Stream;

using Microsoft.AspNetCore.Mvc;
using Operation.Command;
using Operation.Query;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Response;
using Undersoft.SDK.Service.Data.Store;

[StreamData]
public abstract class StreamDataController<TKey, TEntry, TReport, TEntity, TDto> : ControllerBase, IStreamDataController<TDto>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TEntry : IDataServerStore
    where TReport : IDataServerStore
{
    protected Func<TKey, Func<TDto, object>> _keysetter = k => e => e.SetId(k);
    protected Func<TKey, Expression<Func<TEntity, bool>>> _keymatcher;
    protected Func<TDto, Expression<Func<TEntity, bool>>> _predicate;
    protected readonly IServicer _servicer;
    protected readonly EventPublishMode _publishMode;

    public StreamDataController() : this(new Servicer(), null, k => e => e.SetId(k), null, EventPublishMode.PropagateCommand) { }

    public StreamDataController(IServicer servicer) : this(servicer, null, k => e => e.SetId(k), null, EventPublishMode.PropagateCommand) { }

    public StreamDataController(IServicer servicer,
        Func<TDto, Expression<Func<TEntity, bool>>> predicate,
        Func<TKey, Func<TDto, object>> keysetter,
        Func<TKey, Expression<Func<TEntity, bool>>> keymatcher,
        EventPublishMode publishMode = EventPublishMode.PropagateCommand
    )
    {
        _keymatcher = keymatcher;
        _keysetter = keysetter;
        _servicer = servicer;
        _publishMode = publishMode;
    }

    public virtual IAsyncEnumerable<TDto> All()
    {
        return _servicer.CreateStream(new GetAsync<TReport, TEntity, TDto>(0, 0));
    }

    async Task<ResultString> IStreamDataController<TDto>.Count()
    {
        return await Task.FromResult(new ResultString(_servicer.StoreSet<TReport, TEntity>().Count().ToString()));
    }

    public virtual IAsyncEnumerable<TDto> Query(QueryParameters query)
    {
        var entity = typeof(TEntity).ToProxy();

        query.FilterItems.ForEach(
            (fi) =>
                fi.Value = JsonSerializer.Deserialize(
                    ((JsonElement)fi.Value).GetRawText(),
                    entity.Rubrics[fi.Member].RubricType)
        );

        var param = new QueryParameters<TEntity>()
        {
            Filter = query.GetFilter<TEntity>(),
            Sort = query.GetSort<TEntity>()
        };

        return
            _servicer
                .CreateStream(
                    new FilterAsync<TReport, TEntity, TDto>(0, 0, param
                    )
                );
    }

    public virtual IAsyncEnumerable<ResultString> Creates(TDto[] dtos)
    {
        var result = _servicer.CreateStream(new CreateSetAsync<TEntry, TEntity, TDto>
                                                    (_publishMode, dtos));

        var response = result.ForEachAsync(c => new ResultString(c.IsValid
                                             ? c.Id.ToString()
                                             : c.ErrorMessages));
        return response;
    }

    public virtual IAsyncEnumerable<ResultString> Changes(TDto[] dtos)
    {
        var result = _servicer.CreateStream(new ChangeSetAsync<TEntry, TEntity, TDto>
                                                   (_publishMode, dtos));

        var response = result.ForEachAsync(c => new ResultString(c.IsValid
                                              ? c.Id.ToString()
                                              : c.ErrorMessages));
        return response;
    }

    public virtual IAsyncEnumerable<ResultString> Updates(TDto[] dtos)
    {
        var result = _servicer.CreateStream(new UpdateSetAsync<TEntry, TEntity, TDto>
                                                 (_publishMode, dtos));

        var response = result.ForEachAsync(c => new ResultString(c.IsValid
                                              ? c.Id.ToString()
                                              : c.ErrorMessages));
        return response;
    }

    public virtual IAsyncEnumerable<ResultString> Deletes(TDto[] dtos)
    {
        var result = _servicer.CreateStream(new DeleteSetAsync<TEntry, TEntity, TDto>
                                                  (_publishMode, dtos));

        var response = result.ForEachAsync(c => new ResultString(c.IsValid
                                             ? c.Id.ToString()
                                             : c.ErrorMessages));
        return response;
    }
}
