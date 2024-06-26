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
[Validator("OfficeValidator")]
[OpenSearch("Number", "Name")]
public class Office : DataObject, IViewModel
{
    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(8)]
    [OpenQuery("Number")]
    [DisplayRubric("Number")]
    public string? Number { get; set; }

    [VisibleRubric]
    [Filterable]
    [Sortable]
    [RubricSize(64)]
    [OpenQuery("Name")]
    [DisplayRubric("Name")]
    public string? Name { get; set; }
}