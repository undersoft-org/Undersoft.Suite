using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataGrid<TStore, TDto, TModel>
        : GenericDataStore<TStore, TDto, TModel>
        where TStore : IDataServiceStore
        where TDto : class, IOrigin, IInnerProxy
        where TModel : class, IOrigin, IInnerProxy
    {
        private string GridTemplateColumns = string.Empty;

        private int RubricOrdinalSeed = 1;

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
                template += "40px ";
            if (FeatureFlags.Multiselect)
                template += "50px ";

            var dataType = typeof(ViewData<>).MakeGenericType(typeof(TModel));
            var data = dataType.New<IViewData<TModel>>();
            data.MapRubrics(r => r.Rubrics, v => v.Visible);

            var templateArray = data.Rubrics
                .ForEach(r =>
                {
                    if (r.RubricSize < 4)
                        return "0.25fr";
                    if (r.RubricSize < 5)
                        return "0.5fr";
                    if (r.RubricSize < 65)
                        return "1fr";
                    if (r.RubricSize < 129)
                        return "1.25fr";
                    if (r.RubricSize < 513)
                        return "1.5fr";
                    return "2fr";
                })
                .Commit();

            if (templateArray.Any())
            {
                template += templateArray.Aggregate((a, b) => a + " " + b);

                if (EditMode != GUI.Models.EditMode.None)
                    template += " 40px";
            }

            return template;
        }

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
