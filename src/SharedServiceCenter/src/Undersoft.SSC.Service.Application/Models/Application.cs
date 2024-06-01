using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Application : ModelBase<Application, Detail, Setting, MemberGroup>, IViewModel
{
    public Application() { Group = MemberGroup.Application; }

    [VisibleRubric]
    public override long Id { get => base.Id; set => base.Id = value; }

    [VisibleRubric]
    public override string Label { get => base.Label; set => base.Label = value; }

}
