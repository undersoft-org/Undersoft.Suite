// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object.Group;

namespace Undersoft.SCC.Service.Contracts
{
    /// <summary>
    /// The group.
    /// </summary>
    [DataContract]
    public partial class GroupEdge : ObjectGroup<Group>, IContract
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>A <see cref="string? "/></value>
        [Identify]
        [DataMember(Order = 14)]
        public override string? Name { get; set; }
        /// <summary>
        /// Gets or sets the group image.
        /// </summary>
        /// <value>A <see cref="string? "/></value>
        [DataMember(Order = 15)]
        public string? GroupImage { get; set; } = default!;

        /// <summary>
        /// Gets or sets the group image data.
        /// </summary>
        /// <value>A <see cref="byte[]? "/></value>
        [DataMember(Order = 16)]
        public byte[]? GroupImageData { get; set; } = default!;
    }
}
