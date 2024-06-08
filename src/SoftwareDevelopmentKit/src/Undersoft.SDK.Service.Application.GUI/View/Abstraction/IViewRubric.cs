using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Data.Model.Attributes;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewRubric : IRubric, IView
    {
        FieldIdentifier FieldIdentifier { get; set; }

        ISeries<string> Errors { get; set; }

        IViewItem ViewItem { get; set; }

        ISeries<Filter> Filters { get; set; }

        string[]? FilterMembers { get; set; }

        Type? FilteredType { get; set; }

        IViewItem ViewFilter { get; set; }

        string[]? SortMembers { get; set; }

        string ViewId { get; }

        Icon? Icon { get; set; }

        bool Sorted { get; }

        bool Filtered { get; }

        bool Hidden { get; set; }

        bool IsMenuItem { get; set; }

        bool IsMenuGroup { get; set; }

        string? Width { get; set; }

        string? Height { get; set; }

        string? ImageWidth { get; set; }

        string? ImageHeight { get; set; }

        string? Z { get; set; }

        bool IsImage { get; }

        ViewImageMode ImageMode { get; set; }

        ViewGrid? Grid { get; set; }

        ViewStack? Stack { get; set; }

        string? Class { get; set; }

        string? Style { get; set; }

        Align Align { get; set; }
    }
}