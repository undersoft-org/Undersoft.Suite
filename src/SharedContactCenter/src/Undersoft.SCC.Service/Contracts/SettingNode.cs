// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

namespace Undersoft.SCC.Service.Contracts;

/// <summary>
/// The setting.
/// </summary>
public class SettingNode : Setting
{
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
}
