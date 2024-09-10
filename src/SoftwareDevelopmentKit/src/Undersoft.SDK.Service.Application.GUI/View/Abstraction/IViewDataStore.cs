using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewDataStore<TModel> : IViewDataStore, IViewData<TModel> where TModel : class, IOrigin, IInnerProxy
    {
        ISeries<TModel>? Models { get; set; }

        IViewData Attach(TModel model, bool patch = false);

        IViewData Detach(TModel model);

        void Load(IList<TModel> models, bool patch = false);

        Task LoadAsync(IList<TModel> models, bool patch = false);

        event EventHandler<IEnumerable<IViewData>> LoadCompleted;

        Task UnloadAsync(IList<TModel> models);

        event EventHandler<IEnumerable<IViewData>> UnloadCompleted;
    }

    public interface IViewDataStore : IViewData
    {
        IAuthorization? Authorization { get; set; }

        IViewStore? ViewStore { get; }

        ISeries<IViewData>? Items { get; set; }

        Pagination? Pagination { get; set; }

        IQueryParameters? Query { get; set; }

        IQueryParameters MapQuery(Func<IViewRubric, bool>? predicate = null);

        IViewData Attach(object model, bool patch = false);

        IViewData Attach(IViewData viewData, bool patch = false);

        IViewData Detach(object model);

        void Load(IEnumerable<object> models, bool patch = false);

        Task LoadAsync(IEnumerable<object> models, bool patch = false);

        Task LoadAsync();

        Task LoadSingleAsync(object single);

        Task UnloadAsync();

        Task UnloadAsync(IEnumerable<object> models);

        Task SaveAsync(bool changesets = false);

        Task StageAsync(bool changesets = false);
    }
}