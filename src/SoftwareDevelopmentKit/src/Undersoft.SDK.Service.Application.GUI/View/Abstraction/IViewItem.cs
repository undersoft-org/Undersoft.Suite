using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.Models;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewItem : IView
    {
        long Id { get; set; }
        long TypeId { get; set; }
        string? Label { get; set; }
        Icon? Icon { get; set; }
        object? Value { get; set; }
        string? Class { get; set; }
        string? Style { get; set; }
        string? Attributes { get; set; }

        ComponentBase? Reference { get; set; }
        
        EditMode EditMode { get; set; }

        IJSRuntime? JSRuntime { get; set; }

        FeatureFlags FeatureFlags { get; set; }

        StateFlags StateFlags { get; set; }

        IViewRubric Rubric { get; set; }

        IViewData Data { get; set; }

        ISeries<IViewItem>? Children { get; set; }
    }
}