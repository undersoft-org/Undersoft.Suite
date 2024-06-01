using System.ServiceModel;

namespace Undersoft.SDK.Service.Server.Controller.Stream;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Response;

[ServiceContract]
public interface IStreamEventController<TDto> where TDto : class, IOrigin, IInnerProxy
{
    Task<ResultString> Count();
    IAsyncEnumerable<TDto> All();
    IAsyncEnumerable<TDto> Query(QueryParameters query);
    IAsyncEnumerable<ResultString> Creates(TDto[] dtos);
    IAsyncEnumerable<ResultString> Changes(TDto[] dtos);
    IAsyncEnumerable<ResultString> Updates(TDto[] dtos);
    IAsyncEnumerable<ResultString> Deletes(TDto[] dtos);
}