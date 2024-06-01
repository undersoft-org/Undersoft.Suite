using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts
{
    [Validator("GroupValidator")]
    public partial class Group : DataObject, IContract
    {
        [VisibleRubric]
        [RubricSize(256)]
        [DisplayRubric("Group name")]
        public string? Name { get; set; }

        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Group image")]
        [FileRubric(FileRubricType.Path, "GroupImageData")]
        public string? GroupImage { get; set; } = default!;

        public byte[]? GroupImageData { get; set; } = default!;
    }
}
