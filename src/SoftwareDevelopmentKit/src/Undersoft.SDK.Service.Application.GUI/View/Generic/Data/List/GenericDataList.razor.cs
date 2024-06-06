using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataList<TModel> : ViewItem<TModel> where TModel : class, IOrigin, IInnerProxy
    {

    }
}
