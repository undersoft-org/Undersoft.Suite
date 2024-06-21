using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Filters
{
    public partial class GenericDataFilter : ViewItem, IViewFilter
    {
        private bool _isAddable = false;

        protected override void OnInitialized()
        {
            if (Parent == null)
                Root = this;

            Rubric.ViewFilter = this;

            if (Rubric.Filterable)
            {
                FilteredType = Rubric.FilteredType ?? Rubric.RubricType;

                if (Rubric.FilterMembers == null || !Rubric.FilterMembers.Any())
                    Rubric.FilterMembers = new[] { Rubric.RubricName };

                if (Rubric.FilterMembers.Length < 2)
                    NoLabel = true;

                Rubric.FilterMembers.ForEach(m =>
                {
                    var rubricFilters = Filters.Where(f => f.Member == m).Commit();
                    if (rubricFilters.Any())
                    {
                        EmptyFilters.Put(rubricFilters);
                    }
                    else
                    {
                        EmptyFilters.Put(new Filter(m, Rubric.RubricType.Default(), CompareOperand.Equal, LinkOperand.And));
                    }
                });

                if (FilteredType.IsPrimitive || FilteredType.IsAssignableTo(typeof(DateTime)))
                    _isAddable = true;
            }
            base.OnInitialized();
        }

        public bool NoLabel { get; set; }

        public ISeries<Filter> Filters => Rubric.Filters;

        public ISeries<Filter> EmptyFilters { get; set; } = new Listing<Filter>();

        public void CloneLastFilter()
        {
            var lastfilter = EmptyFilters.LastOrDefault();
            if (lastfilter != null)
                EmptyFilters.Put(new Filter(lastfilter.Member, Rubric.RubricType.Default(), lastfilter.Operand, lastfilter.Link) { Added = true });

            StateHasChanged();
        }

        public void ClearFilters()
        {

        }

        public void UpdateFilters()
        {
            EmptyFilters.ForEach(f =>
            {
                if (f.Value != FilteredType.Default())
                {
                    Rubric.Filters.Put(f);
                }
            });
        }

        public async Task ApplyFiltersAsync()
        {
            UpdateFilters();
            await LoadViewAsync();
        }

        public async Task LoadViewAsync()
        {
            await ((IViewLoadable)Parent!).LoadViewAsync();
        }



        public Type FilteredType { get; set; } = default!;

        public virtual bool IsOpen { get; set; }

        [Parameter]
        public bool ShowIcons { get; set; } = true;

        [Parameter]
        public HorizontalPosition Position { get; set; } = HorizontalPosition.Right;

        [Parameter]
        public VerticalPosition VerticalPosition { get; set; } = VerticalPosition.Bottom;

        [Parameter]
        public override string? Style { get; set; }

        [Parameter]
        public string AnchorId { get; set; } = default!;

        [Parameter]
        public bool Anchored { get; set; } = default!;

        [Parameter]
        public MouseButton Trigger { get; set; } = MouseButton.Left;

        private event EventHandler<object> _onMenuItemChange = default!;

        [Parameter]
        public virtual EventHandler<object> OnMenuItemChange { get => _onMenuItemChange; set { if (value != null) _onMenuItemChange += value; } }
    }
}
