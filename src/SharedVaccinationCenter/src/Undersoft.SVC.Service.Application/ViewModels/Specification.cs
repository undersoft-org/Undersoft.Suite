// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;

namespace Undersoft.SVC.Service.Application.ViewModels;

/// <summary>
/// The contact address.
/// </summary>
public class Specification : DataObject, IViewModel
{
    [IgnoreDataMember]
    [JsonIgnore]
    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Image")]
    [ViewImage(ViewImageMode.Regular, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "VaccineImageData")]
    public string? VaccineImage { get; set; }

    public byte[]? VaccineImageData { get; set; }

    public string? Name { get; set; }

    public string? Virus { get; set; }

    public string? Dose { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public long? VaccineId { get; set; }
}