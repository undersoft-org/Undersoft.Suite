﻿using System.Runtime.Serialization;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts;

[DataContract]
public class Claim : InnerProxy, IClaim, IContract
{
    [DataMember(Order = 6)]
    public virtual string? ClaimType { get; set; }

    [DataMember(Order = 7)]
    public virtual string? ClaimValue { get; set; }
}