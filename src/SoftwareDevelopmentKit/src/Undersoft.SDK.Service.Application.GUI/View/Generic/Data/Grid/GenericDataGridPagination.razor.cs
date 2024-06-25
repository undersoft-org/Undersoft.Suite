using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGridPagination : ViewStore
    {
        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

        [CascadingParameter]
        public override IViewDataStore DataStore
        {
            get => base.DataStore;
            set => base.DataStore = value;
        }

        [Parameter]
        public int PagingLimit { get; set; } = 11;

        private Pagination _pagination = default!;

        [CascadingParameter]
        public override FeatureFlags FeatureFlags { get => base.FeatureFlags; set => base.FeatureFlags = value; }

        protected override async Task OnInitializedAsync()
        {
            _pagination = DataStore.Pagination!;
            if (PagingLimit != _pagination.PagingLimit)
                _pagination.SetPagingLimit(PagingLimit);
            await base.OnInitializedAsync();
        }

        private Color PageNexColor() =>
       _pagination.HasNextPage ? Color.Accent : Color.Neutral;

        private Color PagePreviousColor() =>
      _pagination.HasPreviousPage ? Color.Accent : Color.Neutral;

        private Appearance PageButtonAppearance(int pageIndex) =>
        _pagination.PageIndex == pageIndex ? Appearance.Outline : Appearance.Stealth;

        private string? AriaCurrentValue(int pageIndex) =>
            _pagination.PageIndex == pageIndex ? "page" : null;

        private string AriaLabel(int pageIndex) => $"Go to page {pageIndex}";
    }
}
