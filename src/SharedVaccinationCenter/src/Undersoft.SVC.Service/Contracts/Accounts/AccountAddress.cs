// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts.Accounts
{
    public class AccountAddress : DataObject, IContract
    {
        [VisibleRubric]
        [RequiredRubric]
        public string Country { get; set; } = default!;

        [VisibleRubric]
        [RequiredRubric]
        public string State { get; set; } = default!;

        [VisibleRubric]
        [RequiredRubric]
        public string City { get; set; } = default!;

        [VisibleRubric]
        [RequiredRubric]
        public string Postcode { get; set; } = default!;

        [VisibleRubric]
        [RequiredRubric]
        public string Street { get; set; } = default!;

        [VisibleRubric]
        [RequiredRubric]
        public string Building { get; set; } = default!;

        [VisibleRubric]
        [RequiredRubric]
        public string Apartment { get; set; } = default!;

        public long? AccountId { get; set; }
    }
}