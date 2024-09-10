using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Client;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Updating;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewDataStore<TStore, TDto, TModel> : ViewData<TModel>, IViewDataStore<TModel>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected IRemoteRepository<TStore, TDto>? _repository;

    protected IServicer? _servicer => ViewStore?.Servicer;

    protected IServiceScope? _session;

    protected IViewStore? _store;

    public IAuthorization? Authorization { get; set; }

    public ViewDataStore()
        : base()
    {
        Pagination = new Pagination();
    }

    public ViewDataStore(TModel model)
        : base(model)
    {
        Pagination = new Pagination();
    }

    public ViewDataStore(IList<TModel> models)
        : this()
    {
        Load(models);
    }

    public ViewDataStore(IViewStore? store)
        : this()
    {
        if (store != null)
        {
            Root = this;
            ViewItem = store;
            MapRubrics();
            OpenSession();
        }
    }

    public ViewDataStore(IViewStore store, Action<ViewDataStore<TStore, TDto, TModel>>? setup)
        : this(store)
    {
        if (setup != null)
            setup(this);
    }

    public virtual void MapRubrics()
    {
        base.MapRubrics(t => t.Rubrics, p => p.Visible);
        base.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
    }

    public virtual IQueryParameters MapQuery(Func<IViewRubric, bool>? predicate = null)
    {
        var query = Rubrics.GetQuery(predicate);

        if (SearchFilters != null)
            query.FilterItems.Add(SearchFilters);

        Query = new QueryParameters<TDto>(query.FilterItems, query.SortItems);
        Query.Offset = Pagination!.Offset;
        Query.Limit = Pagination.PageSize;
        return Query;
    }

    public virtual IRemoteRepository<TStore, T> RemoteSet<T>()
        where T : class, IOrigin, IInnerProxy
    {
        return _session!.ServiceProvider.GetRequiredService<IRemoteRepository<TStore, T>>();
    }

    public virtual IRemoteRepository<TStore, TDto> RemoteStore()
    {
        if (_repository == null || _repository.InnerContext == null)
        {
            var repository = RemoteSet<TDto>();
            _repository = repository;
            return repository;
        }
        return _repository;
    }

    public virtual void OpenSession()
    {
        if (_servicer != null)
        {
            if (_session != null)
                throw new Exception("Session already opened");
            _session = _servicer.CreateScope();
            _repository = RemoteSet<TDto>();
            SetAuthorization();
        }
    }

    public void SetAuthorization()
    {
        if (Authorization != null && _repository != null)
        {
            _repository.SetAuthorization(Authorization.Credentials.SessionToken);
        }
    }

    public virtual async Task CommitAsync(IRepository? repository = null)
    {
        if (Root != null)
        {
            ViewStore!.ProgressVisible = true;
            if (repository != null)
            {
                await CommitToRemoteAsync(repository);
            }
            else
            {
                await CommitToLocalAsync();
            }
            ViewStore!.ProgressVisible = false;
            ViewItem?.RenderView();
        }
    }

    public virtual async Task SaveAsync(bool changesets = false)
    {
        var savingSession = _session;
        var savingRepository = _repository;
        _session = null!;

        OpenSession();

        using (savingSession)
        {
            await CommitAsync(savingRepository);
        }
    }

    public async Task StageAsync(bool changesets = false)
    {
        await CommitAsync(_repository);
    }

    protected async Task CloseAsync(bool changesets = false)
    {
        using (_session)
        {
            await CommitAsync(_repository);
        }
    }

    public IQueryParameters? Query { get; set; }

    public virtual async Task LoadSingleAsync(TModel single)
    {
        await Task.Run(() =>
        {
            var m = RemoteStore().Find<TModel>(single.Id);
            if (m != null)
            {
                var models = new Listing<TModel>();
                models.Add(m);
                Models = models;
                Items = new Listing<IViewData>(Attach(m).ToEnumerable());
                Pagination.SetTotalCount(1);
            }
        });
    }

    private async Task<QueryOperationResponse<TDto>> LoadRemoteAsync(
        int offset,
        int pageSize,
        bool includeCount
    )
    {
        MapQuery();

        _repository = RemoteStore();

        SetAuthorization();

        var predicate = Query!.GetFilter<TDto>();
        var sort = Query.GetSort<TDto>();
        var expands = Query.GetExpanders<TDto>();

        IQueryable<TDto> query =
            predicate != null
                ? _repository[offset, pageSize, _repository[predicate, sort, expands]]
                : _repository[offset, pageSize, _repository[sort, expands]];

        return (QueryOperationResponse<TDto>)(
            await ((DataServiceQuery<TDto>)query).IncludeCount(includeCount).ExecuteAsync()
        );
    }

    public virtual async Task LoadAsync()
    {
        IEnumerable<IViewData> loadedData;
        if (_session != null)
        {
            bool includeCount = false;
            if (Pagination!.TotalCount < 0 || Pagination.CountChanged)
                includeCount = true;

            var response = await LoadRemoteAsync(
                Pagination.Offset,
                Pagination.PageSize,
                includeCount
            );

            if (includeCount)
            {
                Pagination.SetTotalCount((int)response.Count);
                Pagination.CountChanged = false;
            }

            if (Models == null)
                Models = new Listing<TModel>();

            if (typeof(TDto) != typeof(TModel))
                loadedData = Items = response
                    .ForEach(r => Attach(Models.Put(r.PutTo<TModel>()).Value))
                    .ToListing();
            else
                loadedData = Items = response
                    .Cast<TModel>()
                    .ForEach(r => Attach(Models.Put(r).Value))
                    .ToListing();
        }
        else
        {
            if (Models == null || Models.Count == 0)
                return;

            loadedData = await LoadLocalAsync();
        }
        if (LoadCompleted != null)
            LoadCompleted.Invoke(this, loadedData);
    }

    public virtual event EventHandler<IEnumerable<IViewData>>? LoadCompleted;

    public virtual async Task UnloadAsync(IList<TModel> models)
    {
        if (models == null || models.Count == 0)
            return;
        IEnumerable<IViewData> unloadedData;
        if (Models != null && Models.Count >= models.Count)
        {
            unloadedData = models
                .ForEach(m =>
                {
                    return Detach(Models.Remove(m));
                })
                .Commit();
        }
        else
            return;

        Pagination!.DecreaseTotalCount(models.Count);

        if (
            _session != null
            && (Pagination.TotalCount + models.Count) > (Pagination.Offset + Pagination.PageSize)
        )
        {
            var response = await LoadRemoteAsync(Pagination.Offset, Pagination.PageSize, false);

            if (Models == null)
                Models = new Listing<TModel>();

            if (typeof(TDto) != typeof(TModel))
                Items = response
                    .ForEach(r => Attach(Models.Put(r.PutTo<TModel>()).Value))
                    .ToListing();
            else
                Items = response
                    .Cast<TModel>()
                    .ForEach(r => Attach(Models.Put(r).Value))
                    .ToListing();
        }
        else
        {
            await LoadLocalAsync();
        }

        if (UnloadCompleted != null)
            UnloadCompleted.Invoke(this, unloadedData);
    }

    public virtual event EventHandler<IEnumerable<IViewData>>? UnloadCompleted;

    private async Task CommitToRemoteAsync(IRepository repository)
    {
        if (Root != null)
        {
            var append = (IRemoteRepository<TStore, TDto>)repository;

            if (Root.Any(m => m.StateFlags.Added))
                await LoadAsync(
                    Root.Where(d => d.StateFlags.Added)
                        .ForEach(m =>
                        {
                            m.StateFlags.Added = false;
                            var model = (TModel)m.Model;
                            append.AddBy(model);
                            return model;
                        })
                        .Commit()
                );

            if (Root.Any(m => m.StateFlags.Changed))
                await LoadAsync(
                    await Task.WhenAll(
                        Root.Where(d => d.StateFlags.Changed)
                            .ForEach(async m =>
                            {
                                m.StateFlags.Changed = false;
                                var model = (TModel)m.Model;
                                await append.PatchBy(model);
                                return model;
                            })
                            .Commit()
                    ),
                    true
                );

            if (Root.Any(m => m.StateFlags.Updated))
                await LoadAsync(
                    await Task.WhenAll(
                        Root.Where(d => d.StateFlags.Updated)
                            .ForEach(async m =>
                            {
                                m.StateFlags.Updated = false;
                                var model = (TModel)m.Model;
                                await append.SetBy(model);
                                return model;
                            })
                            .Commit()
                    ),
                    true
                );

            if (Root.Any(m => m.StateFlags.Deleted))
                await UnloadAsync(
                    await Task.WhenAll(
                        Root.Where(d => d.StateFlags.Deleted)
                            .ForEach(async m =>
                            {
                                m.StateFlags.Deleted = false;
                                var model = (TModel)m.Model;
                                await append.DeleteBy(model);
                                return model;
                            })
                            .Commit()
                    )
                );
        }
    }

    private async Task CommitToLocalAsync()
    {
        if (Root != null)
        {
            if (Root.Any(m => m.StateFlags.Added))
                await LoadAsync(
                    Root.Where(d => d.StateFlags.Added)
                        .ForEach(m =>
                        {
                            m.StateFlags.Added = false;
                            return (TModel)m.Model;
                        })
                        .Commit()
                );

            if (Root.Any(m => m.StateFlags.Changed))
                await LoadAsync(
                    Root.Where(d => d.StateFlags.Changed)
                        .ForEach(m =>
                        {
                            m.StateFlags.Changed = false;
                            return (TModel)m.Model;
                        })
                        .Commit(),
                    true
                );

            if (Root.Any(m => m.StateFlags.Updated))
                await LoadAsync(
                    Root.Where(d => d.StateFlags.Updated)
                        .ForEach(m =>
                        {
                            m.StateFlags.Updated = false;
                            return (TModel)m.Model;
                        })
                        .Commit(),
                    true
                );

            if (Root.Any(m => m.StateFlags.Deleted))
                await UnloadAsync(
                    Root.Where(d => d.StateFlags.Deleted)
                        .ForEach(m =>
                        {
                            m.StateFlags.Deleted = false;
                            return (TModel)m.Model;
                        })
                );
        }
    }

    public IViewDataStore<TModel> LocalStore() => this;

    public IViewStore? ViewStore =>
        _store ??=
            (ViewItem != null && ViewItem.GetType().IsAssignableTo(typeof(IViewStore)))
                ? (IViewStore?)ViewItem!
                : null;

    public ISeries<TModel>? Models { get; set; }

    public ISeries<IViewData>? Items { get; set; }

    public Pagination? Pagination { get; set; }

    public virtual IViewData Attach(object model, bool patch = false)
    {
        return this.Attach((TModel)model, patch);
    }

    public virtual IViewData Attach(TModel model, bool patch = false)
    {
        if (!TryGet(model.Id, out IViewData data))
        {
            data = new ViewData<TModel>(model) { Root = this.Root, Parent = this };

            data.Rubrics.Add(Rubrics);
            data.ExtendedRubrics.Add(ExtendedRubrics);
            data.ValidatorType = this.ValidatorType;
            data.Validator = this.Validator;

            var extends = ExtendedRubrics
                .ForEach(r =>
                {
                    var type = r.RubricType;
                    var id = r.RubricId;
                    var value = model.Proxy[id];
                    if (value == null)
                    {
                        value = type.New();
                        model.Proxy[id] = value;
                    }

                    if (type.IsAssignableTo(typeof(IEnumerable)))
                    {
                        type = type.GetEnumerableElementType();
                        return typeof(ViewDataStore<,>)
                            .MakeGenericType(typeof(TStore), type)
                            .New<IViewData>(value);
                    }
                    else
                    {
                        return typeof(ViewData<>).MakeGenericType(type).New<IViewData>(value);
                    }
                })
                .Commit();

            if (extends.Any())
                data.Add(extends);

            this.Add(data);
        }
        else if (patch)
        {
            model.PatchTo(data.Model);
        }
        data.StateFlags.ClearCommandStates();
        return data;
    }

    public virtual IViewData Attach(IViewData viewData, bool patch = false)
    {
        if (!TryGet(viewData.Model.Id, out IViewData data))
        {
            data = viewData;
            data.Root = this.Root;
            data.Parent = this;
            data.Rubrics.Add(Rubrics);
            data.ExtendedRubrics.Add(ExtendedRubrics);

            var extends = ExtendedRubrics
                .ForEach(r =>
                {
                    var type = r.RubricType;
                    var id = r.RubricId;
                    var value = data.Model.Proxy[id];
                    if (value == null)
                    {
                        value = type.New();
                        data.Model.Proxy[id] = value;
                    }

                    if (type.IsAssignableTo(typeof(IEnumerable)))
                        type = type.GetEnumerableElementType();

                    return typeof(ViewDataStore<,>)
                        .MakeGenericType(typeof(TStore), type)
                        .New<IViewData>(value);
                })
                .Commit();

            if (extends.Any())
                data.Add(extends);

            this.Add(data);
        }
        else if (patch && ((TModel)viewData.Model).Modified != ((TModel)data.Model).Modified)
        {
            viewData.Model.PatchTo(data.Model);
        }
        data.StateFlags.ClearCommandStates();
        return data;
    }

    public virtual IViewData Detach(TModel model)
    {
        if (TryGet(model.Id, out IViewData data))
        {
            data = this.Remove(model);
        }
        data.StateFlags.ClearCommandStates();
        return data;
    }

    public virtual IViewData Detach(object model)
    {
        return this.Detach((TModel)model);
    }

    protected Task<ISeries<IViewData>> LoadLocalAsync(bool patch = false)
    {
        return Task.Factory.StartNew(() =>
        {
            return LoadLocal(patch);
        });
    }

    protected ISeries<IViewData> LoadLocal(bool patch = false)
    {
        MapQuery();
        var predicate = Query?.GetFilter<TModel>();
        var sort = Query?.GetSort<TModel>();
        var expands = Query?.GetExpanders<TModel>();

        IQueryable<TModel>? query = null;
        if (predicate != null)
            query = sort?.Sort(Models?.AsQueryable().Where(predicate));
        else
            query = sort?.Sort(Models?.AsQueryable());

        query = query?.Skip(Pagination!.Offset).Take(Pagination.PageSize);

        return Items = query.ForEach(d => Attach(d, patch)).ToListing();
    }

    public virtual void Load(IList<TModel> models, bool patch = false)
    {
        if (models == null || models.Count == 0)
            return;

        if (Models != null && Models.Count > 0)
            Models.Put(models);
        else
            Models = models.ToListing();

        var loadedData = LoadLocal(patch);

        if(!patch)
            Pagination!.IncreaseTotalCount(models.Count);

        if (LoadCompleted != null)
            LoadCompleted.Invoke(this, loadedData);
    }

    public virtual void Load(IEnumerable<object> models, bool patch = false)
    {
        Load(models.Cast<TModel>().ToListing(), patch);
    }

    public virtual async Task LoadAsync(IEnumerable<object> models, bool patch = false)
    {
        await Task.Factory.StartNew(() => Load(models));
    }

    public virtual async Task LoadAsync(IList<TModel> models, bool patch = false)
    {
        await Task.Factory.StartNew(() => Load(models, patch));
    }

    public virtual async Task UnloadAsync()
    {
        await Task.Run(() =>
        {
            if (Models != null && Models.Count > 0)
            {
                Models.Clear();
                this.Clear();
            }
            else
                return;

            Items = this;

            Pagination?.SetTotalCount(-1);
        });
    }

    public virtual async Task UnloadAsync(IEnumerable<object> models)
    {
        await UnloadAsync(models.Cast<TModel>().ToListing());
    }

    public virtual async Task LoadSingleAsync(object single)
    {
        await this.LoadSingleAsync((TModel)single);
    }
}

public class ViewDataStore<TStore, TModel> : ViewDataStore<TStore, TModel, TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    public ViewDataStore()
        : base()
    {
        MapRubrics(t => t.Rubrics, p => p.Visible);
    }

    public ViewDataStore(TModel model)
        : base(model) { }

    public ViewDataStore(IList<TModel> models)
        : base(models) { }

    public ViewDataStore(IViewStore? store)
        : base(store) { }

    public ViewDataStore(IViewStore store, Action<ViewDataStore<TStore, TModel>>? setup)
        : base(store)
    {
        if (setup != null)
            setup(this);
    }
}
