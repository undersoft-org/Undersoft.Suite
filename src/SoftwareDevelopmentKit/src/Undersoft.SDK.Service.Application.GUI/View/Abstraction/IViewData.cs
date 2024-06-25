using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewData<TModel> : IViewData where TModel : class, IOrigin, IInnerProxy
    {
        new TModel Model { get; set; }
    }

    public interface IViewData : ISeries<IViewData>, IView, IIdentifiable
    {
        string ViewId { get; }
        string? Description { get; set; }
        string? Class { get; set; }
        bool IsSingle { get; }
        string Height { get; set; }
        string? Z { get; set; }
        Icon? Icon { get; set; }
        Icon? Logo { get; set; }
        string? Href { get; set; }
        string? NextHref { get; set; }
        string? Title { get; set; }
        string Width { get; set; }

        Type ModelType { get; }

        IInnerProxy Model { get; set; }

        IViewRubric? ActiveRubric { get; set; }

        IViewRubrics Rubrics { get; set; }

        bool RubricsEnabled { get; set; }

        IViewRubrics ExtendedRubrics { get; set; }

        bool ExtendedRubricsEnabled { get; set; }

        EditMode EditMode { get; set; }

        EntryMode EntryMode { get; set; }

        StateFlags StateFlags { get; set; }

        IViewValidator? Validator { get; set; }

        Type? ValidatorType { get; set; }

        string? ValidatorTypeName { get; set; }

        IViewData? Root { get; set; }

        IViewData? Parent { get; set; }

        IViewItem? ViewItem { get; set; }

        OperationType Operation { get; set; }

        HorizontalAlignment PanelAlignment { get; set; }

        public ViewGrid? Grid { get; set; }

        public ViewStack? Stack { get; set; }

        OperationNotes Notes { get; set; }

        IView? View { get; set; }

        string? ViewTypeName { get; set; }

        public string[]? SearchMembers { get; set; }

        public ISeries<Filter>? SearchFilters { get; set; }

        public bool Searchable => SearchMembers != null && SearchMembers.Length > 0;

        void ClearErrors();

        void MapAttributes();

        IViewRubrics MapRubrics(Func<IViewData, IViewRubrics> forRubrics, Func<IRubric, bool> predicate, bool resolveAttributes = true);

        void ClearData();

        void InstantiateNulls(Func<IViewData, IViewRubrics> forRubrics);

        void SetRequired(params string[] requiredList);

        void SetVisible(params string[] visibleList);

        void SetExpandable(params string[] expandableList);

        void SetEditable(params string[] editableList);
    }
}