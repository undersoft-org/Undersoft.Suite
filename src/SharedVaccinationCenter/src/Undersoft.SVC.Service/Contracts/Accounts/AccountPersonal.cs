﻿// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;

namespace Undersoft.SVC.Service.Contracts.Accounts;

public class AccountPersonal : DataObject, IContract
{

    [VisibleRubric]
    [DisplayRubric("Upload image")]
    [ViewImage(ViewImageMode.Persona, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "ImageData")]
    public string? Image { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("First name")]
    public string? FirstName { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Last name")]
    public string? LastName { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    public string? Email { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Phone number")]
    public string? PhoneNumber { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Day of birth")]
    public DateTime Birthdate { get; set; } = DateTime.Parse("01.01.1990");

    public byte[]? ImageData { get; set; }

    public long? AccountId { get; set; }
}