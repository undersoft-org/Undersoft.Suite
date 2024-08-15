using System.Collections.ObjectModel;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Serialization;
using Undersoft.SDK.Series;

[assembly:BinaryDocumentTraceAssembly]

namespace Undersoft.SDK.Tests.Mocks.Models.Agreements
{
    [BinaryDocument]
    public class Agreement : Origin
    {
        private BinaryDocumentSerializer serializer = new BinaryDocumentSerializer();

        [DisplayRubric("Agreement kind")]
        public AgreementKind Kind { get; set; }
        [IdentityRubric]
        [RequiredRubric]
        public Guid UserId { get; set; }
        [RequiredRubric]
        [DisplayRubric("Version number")]
        public long VersionId { get; set; }
        [RequiredRubric]
        public long FormatId { get; set; }
        [RequiredRubric]
        public string Language { get; set; } = "pl";
        [VisibleRubric]
        public string Comments { get; set; } = "Comments";
        [DisplayRubric("Email address")]
        public string Email { get; set; } = "fdfds";
        [DisplayRubric("Phone number")]
        public string PhoneNumber { get; set; } = "3453453";

        public virtual Listing<AgreementFormat> Formats { get; set; } = null;
        public virtual Listing<AgreementVersion> Versions { get; set; } = null;
        public virtual AgreementType Type { get; set; } = null;      
    }

    public class Agreements : KeyedCollection<long, Agreement>
    {
        protected override long GetKeyForItem(Agreement item)
        {
            return item.Id == 0 ? item.AutoId() : item.Id;
        }
    }
}
