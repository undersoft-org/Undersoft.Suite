using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts
{
    [Validator("GroupValidator")]
    [ViewSize(width: "400px", height: "350px")]
    public partial class Group : DataObject, IContract
    {
        [VisibleRubric]
        [RubricSize(128)]
        [DisplayRubric("Group name")]
        public string? Name { get; set; }

        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Group image")]
        [ViewImage(ViewImageMode.Regular, "50px", "50px")]
        [FileRubric(FileRubricType.Property, "GroupImageData")]
        public string? GroupImage { get; set; } = default!;

        public byte[]? GroupImageData { get; set; } = default!;
    }
}
