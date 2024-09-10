using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Service : ContractBase<Service, Detail, Setting, Group>, IContract
{
    [DataMember(Order = 22)]
    public virtual Listing<Activity>? Activities { get; set; }

    [DataMember(Order = 23)]
    public virtual Listing<Resource>? Resources { get; set; }

    [DataMember(Order = 24)]
    public virtual Listing<Schedule>? Schedules { get; set; }

    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}


