using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Identifier;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Application.Server.Controllers.Open;

public class MemberIdentifierController
    : OpenDataRemoteController<
        long,
        IDataStore,
        Identifier<Member>,
        Identifier<Member>,
        ServiceManager
    >
{
    public MemberIdentifierController(IServicer ultimatr) : base(ultimatr) { }
}
