using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Filters
{
    public partial class GenericDataFilter : ViewItem, IViewFilter
    {
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

                Rubric.FilterMembers.ForEach(m =>
                {
                    var rubricFilters = Filters.Where(f => f.Member == m).Commit();
                    if (rubricFilters.Any())
                    {
                        FilterEntries.Put(rubricFilters);
                    }
                    else
                    {
                        FilterEntries.Put(new Filter(m, FilteredType.DefaultNotNullable(), CompareOperand.Equal, LinkOperand.And));
                    }
                });
            }
            base.OnInitialized();
        }

        public ISeries<Filter> Filters => Rubric.Filters;

        public ISeries<Filter> FilterEntries { get; set; } = new Listing<Filter>();

        public bool Added => FilterEntries.Where(f => f.Added).Any();

        public bool IsAddable => Rubric.FilterMembers!.Length < 2;

        public void CloneLastFilter()
        {
            var lastfilter = FilterEntries.LastOrDefault();
            if (lastfilter != null)
                FilterEntries.Put(new Filter(lastfilter.Member, FilteredType.DefaultNotNullable(), lastfilter.Operand, lastfilter.Link) { Added = true });

            RenderView();
        }

        public void RemoveLastFilter()
        {
            var lastfilter = FilterEntries.LastOrDefault();
            if (lastfilter != null)
            {
                FilterEntries.Remove(lastfilter);
                Filters.Remove(lastfilter);
            }

            RenderView();
        }

        public void Close()
        {
            IsOpen = false;
            RenderView();
        }

        public void ClearFilters()
        {
            Filters.Clear();
        }

        public void UpdateFilters()
        {
            FilterEntries.ForEach(f =>
            {
                if (!Filters.Contains(f) && f.Value != FilteredType.DefaultNotNullable())
                    Rubric.Filters.Put(f);
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

        public override void RenderView()
        {
            Parent?.RenderView();
            base.RenderView();
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
        public override string? Style { get; set; } = "margin-top:7px;";

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
