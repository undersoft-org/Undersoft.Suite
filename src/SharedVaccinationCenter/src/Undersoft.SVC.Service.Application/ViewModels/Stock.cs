// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SVC.Service.Application.ViewModels
{
    public class Stock : DataObject, IViewModel
    {
        public virtual string? Notes { get; set; }

        public long? VaccineId { get; set; }

        [Extended]
        public virtual Vaccine? Vaccine { get; set; }

        public virtual float? Amount { get; set; }
    }

}
