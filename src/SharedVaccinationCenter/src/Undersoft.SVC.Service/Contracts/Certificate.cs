// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts
{
    public class Certificate : DataObject, IContract
    {
        public virtual string? Title { get; set; }

        public virtual long? ProcedureId { get; set; }

        [AutoExpand]
        public virtual Procedure? Procedure { get; set; }

        public virtual long? PersonalId { get; set; }

        [AutoExpand]
        public virtual Personal? Personal { get; set; }

        public virtual long? VaccineId { get; set; }

        [AutoExpand]
        public virtual Vaccine? Vaccine { get; set; }

        public virtual long? TermId { get; set; }

        [AutoExpand]
        public virtual Term? Term { get; set; }

        public virtual long? PaymentId { get; set; }

        [AutoExpand]
        public virtual Payment? Payment { get; set; }
    }
}
