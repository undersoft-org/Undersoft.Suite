using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Workflow : ModelBase<Service, Detail, Setting, Group>, IViewModel
{
    public Workflow() { }

    [Detail]
    public Contracts.Account? Identity { get; set; }
}
