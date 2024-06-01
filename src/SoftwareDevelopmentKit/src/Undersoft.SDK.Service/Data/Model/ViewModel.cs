using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Entity;

using Undersoft.SDK.Service.Data.Model;
using Undersoft.SDK.Service.Data.Object;


[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class ViewwModel : DataObject, IViewModel
{
    public ViewwModel() : base() { }
}
