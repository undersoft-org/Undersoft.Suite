using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic.Data.Grid
{
    public partial class GenericDataList<TStore, TDto, TModel> : GenericDataStore<TStore, TDto, TModel>
        where TStore : IDataServiceStore
        where TDto : class, IOrigin, IInnerProxy
        where TModel : class, IOrigin, IInnerProxy
    {

    }
}
