using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid.Body
{
    public partial class GenericDataGridBodyItemSubGrid : ViewStore
    {
        private string GridTemplateColumns = string.Empty;

        private int RubricOrdinalSeed = 1;

        protected override void OnInitialized()
        {
            RubricOrdinalSeed = CalculateOrdinalSeed();
            GridTemplateColumns = CalculateTemplateColumns();
            base.OnInitialized();
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

            var templateArray = DataStore.Rubrics.ForEach(r =>
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
            }).Commit();

            if (templateArray.Any())
            {
                template += templateArray.Aggregate((a, b) => a + " " + b);

                if (EditMode != GUI.Models.EditMode.None)
                    template += " 40px";
            }

            return template;
        }

        [CascadingParameter]
        public bool Resizable { get; set; }

        [CascadingParameter]
        public override IViewItem? Root
        {
            get => base.Root;
            set => base.Root = value;
        }

    }
}
