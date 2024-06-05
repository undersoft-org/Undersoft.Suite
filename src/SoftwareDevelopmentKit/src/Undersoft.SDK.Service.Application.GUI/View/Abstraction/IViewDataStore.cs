using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewDataStore<TModel> : IViewDataStore, IViewData<TModel> where TModel : class, IOrigin, IInnerProxy
    {
        ISeries<TModel> Models { get; set; }

        IQueryParameters<TModel> Query { get; set; }

        IViewData Attach(TModel model, bool patch = false);

        IViewData Detach(TModel model);

        void Load(IList<TModel> models, bool patch = false);

        Task LoadAsync(IList<TModel> models, bool patch = false);

        Task LoadSingleAsync(TModel model);

        event EventHandler<IEnumerable<TModel>> LoadCompleted;

        Task UnloadAsync(IList<TModel> models);

        event EventHandler<IEnumerable<TModel>> UnloadCompleted;
    }

    public interface IViewDataStore : IViewData
    {
        IViewStore ViewStore { get; set; }

        ISeries<IViewData> Items { get; set; }

        Pagination Pagination { get; set; }

        IQueryParameters MapQuery(Func<IViewData, IViewRubrics>? fromRubrics = null, Func<IViewRubric, bool>? predicate = null);

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