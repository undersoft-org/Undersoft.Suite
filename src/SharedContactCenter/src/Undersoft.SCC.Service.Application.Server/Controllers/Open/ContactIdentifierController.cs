using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open;

public class ContactIdentifierController
    : OpenDataRemoteController<
        long,
        IDataStore,
        Identifier<Contracts.Contact>,
        Identifier<Contracts.Contact>,
        ServiceManager
    >
{
    public ContactIdentifierController(IServicer ultimatr) : base(ultimatr) { }
}
