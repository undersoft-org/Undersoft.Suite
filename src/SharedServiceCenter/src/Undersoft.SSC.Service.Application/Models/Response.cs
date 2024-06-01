using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SSC.Service.Contracts;
using Undersoft.SSC.Service.Contracts.Details;

namespace Undersoft.SSC.Service.Application.Models;

public class Response : Activity, IViewModel
{
    [Detail]
    public Registration? Registration { get; set; }
}
