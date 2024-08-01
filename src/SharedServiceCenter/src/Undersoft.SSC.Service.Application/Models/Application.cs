using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Models;

public class Application : ModelBase<Application, Detail, Setting, Group>, IViewModel
{
    public Application() { }

    [VisibleRubric]
    public override long Id { get => base.Id; set => base.Id = value; }

    [VisibleRubric]
    public override string Label { get => base.Label; set => base.Label = value; }

}
