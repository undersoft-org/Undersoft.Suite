using System.Runtime.Serialization;
using Undersoft.SSC.Domain.Entities;

namespace Undersoft.SSC.Service.Contracts.Base;

[DataContract]
public class ActivityBase : ContractBase<ActivityBase, Detail, Setting, Group>
{
}
