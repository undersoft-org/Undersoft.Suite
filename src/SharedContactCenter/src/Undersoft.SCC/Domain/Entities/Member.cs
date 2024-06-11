// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SCC.Domain.Entities.Enums;

namespace Undersoft.SCC.Domain.Entities
{
    /// <summary>
    /// The contact.
    /// </summary>
    public class Member : OpenEntity<Member, Detail, Setting, Group>, IEntity
    {
        /// <summary>
        /// Gets or sets the related from.
        /// </summary>
        public virtual EntitySet<Member>? RelatedFrom { get; set; }

        /// <summary>
        /// Gets or sets the related converts to.
        /// </summary>
        public virtual EntitySet<Member>? RelatedTo { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>A string?</value>
        public virtual string? Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>A string?</value>
        public virtual string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>A ContactType</value>
        public virtual MemberKind Type { get; set; }


    }

}
