using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Object.Group;
[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class ObjectGroup<TGroup> : DataObject, IGroup
    where TGroup : class, IGroup
{

    [DataMember(Order = 14)]
    public virtual string Name { get; set; }

}
