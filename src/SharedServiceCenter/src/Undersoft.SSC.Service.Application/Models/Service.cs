using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Service : ModelBase<Service, Detail, Setting, MemberGroup>, IViewModel
{
    public Service() { Group = MemberGroup.SharedService; }

    [VisibleRubric]
    public override long Id { get => base.Id; set => base.Id = value; }

    [VisibleRubric]
    public override string Label { get => base.Label; set => base.Label = value; }
}
