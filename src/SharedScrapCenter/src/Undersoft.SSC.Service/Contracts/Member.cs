using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Member : MemberBase
{
    [DataMember(Order = 20)]
    public virtual Listing<MemberBase>? RelatedFrom { get; set; }

    [DataMember(Order = 21)]
    public virtual Listing<MemberBase>? RelatedTo { get; set; }

    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}


