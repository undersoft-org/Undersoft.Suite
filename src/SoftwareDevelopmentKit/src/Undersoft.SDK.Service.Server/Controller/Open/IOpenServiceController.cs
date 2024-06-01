namespace Undersoft.SDK.Service.Server.Controller.Open;
public interface IOpenServiceController<TStore, TService, TModel> where TModel : class, IOrigin, IInnerProxy, new()
    where TService : class
    where TStore : IDataServerStore
{

}