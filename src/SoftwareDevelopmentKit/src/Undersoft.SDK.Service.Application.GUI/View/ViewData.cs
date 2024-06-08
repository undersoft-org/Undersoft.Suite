using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series.Base;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Attributes;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Validator;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewData<TModel> : ListingBase<IViewData>, IViewData<TModel>
    where TModel : class, IOrigin, IInnerProxy
{
    protected IProxy _proxy = default!;
    protected IView _view = default!;
    protected IViewItem _viewItem = default!;
    protected long? typeId;

    public ViewData() : this(typeof(TModel).New<TModel>(), OperationType.Any) { }

    public ViewData(OperationType mode) : this(typeof(TModel).New<TModel>(), mode) { }

    public ViewData(TModel data) : this(data, OperationType.Any) { }

    public ViewData(TModel data, OperationType mode, string title = "") : base(true)
    {
        Model = data;
        _proxy = data.Proxy;
        Title = title;
        Operation = mode;
    }

    public virtual void ClearData()
    {
        Model = typeof(TModel).New<TModel>();
        this.ForEach(i => i.ClearData());
    }

    public virtual TModel Model { get; set; } = default!;

    public virtual IViewRubrics MapRubrics(Func<IViewData, IViewRubrics> forRubrics, Func<IRubric, bool> predicate)
    {
        var rubrics = forRubrics(this);
        if (!rubrics.Any())
        {
            if (Parent != null && Parent.TypeId == TypeId)
            {
                rubrics.Add(forRubrics(Parent).ForEach(r =>
                    {
                        var rubric = (ViewRubric)(object)r.ShallowCopy(new ViewRubric());
                        rubric.ViewItem = default!;
                        return rubric;
                    }
                ));
                Validator = Parent.Validator;
                ValidatorType = Parent.ValidatorType;
                ValidatorTypeName = Parent.ValidatorTypeName;
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

                rubrics.Add(
                    _proxy.Rubrics.ForOnly(r =>
                        predicate(r),
                        r => (ViewRubric)(object)r.ShallowCopy(new ViewRubric())
                    )
                );
                rubrics.ForEach(r => ViewAttributes.Resolve(r)).Commit();
                rubrics
                    .ForOnly(r => r.IconMember != null, r => r.Icon = (Icon)_proxy[r.IconMember])
                    .Commit();
                rubrics
                    .ForOnly(
                        r => r.Icon == null && _proxy.Rubrics.ContainsKey(r.RubricName + "Icon"),
                        r => r.Icon = (Icon)_proxy[r.RubricName + "Icon"]
                    )
                    .Commit();
                rubrics.ForEach((r, x) => r.RubricOrdinal = x).Commit();
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
        var rubrics = forRubrics(this);
        rubrics.ForOnly(r => _proxy[r.RubricId] == null, r => _proxy[r.RubricId] = r.RubricType.New()).Commit();
    }

    public virtual void SetRequired(params string[] requiredList)
    {
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

    public string ViewId => Model.CodeNo;

    public virtual int Index
    {
        get => (Parent != null) ? Parent.IndexOf(this) : Model.Index;
        set => throw new Exception("Cannot set index");
    }

    public bool IsSingle => Count == 0;

    public string? Title { get; set; } = null;

    public string? Description { get; set; } = null;

    public Icon? Icon { get; set; }

    public Icon? Logo { get; set; }

    public string Height { get; set; } = "auto";

    public string Width { get; set; } = "360px";

    public string Z { get; set; }

    public string? Href { get; set; }

    public string? Class { get; set; }

    public string? Style { get; set; }

    public OperationNotes Notes { get; set; } = new OperationNotes();

    public HorizontalAlignment HorizontalAlignment { get; set; }

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

    public IViewRubric ActiveRubric { get; set; } = default!;

    public IViewRubrics Rubrics { get; set; } = new ViewRubrics();

    public bool RubricsEnabled { get; set; } = true;

    public IViewRubrics ExtendedRubrics { get; set; } = new ViewRubrics();

    public bool ExtendedRubricsEnabled { get; set; } = true;

    public IViewValidator Validator { get; set; } = default!;

    public Type? ValidatorType { get; set; } = default!;

    public string? ValidatorTypeName { get; set; } = default!;

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

    public IViewData? Root { get; set; } = default!;

    public IViewData? Parent { get; set; } = default!;

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
