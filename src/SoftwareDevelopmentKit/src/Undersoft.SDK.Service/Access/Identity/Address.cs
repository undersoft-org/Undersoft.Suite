// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Identity
{
    public class Address : DataObject, IContract
    {
        public AddressType AddressType { get; set; }

        [VisibleRubric]
        [RequiredRubric]
        public string Country { get; set; }

        [VisibleRubric]
        [RequiredRubric]
        public string State { get; set; }

        [VisibleRubric]
        [RequiredRubric]
        public string City { get; set; }

        [VisibleRubric]
        [RequiredRubric]
        public string Postcode { get; set; }

        [VisibleRubric]
        [RequiredRubric]
        public string Street { get; set; }

        [VisibleRubric]
        [RequiredRubric]
        public string Building { get; set; }

        [VisibleRubric]
        [RequiredRubric]
        public string Apartment { get; set; }

        public string Notes { get; set; }
    }
}