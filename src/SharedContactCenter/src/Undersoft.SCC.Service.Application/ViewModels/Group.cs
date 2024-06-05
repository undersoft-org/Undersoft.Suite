using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Application.ViewModels
{
    [Validator("GroupValidator")]
    [ViewSize(width: "400px", height: "350px")]
    public partial class Group : DataObject, IViewModel
    {
        [VisibleRubric]
        [RubricSize(3)]
        [DisplayRubric("Image")]
        [ViewImage(ViewImageMode.Regular, "30px", "20px")]
        [FileRubric(FileRubricType.Property, "GroupImageData")]
        public string? GroupImage { get; set; } = default!;

        [VisibleRubric]
        [RubricSize(256)]
        [Filterable]
        [Sortable]
        [DisplayRubric("Group name")]
        public string? Name { get; set; }

        public byte[]? GroupImageData { get; set; } = default!;
    }
}
