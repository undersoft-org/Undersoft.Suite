﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewRubric : MemberRubric, IViewRubric
{
    public FieldIdentifier FieldIdentifier { get; set; }

    public ISeries<string> Errors { get; set; } = new Listing<string>();

    public IViewItem ViewItem { get; set; } = default!;

    public string? Class { get; set; }

    public string? Style { get; set; }

    public Align Align { get; set; }

    public Icon? Icon { get; set; }

    public ViewGrid? Grid { get; set; }

    public ViewStack? Stack { get; set; }

    public bool Sorted => SortBy != System.Linq.SortDirection.None;

    public bool Filtered => Filters.Any();

    public bool Hidden { get; set; }

    public bool IsMenuItem { get; set; }

    public bool IsMenuGroup { get; set; }

    public ISeries<FilterItem> Filters { get; set; } = new Listing<FilterItem>();

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
