// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SVC.Service.Application.ViewModels.Catalogs;

/// <summary>
/// The contact address.
/// </summary>
[Validator("ManufacturerValidator")]
[OpenSearch("FullName")]
public class Manufacturer : DataObject, IViewModel
{
    [VisibleRubric]
    [RubricSize(8)]
    [DisplayRubric("Image")]
    [ViewImage(ViewImageMode.Regular, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "ManufacturerImageData")]
    public string? ManufacturerImage { get; set; }

    public byte[]? ManufacturerImageData { get; set; }

    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(64)]
    [OpenQuery("FullName")]
    [DisplayRubric("Full name")]
    public string? FullName { get; set; }

    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(64)]
    [OpenQuery("Name")]
    [DisplayRubric("Short name")]
    public string? Name { get; set; }
}