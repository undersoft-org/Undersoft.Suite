using System.Runtime.Serialization;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels
{
    [DataContract]
    public partial class Group : DataObject, IViewModel
    {
        [VisibleRubric]
        public string? Name { get; set; }

        [VisibleRubric]
        [DisplayRubric("Group image")]
        [FileRubric(FileRubricType.Path, "GroupImageData")]
        public string GroupImage { get; set; } = default!;

        public byte[] GroupImageData { get; set; } = default!;
    }
}
