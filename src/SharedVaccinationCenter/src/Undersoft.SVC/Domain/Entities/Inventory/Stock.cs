// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Domain.Entities.Catalogs;

namespace Undersoft.SVC.Domain.Entities.Inventory
{
    public class Stock : Entity, IEntity
    {
        public virtual string? Notes { get; set; }

        public long? VaccineId { get; set; }

        public virtual Vaccine? Vaccine { get; set; }

        public virtual float? Amount { get; set; }

        public virtual EntitySet<Traffic>? Traffics { get; set; }

        public virtual EntitySet<Request>? Requests { get; set; }
    }

}
