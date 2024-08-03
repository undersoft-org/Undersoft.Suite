using System.Runtime.Serialization;

namespace Undersoft.GDC.Service.Contracts.Base;

[DataContract]
public class MemberBase : ContractBase<MemberBase, Detail, Setting, Group>
{
}
