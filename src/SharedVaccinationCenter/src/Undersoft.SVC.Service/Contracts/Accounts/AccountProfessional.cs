﻿// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts.Accounts;

public class AccountProfessional : DataObject, IContract
{
    [VisibleRubric]
    [DisplayRubric("Industry")]
    public string? ProfessionIndustry { get; set; }

    [VisibleRubric]
    public string? Profession { get; set; } = default!;

    [VisibleRubric]
    [DisplayRubric("Email")]
    public string? ProfessionalEmail { get; set; }

    [VisibleRubric]
    [DisplayRubric("Phone number")]
    public string? ProfessionalPhoneNumber { get; set; }

    [VisibleRubric]
    [DisplayRubric("Social media")]
    public string? ProfessionalSocialMedia { get; set; }

    [VisibleRubric]
    [DisplayRubric("Websites")]
    public string? ProfessionalWebsites { get; set; }

    [VisibleRubric]
    [DisplayRubric("Experience in years")]
    public float ProfessionalExperience { get; set; }

    public long? AccountId { get; set; }













}