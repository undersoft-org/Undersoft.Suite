using System.Runtime.Serialization;
using Undersoft.SBC.Domain.Entities.Enums;

namespace Undersoft.SBC.Service.Contracts;

[DataContract]
public class Location : DataObject
{
    [DataMember(Order = 12)]
    public virtual string? Name { get; set; }

    [DataMember(Order = 13)]
    public virtual LocaleType LocaleType { get; set; }

    [DataMember(Order = 14)]
    public virtual string? Email { get; set; }

    [DataMember(Order = 15)]
    public virtual PhoneType PhoneType { get; set; }

    [DataMember(Order = 16)]
    public virtual string? PhoneNumber { get; set; }

    [DataMember(Order = 17)]
    public virtual string? Notices { get; set; }

    [DataMember(Order = 18)]
    public virtual string? Website { get; set; }

    [DataMember(Order = 19)]
    public virtual string? SocialMedia { get; set; }


}
