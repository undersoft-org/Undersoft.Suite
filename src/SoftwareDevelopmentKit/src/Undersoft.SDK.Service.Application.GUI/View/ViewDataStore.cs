using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Client;
using System.Collections;
using System.Linq.Expressions;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewDataStore<TStore, TModel> : ViewData<TModel>, IViewDataStore<TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    private IRemoteRepository<TStore, TModel> _repository = default!;

    private IServicer _servicer = default!;

    private IServiceScope _session = default!;

    public ViewDataStore() : base()
    {
        MapRubrics(t => t.Rubrics, p => p.Visible);
    }

    public ViewDataStore(TModel model) : base(model)
    {
        MapRubrics(t => t.Rubrics, p => p.Visible);
    }

    public ViewDataStore(IList<TModel> models) : this()
    {
        Load(models);
    }

    public ViewDataStore(IViewStore? store) : this()
    {
        if (store != null)
        {
            Root = this;
            ViewStore = store;
            _servicer = ViewStore.Servicer;
            OpenSession();
        }
    }

    public ViewDataStore(
        IViewStore store,
        Action<ViewDataStore<TStore, TModel>>? setup
    ) : this(store)
    {
        if (setup != null)
            setup(this);
    }

    public override IViewRubrics MapRubrics(
        Func<IViewData, IViewRubrics> forRubrics,
        Func<IRubric, bool> predicate
    )
    {
        base.MapRubrics(forRubrics, predicate);
        base.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);
        ExtendedRubrics.ForEach(r => Query.ExpandItems.Add(r.RubricName));
        return Rubrics;
    }

    private void OpenSession()
    {
        if (_servicer != null)
        {
            if (_session != null)
                throw new Exception("Session already opened");
            _session = _servicer.CreateScope();
            _repository = _session.ServiceProvider.GetRequiredService<IRemoteRepository<TStore, TModel>>();
        }
    }

    public virtual async Task SaveAsync(bool changesets = false)
    {
        var processSession = _session;
        var processRepository = _repository;
        _session = null!;

        OpenSession();

        using (processSession)
        {
            await SaveAsync(processRepository, changesets);
        }
    }

    private async Task CloseAsync(bool changesets = false)
    {
        using (_session)
        {
            await SaveAsync(_repository, changesets);
        }
    }

    public IViewStore ViewStore { get; set; } = default!;

    public ISeries<TModel> Models { get; set; } = default!;

    public ISeries<IViewData> Items { get; set; } = default!;

    private Expression<Func<TModel, bool>>? _predicate => Query.GetFilter<TModel>();

    private SortExpression<TModel> _sort => Query.GetSort<TModel>();

    private Expression<Func<TModel, object>>[] _expanders => Query.GetExpanders<TModel>();

    public IQueryParameters<TModel> Query { get; set; } = new QueryParameters<TModel>();

    public Pagination Pagination { get; set; } = new Pagination();

    public virtual IViewData Attach(object model, bool patch = false)
    {
        return this.Attach((TModel)model, patch);
    }

    public virtual IViewData Attach(TModel model, bool patch = false)
    {
        if (!TryGet(model.Id, out IViewData data))
        {
            data = new ViewData<TModel>(model) { Root = this.Root, Parent = this };

            data.MapRubrics(t => t.Rubrics, p => p.Visible);
            data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);

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
        else if (patch && model.Modified != ((TModel)data.Model).Modified)
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
            data.MapRubrics(t => t.Rubrics, p => p.Visible);
            data.MapRubrics(t => t.ExtendedRubrics, p => p.Extended);

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

    public virtual void Load(IList<TModel> models, bool patch = false)
    {
        if (models == null || models.Count == 0)
            return;

        if (Models != null && Models.Count > 0)
            Models.Put(models);
        else
            Models = models.ToListing();

        IQueryable<TModel>? query = null;
        if (_predicate != null)
            query = _sort.Sort(Models.AsQueryable().Where(_predicate));
        else
            query = _sort.Sort(Models.AsQueryable());

        query = query.Skip(Pagination.Offset).Take(Pagination.PageSize);

        Items = query.ForEach(d => Attach(d, patch)).ToListing();

        Pagination.IncreaseTotalCount(models.Count);

        if (LoadCompleted != null)
            LoadCompleted.Invoke(this, models);
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

    public virtual async Task LoadSingleAsync(TModel single)
    {
        var m = await _repository.Find(single.Id);
        if (m != null)
        {
            var models = new Listing<TModel>();
            models.Add(m);
            Models = models;
            Items = new Listing<IViewData>(Attach(m).ToEnumerable());
            Pagination.SetTotalCount(1);
        }
    }

    private async Task<QueryOperationResponse<TModel>> LoadRemoteAsync(int offset, int pageSize, bool includeCount)
    {
        if (_predicate != null)
        {
            return (QueryOperationResponse<TModel>)(await (
                (DataServiceQuery<TModel>)
                    _repository[_predicate, _sort, _expanders]
                        .Skip(offset)
                        .Take(pageSize)
            )
                .IncludeCount(includeCount)
                .ExecuteAsync());
        }
        else
        {
            return (QueryOperationResponse<TModel>)(await (
                (DataServiceQuery<TModel>)
                    _repository[_sort, _expanders]
                        .Skip(offset)
                        .Take(pageSize)
            )
                .IncludeCount(includeCount)
                .ExecuteAsync());
        }
    }

    public virtual async Task LoadAsync()
    {
        IEnumerable<TModel> models;
        if (_repository != null)
        {
            bool includeCount = false;
            if (Pagination.TotalCount < 0 || Pagination.CountChanged)
                includeCount = true;

            var response = await LoadRemoteAsync(Pagination.Offset, Pagination.PageSize, includeCount);

            if (includeCount)
            {
                Pagination.SetTotalCount((int)response.Count);
                Pagination.CountChanged = false;
            }

            if (Models != null && Models.Count > 0)
            {
                models = response.ToArray();
                Models.Put(models);
                Items = models.ForEach(m => Attach(m, false)).ToListing();
            }
            else
            {
                models = Models = response.ToListing();
                Items = Models.ForEach(m => Attach(m, false)).ToListing();
            }
        }
        else
        {
            if (Models == null || Models.Count == 0)
                return;

            IQueryable<TModel>? query = null;
            if (_predicate != null)
                query = _sort.Sort(Models.AsQueryable().Where(_predicate));
            else
                query = _sort.Sort(Models.AsQueryable());

            Pagination.SetTotalCount(query.Count());

            query = query.Skip(Pagination.Offset).Take(Pagination.PageSize);

            Items = query.ForEach(d => Attach(d)).ToListing();

            models = query.AsEnumerable();
        }
        if (LoadCompleted != null)
            LoadCompleted.Invoke(this, models);
    }

    public event EventHandler<IEnumerable<TModel>>? LoadCompleted;

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

            Pagination.SetTotalCount(-1);
        });
    }

    public virtual async Task UnloadAsync(IList<TModel> models)
    {
        if (models == null || models.Count == 0)
            return;

        if (Models != null && Models.Count >= models.Count)
        {
            models.ForEach(m =>
            {
                Models.Remove(m);
                Detach(m);
            });
        }
        else
            return;

        Pagination.DecreaseTotalCount(models.Count);

        if ((Pagination.TotalCount + models.Count) > (Pagination.Offset + Pagination.PageSize))
        {
            var response = await LoadRemoteAsync(Pagination.Offset, Pagination.PageSize, false);
            var array = response.ToArray();
            Models.Put(array);
            Items = array.ForEach(m => Attach(m, false)).ToListing();
        }
        else
        {
            IQueryable<TModel>? query = null;
            if (_predicate != null)
                query = _sort.Sort(Models.AsQueryable().Where(_predicate));
            else
                query = _sort.Sort(Models.AsQueryable());

            query = query.Skip(Pagination.Offset).Take(Pagination.PageSize);

            Items = query.ForEach(d => Attach(d)).ToListing();
        }

        if (UnloadCompleted != null)
            UnloadCompleted.Invoke(this, models);
    }

    public virtual async Task UnloadAsync(IEnumerable<object> models)
    {
        await UnloadAsync(models.Cast<TModel>().ToListing());
    }

    public event EventHandler<IEnumerable<TModel>>? UnloadCompleted;

    public virtual async Task LoadSingleAsync(object single)
    {
        await this.LoadSingleAsync((TModel)single);
    }

    private async Task SaveAsync(IRemoteRepository<TStore, TModel> repository, bool changesets = false)
    {
        if (Root != null)
        {
            var final = repository;

            if (Root.Any(m => m.StateFlags.Added))
                await LoadAsync(
                    final
                        .Add(Root.Where(d => d.StateFlags.Added).Select(m => (TModel)m.Model))
                        .Commit()
                );

            if (Root.Any(m => m.StateFlags.Changed))
                await LoadAsync(
                    Root.Where(d => d.StateFlags.Changed).ForEach(m => final.Patch((TModel)m.Model))
                        .Commit(),
                    true
                );

            if (Root.Any(m => m.StateFlags.Updated))
                await LoadAsync(
                     Root.Where(d => d.StateFlags.Updated).ForEach(m => final.Update((TModel)m.Model))
                        .Commit(),
                    true
                );

            if (Root.Any(m => m.StateFlags.Deleted))
                await UnloadAsync(
                        Root.Where(d => d.StateFlags.Deleted).ForEach(m => final.Delete((TModel)m.Model))
                        .Commit()
                );

            ViewStore.RenderView();
        }
    }
}
