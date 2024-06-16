// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts
{
    public class Term : DataObject, IContract
    {
        public virtual string? Name { get; set; }

        public virtual string? Description { get; set; }

        public virtual string? Dose { get; set; }

        public virtual DateTime? Date { get; set; }

        public virtual TimeSpan? Interval { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual long? ProcedureId { get; set; }

        public virtual long? PostSymptomId { get; set; }

        public virtual long? CertificateId { get; set; }

    }

}
