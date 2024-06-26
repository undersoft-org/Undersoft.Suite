// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Domain.Entities.Catalogs;
using Undersoft.SVC.Domain.Entities.Inventory;
using Undersoft.SVC.Domain.Entities.Vaccination;

namespace Undersoft.SVC.Domain.Entities
{
    public class Price : Entity, IEntity
    {
        public virtual string? Name { get; set; }

        public virtual double? Value { get; set; }

        public virtual double? Tax { get; set; }

        public virtual double? Amount { get; set; }

        public virtual long? ProcedureId { get; set; }

        public virtual Procedure? Procedure { get; set; }

        public virtual long? CampaignId { get; set; }

        public virtual Campaign? Campaign { get; set; }

        public virtual long? TrafficId { get; set; }

        public virtual Traffic? Traffic { get; set; }
    }

}
