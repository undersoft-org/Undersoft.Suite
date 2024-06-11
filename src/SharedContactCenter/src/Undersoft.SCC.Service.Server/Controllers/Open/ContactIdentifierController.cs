using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server.Controller.Open;

namespace Undersoft.SCC.Service.Server.Controllers.Open;

using Undersoft.SCC.Domain.Entities;

public class ContactIdentifierController
   : OpenCqrsController<
       long,
       IEntryStore,
       IReportStore,
       Identifier<Contact>,
       Identifier<Contracts.Contact>,
       ServiceManager
   >
{
    public ContactIdentifierController(IServicer ultimatr) : base(ultimatr) { }
}

