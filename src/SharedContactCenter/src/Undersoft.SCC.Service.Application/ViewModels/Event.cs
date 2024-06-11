// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Application.ViewModels;

[Validator("EventValidator")]
public class Event : DataObject, IViewModel
{
    public Event() : base() { }

    [VisibleRubric]
    [DisplayRubric("ID")]
    public override long Id { get => base.Id; set => base.Id = value; }

    [VisibleRubric]
    public virtual uint Version { get; set; }

    [VisibleRubric]
    [DisplayRubric("Type")]
    public virtual string EventType { get; set; }

    [VisibleRubric]
    [DisplayRubric("Entity ID")]
    public virtual long EntityId { get; set; }

    [VisibleRubric]
    [DisplayRubric("Entity type")]
    public virtual string EntityTypeName { get; set; }

    [VisibleRubric]
    [DisplayRubric("Time")]
    public virtual DateTime PublishTime { get; set; }

    [VisibleRubric]
    [DisplayRubric("Status")]
    public virtual EventPublishStatus PublishStatus { get; set; }
}