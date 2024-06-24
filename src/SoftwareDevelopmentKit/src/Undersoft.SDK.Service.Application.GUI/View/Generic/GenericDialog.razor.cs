using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic;

public partial class GenericDialog<TModel> : ViewItem<TModel>, IGenericDialog
    where TModel : class, IOrigin, IInnerProxy
{
    private IViewItem _form = default!;

    [Inject]
    public override IJSRuntime? JSRuntime
    {
        get => base.JSRuntime;
        set => base.JSRuntime = value;
    }

    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    public virtual IViewItem Form
    {
        get => _form ??= this;
        set => _form = value;
    }

    protected override void OnParametersSet()
    {
        Content.View = this;
        base.OnParametersSet();
    }
}
