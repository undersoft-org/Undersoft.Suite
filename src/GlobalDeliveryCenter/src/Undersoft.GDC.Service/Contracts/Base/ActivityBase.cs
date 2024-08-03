using System.Runtime.Serialization;
using Undersoft.GDC.Domain.Entities;

namespace Undersoft.GDC.Service.Contracts.Base;

[DataContract]
public class ActivityBase : ContractBase<ActivityBase, Detail, Setting, Group>
{
}
