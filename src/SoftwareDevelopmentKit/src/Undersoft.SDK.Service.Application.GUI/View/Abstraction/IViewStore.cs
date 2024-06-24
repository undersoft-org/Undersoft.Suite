using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewStore<TStore, TDto, TModel> : IViewStore
        where TStore : IDataServiceStore
        where TDto : class, IOrigin, IInnerProxy
        where TModel : class, IOrigin, IInnerProxy
    {
        IViewDataStore<TModel> Contents { get; set; }

        ISeries<TModel>? Models { get; }

        Action<ViewDataStore<TStore, TDto, TModel>> Setup { get; set; }
    }

    public interface IViewStore<TStore, TModel> : IViewStore<TStore, TModel, TModel>
       where TStore : IDataServiceStore
       where TModel : class, IOrigin, IInnerProxy
    {
    }


    public interface IViewStore : IViewItem, IViewProgress, IViewLoadable
    {
        IServicer? Servicer { get; set; }

        IDialogService? DialogService { get; set; }

        IEnumerable<IViewData>? Items { get; }

        IViewDataStore DataStore { get; set; }

        Task SaveViewAsync(bool changesets = false);

        Task StageViewAsync(bool changesets = false);
    }
}