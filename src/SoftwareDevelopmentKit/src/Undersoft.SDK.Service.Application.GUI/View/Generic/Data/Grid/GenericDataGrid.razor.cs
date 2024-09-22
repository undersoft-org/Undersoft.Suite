using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGrid<TStore, TDto, TModel>
        : GenericDataStore<TStore, TDto, TModel>, IGenericDataGrid
        where TStore : IDataServiceStore
        where TDto : class, IOrigin, IInnerProxy
        where TModel : class, IOrigin, IInnerProxy
    {
        public string? GridTemplateColumns { get; set; }

        public int RubricOrdinalSeed { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            Root = this;
            RubricOrdinalSeed = CalculateOrdinalSeed();
            GridTemplateColumns = CalculateTemplateColumns();
            await base.OnInitializedAsync();
        }

        private int CalculateOrdinalSeed()
        {
            var seed = 1;
            if (FeatureFlags.Expandable)
                seed++;
            if (FeatureFlags.Multiselect)
                seed++;
            return seed;
        }

        private string CalculateTemplateColumns()
        {
            string template = "";

            if (FeatureFlags.Expandable)
                template += "30px ";
            if (FeatureFlags.Multiselect)
                template += "35px ";

            var dataType = typeof(ViewData<>).MakeGenericType(typeof(TModel));
            var data = dataType.New<IViewData<TModel>>();
            data.MapRubrics(r => r.Rubrics, v => v.Visible, false);

            var templateArray = data.Rubrics
                .ForEach(r =>
                {
                    if (r.RubricSize < 4)
                        return "0.1fr";
                    if (r.RubricSize < 9)
                        return "0.2fr";
                    if (r.RubricSize < 17)
                        return "0.4fr";
                    if (r.RubricSize < 33)
                        return "0.5fr";
                    if (r.RubricSize < 65)
                        return "0.6fr";
                    if (r.RubricSize < 129)
                        return "0.8fr";
                    if (r.RubricSize < 259)
                        return "1fr";
                    return "0.4fr";
                })
                .Commit();

            if (templateArray.Any())
            {
                template += templateArray.Aggregate((a, b) => a + " " + b);

                if (EditMode != GUI.Models.EditMode.None)
                    template += " 30px";
            }

            return template;
        }

        [Parameter]
        public Func<string, string> ForRowStyle { get; set; } = s => s + " max-height:80px;";

        [Parameter]
        public Func<string, string> ForRowClass { get; set; } = c => c + " datagrid-item";

        [Parameter]
        public Func<string, string> ForSubRowStyle { get; set; } = s => s + " max-height:80px;";

        [Parameter]
        public Func<string, string> ForSubRowClass { get; set; } = c => c + " datagrid-item";

        [Parameter]
        public string? RowStyle { get; set; }

        [Parameter]
        public bool Multiline { get; set; } = true;

        [Parameter]
        public bool ShowHover { get; set; } = false;

        [Parameter]
        public bool ShowTitle { get; set; } = true;

        [Parameter]
        public string Title { get; set; } = typeof(TModel).Name;

        [Parameter]
        public bool Checked
        {
            get => Data.StateFlags.Checked;
            set => Data.StateFlags.Checked = value;
        }

        [Parameter]
        public bool Resizable
        {
            get => FeatureFlags.Resizable;
            set => FeatureFlags.Resizable = value;
        }

        [Parameter]
        public bool Expandable
        {
            get => FeatureFlags.Expandable;
            set => FeatureFlags.Expandable = value;
        }

        [Parameter]
        public bool Multiselect
        {
            get => FeatureFlags.Multiselect;
            set => FeatureFlags.Multiselect = value;
        }

        [Parameter]
        public bool Editable
        {
            get => FeatureFlags.Editable;
            set => FeatureFlags.Editable = value;
        }

        [Parameter]
        public bool Showable
        {
            get => FeatureFlags.Showable;
            set => FeatureFlags.Showable = value;
        }

        [CascadingParameter]
        public override IViewDataStore DataStore
        {
            get => base.DataStore;
            set
            {
                value.ViewItem = null;
                base.DataStore = value;
            }
        }
    }
}
