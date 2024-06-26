// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Domain.Entities.Vaccination;

namespace Undersoft.SVC.Domain.Entities.Catalogs
{
    public class Campaign : Entity, IEntity
    {
        public virtual string? Name { get; set; }

        public virtual long? PriceId { get; set; }

        public virtual Price? Price { get; set; }

        public virtual EntitySet<Vaccine>? Vaccines { get; set; }

        public virtual EntitySet<Appointment>? Appointments { get; set; }
    }

}
