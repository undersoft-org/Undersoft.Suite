using System.Runtime.Serialization;

namespace Undersoft.SSC.Service.Contracts.Base;

[DataContract]
public class ScheduleBase : ContractBase<ScheduleBase, Detail, Setting, Group>
{
}
