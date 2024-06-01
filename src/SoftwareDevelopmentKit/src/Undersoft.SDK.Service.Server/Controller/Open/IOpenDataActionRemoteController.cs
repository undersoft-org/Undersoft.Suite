namespace Undersoft.SDK.Service.Server.Controller.Open;
public interface IOpenDataActionRemoteController<TStore, TService, TDto>
    where TService : class
    where TDto : class
    where TStore : IDataServiceStore
{
}
