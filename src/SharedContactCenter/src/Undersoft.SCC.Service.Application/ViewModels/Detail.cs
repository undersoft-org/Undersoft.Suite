// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using System.Runtime.Serialization;
using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SCC.Service.Application.ViewModels
{
    /// <summary>
    /// The detail.
    /// </summary>
    [DataContract]
    public class Detail : ObjectDetail<Detail, DetailKind>, IDetail, IViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailNode"/> class.
        /// </summary>
        public Detail() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailNode"/> class.
        /// </summary>
        /// <param name="kind">The kind.</param>
        public Detail(DetailKind kind) : base(kind) { }
    }
}
