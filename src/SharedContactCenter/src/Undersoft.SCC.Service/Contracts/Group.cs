﻿// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts
{
    /// <summary>
    /// The group.
    /// </summary>
    [Validator("GroupValidator")]
    [ViewSize(width: "400px", height: "350px")]
    public partial class Group : DataObject, IContract
    {
        /// <summary>
        /// Gets or sets the group image.
        /// </summary>
        /// <value>A <see cref="string? "/></value>
        [VisibleRubric]
        [RubricSize(3)]
        [DisplayRubric("Group image")]
        [ViewImage(ViewImageMode.Regular, "30px", "20px")]
        [FileRubric(FileRubricType.Property, "GroupImageData")]
        public string? GroupImage { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>A <see cref="string? "/></value>
        [VisibleRubric]
        [RubricSize(128)]
        [Filterable]
        [Sortable]
        [DisplayRubric("Group name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the group image data.
        /// </summary>
        /// <value>A <see cref="byte[]? "/></value>
        public byte[]? GroupImageData { get; set; } = default!;
    }
}
