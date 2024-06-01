using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SSC.Service.Contracts.Base;

[DataContract]
public class ContractBase<TContract, TDetail, TSetting, TGroup>
    : OpenContract<TContract, TDetail, TSetting, TGroup>
    where TContract : IDataObject
    where TDetail : class, IDetail
    where TSetting : class, ISetting
    where TGroup : struct, Enum
{
    public ContractBase() { }

    [DataMember(Order = 16)]
    public virtual long? DefaultId { get; set; }

    [DataMember(Order = 18)]
    public virtual long? LocationId { get; set; }
}
