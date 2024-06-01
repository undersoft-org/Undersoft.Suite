using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SSC.Service.Application.Models;

[DataContract]
public class ModelBase<TViewModel, TDetail, TSetting, TGroup>
    : OpenModel<TViewModel, TDetail, TSetting, TGroup>
    where TViewModel : class, IDataObject
    where TDetail : class, IDetail, new()
    where TSetting : class, ISetting, new()
    where TGroup : struct, Enum
{
    public ModelBase() : base() { }

    [DataMember(Order = 16)]
    public long? DefaultId { get; set; }

    [DataMember(Order = 17)]
    public long? LocationId { get; set; }
}
