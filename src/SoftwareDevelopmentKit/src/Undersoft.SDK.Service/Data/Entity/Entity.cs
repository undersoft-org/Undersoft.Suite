using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Object;


[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class Entity : DataObject, IEntity
{
    public Entity() : base() { }
}
