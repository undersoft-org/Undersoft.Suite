using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object.Group;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Group : ObjectGroup<Group>, IGroup, IContract
{
    public Group() : base() { }
}
