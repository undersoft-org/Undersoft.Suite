// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Serialization;
using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SCC.Domain.Entities;

/// <summary>
/// The detail.
/// </summary>
public class Detail : ObjectDetail<Detail, DetailKind>, IEntity, ISerializableJsonDocument, IDetail
{
    public Detail() : base() { }

    public Detail(DetailKind kind) : base(kind) { }

    /// <summary>
    /// Gets or sets the related from.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Detail>? RelatedFrom { get; set; }

    /// <summary>
    /// Gets or sets the related converts to.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Detail>? RelatedTo { get; set; }

    /// <summary>
    /// Gets or sets the contacts.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Member>? Members { get; set; }
}
