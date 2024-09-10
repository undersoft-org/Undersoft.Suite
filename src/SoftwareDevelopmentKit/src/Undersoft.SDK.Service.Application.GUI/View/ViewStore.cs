using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View
{
    public class ViewStore<TStore, TDto, TModel> : ViewStore, IViewStore<TStore, TDto, TModel>
        where TStore : IDataServiceStore
        where TDto : class, IOrigin, IInnerProxy
        where TModel : class, IOrigin, IInnerProxy
    {
        protected override async Task OnInitializedAsync()
        {
            await CreateStore();
            await base.OnInitializedAsync();
        }

        [Parameter]
        public virtual Action<ViewDataStore<TStore, TDto, TModel>> Setup { get; set; } = default!;

        public virtual ISeries<TModel>? Models => Contents.Models;

        [Parameter]
        public virtual IViewDataStore<TModel> Contents
        {
            get => (IViewDataStore<TModel>)base.Data;
            set => base.Data = value;
        }

        public virtual async Task CreateStore(Type contractType)
        {
            ProgressVisible = true;
            DataStore = typeof(ViewDataStore<,,>)
                .MakeGenericType(typeof(TStore), contractType, typeof(TModel))
                .New<IViewDataStore>(this, Setup);
            await SetAuthorization();
            await DataStore.LoadAsync();
            ProgressVisible = false;
        }

        public virtual async Task CreateStore<T>() where T : class, IOrigin, IInnerProxy
        {
            if (Data == null)
            {
                ProgressVisible = true;
                DataStore = new ViewDataStore<TStore, T, TModel>(this);
                await SetAuthorization();
                await DataStore.LoadAsync();
                ProgressVisible = true;
            }
        }

        public virtual async Task CreateStore()
        {
            if (Data == null)
            {
                ProgressVisible = true;
                DataStore = new ViewDataStore<TStore, TDto, TModel>(this, Setup);
                await SetAuthorization();
                await DataStore.LoadAsync();
                ProgressVisible = false;
            }
        }
    }

    public class ViewStore<TStore, TModel> : ViewStore<TStore, TModel, TModel>, IViewStore<TStore, TModel>
        where TModel : class, IOrigin, IInnerProxy
        where TStore : IDataServiceStore
    {
    }

    public class ViewStore : ViewItem, IViewStore
    {
        public virtual IServiceScope? ServiceScope { get; set; }

        [Parameter]
        public virtual IServicer? Servicer { get; set; }

        [Parameter]
        public virtual IDialogService? DialogService { get; set; }

        [Parameter]
        public virtual IAccessProvider? Access { get; set; }

        public virtual IEnumerable<IViewData>? Items => DataStore.Items;

        [Parameter]
        public virtual IViewDataStore DataStore
        {
            get => (IViewDataStore)base.Data;
            set => base.Data = value;
        }

        public async Task SetAuthorization()
        {
            if (Access != null)
            {
                await Access.CurrentState();
                DataStore.Authorization = Access.Authorization;
            }
        }

        public bool ProgressVisible { get; set; }

        public virtual async Task NextPageAsync()
        {
            if (DataStore.Pagination!.HasNextPage)
            {
                DataStore.Pagination.SetPageIndex(DataStore.Pagination.PageIndex + 1);
                await LoadViewAsync();
            }
        }

        public virtual async Task PreviousPageAsync()
        {
            if (DataStore.Pagination!.HasPreviousPage)
            {
                DataStore.Pagination.SetPageIndex(DataStore.Pagination.PageIndex - 1);
                await LoadViewAsync();
            }
        }

        public virtual async Task GoToPageAsync(int page)
        {
            if (page >= DataStore.Pagination!.IndexFrom && DataStore.Pagination.TotalPages >= page)
            {
                DataStore.Pagination.SetPageIndex(page);
                await LoadViewAsync();
            }
        }

        public async Task LoadViewAsync()
        {
            ProgressVisible = true;
            await DataStore.LoadAsync();
            ProgressVisible = false;
            DataStore.ViewItem?.RenderView();
        }

        public async Task StageViewAsync(bool changesets = false)
        {
            ProgressVisible = true;
            await DataStore.StageAsync(changesets);
            ProgressVisible = false;
            DataStore.ViewItem?.RenderView();
        }

        public async Task SaveViewAsync(bool changesets = false)
        {
            ProgressVisible = true;
            await DataStore.SaveAsync(changesets);
            ProgressVisible = false;
            DataStore.ViewItem?.RenderView();
        }
    }
}
