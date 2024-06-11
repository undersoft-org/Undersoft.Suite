using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Filters;

public partial class GenericDataFilterButtons : ViewItem
{
    private Type _type = default!;
    private string? _name { get; set; } = "";
    private string? _label { get; set; }
    private bool _isAddable = false;
    private IViewStore? _store;

    [CascadingParameter]
    private bool IsOpen { get; set; }

    [CascadingParameter]
    public bool ShowIcons { get; set; } = true;

    protected override void OnInitialized()
    {
        if (
            Rubric.FilteredType!.IsPrimitive
            || Rubric.FilteredType.IsAssignableTo(typeof(DateTime))
        )
            _isAddable = true;

        base.OnInitialized();
    }

    [CascadingParameter]
    public override IViewItem? Root
    {
        get => base.Root;
        set => base.Root = value;
    }

    public void Add()
    {
        if (Root != null)
        {
            ((GenericDataFilter)Root).CloneLastFilter();
        }
    }

    public void Close()
    {
        if (Root != null)
        {
            ((GenericDataFilter)Root).IsOpen = false;

            StateHasChanged();
        }
    }

    public void Dismiss()
    {
        if (Parent != null)
        {
            ((IViewFilter)Parent).ClearFilters();
        }
    }

    public async Task Apply()
    {
        if (Parent != null)
        {
            await ((IViewFilter)Parent).ApplyFiltersAsync();
        }
    }
}
