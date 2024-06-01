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

    public ViewDataStore(IRemoteRepository<TStore, TModel>? repository) : this()
    {
        if (repository != null)
        {
            _repository = repository;
            Root = this;
        }
    }

    public ViewDataStore(
        IRemoteRepository<TStore, TModel>? repository,
        Action<ViewDataStore<TStore, TModel>>? setup
    ) : this(repository)
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

    public IViewStore ViewStore { get; set; } = default!;

    public IList<TModel> Models { get; set; } = default!;

    public IQueryable<IViewData> Items { get; set; } = default!;

    private Expression<Func<TModel, bool>>? _predicate => Query.GetFilter<TModel>();

    private SortExpression<TModel> _sort => Query.GetSort<TModel>();

    private Expression<Func<TModel, object>>[] _expanders => Query.GetExpanders<TModel>();

    public IQueryParameters<TModel> Query { get; set; } = new QueryParameters<TModel>();

    public Pagination Pagination { get; set; } = new Pagination();

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
        return data;
    }

    public virtual void Load(IList<TModel> models)
    {
        Models = models;

        IQueryable<TModel>? query = null;
        if (_predicate != null)
            query = _sort.Sort(Models.AsQueryable().Where(_predicate));
        else
            query = _sort.Sort(Models.AsQueryable());
        query = query.Skip(Pagination.Offset).Take(Pagination.PageSize);

        Items = query.ForEach(d => Attach(d));

        Pagination.SetTotalCount(query.Count());
    }

    public virtual async Task LoadAsync(TModel model)
    {
        await Task.Run(() =>
        {
            var models = new Listing<TModel>();
            models.Add(model);
            Models = models;
            Items = Attach(model).ToQueryable();
            Pagination.SetTotalCount(1);
        });
    }

    public virtual async Task LoadAsync()
    {
        if (_repository != null)
        {
            bool includeCount = false;
            if (Pagination.TotalCount < 0 || Pagination.CountChanged)
                includeCount = true;
            IEnumerable<TModel> models;
            if (_predicate != null)
            {
                models = await (
                    (DataServiceQuery<TModel>)
                        _repository[_predicate, _sort, _expanders]
                            .Skip(Pagination.Offset)
                            .Take(Pagination.PageSize)
                )
                    .IncludeCount(includeCount)
                    .ExecuteAsync();
            }
            else
            {
                models = await (
                    (DataServiceQuery<TModel>)
                           _repository[_sort, _expanders]
                            .Skip(Pagination.Offset)
                            .Take(Pagination.PageSize)
                )
                    .IncludeCount(includeCount)
                    .ExecuteAsync();
            }
            if (includeCount)
            {
                Pagination.SetTotalCount((int)((QueryOperationResponse<TModel>)models).Count);
                Pagination.CountChanged = false;
            }
            Models = models.ToListing();
            Items = Models.ForEach(m => Attach(m, true)).AsQueryable();
        }
        else
        {
            IQueryable<TModel>? query = null;
            if (_predicate != null)
                query = _sort.Sort(Models.AsQueryable().Where(_predicate));
            else
                query = _sort.Sort(Models.AsQueryable());

            Pagination.SetTotalCount(query.Count());

            query = query.Skip(Pagination.Offset).Take(Pagination.PageSize);

            Items = query.ForEach(d => Attach(d));
        }
    }

    public virtual void OnLoadCompleted(object? sender, LoadCompletedEventArgs args)
    {
        if (sender != null)
        {
            Models = (IList<TModel>)sender;

            Items = Models.ForEach(m => Attach(m, true)).AsQueryable();
        }
    }

    public virtual async Task LoadAsync(object key)
    {
        var m = await _repository.Find(key);
        if (m != null)
        {
            var models = new Listing<TModel>();
            models.Add(m);
            Models = models;
            Items = Attach(m).ToQueryable();
            Pagination.SetTotalCount(1);
        }
    }

    public virtual async Task SaveAsync()
    {
        if (Root != null)
        {
            if (Root.Any(m => m.StateFlags.Added))
                _repository
                    .Add(Root.Where(d => d.StateFlags.Added).Select(m => (TModel)m.Model))
                    .Commit();

            if (Root.Any(m => m.StateFlags.Changed))
                _repository
                    .Patch(Root.Where(d => d.StateFlags.Changed).Select(m => (TModel)m.Model))
                    .Commit();

            if (Root.Any(m => m.StateFlags.Updated))
                _repository
                    .Set(Root.Where(d => d.StateFlags.Updated).Select(m => (TModel)m.Model))
                    .Commit();

            if (Root.Any(m => m.StateFlags.Deleted))
                _repository
                    .Delete(Root.Where(d => d.StateFlags.Deleted).Select(m => (TModel)m.Model))
                    .Commit();
        }
        await _repository.Save(false);
    }
}
