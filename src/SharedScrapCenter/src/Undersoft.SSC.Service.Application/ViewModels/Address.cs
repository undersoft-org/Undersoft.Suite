// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Application.ViewModels
{
    public class Address : DataObject, IViewModel
    {
        public AddressType AddressType { get; set; }

        [VisibleRubric]
        public string? Country { get; set; }

        [VisibleRubric]
        public string? State { get; set; }

        [VisibleRubric]
        public string? City { get; set; }

        [VisibleRubric]
        public string? Postcode { get; set; }

        [VisibleRubric]
        public string? Street { get; set; }

        [VisibleRubric]
        public string? Building { get; set; }

        [VisibleRubric]
        public string? Apartment { get; set; }

        public string? Notes { get; set; }

        public long? SupplierId { get; set; }
    }
}