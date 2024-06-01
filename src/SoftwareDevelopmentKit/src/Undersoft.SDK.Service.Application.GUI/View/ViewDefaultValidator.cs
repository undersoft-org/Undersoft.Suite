namespace Undersoft.SDK.Service.Application.GUI.View.Model;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View;

public class ViewDefaultValidator<TModel> : ViewValidator<TModel> where TModel : class, IOrigin, IInnerProxy
{
    public ViewDefaultValidator(IServicer servicer) : base(servicer) { }
}
