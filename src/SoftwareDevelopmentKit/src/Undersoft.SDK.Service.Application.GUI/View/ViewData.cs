using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;
using Undersoft.SDK.Series.Base;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Validator;
using Undersoft.SDK.Service.Operation;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewData<TModel> : ListingBase<IViewData>, IViewData<TModel>
    where TModel : class, IOrigin, IInnerProxy
{
    protected IView _view = default!;
    protected IViewItem _viewItem = default!;
    protected long? typeId;

    public ViewData() : this(null, OperationType.Any) { }

    public ViewData(OperationType mode) : this(null, mode) { }

    public ViewData(TModel? data) : this(data, OperationType.Any) { }

    public ViewData(TModel? data, IViewData parent) : this(data, OperationType.Any) { Parent = parent; }

    public ViewData(TModel? data, OperationType mode, string title = "") : base(true)
    {
        Model = data ?? typeof(TModel).New<TModel>();
        Title = title;
        Operation = mode;
    }

    public virtual void ClearData()
    {
        Model = typeof(TModel).New<TModel>();
        this.ForEach(i => i.ClearData());
    }

    public virtual TModel Model { get; set; } = default!;

    public virtual void MapParent(IViewData source)
    {
        Validator = source.Validator;
        ValidatorType = source.ValidatorType;
        ValidatorTypeName = source.ValidatorTypeName;
        Width = source.Width;
        Height = source.Height;
        Grid = source.Grid;
        Stack = source.Stack;
        SearchMembers = source.SearchMembers;
    }

    public virtual void MapAttributes()
    {
        if (Parent != null && Parent.Model.TypeId == Model.TypeId)
        {
            MapParent(Parent);
        }
        else
        {
            ViewAttributes.Resolve(this);
            if (ValidatorType == null)
            {
                ValidatorType = typeof(ViewValidator<>).MakeGenericType(typeof(TModel));
                ValidatorTypeName = ValidatorType.FullName;
                Validator = typeof(GenericValidator<,>).MakeGenericType(ValidatorType, typeof(TModel)).New<IViewValidator>();
            }
        }
    }

    public virtual IViewRubrics MapRubrics(Func<IViewData, IViewRubrics> forRubrics, Func<IRubric, bool> predicate, bool resolveAttributes = true)
    {
        var rubrics = forRubrics(this);
        if (!rubrics.Any())
        {
            if (Parent != null && Parent.Model.TypeId == Model.TypeId)
            {
                rubrics.Add(forRubrics(Parent));
                MapParent(Parent);
            }
            else
            {
                if (resolveAttributes)
                    MapAttributes();

                var _proxy = Model.Proxy;
                int ordinal = 0;
                foreach (var mr in _proxy.Rubrics.Where(_mr => predicate(_mr)))
                {
                    ViewRubric vr = (ViewRubric)mr.ShallowCopy(new ViewRubric());
                    vr.RubricOrdinal = ordinal++;
                    if (resolveAttributes)
                        ViewAttributes.Resolve(vr);
                    if (vr.IconMember != null)
                        vr.Icon = (Icon)_proxy[vr.IconMember];
                    rubrics.Put(vr);
                }
            }
        }
        return rubrics;
    }

    public override long Id
    {
        get => Model.Id;
        set => Model.Id = value;
    }

    public override long TypeId
    {
        get => Model.TypeId;
        set => Model.TypeId = value;
    }

    public virtual void InstantiateNulls(Func<IViewData, IViewRubrics> forRubrics)
    {
        var _proxy = Model.Proxy;
        var rubrics = forRubrics(this);
        rubrics.ForOnly(r => _proxy[r.RubricId] == null, r => _proxy[r.RubricId] = r.RubricType.New()).Commit();
    }

    public virtual void SetRequired(params string[] requiredList)
    {
        var _proxy = Model.Proxy;
        var rubrics = requiredList
            .ForEach(r => (ViewRubric)(object)_proxy.Rubrics[r].ShallowCopy(new ViewRubric()))
            .Commit();
        rubrics.ForEach(
            (r, x) =>
            {
                r.Required = true;
                r.Visible = true;
                r.Editable = true;
                r.RubricOrdinal = x;
            }
        );

        Rubrics.Put(rubrics);
    }

    public virtual void SetVisible(params string[] visibleList)
    {
        var _proxy = Model.Proxy;
        var rubrics = visibleList
            .ForEach(r => (ViewRubric)(object)_proxy.Rubrics[r].ShallowCopy(new ViewRubric()))
            .Commit();
        rubrics.ForEach(
            (r, x) =>
            {
                r.Visible = true;
                r.RubricOrdinal = x;
            }
        );
        Rubrics.Put(rubrics);
    }

    public virtual void SetExpandable(params string[] expandableList)
    {
        var _proxy = Model.Proxy;
        var rubrics = expandableList
            .ForEach(r => (ViewRubric)(object)_proxy.Rubrics[r].ShallowCopy(new ViewRubric()))
            .Commit();
        rubrics.ForEach(
            (r, x) =>
            {
                r.Extended = true;
                r.RubricOrdinal = x;
            }
        );
        ExtendedRubrics.Put(rubrics);
    }

    public virtual void SetEditable(params string[] editableList)
    {
        var _proxy = Model.Proxy;
        var rubrics = editableList
            .ForEach(r => (ViewRubric)(object)_proxy.Rubrics[r].ShallowCopy(new ViewRubric()))
            .Commit();
        rubrics.ForEach(
            (r, x) =>
            {
                r.Visible = true;
                r.Editable = true;
                r.RubricOrdinal = x;
            }
        );
        Rubrics.Put(rubrics);
    }

    public string ViewId => "cn" + Model.CodeNo;

    public virtual int Index
    {
        get => (Parent != null) ? Parent.IndexOf(this) : Model.Index;
        set => throw new Exception("Cannot set index");
    }

    public bool IsSingle => Count == 0;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public Icon? Icon { get; set; }

    public Icon? Logo { get; set; }

    public ViewGrid? Grid { get; set; }

    public ViewStack? Stack { get; set; }

    public string Height { get; set; } = "auto";

    public string Width { get; set; } = "400px";

    public string? Z { get; set; }

    public string? Href { get; set; }

    public string? Class { get; set; }

    public string? Style { get; set; }

    public OperationNotes Notes { get; set; } = new OperationNotes();

    public HorizontalAlignment PanelAlignment { get; set; }

    private string? nextHref = null;
    public string? NextHref
    {
        get { return nextHref; }
        set
        {
            nextHref = value;
            StateFlags.HaveNext = true;
        }
    }

    public EditMode EditMode { get; set; }

    public EntryMode EntryMode { get; set; }

    public StateFlags StateFlags { get; set; } = new();

    public OperationType Operation { get; set; }

    public IViewRubric? ActiveRubric { get; set; }

    public IViewRubrics Rubrics { get; set; } = new ViewRubrics();

    public bool RubricsEnabled { get; set; } = true;

    public IViewRubrics ExtendedRubrics { get; set; } = new ViewRubrics();

    public bool ExtendedRubricsEnabled { get; set; } = true;

    public IViewValidator? Validator { get; set; }

    public Type? ValidatorType { get; set; }

    public string? ValidatorTypeName { get; set; }

    public IView? View
    {
        get => _view;
        set
        {
            if (value != null)
                _view = value;
        }
    }

    public string? ViewTypeName { get; set; }

    Type IViewData.ModelType => typeof(TModel);

    IInnerProxy IViewData.Model
    {
        get => Model;
        set => Model = (TModel)value;
    }

    public IViewData? Root { get; set; }

    public IViewData? Parent { get; set; }

    public string[]? SearchMembers { get; set; }

    public ISeries<Filter>? SearchFilters { get; set; }

    public void RenderView()
    {
        if (View != null)
            View.RenderView();
        else if (ViewItem != null)
            ViewItem.RenderView();
        else if (Rubrics.Concat(ExtendedRubrics).Any())
        {
            Rubrics.Concat(ExtendedRubrics).ForEach(r =>
            {
                if (r.ViewItem != null)
                    r.ViewItem.RenderView();
            });
        }
        this.ForEach(d => d.RenderView());
    }

    public void ClearErrors()
    {
        Notes.Errors = null;
        Rubrics.ForEach(r => r.Errors.Clear());
    }

    public IViewItem? ViewItem
    {
        get => _viewItem;
        set
        {
            if (value != null)
            {
                _viewItem = value;
            }
        }
    }
}
