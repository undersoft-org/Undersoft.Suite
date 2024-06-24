// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SVC.Service.Application.ViewModels;

[Validator("EventValidator")]
public class Event : DataObject, IViewModel
{
    public Event() : base() { }

    [VisibleRubric]
    [RubricSize(8)]
    [DisplayRubric("ID")]
    public override long Id { get => base.Id; set => base.Id = value; }

    public virtual uint Version { get; set; }

    [VisibleRubric]
    [RubricSize(512)]
    [DisplayRubric("Type")]
    public virtual string EventType { get; set; }

    [VisibleRubric]
    [RubricSize(8)]
    [DisplayRubric("Entity ID")]
    public virtual long EntityId { get; set; }

    [VisibleRubric]
    [RubricSize(32)]
    [DisplayRubric("Entity type")]
    public virtual string EntityTypeName { get; set; }

    [VisibleRubric]
    [RubricSize(8)]
    [DisplayRubric("Time")]
    public virtual DateTime PublishTime { get; set; }

    [VisibleRubric]
    [RubricSize(4)]
    [DisplayRubric("Status")]
    public virtual EventPublishStatus PublishStatus { get; set; }
}