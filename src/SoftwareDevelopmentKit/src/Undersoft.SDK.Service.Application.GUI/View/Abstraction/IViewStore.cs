using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewStore<TStore, TModel> : IViewStore
        where TStore : IDataServiceStore
        where TModel : class, IOrigin, IInnerProxy
    {
        IViewDataStore<TModel> Contents { get; set; }

        IList<TModel> Models { get; }

        Action<ViewDataStore<TStore, TModel>> Setup { get; set; }
    }

    public interface IViewStore : IViewItem
    {
        IServicer Servicer { get; set; }

        IDialogService DialogService { get; set; }

        IEnumerable<IViewData> Items { get; }

        IViewDataStore DataStore { get; set; }

        Task UpdateDataViewAsync();
    }
}