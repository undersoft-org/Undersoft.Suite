using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewDataStore<TModel> : IViewDataStore, IViewData<TModel> where TModel : class, IOrigin, IInnerProxy
    {
        IList<TModel> Models { get; set; }

        IQueryParameters<TModel> Query { get; set; }

        IViewData Attach(TModel model, bool patch = false);
        void Load(IList<TModel> models);
        Task LoadAsync(TModel model);
    }

    public interface IViewDataStore : IViewData
    {
        IViewStore ViewStore { get; set; }

        IQueryable<IViewData> Items { get; set; }

        Pagination Pagination { get; set; }

        Task LoadAsync();

        Task LoadAsync(object key);

        Task SaveAsync();
    }
}