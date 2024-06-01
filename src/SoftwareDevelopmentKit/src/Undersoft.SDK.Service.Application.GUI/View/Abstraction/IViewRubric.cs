using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewRubric : IRubric, IView
    {
        FieldIdentifier FieldIdentifier { get; set; }

        ISeries<string> Errors { get; set; }

        IViewItem ViewItem { get; set; }

        Icon? Icon { get; set; }

        bool Sorted { get; }

        bool Filtered { get; }

        bool Hidden { get; set; }

        bool IsMenuItem { get; set; }

        bool IsMenuGroup { get; set; }

        string? Class { get; set; }

        string? Style { get; set; }

        Align Align { get; set; }
    }
}