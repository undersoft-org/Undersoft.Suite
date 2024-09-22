using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Search
{
    public partial class GenericDataSearchItem : ViewItem
    {
        private Type _type = default!;
        private int _index;
        private string? _name { get; set; } = "";
        private string? _label { get; set; }
        private List<Option<string>>? _operandOptions { get; set; }
        private List<Option<string>>? _linkOptions { get; set; }

        public FluentSearch? FluentSearch { get; set; }

        [CascadingParameter]
        private bool IsOpen { get; set; }

        [CascadingParameter]
        public bool ShowIcons { get; set; } = true;

        [Parameter]
        public override int Index
        {
            get => base.Index;
            set => base.Index = value;
        }

        [Parameter]
        public bool AutoFocus { get; set; }

        protected override void OnInitialized()
        {
            _name = Data.ModelType.Name;
            _label = _name;

            base.OnInitialized();
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //        await JSRuntime!.InvokeVoidAsync("GenericUtilites.setFocusByElement", FluentSearch!.Element);

        //    await base.OnAfterRenderAsync(firstRender);
        //}

        public virtual string? SearchValue { get; set; }

        [CascadingParameter]
        public override IViewData Data
        {
            get => base.Data;
            set => base.Data = value;
        }

        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

        private async Task HandleSearchAsync()
        {
            if (SearchValue != null && SearchValue.Length > 2)
            {
                var parent = ((GenericDataSearch)Parent!);
                var searchFilters = new Listing<Filter>();
                string[] words = SearchValue.Split(' ');
                words.ForEach(w =>
                {
                    var _w = w.Trim();
                    searchFilters.Add(
                        parent.FilterEntries.ForEach(f => new Filter(
                            f.Member,
                            _w,
                            CompareOperand.Contains,
                            LinkOperand.Or
                        ))
                    );
                });
                Data.SearchFilters = searchFilters;

                await parent!.LoadViewAsync();
            }
        }

        public async Task LoadViewAsync()
        {
            await ((IViewStore)Parent!).LoadViewAsync();
        }

        private event EventHandler<object> _onMenuItemChange = default!;

        [CascadingParameter]
        public EventHandler<object> OnMenuItemChange
        {
            get => _onMenuItemChange;
            set
            {
                if (value != null)
                    _onMenuItemChange += value;
            }
        }

        public void OnClick()
        {
            if (Rubric.Invoker != null)
            {
                Rubric.Invoker.Invoke(Value);
            }
            else if (OnMenuItemChange != null)
            {
                OnMenuItemChange(this, Data);
            }
        }
    }
}
