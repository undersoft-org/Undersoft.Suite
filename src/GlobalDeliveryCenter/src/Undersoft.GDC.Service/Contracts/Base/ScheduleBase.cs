using System.Runtime.Serialization;

namespace Undersoft.GDC.Service.Contracts.Base;

[DataContract]
public class ScheduleBase : ContractBase<ScheduleBase, Detail, Setting, Group>
{
}
