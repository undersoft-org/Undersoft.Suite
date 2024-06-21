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
    public class Cost : DataObject, IViewModel
    {
        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Name")]
        public virtual string? Name { get; set; }

        [VisibleRubric]
        [RubricSize(8)]
        [DisplayRubric("Value")]
        public virtual double? Value { get; set; }

        [VisibleRubric]
        [RubricSize(4)]
        [DisplayRubric("Tax")]
        public virtual double? Tax { get; set; }

        [VisibleRubric]
        [RubricSize(8)]
        [DisplayRubric("Amount")]
        public virtual double? Amount { get; set; }

        public virtual long? ProcedureId { get; set; }

        public virtual long? TrafficId { get; set; }
    }

}
