﻿// *************************************************
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
    /// The detail.
    /// </summary>    ]
    public class DetailNode : Detail
    {
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
    }
}
