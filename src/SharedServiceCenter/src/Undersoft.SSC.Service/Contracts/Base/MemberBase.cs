using System.Runtime.Serialization;

namespace Undersoft.SSC.Service.Contracts.Base;

[DataContract]
public class MemberBase : ContractBase<MemberBase, Detail, Setting, Group>
{
}
