using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Model.Attributes;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewRubric : MemberRubric, IViewRubric
{
    public FieldIdentifier FieldIdentifier { get; set; }

    public ISeries<string> Errors { get; set; } = new Listing<string>();

    public IViewItem ViewItem { get; set; } = default!;

    public string ViewId => CodeNo;

    public string? Class { get; set; }

    public string? Style { get; set; }

    public string? Width { get; set; }

    public string? Height { get; set; }

    public string? ImageWidth { get; set; }

    public string? ImageHeight { get; set; }

    public string? Z { get; set; }

    public ViewImageMode ImageMode { get; set; }

    public bool IsImage => ImageMode != ViewImageMode.None;

    public Align Align { get; set; }

    public Icon? Icon { get; set; }

    public ViewGrid? Grid { get; set; }

    public ViewStack? Stack { get; set; }

    public string[]? SortMembers { get; set; }

    public bool Sorted => SortBy != SDK.SortDirection.None;

    public bool Filtered => Filters.Any();

    public bool Hidden { get; set; }

    public bool IsMenuItem { get; set; }

    public bool IsMenuGroup { get; set; }

    public Type? FilteredType { get; set; }

    public string[]? FilterMembers { get; set; }

    public IViewItem? ViewFilter { get; set; }

    public ISeries<Filter> Filters { get; set; } = new Listing<Filter>();

    public void RenderView()
    {
        ViewItem?.RenderView();
    }

    public ViewRubric ShallowCopy(ViewRubric dst)
    {
        object result = ShallowCopy((MemberRubric)dst);
        return (ViewRubric)result;
    }
}

