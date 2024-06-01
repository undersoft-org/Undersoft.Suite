using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Abstraction
{
    public interface IViewPanel<TModel> : IViewDialog<TModel>, IViewPanel where TModel : class, IOrigin, IInnerProxy
    {
    }

    public interface IViewPanel : IViewDialog
    {
    }
}