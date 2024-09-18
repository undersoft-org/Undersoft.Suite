// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Service.Application.ViewModels.Catalogs;

namespace Undersoft.SVC.Service.Application.ViewModels.Vaccination
{
    public class PostSymptom : DataObject, IViewModel
    {

        public virtual string? Title { get; set; }

        public virtual long? PersonalId { get; set; }

        public virtual Personal? Personal { get; set; }

        public virtual long? VaccineId { get; set; }

        public virtual Vaccine? Vaccine { get; set; }

        public virtual long? TermId { get; set; }

        public virtual Term? Term { get; set; }

        public virtual long? ProcedureId { get; set; }

        public virtual Procedure? Procedure { get; set; }
    }
}
