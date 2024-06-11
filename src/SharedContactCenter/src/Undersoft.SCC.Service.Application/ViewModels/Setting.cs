// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.SCC.Service.Application.ViewModels
{
    /// <summary>
    /// The setting.
    /// </summary>
    public class Setting : SettingEdge
    {
        /// <summary>
        /// Gets or sets the related from.
        /// </summary>
        /// <value>An TODO: Add missing XML "/&gt;</value>
        public virtual EntitySet<SettingEdge>? RelatedFrom { get; set; }

        /// <summary>
        /// Gets or sets the related converts to.
        /// </summary>
        /// <value>An TODO: Add missing XML "/&gt;</value>
        public virtual EntitySet<SettingEdge>? RelatedTo { get; set; }
    }
}
