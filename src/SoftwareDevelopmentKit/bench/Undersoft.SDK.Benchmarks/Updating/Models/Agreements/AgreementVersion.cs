using System.Collections.ObjectModel;
using Undersoft.SDK.Series;

namespace Undersoft.SDK.Benchmarks.Updating.Models.Agreements
{
    public class AgreementVersion : Identifiable
    {
        public int VersionNumber { get; set; } = 10;

        public int OriginId { get; set; } = 10;

        public virtual AgreementType Type { get; set; } = new AgreementType();
        public virtual Listing<AgreementText> Texts { get; set; } = new Listing<AgreementText>(new[] { new AgreementText() });
    }

    public class EmptyAgreementVersion : Identifiable
    {
        public int VersionNumber { get; set; }

        public int OriginId { get; set; }

        public virtual AgreementType Type { get; set; } = new AgreementType();
        public virtual Listing<AgreementText> Texts { get; set; } = new Listing<AgreementText>(new[] { new AgreementText() });
    }

    public class AgreementVersions : KeyedCollection<long, AgreementVersion>
    {
        protected override long GetKeyForItem(AgreementVersion item)
        {
            return item.Id == 0 ? item.AutoId() : item.Id;
        }
    }
}
