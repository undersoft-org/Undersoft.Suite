using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Filters;

public partial class GenericDataFilterButtons : ViewItem
{
    private Type _type = default!;
    private string? _name { get; set; } = "";
    private string? _label { get; set; }
    private IViewStore? _store;

    [CascadingParameter]
    private bool IsOpen { get; set; }

    [CascadingParameter]
    public bool ShowIcons { get; set; } = true;

    protected override void OnInitialized()
    {
        var type = FilteredType.GetNotNullableType();
        base.OnInitialized();
    }

    [CascadingParameter]
    public Type FilteredType { get; set; } = default!;

    public bool IsAddable => Parent != null ? ((IViewFilter)Parent).IsAddable : false;

    [CascadingParameter]
    public override IViewItem? Root
    {
        get => base.Root;
        set => base.Root = value;
    }

    public bool Added => Parent != null ? ((IViewFilter)Parent).Added : false;

    public void Add()
    {
        if (Parent != null)
        {
            ((IViewFilter)Parent).CloneLastFilter();
        }
    }

    public void Subtract()
    {
        if (Parent != null)
        {
            ((IViewFilter)Parent).RemoveLastFilter();
        }
    }

    public void Close()
    {
        if (Parent != null)
        {
            ((IViewFilter)Parent).Close();
        }
    }

    public void Dismiss()
    {
        if (Parent != null)
        {
            Close();
            ((IViewFilter)Parent).ClearFilters();
        }
    }

    public async Task Apply()
    {
        if (Parent != null)
        {
            Close();
            await ((IViewFilter)Parent).ApplyFiltersAsync();
        }
    }
}
