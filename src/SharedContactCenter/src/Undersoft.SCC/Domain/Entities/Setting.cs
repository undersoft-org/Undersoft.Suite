// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SCC.Domain.Entities;

/// <summary>
/// The setting.
/// </summary>
public class Setting : ObjectSetting<Setting, SettingKind>, IEntity
{
    public Setting() : base() { }

    public Setting(SettingKind kind) : base(kind) { }

    /// <summary>
    /// Gets or sets the related from.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Setting>? RelatedFrom { get; set; }

    /// <summary>
    /// Gets or sets the related converts to.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Setting>? RelatedTo { get; set; }

    /// <summary>
    /// Gets or sets the contacts.
    /// </summary>
    /// <value>An TODO: Add missing XML "/&gt;</value>
    public virtual EntitySet<Member>? Members { get; set; }
}
