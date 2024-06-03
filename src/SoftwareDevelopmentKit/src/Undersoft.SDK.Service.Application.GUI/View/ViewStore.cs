using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View
{
    public class ViewStore<TStore, TModel> : ViewStore, IViewStore<TStore, TModel>
        where TModel : class, IOrigin, IInnerProxy
        where TStore : IDataServiceStore
    {
        protected override async Task OnInitializedAsync()
        {
            await LoadDataStore();
            await base.OnInitializedAsync();
        }

        [Parameter]
        public virtual Action<ViewDataStore<TStore, TModel>> Setup { get; set; } = default!;

        public virtual IList<TModel> Models => Contents.Models;

        [Parameter]
        public virtual IViewDataStore<TModel> Contents
        {
            get => (IViewDataStore<TModel>)base.Data;
            set => base.Data = value;
        }

        public virtual async Task LoadDataStore()
        {
            if (Data == null)
            {
                DataStore = new ViewDataStore<TStore, TModel>(this, Setup);
                await DataStore.LoadAsync();
            }
        }
    }

    public class ViewStore : ViewItem, IViewStore
    {
        public virtual IServiceScope ServiceScope { get; set; } = default!;

        [Parameter]
        public virtual IServicer Servicer { get; set; } = default!;

        [Parameter]
        public virtual IDialogService DialogService { get; set; } = default!;

        public virtual IEnumerable<IViewData> Items => DataStore.Items;

        [Parameter]
        public virtual IViewDataStore DataStore
        {
            get => (IViewDataStore)base.Data;
            set => base.Data = value;
        }

        public virtual async Task NextPageAsync()
        {
            if (DataStore.Pagination.HasNextPage)
            {
                DataStore.Pagination.SetPageIndex(DataStore.Pagination.PageIndex + 1);
                await UpdateDataViewAsync();
            }
        }

        public virtual async Task PreviousPageAsync()
        {
            if (DataStore.Pagination.HasPreviousPage)
            {
                DataStore.Pagination.SetPageIndex(DataStore.Pagination.PageIndex - 1);
                await UpdateDataViewAsync();
            }
        }

        public virtual async Task GoToPageAsync(int page)
        {
            if (page >= DataStore.Pagination.IndexFrom && DataStore.Pagination.TotalPages >= page)
            {
                DataStore.Pagination.SetPageIndex(page);
                await UpdateDataViewAsync();
            }
        }

        public async Task UpdateDataViewAsync()
        {
            await DataStore.LoadAsync();
            DataStore.ViewItem?.RenderView();
        }
    }
}
