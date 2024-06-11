using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Server.Controllers.Open;

using Undersoft.SCC.Domain.Entities;

public class MemberIdentifierController
   : OpenCqrsController<
       long,
       IEntryStore,
       IReportStore,
       Identifier<Member>,
       Identifier<Contracts.Member>,
       ServiceManager
   >
{
    public MemberIdentifierController(IServicer ultimatr) : base(ultimatr) { }
}

